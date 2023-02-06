using Npgsql;
using PizzaClass;
using DrinksClass;
using CustomerClass;
using OrderClass;
using CourierClass;
using CookerClass;


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



        public void insertOrder(List<Pizza> selectedPizzas, List<Drinks> selectedDrinks, Customer customer, Order order)
        {
            
            var timeStamp = DateTime.Now;
            System.Console.WriteLine("Время заказа: " + timeStamp);
            

            NpgsqlConnection conn = new NpgsqlConnection(connString);

            try
            {
                conn.Open();

                //insert customerid and date to orders table
                using ( NpgsqlCommand command = new NpgsqlCommand("INSERT INTO Orders (CustomerId, Date) VALUES (@CI1, @D1)", conn))
                {
                    command.Parameters.AddWithValue("CI1", customer.GetId());
                    command.Parameters.AddWithValue("D1", timeStamp);
                    command.ExecuteNonQuery();
                    System.Console.WriteLine("Запись CustomerId внесена в таблицу Orders");
                }
                conn.Close();

                conn.Open();
                //get orderId from orders table
                using ( NpgsqlCommand command = new NpgsqlCommand("Select OrderId from Orders where CustomerId = @CI1 and Date = @D1 ", conn))
                {
                    command.Parameters.AddWithValue("CI1" , customer.GetId());
                    command.Parameters.AddWithValue("D1", timeStamp);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        order.setId(reader.GetInt32(0));
                    }

                    System.Console.WriteLine("ID заказа - " + order.getId());
                }

                conn.Close();

                

                for (int i = 0; i < selectedPizzas.Count(); i++)
                {
                    conn.Open();
                    using ( NpgsqlCommand command = new NpgsqlCommand("INSERT INTO OrdersMenuItems (OrderId, MenuItemsId) VALUES (@OI1 , @MI1)", conn))
                    {
                        command.Parameters.AddWithValue("OI1", order.getId() );
                        command.Parameters.AddWithValue("MI1", selectedPizzas[i].GetId());
                        command.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                
                for (int i = 0; i < selectedDrinks.Count(); i++)
                    {
                        conn.Open();
                        using ( NpgsqlCommand command = new NpgsqlCommand("INSERT INTO OrdersMenuItems (OrderId, MenuItemsId) VALUES (@OI1 , @MI1)", conn))
                        {
                            command.Parameters.AddWithValue("OI1", order.getId() );
                            command.Parameters.AddWithValue("MI1", selectedDrinks[i].GetId());
                            command.ExecuteNonQuery();
                        }
                        conn.Close();
                    }


                // using ( NpgsqlCommand command = new NpgsqlCommand("INSERT INTO OrdersMenuItems (OrderId, MenuItemsId) VALUES (@OI1 , @MI1)", conn))
                // {
                //     // foreach (var item in selectedPizzas)
                //     // {
                //     //     System.Console.WriteLine( "Пицца - " + item.getName());
                //     //     command.Parameters.AddWithValue("OI1", order.getId());
                //     //     command.Parameters.AddWithValue("MI1", item.GetId());
                //     //     command.ExecuteNonQuery();
                //     // }

                //     for (int i = 0; i < selectedPizzas.Count(); i++)
                //     {
                //         command.Parameters.AddWithValue("OI1", order.getId() );
                //         command.Parameters.AddWithValue("MI1", selectedPizzas[i].GetId());
                //         command.ExecuteNonQuery();
                //     }

                    

                //     // foreach (var item in selectedDrinks)
                //     // {
                //     //     System.Console.WriteLine( "Напиток - " + item.getName());
                //     //     command.Parameters.AddWithValue("OI1", order.getId());
                //     //     command.Parameters.AddWithValue("MI1", item.GetId());
                //     //     command.ExecuteNonQuery();
                //     // }
                // }
            }

            catch(Exception ex)
            {
                System.Console.WriteLine(ex);
            }   

            conn.Close();
        }

        public void ReadCourier (Queue <Courier> courierQueue)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connString);
            try
            {
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand("select * from Courier", conn);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    int completedOrdersCount;
                    
                    if(reader.IsDBNull(2))
                    {
                        completedOrdersCount = 0;
                    }
                    else
                    {
                        completedOrdersCount = reader.GetInt32(2);
                    }

                    Courier courier = new Courier(id, name, completedOrdersCount);
                    courierQueue.Enqueue(courier);
                }
                

            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }
        }

        public void SetCourierId(Courier courier, Order order)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connString);
            try
            {
                conn.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("UPDATE Orders SET CourierId = @CID WHERE orderId = @OID", conn))
                {       
                    command.Parameters.AddWithValue("CID", courier.GetID());
                    command.Parameters.AddWithValue("OID", order.getId());
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
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

        public void ReadCooker(Queue<Cooker> cookersQueue)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connString);
            try
            {
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand("select * from cooker", conn);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    int cookingSpeed;
                    
                    if(reader.IsDBNull(2))
                    {
                        cookingSpeed = 0;
                    }
                    else
                    {
                        cookingSpeed = reader.GetInt32(2);
                    }

                    Cooker cooker = new Cooker(id, name, cookingSpeed);
                    cookersQueue.Enqueue(cooker);
                }
                

            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }
        }

        public void SetCookerIdToOrder(Cooker cooker, Order order)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connString);
            try
            {
                conn.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("UPDATE Orders SET CookerId = @CID WHERE orderId = @OID", conn))
                
                {
                    command.Parameters.AddWithValue("CID", cooker.GetID());
                    command.Parameters.AddWithValue("OID", order.getId());
                    command.ExecuteNonQuery();
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
