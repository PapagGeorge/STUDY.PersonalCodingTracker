using System.Data;

namespace DataSetWithDataTable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DataTable Customers = new DataTable();

                DataColumn CustomerId = new DataColumn("Id", typeof(Int32));
                Customers.Columns.Add(CustomerId);
                DataColumn Phone = new DataColumn("Phone", typeof(string));
                Customers.Columns.Add(Phone);
                DataColumn Email = new DataColumn("Email", typeof(string));
                Customers.Columns.Add(Email);

                Customers.Rows.Add(1, 6030862, "papag@hi5.gr");
                Customers.Rows.Add(2, 6030863, "papag@hi6.gr");
                Customers.Rows.Add(3, 6030864, "papag@hi7.gr");

                DataTable Orders = new DataTable();

                DataColumn OrderId = new DataColumn("OrderId", typeof(Int32));
                Orders.Columns.Add(OrderId);
                DataColumn CustId = new DataColumn("CustomerId", typeof(Int32));
                Orders.Columns.Add(CustId);
                DataColumn OrderAmount = new DataColumn("Amount", typeof(decimal));
                Orders.Columns.Add(OrderAmount);

                Orders.Rows.Add(1, 1, 19.90);
                Orders.Rows.Add(2, 2, 9.90);
                Orders.Rows.Add(3, 3, 5.90);

                DataSet dataSet = new DataSet();
                dataSet.Tables.Add(Customers);
                dataSet.Tables.Add(Orders);

                Console.WriteLine("Customers Table");
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    Console.WriteLine(row["Id"] + " " + row["Phone"] + " " + row["Email"]);
                }
                Console.WriteLine($"\n------------------------\n");
                

                Console.WriteLine("Orders Table");
                foreach (DataRow row in dataSet.Tables[1].Rows)
                {
                    Console.WriteLine(row["OrderId"] + " " + row["CustomerId"] + " " + row["Amount"]);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to retrieve data from database" + ex.Message);
            }
        }
           
    }
}
