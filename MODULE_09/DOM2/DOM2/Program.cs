using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM2
{
    public interface IPaymentProcessor
    {
        void ProcessPayment(double amount);
    }

    public class PayPalPaymentProcessor : IPaymentProcessor
    {
        public void ProcessPayment(double amount)
        {
            Console.WriteLine($"Processing payment of {amount} using PayPal.");
        }
    }

    public class StripePaymentService
    {
        public void MakeTransaction(double totalAmount)
        {
            Console.WriteLine($"Making a transaction of {totalAmount} using Stripe.");
        }
    }

    public class StripePaymentAdapter : IPaymentProcessor
    {
        private StripePaymentService _stripeService;

        public StripePaymentAdapter(StripePaymentService stripeService)
        {
            _stripeService = stripeService;
        }

        public void ProcessPayment(double amount)
        {
            _stripeService.MakeTransaction(amount);
        }
    }

    public class SquarePaymentService
    {
        public void SubmitPayment(double totalAmount)
        {
            Console.WriteLine($"Submitting payment of {totalAmount} using Square.");
        }
    }

    public class SquarePaymentAdapter : IPaymentProcessor
    {
        private SquarePaymentService _squareService;

        public SquarePaymentAdapter(SquarePaymentService squareService)
        {
            _squareService = squareService;
        }

        public void ProcessPayment(double amount)
        {
            _squareService.SubmitPayment(amount);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IPaymentProcessor payPalProcessor = new PayPalPaymentProcessor();
            payPalProcessor.ProcessPayment(150.0);

            StripePaymentService stripeService = new StripePaymentService();
            IPaymentProcessor stripeAdapter = new StripePaymentAdapter(stripeService);
            stripeAdapter.ProcessPayment(200.0);

            SquarePaymentService squareService = new SquarePaymentService();
            IPaymentProcessor squareAdapter = new SquarePaymentAdapter(squareService);
            squareAdapter.ProcessPayment(250.0);
        }
    }
}
