using PizzaClass;
using DrinksClass;

namespace MenuClass
{
        public class Menu
    {
        private List<Pizza> pizzaMenu = new List<Pizza>();
        private List<Drinks> drinksMenu = new List<Drinks>();
        
        public Menu (List<Pizza> pizzaMenu, List<Drinks> drinksMenu)
        {
            foreach (var pizza in pizzaMenu)
            {
                this.pizzaMenu.Add(pizza);
            }

            foreach (var drinks in drinksMenu)
            {
                this.drinksMenu.Add(drinks);
            }
        }

        public Menu (List<Pizza> pizzaMenu)
        {
           foreach (var pizza in pizzaMenu)
            {
                this.pizzaMenu.Add(pizza);
            } 
        }

        public Menu (List<Drinks> drinksMenu)
        {
           foreach (var drinks in drinksMenu)
            {
                this.drinksMenu.Add(drinks);
            } 
        }

        public List<Pizza> PizzaMenu ()
        {
            return pizzaMenu;
        }

        public List<Drinks> DrinksMenu ()
        {
            return drinksMenu;
        }

        public int pizzaCount()
        {
            return pizzaMenu.Count();
        }

        public int drinksCount()
        {
            return drinksMenu.Count();
        }
        

        

        public void set (Pizza pizza)
        {
            pizzaMenu.Add(pizza);
        }

        public void set (Drinks drinks)
        {
            drinksMenu.Add(drinks);
        }

        public void set (Pizza pizza, Drinks drinks )
        {
            pizzaMenu.Add(pizza);
            drinksMenu.Add(drinks);
        }

        public void show()
        {
            Console.WriteLine("Меню заведения:\n");
            System.Console.WriteLine("Меню пицц:\n");
            for(int pizzaNumber = 1; pizzaNumber <= pizzaMenu.Count(); pizzaNumber++)
            {
                Console.WriteLine(pizzaNumber + ") " + pizzaMenu[pizzaNumber - 1].getNameAndPrice());
            }

            System.Console.WriteLine("\nМеню напитков");
            for(int drinkNumber = 1; drinkNumber <= drinksMenu.Count(); drinkNumber++)
            {
                Console.WriteLine(drinkNumber + ") " + drinksMenu[drinkNumber - 1].getNameAndPrice());
            }
        }

        public void showPizza()
        {
            System.Console.WriteLine("Меню пицц:\n");
            for(int pizzaNumber = 1; pizzaNumber <= pizzaMenu.Count(); pizzaNumber++)
            {
                Console.WriteLine(pizzaNumber + ") " + pizzaMenu[pizzaNumber - 1].getNameAndPrice());
            }
        }

        public void showDrinks()
        {
            System.Console.WriteLine("\nМеню напитков");
            for(int drinkNumber = 1; drinkNumber <= drinksMenu.Count(); drinkNumber++)
            {
                Console.WriteLine(drinkNumber + ") " + drinksMenu[drinkNumber - 1].getNameAndPrice());
            }
        }


    } 
}
