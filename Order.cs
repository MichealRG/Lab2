using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2
{
    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<OrderDetails> Details { get; set; } //zeby to powiazac musze zrobic sobie kolekcje ORderDtailsow //jak to dziąła
        public Order()
        {
            Details = new List<OrderDetails>();
        }
    }
}
