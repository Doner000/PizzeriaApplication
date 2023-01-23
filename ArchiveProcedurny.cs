using System.IO;
class PizzeriaApplicationArchive
{
    //ПРОЦЕДУРНЫЙ КООООООООООООООД
    static void Main2()
    {
        Queue<string> ordersQueue = new Queue<string>();
        Queue<string> warehouseQueue = new Queue<string>();
        
        Console.WriteLine("Pizzeria");
        
        List<string> menu = new ();
        menu.Add("Моцарелла - 2 000 тенге");
        menu.Add("Пепперони - 2 350 тенге");
        menu.Add("Пицца с ананасами - 2 650 тенге");
        
            Task taskCustomer = Task.Run 
            ( 
                () => 
                {
                    customerTask(menu,ordersQueue);
                }
            );
            

            Task taskKitchen = Task.Run
            (
                () =>
                { 
                    Thread.Sleep(60000);
                    kitchenTask(ordersQueue, warehouseQueue);
                }
            );

            Task taskCourier = Task.Run
            (
                () =>
                {
                    courierTask(warehouseQueue);
                }
            );
        
        taskCustomer.Wait();
        taskKitchen.Wait();
        taskCourier.Wait();
        
        
        
    }

    public static void kitchenTask (Queue<string> ordersQueue, Queue<string> warehouseQueue)
    {
        
            //Проверка очереди и запись очереди в Queue
        while (true)
        {
            // if (ordersQueue.Count() == 0)
            // {
            //     // ordersQueue = readOrdersFromFile("C:\\Users\\User\\Desktop\\Программирование\\PizzeriaApplication\\OrdersQueue.txt");
            // }

            
            if(ordersQueue.Count() > 0)
            {
                Thread.Sleep(3000);
            //Передача первого заказа Пекарю
            string takenOrder = ordersQueue.Dequeue();
            
            Console.WriteLine("Ваш заказ - " + takenOrder + " в процессе готовки");
            

            //Процесс готовки заказа (таймером)
            Console.WriteLine("Готовка займет 30 секунд\n");
            Thread.Sleep(30000);
            Console.WriteLine($"Заказ {takenOrder}  отправляется на склад");

            //Передача готового заказа на склад
            warehouseQueue.Enqueue(takenOrder);
            Console.WriteLine("Заказ на складе. Курьер скоро заберет ваш заказ со склада");
            
            //запись готового заказа в файл
            // StreamWriter sw = new StreamWriter("C:\\Users\\User\\Desktop\\Программирование\\PizzeriaApplication\\PizzeriaWarehouseAndDeliver\\Queue.txt", true);
            // sw.WriteLine(takenOrder);
            // sw.Close();

            //Удаление первой строки файла
            // StreamWriter sW = new StreamWriter("C:\\Users\\User\\Desktop\\Программирование\\PizzeriaApplication\\OrdersQueue.txt");
            // for (int i = ordersQueue.Count(); i > 0; i--)
            // {
            //     sw.WriteLine(ordersQueue.Dequeue);
            // }
            // sW.Close();

            Thread.Sleep(5000);
            }
            
        }    

            
            
        
    }


    public static void customerTask (List<string> menu, Queue<string> queue)
    {
       
        // Показать меню покупателю
        while (true)
        {
            Console.WriteLine("Меню заведения");
            for(int pizzaNumber = 1; pizzaNumber <= menu.Count(); pizzaNumber++)
            {
                Console.WriteLine(pizzaNumber + ")" + menu[pizzaNumber - 1]);
            }

            // Покупатель выбирает пиццу

            Console.WriteLine("Выберите пиццу");

            int choosenPizzaNumber = Convert.ToInt32(Console.ReadLine());
            string selectedPizza = "error";
            for (int pizzaNumber = 1; pizzaNumber <= menu.Count(); pizzaNumber++)
            {
                if (pizzaNumber == choosenPizzaNumber)
                {
                    selectedPizza = menu[pizzaNumber - 1];
                    Console.WriteLine("Вы выбрали: " + selectedPizza + "\nОжидайте свой заказ. Статус заказа будет отправляться вам в телеграм");
                }
            }
            if (selectedPizza == "error")
            {
                return;
            }

            // Добавление заказа в очередь
            queue.Enqueue(selectedPizza);

            // StreamWriter sw = new StreamWriter("C:\\Users\\User\\Desktop\\Программирование\\PizzeriaApplication\\OrdersQueue.txt", true);
            // sw.WriteLine(selectedPizza);
            // sw.Close();
        }
            
        
    }    

    public static void courierTask (Queue<string> warehouseQueue)
    {
        while(true)
        {
            // Console.WriteLine("Заказы ожидающие отправки покупателям");//тут еще в каждом заказе будет инфа о покупателе(имя, адрес и тд.)
            // while(warehouseQueue.Count() == 0)
            // {
            //     warehouseQueue = readOrdersFromFile("C:\\Users\\User\\Desktop\\Программирование\\PizzeriaApplication\\PizzeriaWarehouseAndDeliver\\Queue.txt");
            //     Thread.Sleep(15000);
            // }

            while (warehouseQueue.Count() == 0)
            {
                Thread.Sleep(30000);
            }
            

            Thread.Sleep(35000);
            System.Console.WriteLine("ЗАКАЗЫ НА СКЛАДЕ:");
            foreach (string queue in warehouseQueue)
            {
                Console.WriteLine(queue);
            }
            string selectedPizza = warehouseQueue.Dequeue();
            Thread.Sleep(10000);
            System.Console.WriteLine($"Курьер получил заказ {selectedPizza} \nЧерез 30 секунд курьер будет у вас");
            Thread.Sleep(30000);
            System.Console.WriteLine($"Заказ {selectedPizza} доставлен. Спасибо за покупку!!!");
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