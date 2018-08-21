using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NinjaStore.Common;
using NinjaStore.Common.Models;
using NinjaStore.Common.Repositories;

namespace NinjaStore.UnitTest
{
    [TestClass]
    public class SOAPTest
    {
        OrderHistoryService.OrdersClient client = new OrderHistoryService.OrdersClient();

        [TestMethod]
        public void TestGetAllOrders()
        {
            OrderHistoryService.Order[] orders =  client.GetAllOrders();

            Assert.AreEqual(orders.Length, 3);
        }

        [TestMethod]
        public void GetOrderbyOrderID()
        {
            OrderHistoryService.Order orders = client.GetOrderbyOrderID(3);

            Assert.AreEqual(orders.Product.ProductId, "3");

        }

        [TestMethod]
        public void GetOrdersbyProductID()
        {
            OrderHistoryService.Order[] orders = client.GetOrdersbyProductID("1");

            Assert.AreEqual(orders[0].OrderId, 1);
        }

        [TestMethod]
        public void GetOrdersbyCustomerID()
        {
            OrderHistoryService.Order[] orders = client.GetOrdersbyCustomerID(2);

            Assert.AreEqual(orders[0].OrderId, 2);
        }
    }

    [TestClass]
    public class CommonTest
    {
        OrderRepository orderRepository = new OrderRepository();

        [TestMethod]
        public void TestGetAllOrders()
        {
            List<Order> orders = orderRepository.GetAllOrders();

            Assert.AreEqual(orders.Count, 3);
        }

        [TestMethod]
        public void GetOrderbyOrderID()
        {
            Order order = orderRepository.GetOrderByOrderId(3);

            Assert.AreEqual(order.Product.ProductId, "3");

        }

        [TestMethod]
        public void GetOrdersbyProductID()
        {
            List<Order> orders = orderRepository.GetOrdersByProductId("1");

            Assert.AreEqual(orders[0].OrderId, 1);
        }

        [TestMethod]
        public void GetOrdersbyCustomerID()
        {
            List<Order> orders = orderRepository.GetOrdersByCustomerId(2);

            Assert.AreEqual(orders[0].OrderId, 2);
        }
    }
    }
