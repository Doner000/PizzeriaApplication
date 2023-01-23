// using System.IO;
// using PizzaClass;
// using MenuClass;
// using CustomerClass;
// using OrderClass;
// using CookerClass;
// using CourierClass;
// class PizzeriaApplicationArchiveOOP
// {
    
//     static void Main1()
//     {
//         Queue <Order> ordersQueue = new ();
//         Queue<Order> warehouseQueue = new Queue<Order>();

//         Cooker cookerDamir = new Cooker("Дамир", 120);
//         Cooker cookerAdil = new Cooker("Адиль", 160);
//         Queue <Cooker> cookersQueue = new ();
//         cookersQueue.Enqueue(cookerDamir);
//         cookersQueue.Enqueue(cookerAdil);

//         Courier courier = new Courier("Елжас");

//         List<Dish> menu = new List<Dish>();
//         menu.Add(new Dish("Моцаррела", "2000 тенге"));
//         menu.Add(new Dish("Пепперони", "2350 тенге"));
//         menu.Add(new Dish("Пицца с ананасами", "2650 тенге"));

        

//         //Заказ


//         showMenu(menu);
//         // Выбор пиццы покупателем
//         System.Console.WriteLine("Желаете сделать заказ? (Да/Нет)");
//         string answer = Console.ReadLine();
//         Customer customer = new ("","","");
//         if(answer.ToLower() == "да")
//         {
//             System.Console.WriteLine("Введите свое имя");
//             string customerName = Console.ReadLine();
//             System.Console.WriteLine("Ваш адрес");
//             string customerAdress = Console.ReadLine();
//             System.Console.WriteLine("Введите свой telegram ID. Туда будет приходить актуальная информация по вашему заказу");
//             string telegramID = Console.ReadLine();
//             customer.setInformation(customerName,customerAdress,telegramID);
//             System.Console.WriteLine("Ваши данные?\n ");
//             customer.information();
//         }        
        
//         else
//         {
//             System.Console.WriteLine("Неправильный ввод данных. Попробуйте снова");
//             return;
//         }

//         Dish selectedPizza = new Dish();
//         selectedPizza = customer.pizzaSelection(menu);
//         Order order = new Order(selectedPizza,customer);
        
//         ordersQueue.Enqueue(order);
        
//         System.Console.WriteLine(order.getSelectedPizzaName());


//         //Кухня

//         // Готовка пиццы пекарем
//         Order takenOrder = ordersQueue.Dequeue();
//         Cooker currentCooker = cookersQueue.Dequeue();

//         System.Console.WriteLine($"Пекарь {currentCooker.getCookerName()} принял ваш заказ.\nВаш заказ {takenOrder.getSelectedPizzaName()} в процессе готовки");

//         System.Console.WriteLine($"Готовка заказа займет {currentCooker.getCookingSpeed()} секунд");
//         Thread.Sleep(currentCooker.getCookingSpeed());
//         System.Console.WriteLine($"Заказ {takenOrder.getSelectedPizzaName()} отправляется на склад");
//         Thread.Sleep(5000);//Типо 5 сек относит на склад

//         //Передача готового заказа на склад
//         warehouseQueue.Enqueue(takenOrder);
//         System.Console.WriteLine("Заказ на складе. Курьер скоро заберет ваш заказ со склада");

//         Thread.Sleep(5000);


//         //Склад

//         //Когда буду раскидывать по таскам, нужно дополнить код

//         System.Console.WriteLine("ЗАКАЗЫ НА СКЛАДЕ");//потом убрать это
//         foreach (Order queue in warehouseQueue)
//         {
//             Console.WriteLine(queue.getSelectedPizzaName());
//         }

//         Order orderToDeliver = warehouseQueue.Dequeue();
//         Thread.Sleep(10000);
//         System.Console.WriteLine($"Курьер {courier.getName()} получил ваш заказ\nЧерез 30 секуд курьер будет у вас");
//         Thread.Sleep(30000);
//         System.Console.WriteLine($"Заказ {order.getSelectedPizzaName()} доставлен.\nСпасибо за покупку!!!");

//     }

//     public static void showMenu(List<Dish> menu)
//     {
//         Console.WriteLine("Меню заведения:\n");
//         for(int pizzaNumber = 1; pizzaNumber <= menu.Count(); pizzaNumber++)
//         {
//             Console.WriteLine(pizzaNumber + ") " + menu[pizzaNumber - 1].getDishNameAndPrice());
//         }
//     }

// }    