using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using NinjaStore.Common.Models;

namespace NinjaStore.Common.Repositories
{
    public class ProductRepository
    {
        #region Data Members

        private readonly DocumentClient _documentClient;
        private readonly DocumentDbSettings _documentDbSettings;

        #endregion

        #region Constructors

        internal ProductRepository(DocumentDbSettings settings)
        {
            // Save the document db settings into a private data member and 
            // instantiate an instance of the client.
            _documentDbSettings = settings;
            _documentClient = new DocumentClient(new Uri(_documentDbSettings.Endpoint),
                _documentDbSettings.AuthKey);
        }

        public ProductRepository() : this(DocumentDbSettings.GetDbSettings())
        {
        }

        #endregion

        #region Public Methods

        public async Task Initialize()
        {
            await CreateDatabaseIfNotExistsAsync();
            await CreateCollectionIfNotExistsAsync();

            var ninjaStar = new Product() { ProductId = "1", Count = 5, Name = "Ninja Stars", Price = 5.99};
            CreateProductDocumentIfNotExists(ninjaStar).Wait();

            var sword = new Product() { ProductId = "2", Count = 12, Name = "Sword", Price = 199.99 };
            CreateProductDocumentIfNotExists(sword).Wait();

            var nunchucks = new Product() { ProductId = "3", Count = 12, Name = "Nunchucks", Price = 24.79 };
            CreateProductDocumentIfNotExists(nunchucks).Wait();
        }

        public List<Product> GetAllProducts()
        {
            var results = _documentClient.CreateDocumentQuery<Product>(this.CollectionUri);
            return results.ToList();
        }

        public Product GetProductById(string productId)
        {
            var queryOptions = new FeedOptions { MaxItemCount = -1 };
            var query = _documentClient.CreateDocumentQuery<Product>(this.CollectionUri, queryOptions)
                .Where(p => p.ProductId.ToLower() == productId.ToLower()).ToList();
            return query.FirstOrDefault();
        }

        public async Task<bool> CreateProduct(Product product)
        {
            var documentUri = UriFactory.CreateDocumentUri(_documentDbSettings.DatabaseId, _documentDbSettings.CollectionId, product.ProductId);
            await _documentClient.CreateDocumentAsync(documentUri, product);

            return true;
        }

        #endregion

        #region Properties

        private Uri CollectionUri => UriFactory.CreateDocumentCollectionUri(_documentDbSettings.DatabaseId, _documentDbSettings.CollectionId);

        #endregion

        #region Private Methods

        private async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                var databaseUri = UriFactory.CreateDatabaseUri(_documentDbSettings.DatabaseId);
                await _documentClient.ReadDatabaseAsync(databaseUri);
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    var database = new Database { Id = _documentDbSettings.DatabaseId };
                    await _documentClient.CreateDatabaseAsync(database);
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task CreateCollectionIfNotExistsAsync()
        {
            try
            {
                var collectionUri = UriFactory.CreateDocumentCollectionUri(_documentDbSettings.DatabaseId, _documentDbSettings.CollectionId);
                await _documentClient.ReadDocumentCollectionAsync(collectionUri);
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    var databaseUri = UriFactory.CreateDatabaseUri(_documentDbSettings.DatabaseId);
                    await _documentClient.CreateDocumentCollectionAsync(databaseUri,
                        new DocumentCollection { Id = _documentDbSettings.CollectionId },
                        new RequestOptions { OfferThroughput = 400 });
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task CreateProductDocumentIfNotExists(Product product)
        {
            try
            {
                var documentUri = UriFactory.CreateDocumentUri(_documentDbSettings.DatabaseId, _documentDbSettings.CollectionId, product.ProductId);
                await _documentClient.ReadDocumentAsync(documentUri);
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    var documentCollectionUri = UriFactory.CreateDocumentCollectionUri(_documentDbSettings.DatabaseId, _documentDbSettings.CollectionId);
                    await _documentClient.CreateDocumentAsync(documentCollectionUri, product);
                }
                else
                {
                    throw;
                }
            }
        }

        #endregion
    }
}
