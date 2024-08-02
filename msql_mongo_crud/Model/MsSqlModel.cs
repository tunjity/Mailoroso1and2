using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace msql_mongo_crud.Model
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }

        // for tracking thou not part of the listed model in the assessment
        public int CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
