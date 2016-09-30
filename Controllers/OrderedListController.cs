using OrderedListApi.BusinessLayer;
using OrderedListApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("OrderedListItems/{listId}")]
        public HttpResponseMessage GetOrderedListItems([FromUri] Guid listId)
        {
            var listItems = _orderedListItemBusinessLayer.GetOrderedListItems(listId);
            return this.Request.CreateResponse(HttpStatusCode.OK, listItems);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("OrderedListItems")]
        public HttpResponseMessage GetAllOrderedListItems()
        {
            var listItems = _orderedListItemBusinessLayer.GetOrderedListItems();
            return this.Request.CreateResponse(HttpStatusCode.OK, listItems);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("OrderedListDetails")]
        public HttpResponseMessage GetAllOrderedListDetails()
        {
            var listDetails = _orderedListItemBusinessLayer.GetOrderedListDetails();
            return this.Request.CreateResponse(HttpStatusCode.OK, listDetails);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("OrderedListItems/{listId}")]
        public HttpResponseMessage SaveOrderedListItems([FromUri] Guid listId, 
            [FromBody] List<OrderedListItem> orderedListItems)
        {
             _orderedListItemBusinessLayer.UpdateOrderedlistItems(orderedListItems, listId);
            return this.Request.CreateResponse(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("OrderedListItems/")]
        public HttpResponseMessage CreateOrderedListItems([FromBody] List<OrderedListItem> orderedListItems)
        {
            var listId = _orderedListItemBusinessLayer.SaveOrderedlistItems(orderedListItems);
            return this.Request.CreateResponse(HttpStatusCode.OK, listId);
        }
    }
}