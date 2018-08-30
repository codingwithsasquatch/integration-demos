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
        public void GetProductByIdTest()
        {
            var result = _productRepository.GetProductById("2");
            Assert.IsTrue(result.Count > 0);
            Assert.IsTrue(result.Id == "2");
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

        [TestMethod]
        public async Task UpdateProductTest()
        {
            const string productId = "1";
            var product = _productRepository.GetProductById(productId);

            // Assign a random value to the price
            // update the product.
            var random = new Random();
            product.Price = random.NextDouble() * (500.0 - 1.0) + 1.0;
            await _productRepository.UpdateProduct(product);

            var result = _productRepository.GetProductById(product.Id);
            Assert.AreEqual(result.Price, product.Price);
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


