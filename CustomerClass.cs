using PizzaClass;
using MenuClass;
using DrinksClass;
using OrderClass;
using DataBaseClass;
using static PizzeriaApplication;

namespace CustomerClass
{
    public class Customer
    {
        private int customerId;
        private string customerName;
        private string customerAdress;
        private string telegramUserName;
        private DateTime timeOfTheOrder;

        public Customer (string customerName, string customerAdress, string telegramUserName)
        {
            this.customerName = customerName;
            this.customerAdress = customerAdress;
            this.telegramUserName = telegramUserName;
            // this.timeOfTheOrder = timeOfTheOrder;
        }

        public Customer ()
        {

        }

        public void setInformation (string customerName, string customerAdress, string telegramUserName, DB dataBase, Customer customer)
        {
            this.customerName = customerName;
            this.customerAdress = customerAdress;
            this.telegramUserName = telegramUserName;
            dataBase.InsertCustomerInfo(customerName, customerAdress, telegramUserName);
            dataBase.CustomerGetId(telegramUserName,customer);
            System.Console.WriteLine("ID - " + this.customerId);
            
        }

        public void SetId (int customerId)
        {
            this.customerId = customerId;
        }

        public int GetId ()
        {
            return customerId;
        }

        public string getCustomerName()
        {
            return customerName;
        }

        public string getCustomerAdress()
        {
            return customerAdress;
        }

        public string getcustomerTelegramID()
        {
            return telegramUserName;
        }

        public void information ()
        {
            System.Console.WriteLine($"Ваше имя {customerName}. Адрес {customerAdress}. Telegram Username {telegramUserName}");
        }

        public void MakingOrder (Order order ,Menu menu, Customer customer, DB dataBase)
        {
            Pizza selectedPizza = new ();
            List<Pizza> pizzaCart = new();
            Drinks selectedDrink = new();
            List<Drinks> drinksCart = new();

            

            bool wantMorePizzaOrNot = true;
            bool wantMoreDrinkOrNot = true;
            bool wantMoreOrderOrNot = true;

            while (wantMoreOrderOrNot)
            {
                menu.show();
                System.Console.WriteLine("\nВыберите категорию\n");
                System.Console.WriteLine("1) Пицца\n2) Напитки\n");
                int choosenCategory = Convert.ToInt32(Console.ReadLine());
                
                if (choosenCategory == 1)
                {
                    if(wantMorePizzaOrNot == false)
                    {
                        System.Console.WriteLine("Желаете добавить ещё пиццу? (Да/Нет)");
                        string answer = Console.ReadLine();
                        if (answer.ToLower() == "да")
                        {
                            wantMorePizzaOrNot = true;
                        }
                        else
                        {
                            System.Console.WriteLine("Попробуйте заново");
                        }
                    }
                    while (wantMorePizzaOrNot)
                    {
                        menu.showPizza();
                        
                        System.Console.Write("\nВыберите пиццу (Введите цифру): ");
                        int choosenNumber = Convert.ToInt32(Console.ReadLine());
                        

                        if (choosenNumber <= menu.pizzaCount())
                        {
                            System.Console.WriteLine("В каком количестве? (Введите цифру)");
                            int choosenCount = Convert.ToInt32(Console.ReadLine());

                            for (int pizzaNumber = 1; pizzaNumber <= menu.pizzaCount(); pizzaNumber++)
                            {
                                if (pizzaNumber == choosenNumber)
                                {
                                    selectedPizza = menu.PizzaMenu()[pizzaNumber - 1];
                                    for (int count = 1; count <= choosenCount; count++)
                                    {
                                        pizzaCart.Add(selectedPizza);
                                    }
                                    
                                    System.Console.WriteLine($"{selectedPizza.getName()} в количестве {choosenCount} штук добавлена в корзину\n");

                                    System.Console.WriteLine("Хотите заказать ещё пиццы? (Да/Нет)");
                                    string answerPizza = Console.ReadLine();
                                    if(answerPizza.ToLower() == "да")
                                    {
                                        wantMorePizzaOrNot = true;
                                    }
                                    else
                                    {
                                        wantMorePizzaOrNot = false;
                                        System.Console.WriteLine("Желаете добавить в заказ что-то из другой категории? (Да/Нет)");
                                        string wantAddToOrderOrNot = Console.ReadLine();
                                        if (wantAddToOrderOrNot.ToLower() == "да")
                                        {
                                            wantMoreOrderOrNot = true;
                                        }
                                        else
                                        {
                                            wantMoreOrderOrNot = false;
                                            
                                        }
                                    }
                                }
                                
                            }
                        }

                        else
                        {
                            System.Console.WriteLine("Неверно введены данные. Попробуйте заново");
                        }
                    }
                    
                }
                //Вопрос: Можно ли писать два условия if подряд без использования else, или же второе уловие if нужно засунуть в else
                else
                {
                    if (choosenCategory == 2)
                    {
                        if(wantMoreDrinkOrNot == false)
                        {
                            System.Console.WriteLine("Желаете добавить ещё напитки? (Да/Нет)");
                            string answer = Console.ReadLine();
                            if (answer.ToLower() == "да")
                            {
                                wantMoreDrinkOrNot = true;
                            }
                            else
                            {
                                System.Console.WriteLine("Попробуйте заново");
                            }
                        }
                        while (wantMoreDrinkOrNot)
                        {
                            menu.showDrinks();
                            System.Console.Write("\nВыберите напиток (Введите цифру): ");
                            int choosenNumber = Convert.ToInt32(Console.ReadLine());
                            

                            if (choosenNumber <= menu.drinksCount())
                            {
                                System.Console.WriteLine("В каком количестве? (Введите цифру)");
                                int choosenCount = Convert.ToInt32(Console.ReadLine());
                                for (int drinkNumber = 1; drinkNumber <= menu.drinksCount(); drinkNumber++)
                                {
                                    if (drinkNumber == choosenNumber)
                                    {
                                        selectedDrink = menu.DrinksMenu()[drinkNumber - 1];
                                        for (int count = 1; count <= choosenCount; count++)
                                        {
                                            drinksCart.Add(selectedDrink);
                                        }
                                        
                                        System.Console.WriteLine($"{selectedDrink.getName()} в количестве {choosenCount} штук добавлена в корзину\n");

                                        System.Console.WriteLine("Хотите заказать ещё напитков? (Да/Нет)");
                                        string answerDrink = Console.ReadLine();
                                        if(answerDrink.ToLower() == "да")
                                        {
                                            wantMoreDrinkOrNot = true;
                                        }
                                        else
                                        {
                                            wantMoreDrinkOrNot = false;
                                            System.Console.WriteLine("Желаете добавить в заказ что-то из другой категории? (Да/Нет)");
                                            string wantAddToOrderOrNot = Console.ReadLine();
                                            if (wantAddToOrderOrNot.ToLower() == "да")
                                            {
                                                wantMoreOrderOrNot = true;
                                            }
                                            else
                                            {
                                                wantMoreOrderOrNot = false;
                                                
                                            }
                                        }
                                    }
                                }
                            }

                            else 
                            {
                                System.Console.WriteLine("Неверно введены данные. Попробуйте снова");
                            }
                        }
                    }

                }
                
                
            }

            order.setInformation(pizzaCart, drinksCart, customer, dataBase, order);
            order.show();
            
            

            //Добавить возможность возвращаться к добавлению заказа после показа выбранных продуктов
            
        }

//         public Pizza pizzaSelection(Menu menu)
//         {
            
//             Pizza selectedPizza = new ();
//             List<Pizza> pizzaCart = new();
//             Drinks selectedDrink = new();
//             List<Drinks> drinksCart = new();

//             bool wantMoreOrNot = true;

//             // Здесь закончил сегодня. Завтра продолжу переводить поля Menu в private и работу на выбором пиццы (или нескольких пицц) и напитков
//             while (wantMoreOrNot)
//             {
//                 //Добавить категории в меню и в тут уже спрашивать какую категорию желают выбрать 
//                 menu.show();

//                 System.Console.Write("\nВыберите пиццу (Введите цифру): ");
//                 int choosenPizzaNumber = Convert.ToInt32(Console.ReadLine());

//                 if (choosenPizzaNumber <= menu.pizzaCount())
//                 {
//                     for (int pizzaNumber = 1; pizzaNumber <= menu.pizzaCount(); pizzaNumber++)
//                     {
//                         if (pizzaNumber == choosenPizzaNumber)
//                         {
//                             selectedPizza = menu.PizzaMenu()[pizzaNumber - 1];
//                             pizzaCart.Add(selectedPizza);
//                             System.Console.WriteLine($"{selectedPizza.getName()} добавлена в корзину\n");

//                             System.Console.WriteLine("Хотите заказать ещё пиццы?");
//                             string answerPizza = Console.ReadLine();
//                             if(answerPizza.ToLower() == "да")
//                             {
//                                 wantMoreOrNot = true;
//                             }
//                             else
//                             {
//                                 bool wantMoreDrinkOrNot = true;
//                                 System.Console.WriteLine("Хотите добавить напиток? (Да/Нет)");
//                                 string answerDrinks = Console.ReadLine();
//                                 if(answerDrinks.ToLower() == "да")
//                                 {
//                                     menu.showDrinks();
//                                     System.Console.WriteLine("Выберите напиток (Введите цифру)");
//                                     int choosenDrinkNumber = Convert.ToInt32(Console.ReadLine());
//                                     if (choosenDrinkNumber <= menu.drinksCount())
//                                     {
//                                         for (int drinkNumber = 1; drinkNumber <= menu.drinksCount(); drinkNumber++)
//                                         {
//                                             if (drinkNumber == choosenDrinkNumber)
//                                             {
//                                                 selectedDrink = menu.DrinksMenu()[drinkNumber - 1];
//                                                 drinksCart.Add(selectedDrink);
//                                                 System.Console.WriteLine($"{selectedDrink.getName()}");

//                                                 System.Console.WriteLine("Хотите добавить ещё напиток? (Да/Нет)");
//                                                 string answerWanrOrNot =  Console.ReadLine();
//                                                 if (answerWanrOrNot.ToLower() == "да" )
//                                                 {
//                                                     wantMoreDrinkOrNot = true;
//                                                 }
//                                                 else
//                                                 {
//                                                     wantMoreDrinkOrNot = false;
//                                                     wantMoreOrNot = false;
//                                                 }
                                
//                                             }
//                                         }
//                                     }
//                                 }

//                             }
//                         }
//                     }
                    
//                 }
//             }
//             Console.WriteLine("\nВы выбрали: " + selectedPizza.getName() + "\n\nОжидайте свой заказ. Статус заказа будет отправляться вам в телеграм");
            
//             return selectedPizza;
             
//         }
    }
}