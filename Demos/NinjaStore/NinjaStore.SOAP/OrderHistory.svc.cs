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

namespace NinjaStore.SOAP
{

    public class OrderHistory : IOrders
    {

        #region Public Methods
        public List<Order> GetAllOrders()
        {
            var repo = new OrderRepository();
            return ConvertOrders(repo.GetAllOrders());

        }

        public Order GetOrderbyOrderID(int OrderId)
        {
            var repo = new OrderRepository();
            return ConvertOrder(repo.GetOrderByOrderId(OrderId));

        }

        public List<Order> GetOrdersbyProductID(string ProductId)
        {
            var repo = new OrderRepository();
            return ConvertOrders(repo.GetOrdersByProductId(ProductId));

        }

        public List<Order> GetOrdersbyCustomerID(int CustomerId)
        {
            var repo = new OrderRepository();
            return ConvertOrders(repo.GetOrdersByCustomerId(customerId: CustomerId));
        }

        #endregion

        #region Private Methods

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
                CustomerID = commonorder.Customer.CustomerId,
                CustomerLocation = commonorder.Customer.CustomerLocation,
                CustomerName = commonorder.Customer.CustomerName
            };

            Product product = new Product()
            {
                ProductId = commonorder.Product.ProductId,
                ProductName = commonorder.Product.Name,
                Price = commonorder.Product.Price
            };

            Order order = new Order()
            {
                OrderId = commonorder.OrderId,
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
