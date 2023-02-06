namespace CourierClass
{
    public class Courier
    {
        private int id;
        private string name;
        private int completedOrdersCount;

        public Courier (int id, string name, int completedOrdersCount)
        {
            this.id = id;
            this.name = name;
            this.completedOrdersCount = completedOrdersCount;
        }

        public Courier ()
        {

        }

        public string getName()
        {
            return name;
        }

        public int GetID()
        {
            return id;
        }
    }
}
