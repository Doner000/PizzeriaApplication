using PizzaClass;
using CustomerClass;
using DrinksClass;
using DataBaseClass;

namespace OrderClass
{
    public class Order
    {
        private int orderId;
        private List<Pizza> selectedPizzas;
        private List<Drinks> selectedDrinks;
        private Customer customer;

        private string pizzaName;
        private string pizzaPrice;

        private string currentCustomerName;
        private string currentCustomerAdress;
        private string currentCustomerTelegramID;
        //в будущем можно чек выписывать

        private int cost;
        private List<Order> cart = new List<Order>();
        public Order (List<Pizza> selectedPizzas, List<Drinks> selectedDrinks, Customer customer)
        {
            this.selectedPizzas = selectedPizzas;
            this.selectedDrinks = selectedDrinks;
            
            this.customer = customer;
            
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

        public void setId (int orderId)
        {
            this.orderId = orderId;
        }

        public int getId ()
        {
            return orderId;
        }

        public void setInformation(List<Pizza> selectedPizzas, List<Drinks> selectedDrinks, Customer customer, DB dataBase, Order order)
        {
            this.selectedPizzas = selectedPizzas;
            this.selectedDrinks = selectedDrinks;
            this.customer = customer;

            dataBase.insertOrder(selectedPizzas, selectedDrinks, customer, order);
            
            
            
            
        }

        //Можно будет удалять из корзины продукты
        public void show ()
        {
            System.Console.WriteLine("Ваш заказ:");
            System.Console.WriteLine("Пицца:\n");
            for (int number = 1; number <= selectedPizzas.Count(); number++)
            {
                System.Console.WriteLine($"{number}) {selectedPizzas[number -1].getName()} - {selectedPizzas[number -1].GetPrice()} тенге");
                cost = cost + selectedPizzas[number -1].GetPrice();
            }
            // foreach (var pizza in selectedPizzas)
            // {
            //     System.Console.WriteLine(pizza.getName());
            // }

            System.Console.WriteLine("\nНапитки:\n");
            for (int number = 1; number <= selectedDrinks.Count(); number++)
            {
                System.Console.WriteLine($"{number}) {selectedDrinks[number -1].getName()} - {selectedDrinks[number -1].GetPrice()} тенге");
                cost = cost + selectedDrinks[number -1].GetPrice();
            }

            System.Console.WriteLine($"Стоимость заказа: {cost} тенге");
            Console.ReadLine();
            
            // foreach (var drinks in selectedDrinks)
            // {
            //     System.Console.WriteLine(drinks.getName());
            // }
        }

        


        
    }
}