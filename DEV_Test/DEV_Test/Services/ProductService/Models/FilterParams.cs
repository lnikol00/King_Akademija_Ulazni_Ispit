using System.ComponentModel.DataAnnotations.Schema;

namespace DEV_Test.Services.ProductService.Models
{
    [Table("FilterHistory")]
    public class FilterParams
    {
        public string Order { get; set; }
        public string Category { get; set; }
    }
}
