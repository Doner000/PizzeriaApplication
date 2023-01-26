using MenuClass;

namespace PizzaClass
{
    public class Pizza
    {
        private string name;
        public int price;

        public List<Pizza> menu = new List<Pizza>();

        public Pizza (string name, int price)
        {
            this.name = name;
            this.price = price;
            List<Pizza> menu = new List<Pizza>();

        }
        public Pizza()
        {

        }

        public string getNameAndPrice()
        {
            return name + " - " + price;
        }

        public string getName()
        {
            return name;
        }

        public int GetPrice ()
        {
            return price;
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
