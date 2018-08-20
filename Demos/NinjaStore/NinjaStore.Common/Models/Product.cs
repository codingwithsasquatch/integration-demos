using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaStore.Common.Models
{
    public class Product
    {
        #region Properties

        public string ProductId { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }

        public double Price { get; set; }

        #endregion
    }
}
