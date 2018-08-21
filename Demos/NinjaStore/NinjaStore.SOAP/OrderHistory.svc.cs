using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using NinjaStore.Common;
using NinjaStore.Common.Repositories;
using System.Threading.Tasks;

namespace NinjaStore.SOAP
{

    public class OrderHistory : IOrders
    {
        OrderRepository repo = null;
        public Task Initialization { get; private set; }

        public OrderHistory()
        {
            Initialization = InitializeAsync();
            Initialization.Wait();
        }


        #region Public Methods
        public List<Order> GetAllOrders()
        {
            return ConvertOrders(repo.GetAllOrders());

        }

        public Order GetOrderbyOrderID(int OrderId)
        {
          
            return ConvertOrder(repo.GetOrderByOrderId(OrderId));

        }

        public List<Order> GetOrdersbyProductID(string ProductId)
        {
            var repo = new OrderRepository();
            return ConvertOrders(repo.GetOrdersByProductId(ProductId));

        }

        public List<Order> GetOrdersbyCustomerID(int CustomerId)
        {
           
            return ConvertOrders(repo.GetOrdersByCustomerId(customerId: CustomerId));
        }

        #endregion

        #region Private Methods

        private async Task InitializeAsync()
        {

            Environment.SetEnvironmentVariable("DocumentDbAuthKey", ConfigurationManager.AppSettings["AuthorizationKey"]);
            Environment.SetEnvironmentVariable("DocumentDbCollectionId", ConfigurationManager.AppSettings["CollectionId"]);
            Environment.SetEnvironmentVariable("DocumentDbEndpoint", ConfigurationManager.AppSettings["EndPointUrl"]);
            Environment.SetEnvironmentVariable("DocumentDbDatabaseId", ConfigurationManager.AppSettings["DatabaseId"]);

            repo = new OrderRepository();
            await repo.Initialize();

        }

        private List<Order> ConvertOrders(List<Common.Models.Order> commonorders)
        {
            List<Order> orders = new List<Order>();
            foreach (Common.Models.Order o in commonorders)
            {
                Order order = ConvertOrder(o);
                orders.Add(order);
            }

            return orders;
        }

        private Order ConvertOrder(Common.Models.Order commonorder)
        {
            Customer customer = new Customer()
            {
                CustomerID = Convert.ToInt32(commonorder.Customer.CustomerId),
                CustomerLocation = commonorder.Customer.CustomerLocation,
                CustomerName = commonorder.Customer.CustomerName
            };

            Product product = new Product()
            {
                ProductId = commonorder.Product.Id,
                ProductName = commonorder.Product.Name,
                Price = commonorder.Product.Price
            };

            Order order = new Order()
            {
                OrderId = Convert.ToInt32(commonorder.OrderId),
                Quantity = commonorder.Quantity,
                Customer = customer,
                Product = product,
                TotalAmount = commonorder.Quantity * product.Price
            };

            return order;
        }

        #endregion
    }
}
