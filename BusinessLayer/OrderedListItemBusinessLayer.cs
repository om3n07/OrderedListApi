using OrderedListApi.DataAccess;
using OrderedListApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderedListApi.BusinessLayer
{
    public class OrderedListItemBusinessLayer
    {
        private static OrderedListDb _dbContext;
        public OrderedListItemBusinessLayer()
        {
            _dbContext = new OrderedListDb();
        }

        public List<OrderedListItem> GetOrderedListItems()
        {
            return _dbContext.OrderedListDetails.FirstOrDefault()?.OrderedList;
        }

        public List<OrderedListDetails> GetOrderedListDetails()
        {
            return _dbContext.OrderedListDetails.ToList();
        }

        /// <summary>
        /// Gets an OrderedList by ListId
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        public List<OrderedListItem> GetOrderedListItems(Guid listId)
        {
            return _dbContext.OrderedListDetails.FirstOrDefault(old => old.ClientReferenceId == listId)?.OrderedList;
        }

        /// <summary>
        /// Updates an OrderedList
        /// </summary>
        /// <param name="orderedList"></param>
        /// <param name="clientReferenceId"></param>
        public void UpdateOrderedlistItems (List<OrderedListItem> orderedList, Guid clientReferenceId)
        {
            var listId = _dbContext.OrderedListDetails.FirstOrDefault(i => i.ClientReferenceId == clientReferenceId).ListId;

            // Removing deleted items
            foreach (var item in _dbContext.OrderedListItems)
            {
                // If am item has our list Id but isn't in the new list, remove it
                if (item.ListId == listId && !orderedList.Exists(i => i.Id == item.Id))
                {
                    _dbContext.OrderedListItems.Remove(item);
                }
            }

            _dbContext.SaveChanges();

            // Adding new items
            foreach (var item in orderedList)
            {
                var dbItem = _dbContext.OrderedListItems.FirstOrDefault(i => i.ListId == listId && i.Id == item.Id);
                {

                    if (dbItem != null)
                    {
                        // Update, if we have an item with this ListId and Id already
                        dbItem.Name = item.Name;
                        dbItem.Position = item.Position;
                    }
                    else
                    {
                        // Insert, otherwise
                        item.ListId = listId;
                        _dbContext.OrderedListItems.Add(item);
                    }
                }
            }

            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Saves a new OrderedList
        /// </summary>
        /// <param name="orderedList"></param>
        /// <returns></returns>
        public Guid SaveOrderedlistItems(List<OrderedListItem> orderedList)
        {
            var orderedListDetails = new OrderedListDetails
            {
                OrderedList = orderedList
            };
            _dbContext.OrderedListDetails.Add(orderedListDetails);
            _dbContext.SaveChanges();


            return orderedListDetails.ClientReferenceId;
        }
    }
}
