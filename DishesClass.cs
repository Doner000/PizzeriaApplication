using MenuClass;

namespace PizzaClass
{
    public class Pizza
    {
        private string pizzaName;
        public string price;

        public List<Pizza> menu = new List<Pizza>();

        public Pizza (string pizzaName, string price)
        {
            this.pizzaName = pizzaName;
            this.price = price;
            List<Pizza> menu = new List<Pizza>();

        }
        public Pizza()
        {

        }

        public string getPizzaNameAndPrice()
        {
            return pizzaName + " - " + price;
        }

        public string getPizzaName()
        {
            return pizzaName;
        }

        // public void showMenu(List<Dish> menu)
        // {
        //     Console.WriteLine("Меню заведения:\n");
        //     for(int pizzaNumber = 1; pizzaNumber <= menu.Count(); pizzaNumber++)
        //     {
        //         Console.WriteLine(pizzaNumber + ") " + menu[pizzaNumber - 1].getDishNameAndPrice());
        //     }
        // }
    }
}
