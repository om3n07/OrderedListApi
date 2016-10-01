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
        [Required]
        public int ListId { get; set; }
        [Required]
        public int Position { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
