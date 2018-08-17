using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NinjaStore.SOAP
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface Orders
    {

        [OperationContract]
       List<Order> GetAllOrders();

        [OperationContract]
        Order GetOrderbyOrderID(int OrderId);

        [OperationContract]
        List<Order> GetOrdersbyProductID(int ProductId);

        [OperationContract]
        List<Order> GetOrdersbyCustomerID(int CustomersId);

    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Order
    {
        int _orderID;
        Customer _customer;
        Product _product;
        int _quantity;
        double _total;

        public int OrderID
        {
            get { return _orderID; }
            set { _orderID = value; }
        }


        [DataMember]
        public Customer Customer
        {
            get { return _customer; }
            set { _customer = value; }
        }

        [DataMember]
        public Product Product
        {
            get { return _product; }
            set { _product = value; }
        }

        [DataMember]
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        [DataMember]
        public double TotalAmount
        {
            get { return _total; }
            set { _total = value; }
        }
    }

    [DataContract]
    public class Customer
    {
        int _customerID;
        string _customerName;
        string _customerLocation;

        [DataMember]
        public int CustomerID
        {
            get { return _customerID; }
            set { _customerID = value; }
        }

        [DataMember]
        public string CustomerName
        {
            get { return _customerName; }
            set { _customerName = value; }
        }

        [DataMember]
        public string CustomerLocation
        {
            get { return _customerLocation; }
            set { _customerLocation = value; }
        }

    }

    [DataContract]
    public class Product
    {
        int _productID;
        string _productName;
        double _price;

        [DataMember]
        public int ProductId
        {
            get { return _productID; }
            set { _productID = value; }
        }

        [DataMember]
        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }

        [DataMember]
        public double Price
        {
            get { return _price; }
            set { _price = value; }


        }
    }
}
