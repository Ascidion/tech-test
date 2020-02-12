using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace AnyCompany.Test
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void Test_Customer_GetCustomersAndTheirOrders()
        {
            // arrange
            var customerService = new CustomerService();

            // act
            var result = customerService.GetCustomersAndTheirOrders();

            // assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Test_Order_PlaceOrder()
        {
            // arrange
            var orderService = new OrderService();
            int customerId = 1;
            Order order = new Order();
            order.CustomerId = customerId;
            order.Amount = 100.00;
            order.OrderId = 1;
            order.VAT = 0.14;

            // act
            var result = orderService.PlaceOrder(order, customerId);

            // assert
            Assert.IsNotNull(result);
        }

    }
}
