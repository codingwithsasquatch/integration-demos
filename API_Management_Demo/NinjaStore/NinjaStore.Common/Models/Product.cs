using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NinjaStore.Common.Models
{
    public class Product
    {
        #region Properties

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "price")]
        public double Price { get; set; }

        #endregion
    }
}
