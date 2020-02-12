using System.Collections;
using System.Collections.Generic;

namespace AnyCompany
{
    public class OrderService
    {
        private readonly OrderRepository orderRepository = new OrderRepository();

        public bool PlaceOrder(Order order, int customerId)
        {
            Customer customer = CustomerRepository.Load(customerId);

            order.CustomerId = customerId;

            if (order.Amount == 0)
                return false;

            if (customer.Country == "UK")
                order.VAT = 0.2d;
            else
                order.VAT = 0;

            orderRepository.Save(order);

            return true;
        }


        public ICollection<Order> LoadOrdersByCustomer(int customerId)
        {
            return orderRepository.LoadOrdersByCustomer(customerId);
        }
    }
}
