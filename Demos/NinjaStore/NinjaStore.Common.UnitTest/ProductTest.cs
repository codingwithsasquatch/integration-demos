using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
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
        private readonly List<string> _cleanupIds = new List<string>();

        #endregion

        #region Initialize and Cleanup Methods

        [TestInitialize]
        public void Initialize()
        {
            InitializeProductRepository().GetAwaiter().GetResult();
        }

        [TestCleanup]
        public void Cleanup()
        {
            foreach (var id in _cleanupIds)
            {
                _productRepository.DeleteProduct(id).GetAwaiter().GetResult();
            }
        }

        #endregion

        #region Tests

        [TestMethod]
        public void GetAllProductsTest()
        {
            var result = _productRepository.GetAllProducts();
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public async Task CreateProductTest()
        {
            var expected = new Product
            {
                Price = 1000.00,
                Count = 5,
                Name = "Create Product Name",
                Id = Guid.NewGuid().ToString()
            };

            await _productRepository.CreateProduct(expected);
            var actual = _productRepository.GetProductById(expected.Id);
            Assert.AreEqual(actual.Id, expected.Id, true, CultureInfo.CurrentCulture);                

            _cleanupIds.Add(expected.Id);
        }

        [TestMethod]
        public async Task DeleteProductTest()
        {
            var newProduct = new Product
            {
                Price = 1000.00,
                Count = 5,
                Name = "Product to delete",
                Id = Guid.NewGuid().ToString()
            };

            // Create the product and make sure it's there
            await _productRepository.CreateProduct(newProduct);
            var actual = _productRepository.GetProductById(newProduct.Id);
            Assert.AreEqual(actual.Id, newProduct.Id, true, CultureInfo.CurrentCulture);

            // Delete the product and check to see if it's gone
            await _productRepository.DeleteProduct(newProduct.Id);
            var result = _productRepository.GetProductById(newProduct.Id);
            Assert.IsNull(result);
        }

        #endregion

        #region Private Methods

        private async Task InitializeProductRepository()
        {
            //var docDbSettings = new DocumentDbSettings
            //{
            //    AuthKey = "",
            //    CollectionId = "",
            //    DatabaseId = "",
            //    Endpoint = ""
            //};

            var docDbSettings = new DocumentDbSettings
            {
                AuthKey = "61132mGkvhbtR4xHFaPwHvCQRzSLyitREjB9b9OSs4IFGGiUVFZzIxhFutFmjUr9pvOHL50ZQa69ZHbLwdRaaA==",
                CollectionId = "ninjastore",
                DatabaseId = "ninjastore",
                Endpoint = "https://ninjastoredabarkol.documents.azure.com:443/"
            };

            _productRepository = new ProductRepository(docDbSettings);
            await _productRepository.Initialize();
        }

        #endregion
    }
}


