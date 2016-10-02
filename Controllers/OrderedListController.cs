using OrderedListApi.BusinessLayer;
using OrderedListApi.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;


namespace OrderedListApi.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api")]
    public class OrderedListController : ApiController
    {

        private OrderedListItemBusinessLayer _orderedListItemBusinessLayer;
        public OrderedListController()
        {
            _orderedListItemBusinessLayer = new OrderedListItemBusinessLayer();
        }

        /// <summary>
        /// Returns OrderedListItms by ClientReferenceId.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("OrderedListItems/{clientReferenceId}")]
        public HttpResponseMessage GetOrderedListItems([FromUri] string clientReferenceId)
        {
            var lId = Guid.Parse(clientReferenceId);
            var listItems = _orderedListItemBusinessLayer.GetOrderedListItems(lId);
            return this.Request.CreateResponse(HttpStatusCode.OK, listItems);
        }

        /// <summary>
        /// Updates OrderedListItems.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("OrderedListItems/{clientReferenceId}")]
        public HttpResponseMessage SaveOrderedListItems([FromUri] string clientReferenceId, 
            [FromBody] List<OrderedListItem> orderedListItems)
        {
            var lId = Guid.Parse(clientReferenceId); 

            if (!IsOrderedListOrdered(orderedListItems))
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, "Items not in order.");
            }
            
             _orderedListItemBusinessLayer.UpdateOrderedlistItems(orderedListItems, lId);
            return this.Request.CreateResponse(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Creates OrderedListItems and returns a ClientReferenceId.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("OrderedListItems/")]
        public HttpResponseMessage CreateOrderedListItems([FromBody] List<OrderedListItem> orderedListItems)
        {
            if (!IsOrderedListOrdered(orderedListItems))
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, "Items not in order.");
            }

            var listId = _orderedListItemBusinessLayer.SaveOrderedlistItems(orderedListItems);
            return this.Request.CreateResponse(HttpStatusCode.OK, listId);
        }

        /// <summary>
        /// Used for validating OrderedListItems Request.
        /// </summary>
        /// <param name="orderedListItems"></param>
        /// <returns></returns>
        private bool IsOrderedListOrdered(List<OrderedListItem> orderedListItems)
        {
            if (orderedListItems == null || orderedListItems.Count == 0) return true;

            var numItems = orderedListItems.Count + 1;
            for (var i = 1; i < numItems; i++)
            {
                if (!orderedListItems.Exists(item => item.Position == i)) return false;
            }
            return true;
        }
    }
}