using System.ComponentModel.DataAnnotations.Schema;

namespace DEV_Test.Services.ProductService.Models
{
    [Table("SearchHistory")]
    public class SearchParams
    {
        public string? Search { get; set; }
    }
}
