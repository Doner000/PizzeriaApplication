using PizzaClass;
using CustomerClass;

namespace OrderClass
{
    public class Order
    {
        private Pizza selectedPizza;
        private Customer customer;

        private string pizzaName;
        private string pizzaPrice;

        private string currentCustomerName;
        private string currentCustomerAdress;
        private string currentCustomerTelegramID;
        private List<Order> cart = new List<Order>();
        public Order (Pizza selectedPizza, Customer customer)
        {
            this.selectedPizza = selectedPizza;
            this.customer = customer;
            this.pizzaName = selectedPizza.getName();
            this.currentCustomerName = customer.getCustomerName();
            this.currentCustomerTelegramID = customer.getcustomerTelegramID();
            this.currentCustomerAdress = customer.getCustomerAdress();
        }
        public Order ()
        {

        }

        public void addToCart(Order order)
        {
            this.cart.Add(order);
        }

        public string getSelectedPizzaName()
        {
            return pizzaName;
        }

        public string getSelectedPizzaPrice()
        {
            return pizzaName;
        }

        public string getCurrentCustomerName()
        {
            return currentCustomerName;
        }

        public string getCurrentCustomerAdress()
        {
            return currentCustomerAdress;
        }

        public string getCurrentCustomerTelegramID()
        {
            return currentCustomerTelegramID;
        }

        public void setInformation(Pizza selectedPizza, Customer customer)
        {
            this.selectedPizza = selectedPizza;
            this.customer = customer;
            this.pizzaName = selectedPizza.getName();
            this.currentCustomerName = customer.getCustomerName();
            this.currentCustomerTelegramID = customer.getcustomerTelegramID();
            this.currentCustomerAdress = customer.getCustomerAdress();
        }

        


        
    }
}