using System;
using System.Collections.Generic;
using System.Text;

namespace Products.Models
{
    public class Product
    {
        #region Properties

        public string Id { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }

        public double Price { get; set; }

        #endregion
    }
}
