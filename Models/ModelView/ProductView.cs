using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaggageWeb.Models.ModelView
{
    public class ProductView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price {  get; set; }
        public int Active { get; set; }
        public int Quantity { get; set; } = 0;
        public string ImageName { get; set; }
    }
}