using System.IO;
using PizzaClass;
using MenuClass;
using CustomerClass;
using OrderClass;
using CookerClass;
using CourierClass;
using DrinksClass;
using DataBaseClass;
using Npgsql;

class PizzeriaApplication
{
    
    static void Main()
    {
        // DB dataBase = new DB ();
        // dataBase.DataInput("Insert into pizza (Name, Price) values ('Пепперони с ананасами', 3000)");

        //Нужно получать данные с базы и уже из них создавать объекты
        
        DB dataBase = new DB();

        Queue <Order> ordersQueue = new ();
        Queue<Order> warehouseQueue = new Queue<Order>();

        Queue <Cooker> cookersQueue = new ();
        dataBase.ReadCooker(cookersQueue);
        
        Queue <Courier> courierQueue = new Queue<Courier>(); 
        dataBase.ReadCourier(courierQueue);

        List <Pizza> pizzaMenu = new List<Pizza>();
        dataBase.ReadPizzaItems(pizzaMenu);

        List <Drinks> drinksMenu = new List<Drinks>();
        dataBase.ReadDrinkItems(drinksMenu);



        // drinksMenu.Add(new Drinks("Coca-Cola (0.5l)", 500));
        // drinksMenu.Add(new Drinks("Fanta (0.5l)", 500));
        // drinksMenu.Add(new Drinks("Fuse-Tea (0.5l)", 500));

        Menu menu = new Menu(pizzaMenu, drinksMenu);

        

        //Заказ

        Task taskCustomer = Task.Run
        (
            () =>
            {
                customerTask(menu,ordersQueue,dataBase);
                
            }
        );

        //Кухня

        Task taskKitchen = Task.Run
        (
            () =>
            {
                kitchenTask(ordersQueue, warehouseQueue, cookersQueue, dataBase);
            }
        );

        //Склад
        
        Task taskCourier = Task.Run
        (
            () =>
            {
                courierTask(warehouseQueue,courierQueue,dataBase);
            }
        );

        taskCustomer.Wait();
        taskKitchen.Wait();
        taskCourier.Wait();

    }

    private static void customerTask (Menu menu, Queue<Order> ordersQueue, DB dataBase)
    {
        while (true)
        {
            menu.show();
            // Выбор пиццы покупателем
            System.Console.WriteLine("Здравствуйте!\n");
            System.Console.Write("\nЖелаете сделать заказ? (Да/Нет) ");
            string answer = Console.ReadLine();
            Customer customer = new ();

            if(answer.ToLower() == "да" || answer.ToLower() == "нет")
            {
                if(answer.ToLower() == "да")
                {
                    System.Console.Write("\nВведите свое имя: ");
                    string customerName = Console.ReadLine();
                    System.Console.Write("\nВаш адрес: ");
                    string customerAdress = Console.ReadLine();
                    System.Console.Write("\nВведите свой telegram ID. Туда будет приходить актуальная информация по вашему заказу: ");
                    string telegramUserName = Console.ReadLine();
                    customer.setInformation(customerName, customerAdress, telegramUserName, dataBase, customer);
                    System.Console.WriteLine("\nВаши данные:\n");
                    customer.information();

                    Order order = new Order();
                    
                    customer.MakingOrder(order,menu,customer,dataBase);
                    

                    ordersQueue.Enqueue(order);

                    System.Console.WriteLine("Ожидайте. Ваш заказ добавлен в очередь на готовку");
                }
                Thread.Sleep(5000);
            }

            else
            {
                System.Console.WriteLine("\nНеправильный ввод данных. Попробуйте снова");
                Thread.Sleep(5000);
            }
        }
        
    }

    public static void kitchenTask (Queue<Order> ordersQueue, Queue<Order> warehouseQueue,Queue <Cooker> cookersQueue, DB dataBase)
    {
        while (true)
        {
            if(ordersQueue.Count() > 0)
            {
                Thread.Sleep(15000);
                // Готовка пиццы пекарем
                Order takenOrder = ordersQueue.Dequeue();
                Cooker currentCooker = cookersQueue.Dequeue();

                System.Console.WriteLine($"\n\n{takenOrder.getCurrentCustomerName()}, пекарь {currentCooker.getCookerName()} принял ваш заказ.");

                System.Console.WriteLine($"\nГотовка займет {currentCooker.getCookingSpeed()} секунд");
                Thread.Sleep(currentCooker.getCookingSpeed()*1000);
                System.Console.WriteLine($"\n{takenOrder.getCurrentCustomerName()} Ваш заказ готов. Отправляем на склад");
                
                
                Thread.Sleep(5000);//Типо 5 сек относит на склад

                //Передача готового заказа на склад
                warehouseQueue.Enqueue(takenOrder);
                System.Console.WriteLine($"\nЗаказ на складе. Курьер скоро заберет заказ со склада");

                dataBase.SetCookerIdToOrder(currentCooker, takenOrder);

                cookersQueue.Enqueue(currentCooker);
                

                Thread.Sleep(5000);
            }
            else
            {
                Thread.Sleep(15000);
            }    
        }
        
    }

    public static void 
    courierTask (Queue<Order> warehouseQueue, Queue<Courier> couriersQueue, DB dataBase)
    {

        while (true)
        {
            while (warehouseQueue.Count() == 0)
            {
                Thread.Sleep(30000);
            }

            Thread.Sleep(35000);

            Courier courier = couriersQueue.Dequeue();
            Order orderToDeliver = warehouseQueue.Dequeue();

            System.Console.WriteLine($"\nКурьер {courier.getName()} получил ваш заказ\n\nЧерез 30 секуд курьер будет у вас");
            Thread.Sleep(30000);
            System.Console.WriteLine($"\nЗаказ доставлен.\n\nСпасибо за покупку!!!");

            dataBase.SetCourierId(courier,orderToDeliver);
            couriersQueue.Enqueue(courier);

            
        }
        
    }

    public static void showMenu(List<Pizza> menu)
    {
        Console.WriteLine("\nМеню заведения:\n");
        for(int pizzaNumber = 1; pizzaNumber <= menu.Count(); pizzaNumber++)
        {
            Console.WriteLine(pizzaNumber + ") " + menu[pizzaNumber - 1].getNameAndPrice());
        }
    }


    

    
    public static Queue<string> readOrdersFromFile (string fileName)
    {
        StreamReader sr = new StreamReader(fileName);
        Queue <string> queue = new Queue<string>();

        while (sr.Peek() > -1)
        {
            queue.Enqueue(sr.ReadLine());
        }
        sr.Close();

        return queue;
    }

    
  }





// ordersQueue.Enqueue(selectedPizza);
            // Console.WriteLine("Заказы в очереди:");
            // int odersNumberInQueue = 1;
            // foreach (string order in ordersQueue)
            // {
                
            //     Console.WriteLine(odersNumberInQueue + ") " + order);
            //     odersNumberInQueue++;
            // }


// Пекарь принимает заказ

        // string takenOrderByCooker = ordersQueue.Peek();
        // Console.WriteLine("Пекарь готовит заказ - " + takenOrderByCooker);

        // Пекарб приготовил о отнес пиццу а склад            