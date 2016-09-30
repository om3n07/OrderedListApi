using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderedListApi.Models
{
    
    public class OrderedListItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [JsonIgnore]
        public int ListId { get; set; }
        public int Position { get; set; }
        public string Name { get; set; }
    }
}
