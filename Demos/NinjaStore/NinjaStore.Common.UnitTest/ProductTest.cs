using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NinjaStore.Common.Models;
using NinjaStore.Common.Repositories;

namespace NinjaStore.Common.UnitTest
{
    [TestClass]
    public class ProductTest
    {
        #region Data Members

        private ProductRepository _productRepository;

        #endregion

        #region Initialize and Cleanup Methods

        [TestInitialize]
        public void Initialize()
        {
            InitializeProductRepository().GetAwaiter().GetResult();
        }

        #endregion

        #region Tests

        [TestMethod]
        public void GetAllProductsTest()
        {
            var result = _productRepository.GetAllProducts();
            Assert.IsTrue(result.Count > 0);
        }

        #endregion

        #region Private Methods

        private async Task InitializeProductRepository()
        {
            var docDbSettings = new DocumentDbSettings
            {
                AuthKey = "",
                CollectionId = "",
                DatabaseId = "",
                Endpoint = ""
            };

            _productRepository = new ProductRepository(docDbSettings);
            await _productRepository.Initialize();
        }

        #endregion
    }
}


