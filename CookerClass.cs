
namespace CookerClass
{
    public class Cooker 
    {
        private string name;
        private int cookingSpeed;

        //private int workExperience; В будущем можно сделать так, будто бы опыт зависит от количества сделанных пицц, и чем больше пицц сделал тем выше скорость готовки. Например через каждые 10 пицц скорость увеличивается на 1 секунду

        public Cooker (string name, int cookingSpeed)
        {
            this.name = name;
            this.cookingSpeed = cookingSpeed;
        }

        public string getCookerName()
        {
            return name;
        }

        public int getCookingSpeed()
        {
            return cookingSpeed;
        }
    }
}   
