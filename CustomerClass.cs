using PizzaClass;
using MenuClass;
using static PizzeriaApplication;

namespace CustomerClass
{
    public class Customer
    {
        private string customerName;
        private string customerAdress;
        private string customerTelegramID;
        private DateTime timeOfTheOrder;

        public Customer (string customerName, string customerAdress, string customerTelegramID)
        {
            this.customerName = customerName;
            this.customerAdress = customerAdress;
            this.customerTelegramID = customerTelegramID;
            this.timeOfTheOrder = timeOfTheOrder;
        }

        public void setInformation (string customerName, string customerAdress, string customerTelegramID)
        {
            this.customerName = customerName;
            this.customerAdress = customerAdress;
            this.customerTelegramID = customerTelegramID;
            
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
            return customerTelegramID;
        }

        public void information ()
        {
            System.Console.WriteLine($"Ваше имя {customerName}. Адрес {customerAdress}. Telegram ID {customerTelegramID}");
        }

        public Pizza pizzaSelection(Menu menu)
        {
            menu.show();
            System.Console.Write("\nВыберите пиццу (Введите цифру): ");
            
            int choosenPizzaNumber = Convert.ToInt32(Console.ReadLine());
            Pizza selectedPizza = new ();

            // Здесь закончил сегодня. Завтра продолжу переводить поля Menu в private и работу на выбором пиццы (или нескольких пицц) и напитков
            
            if (choosenPizzaNumber <= menu.pizzaMenu.Count())
            {
                for (int pizzaNumber = 1; pizzaNumber <= menu.pizzaMenu.Count(); pizzaNumber++)
                {
                    if (pizzaNumber == choosenPizzaNumber)
                    {
                        selectedPizza = menu.pizzaMenu[pizzaNumber - 1];
                        Console.WriteLine("\nВы выбрали: " + selectedPizza.getName() + "\n\nОжидайте свой заказ. Статус заказа будет отправляться вам в телеграм");
                    }
                }
                
            }
            
            return selectedPizza;
             
        }
    }
}