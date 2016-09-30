using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderedListApi.Models
{
    public class OrderedListDetails
    {
        public OrderedListDetails()
        {
            OrderedList = new List<OrderedListItem>();
        }

        // We are using this as a unique hard-to-guess ID we can reference from the client
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ClientReferenceId { get; set; }

        // Smaller than the Guid, better for using as a FK
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int ListId { get; set; }

        [ForeignKey("ListId")]
        public virtual List<OrderedListItem> OrderedList { get; set; }
    }
}
