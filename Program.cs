using System.IO;
using PizzaClass;
using MenuClass;
using CustomerClass;
using OrderClass;
using CookerClass;
using CourierClass;
using DrinksClass;
using Npgsql;
class PizzeriaApplication
{
    
    static void Main()
    {
        Queue <Order> ordersQueue = new ();
        Queue<Order> warehouseQueue = new Queue<Order>();

        Cooker cookerDamir = new Cooker("Damir", 120);
        Cooker cookerAdil = new Cooker("Адиль", 160);

        Queue <Cooker> cookersQueue = new ();
        cookersQueue.Enqueue(cookerDamir);
        cookersQueue.Enqueue(cookerAdil);

        Courier courier = new Courier("Елжас");

        List <Pizza> pizzaMenu = new List<Pizza>();
        pizzaMenu.Add(new Pizza("Моцаррела", "2000 тенге"));
        pizzaMenu.Add(new Pizza("Пепперони", "2350 тенге"));
        pizzaMenu.Add(new Pizza("Пицца с ананасами", "2650 тенге"));

        List <Drinks> drinksMenu = new List<Drinks>();
        drinksMenu.Add(new Drinks("Coca-Cola (0.5l)", "500 тенге"));
        drinksMenu.Add(new Drinks("Fanta (0.5l)", "500 тенге"));
        drinksMenu.Add(new Drinks("Fuse-Tea (0.5l)", "500 тенге"));

        Menu menu = new Menu(pizzaMenu, drinksMenu);

        

        //Заказ

        Task taskCustomer = Task.Run
        (
            () =>
            {
                customerTask(menu,ordersQueue);
                
            }
        );

        //Кухня

        Task taskKitchen = Task.Run
        (
            () =>
            {
                kitchenTask(ordersQueue,warehouseQueue,cookersQueue);
            }
        );

        //Склад
        
        Task taskCourier = Task.Run
        (
            () =>
            {
                courierTask(warehouseQueue,courier);
            }
        );

        taskCustomer.Wait();
        taskKitchen.Wait();
        taskCourier.Wait();

    }

    private static void customerTask (Menu menu, Queue<Order> ordersQueue)
    {
        while (true)
        {
            menu.show();
            // Выбор пиццы покупателем
            System.Console.Write("\nЖелаете сделать заказ? (Да/Нет) ");
            string answer = Console.ReadLine();
            Customer customer = new ("","","");

            if(answer.ToLower() == "да" || answer.ToLower() == "нет")
            {
                if(answer.ToLower() == "да")
                {
                    System.Console.Write("\nВведите свое имя: ");
                    string customerName = Console.ReadLine();
                    System.Console.Write("\nВаш адрес: ");
                    string customerAdress = Console.ReadLine();
                    System.Console.Write("\nВведите свой telegram ID. Туда будет приходить актуальная информация по вашему заказу: ");
                    string telegramID = Console.ReadLine();
                    customer.setInformation(customerName, customerAdress, telegramID);
                    System.Console.WriteLine("\nВаши данные:\n");
                    customer.information();

                    Pizza selectedPizza = new Pizza();
                    selectedPizza = customer.pizzaSelection(menu);
                    Order order = new Order();
                    if (selectedPizza.getName() != null)
                    {
                        order.setInformation(selectedPizza,customer);
                        ordersQueue.Enqueue(order);
                        Thread.Sleep(5000);
                    }
                }
                
                
            }

            else
            {
                System.Console.WriteLine("\nНеправильный ввод данных. Попробуйте снова");
                Thread.Sleep(5000);
            }

            
            
        }
        
    }

    public static void kitchenTask (Queue<Order> ordersQueue, Queue<Order> warehouseQueue,Queue <Cooker> cookersQueue)
    {
        while (true)
        {

            if(ordersQueue.Count() > 0)
            {
                Thread.Sleep(15000);
                // Готовка пиццы пекарем
                Order takenOrder = ordersQueue.Dequeue();
                Cooker currentCooker = cookersQueue.Dequeue();

                System.Console.WriteLine($"\n\n{takenOrder.getCurrentCustomerName()}, пекарь {currentCooker.getCookerName()} принял ваш заказ.\n\n{takenOrder.getSelectedPizzaName()} в процессе готовки");

                System.Console.WriteLine($"\nГотовка займет {currentCooker.getCookingSpeed()} секунд");
                Thread.Sleep(currentCooker.getCookingSpeed()*1000);
                System.Console.WriteLine($"\n{takenOrder.getSelectedPizzaName()} отправляется на склад");
                Thread.Sleep(5000);//Типо 5 сек относит на склад

                //Передача готового заказа на склад
                warehouseQueue.Enqueue(takenOrder);
                System.Console.WriteLine("\nЗаказ на складе. Курьер скоро заберет ваш заказ со склада");
                cookersQueue.Enqueue(currentCooker);

                Thread.Sleep(5000);
            }
            else
            {
                Thread.Sleep(15000);
            }    
        }
        
    }

    public static void courierTask (Queue<Order> warehouseQueue, Courier courier)
    {

        while (true)
        {
            

            while (warehouseQueue.Count() == 0)
            {
                Thread.Sleep(30000);
            }

            Thread.Sleep(35000);

            System.Console.WriteLine("\nЗАКАЗЫ НА СКЛАДЕ");//потом убрать это
            foreach (Order queue in warehouseQueue)
            {
                Console.WriteLine(queue.getSelectedPizzaName());
            }

            Order orderToDeliver = warehouseQueue.Dequeue();
            Thread.Sleep(10000);
            System.Console.WriteLine($"\nКурьер {courier.getName()} получил ваш заказ\n\nЧерез 30 секуд курьер будет у вас");
            Thread.Sleep(30000);
            System.Console.WriteLine($"\nЗаказ {orderToDeliver.getSelectedPizzaName()} доставлен.\n\nСпасибо за покупку!!!");
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