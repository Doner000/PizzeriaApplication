using PizzaClass;

namespace DrinksClass
{
    public class Drinks : Pizza
    {
        public Drinks (int id, string name, int price, string description)
        :base(id ,name, price, description)
        {
            
        }

        public Drinks(int id, string name, int price)
        :base(id, name, price)
        {

        }

        public Drinks (string name, int price)
        :base(name, price)
        {

        }
        public Drinks ()
        :base()
        {
            
        }
    }
}