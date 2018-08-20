using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaStore.Common.Models
{
    public class Order
    {
        #region Properties

        public string OrderId { get; set; }

        public Customer Customer { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }


        #endregion
    }
}
