using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM1
{
    public interface IBeverage
    {
        string GetDescription();
        double Cost();
    }

    // Класс Espresso
    public class Espresso : IBeverage
    {
        public string GetDescription()
        {
            return "Espresso";
        }

        public double Cost()
        {
            return 900;
        }
    }

    // Класс Tea
    public class Tea : IBeverage
    {
        public string GetDescription()
        {
            return "Tea";
        }

        public double Cost()
        {
            return 675;
        }
    }

    public abstract class BeverageDecorator : IBeverage
    {
        protected IBeverage _beverage;

        public BeverageDecorator(IBeverage beverage)
        {
            _beverage = beverage;
        }

        public virtual string GetDescription()
        {
            return _beverage.GetDescription();
        }

        public virtual double Cost()
        {
            return _beverage.Cost();
        }
    }

    public class Milk : BeverageDecorator
    {
        public Milk(IBeverage beverage) : base(beverage) { }

        public override string GetDescription()
        {
            return _beverage.GetDescription() + ", Milk";
        }

        public override double Cost()
        {
            return _beverage.Cost() + 225;
        }
    }

    public class Sugar : BeverageDecorator
    {
        public Sugar(IBeverage beverage) : base(beverage) { }

        public override string GetDescription()
        {
            return _beverage.GetDescription() + ", Sugar";
        }

        public override double Cost()
        {
            return _beverage.Cost() + 90;
        }
    }

    public class WhippedCream : BeverageDecorator
    {
        public WhippedCream(IBeverage beverage) : base(beverage) { }

        public override string GetDescription()
        {
            return _beverage.GetDescription() + ", Whipped Cream";
        }

        public override double Cost()
        {
            return _beverage.Cost() + 315;
        }
    }

    public class Latte : IBeverage
    {
        public string GetDescription()
        {
            return "Latte";
        }

        public double Cost()
        {
            return 1350;
        }
    }

    public class Mocha : IBeverage
    {
        public string GetDescription()
        {
            return "Mocha";
        }

        public double Cost()
        {
            return 1575;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IBeverage beverage1 = new Espresso();
            beverage1 = new Milk(beverage1);
            beverage1 = new Sugar(beverage1);
            Console.WriteLine($"{beverage1.GetDescription()} : {beverage1.Cost()} KZT");

            IBeverage beverage2 = new Latte();
            beverage2 = new WhippedCream(beverage2);
            beverage2 = new Sugar(beverage2);
            Console.WriteLine($"{beverage2.GetDescription()} : {beverage2.Cost()} KZT");

            IBeverage beverage3 = new Mocha();
            beverage3 = new Milk(beverage3);
            beverage3 = new WhippedCream(beverage3);
            Console.WriteLine($"{beverage3.GetDescription()} : {beverage3.Cost()} KZT");
        }
    }
}
