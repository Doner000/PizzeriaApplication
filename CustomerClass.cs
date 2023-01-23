using PizzaClass;
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

        public Pizza pizzaSelection(List <Pizza> menu)
        {
            PizzeriaApplication.showMenu(menu);
            System.Console.Write("\nВыберите пиццу (Введите цифру): ");
            
            int choosenPizzaNumber = Convert.ToInt32(Console.ReadLine());
            Pizza selectedPizza = new ();
            
            if (choosenPizzaNumber <= menu.Count())
            {
                for (int pizzaNumber = 1; pizzaNumber <= menu.Count(); pizzaNumber++)
                {
                    if (pizzaNumber == choosenPizzaNumber)
                    {
                        selectedPizza = menu[pizzaNumber - 1];
                        Console.WriteLine("\nВы выбрали: " + selectedPizza.getPizzaName() + "\n\nОжидайте свой заказ. Статус заказа будет отправляться вам в телеграм");
                    }
                }
                
            }
            
            return selectedPizza;
             
        }
    }
}