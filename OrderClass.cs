using PizzaClass;
using CustomerClass;

namespace OrderClass
{
    public class Order
    {
        private Pizza selectedPizza;
        private Customer customer;

        private string dishName;
        private string pizzaPrice;

        private string currentCustomerName;
        private string currentCustomerAdress;
        private string currentCustomerTelegramID;
        public Order (Pizza selectedPizza, Customer customer)
        {
            this.selectedPizza = selectedPizza;
            this.customer = customer;
            this.dishName = selectedPizza.getPizzaName();
            this.currentCustomerName = customer.getCustomerName();
            this.currentCustomerTelegramID = customer.getcustomerTelegramID();
            this.currentCustomerAdress = customer.getCustomerAdress();
        }
        public Order ()
        {

        }

        public string getSelectedPizzaName()
        {
            return dishName;
        }

        public string getSelectedPizzaPrice()
        {
            return dishName;
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
            this.dishName = selectedPizza.getPizzaName();
            this.currentCustomerName = customer.getCustomerName();
            this.currentCustomerTelegramID = customer.getcustomerTelegramID();
            this.currentCustomerAdress = customer.getCustomerAdress();

        }

        


        
    }
}