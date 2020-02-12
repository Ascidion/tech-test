using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

namespace AnyCompany
{
    public static class CustomerRepository
    {
        // get connection string from config
        private static string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["cn_Customer"].ConnectionString;

        public static Customer Load(int customerId)
        {
            Customer customer = new Customer();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT * FROM Customer WHERE CustomerId = " + customerId, connection);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        customer.Name = reader["Name"].ToString();
                        customer.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                        customer.Country = reader["Country"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                // Add exception handling and event logging
                using (EventLog eventLog = new EventLog("Application"))
                {
                    eventLog.Source = "AnyCompanyApplication";
                    eventLog.WriteEntry("Error retrieving customer data. Error description: " + ex.Message, EventLogEntryType.Error, 101, 1);
                }
            }

            return customer;

            
        }

        public static ICollection<Customer> LoadCustomers()
        {
            List<Customer> result = new List<Customer>();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT * FROM Customers", connection);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Customer customer = new Customer();
                        customer.CustomerId = int.Parse(reader["CustomerId"].ToString());
                        customer.Name = reader["Name"].ToString();
                        customer.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                        customer.Country = reader["Country"].ToString();
                        result.Add(customer);
                    }
                }

            }
            catch (Exception ex)
            {
                // Add exception handling and event logging
                using (EventLog eventLog = new EventLog("Application"))
                {
                    eventLog.Source = "AnyCompanyApplication";
                    eventLog.WriteEntry("Error retrieving order data. Error description: " + ex.Message, EventLogEntryType.Error, 101, 1);
                }
            }

            return result;

        }

    }
}
