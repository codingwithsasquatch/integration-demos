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
    public class OrderRepository
    {
        #region Data Members

        private readonly DocumentClient _documentClient;
        private readonly DocumentDbSettings _documentDbSettings;

        #endregion

        #region Constructors

        internal OrderRepository(DocumentDbSettings settings)
        {
            // Save the document db settings into a private data member and 
            // instantiate an instance of the client.
            _documentDbSettings = settings;
            _documentClient = new DocumentClient(new Uri(_documentDbSettings.Endpoint),
                _documentDbSettings.AuthKey);
        }

        public OrderRepository() : this(DocumentDbSettings.GetDbSettings())
        {
        }

        #endregion

        #region Public Methods

        public async Task Initialize()
        {
            await CreateDatabaseIfNotExistsAsync();
            await CreateCollectionIfNotExistsAsync();

            var ninjaStar = new Product() { ProductId = "1", Count = 5, Name = "Ninja Stars", Price = 5.99 };
            var cust1 = new Customer() { CustomerId = "1", CustomerName = "Awesome Dojo", CustomerLocation = "CA" };
            var starOrder = new Order() { OrderId = "1", Quantity = 3, Product=ninjaStar, Customer= cust1 };
            CreateOrderDocumentIfNotExists(starOrder).Wait();

            var sword = new Product() { ProductId = "2", Count = 12, Name = "Sword", Price = 199.99 };
            var cust2 = new Customer() { CustomerId = "2", CustomerName = "Best Dojo Ever", CustomerLocation = "IN" };
            var swordOrder = new Order() { OrderId = "2", Quantity = 5, Product = sword, Customer = cust2 };
            CreateOrderDocumentIfNotExists(swordOrder).Wait();

            var nunchucks = new Product() { ProductId = "3", Count = 12, Name = "Nunchucks", Price = 24.79 };
            var nunchuckOrder = new Order() { OrderId = "3", Quantity = 5, Product = nunchucks, Customer = cust1 };
            CreateOrderDocumentIfNotExists(nunchuckOrder).Wait();
        }

        public List<Order> GetAllOrders()
        {
            var results = _documentClient.CreateDocumentQuery<Order>(this.CollectionUri);
            return results.ToList();
        }

        public Order GetOrderByOrderId(string orderId)
        {
            var queryOptions = new FeedOptions { MaxItemCount = -1 };
            var query = _documentClient.CreateDocumentQuery<Order>(this.CollectionUri, queryOptions)
                .Where(o => o.OrderId.ToLower() == orderId.ToLower()).ToList();
            return query.FirstOrDefault();
        }

        public List<Order> GetOrdersByProductId(string productId)
        {
            var queryOptions = new FeedOptions { MaxItemCount = -1 };
            var query = _documentClient.CreateDocumentQuery<Order>(this.CollectionUri, queryOptions)
                .Where(o => o.Product.ProductId.ToLower() == productId.ToLower()).ToList();
            return query.ToList();
        }

        public List<Order> GetOrdersByCustomertId(string customerId)
        {
            var queryOptions = new FeedOptions { MaxItemCount = -1 };
            var query = _documentClient.CreateDocumentQuery<Order>(this.CollectionUri, queryOptions)
                .Where(o => o.Customer.CustomerId.ToLower() == customerId.ToLower()).ToList();
            return query.ToList();
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

        private async Task CreateOrderDocumentIfNotExists(Order order)
        {
            try
            {
                var documentUri = UriFactory.CreateDocumentUri(_documentDbSettings.DatabaseId, _documentDbSettings.CollectionId, order.OrderId);
                await _documentClient.ReadDocumentAsync(documentUri);
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    var documentCollectionUri = UriFactory.CreateDocumentCollectionUri(_documentDbSettings.DatabaseId, _documentDbSettings.CollectionId);
                    await _documentClient.CreateDocumentAsync(documentCollectionUri, order);
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
