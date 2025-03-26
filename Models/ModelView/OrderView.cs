using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaggageWeb.Models.ModelView
{
    public class OrderView
    {
        public int Id { get; set; }
        public int ProductId { get; set; } = 0;
        public int MemberId { get; set; } = 0;
        public DateTime CreatedDate { get; set; } =DateTime.Now;
        public int OrderStatus { get; set; } =0;
        public int OrderConfirm { get; set; } = 0;
        public int price { get; set; } = 0;
    }
}