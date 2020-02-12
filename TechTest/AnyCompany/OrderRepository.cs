using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

namespace AnyCompany
{
    internal class OrderRepository
    {

        // get connection string from config
        private static string ConnectionString; 

        public OrderRepository()
        {
            ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["cn_Order"].ConnectionString;
        }

        public void Save(Order order)
        {
            try
            {
                // using statement to ensure the connection closes
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("INSERT INTO Orders VALUES (@OrderId, @CustomerId, @Amount, @VAT)", connection);

                    command.Parameters.AddWithValue("@OrderId", order.OrderId);
                    command.Parameters.AddWithValue("@CustomerId", order.CustomerId);
                    command.Parameters.AddWithValue("@Amount", order.Amount);
                    command.Parameters.AddWithValue("@VAT", order.VAT);

                    command.ExecuteNonQuery();
                }
                    
            }
            catch (Exception ex)
            {
                // Add exception handling and event logging
                using (EventLog eventLog = new EventLog("Application"))
                {
                    eventLog.Source = "AnyCompanyApplication";
                    eventLog.WriteEntry("Error occured while creating order. Error description: " + ex.Message, EventLogEntryType.Error, 101, 1);
                }
            }

        }


        public ICollection<Order> LoadOrdersByCustomer(int customerId)
        {
            
            List<Order> result = new List<Order>();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT * FROM Order WHERE CustomerId = " + customerId, connection);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Order order = new Order();
                        order.CustomerId = int.Parse(reader["CustomerId"].ToString());
                        order.Amount = double.Parse(reader["Amount"].ToString());
                        order.OrderId = int.Parse(reader["OrderId"].ToString());
                        order.VAT = double.Parse(reader["VAT"].ToString());
                        result.Add(order);
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
