using Npgsql;
using PizzaClass;
using DrinksClass;
using CustomerClass;


namespace DataBaseClass
{
    public class DB
    {
        private string connString = "Server=localhost;Username=postgres;Database=postgres;Port=5432;Password=lfvbh2000";

        public DB ()
        {
            
        }

        public void DataInput (string sqlString)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connString);
            try
            {
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand(sqlString, conn);
                command.ExecuteNonQuery();
                System.Console.WriteLine("Данные внесены в базу");
                conn.Close();
            }

            catch(Exception ex)
            {
                System.Console.WriteLine(ex);
            }   
        }

        public void ReadPizzaItems (List<Pizza> pizzaMenu)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connString);
            try
            {
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand("select * from MenuItems where categoryid = 1", conn);
                var reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    int price = reader.GetInt32(2);
                    string description;

                    if(reader.IsDBNull(3))
                    {
                        description = "null";
                    }
                    else
                    {
                        description = reader.GetString(3);
                    }

                    if(description != "null")
                    {
                        pizzaMenu.Add(new Pizza(id,name,price, description));
                    }
                    else if (description == "null")
                    {
                        pizzaMenu.Add(new Pizza(id,name,price));
                    }
                    // Console.WriteLine(
                    //         string.Format(
                    //             "Reading from table=({0}, {1}, {2}, {3}, {4})",
                    //             reader.GetInt32(0).ToString(),
                    //             reader.GetString(1),
                    //             reader.GetInt32(2).ToString(),
                    //             description,
                    //             reader.GetInt32(4)
                    //             )
                    //         );
                }
                conn.Close();
            }
            catch(Exception ex)
            {
                System.Console.WriteLine(ex);
            }
        }

        public void ReadDrinkItems (List<Drinks> drinksMenu)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connString);
            try
            {
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand("select * from MenuItems where categoryid = 2", conn);
                var reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    int price = reader.GetInt32(2);
                    string description;

                    if(reader.IsDBNull(3))
                    {
                        description = "null";
                    }
                    else
                    {
                        description = reader.GetString(3);
                    }

                    if(description != "null")
                    {
                        drinksMenu.Add(new Drinks(id,name,price, description));
                    }
                    else if (description == "null")
                    {
                        drinksMenu.Add(new Drinks(id,name,price));
                    }
                    // Console.WriteLine(
                    //         string.Format(
                    //             "Reading from table=({0}, {1}, {2}, {3}, {4})",
                    //             reader.GetInt32(0).ToString(),
                    //             reader.GetString(1),
                    //             reader.GetInt32(2).ToString(),
                    //             description,
                    //             reader.GetInt32(4)
                    //             )
                    //         );
                }
                conn.Close();
            }
            catch(Exception ex)
            {
                System.Console.WriteLine(ex);
            }
        }

        public void CustomerGetId(string telegramUserName, Customer customer )
        {
            NpgsqlConnection conn = new NpgsqlConnection(connString);

            try
            {
                conn.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("select customerid from customer where TelegramUserName = @t1", conn))
                {
                    command.Parameters.AddWithValue("t1", telegramUserName);
                    var reader = command.ExecuteReader();

                    reader.Read();
                    int customerId = reader.GetInt32(0);

                    customer.SetId(customerId);
                    
                    
                    
                    // while (reader.Read())
                    // {
                    //     customerId = reader["telegramusername"].ToString();
                    //     customer.SetId(customerId);
                    // }
                    
                   
                    
                    
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }
        }

        public void InsertCustomerInfo(string customerName, string customerAdress, string telegramUserName)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connString);
            try
            {
                conn.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO Customer (Name, Adress, TelegramUserName) VALUES (@n1, @a1, @t1)", conn))
                {
                    command.Parameters.AddWithValue("n1", customerName);
                    command.Parameters.AddWithValue("a1", customerAdress);
                    command.Parameters.AddWithValue("t1", telegramUserName);

                    command.ExecuteNonQuery();
                    conn.Close();
                    System.Console.WriteLine("Данные сохранены");
                }
                
                
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }
        }

    }
}
