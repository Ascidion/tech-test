using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany
{
    public class CustomerService
    {
        private OrderRepository orderRepository;

        public CustomerService()
        {
            orderRepository = new OrderRepository();
        }

        public ICollection<Customer> GetCustomersAndTheirOrders()
        {
            IList<Customer> result = new List<Customer>();
            var customerList = CustomerRepository.LoadCustomers();

            // Get the orders for each customer
            foreach (Customer customer in customerList)
            {
                customer.CustomerOrders = orderRepository.LoadOrdersByCustomer(customer.CustomerId);
                result.Add(customer);
            }

            // Return a list of customers with their orders added to their CustomerOrders collections.
            return result;

        }


    }
}
