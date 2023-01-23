namespace CourierClass
{
    public class Courier
    {
        private string name;
        private int completedOrdersCount;

        public Courier (string name)
        {
            this.name = name;
        }

        public string getName()
        {
            return name;
        }
    }
}
