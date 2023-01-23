using PizzaClass;

namespace MenuClass
{
        public class Menu
    {
        public List<Pizza> menu = new List<Pizza>();
        
        public void pizzaSelection()
        {
            System.Console.WriteLine("\nВыберите пиццу (Введите цифру)");
            int choosenPizzaNumber = Convert.ToInt32(Console.ReadLine());
            Pizza selectedPizza = new ();
            for (int pizzaNumber = 1; pizzaNumber <= menu.Count(); pizzaNumber++)
            {
                if (pizzaNumber == choosenPizzaNumber)
                {
                    selectedPizza = menu[pizzaNumber - 1];
                    Console.WriteLine("Вы выбрали: " + selectedPizza.getPizzaName() + "\nОжидайте свой заказ. Статус заказа будет отправляться вам в телеграм");
                }
            }
            if (selectedPizza.getPizzaName() == null)
            {
                return;
            }
        }

        public void setDish (Pizza pizza)
        {
            menu.Add(pizza);
        }

        public void showMenu()
        {
            Console.WriteLine("Меню заведения:\n");
            for(int pizzaNumber = 1; pizzaNumber <= menu.Count(); pizzaNumber++)
            {
                Console.WriteLine(pizzaNumber + ") " + menu[pizzaNumber - 1].getPizzaNameAndPrice());
            }
        }


    } 
}
