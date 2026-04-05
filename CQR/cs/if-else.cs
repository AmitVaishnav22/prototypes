using System;

class Payment
{
    public void pay(string type,double amount)
    {
        if (type == "credit")
        {
            Console.WriteLine("Paying with credit card: " + amount);
        }
        else if (type == "debit")
        {
            Console.WriteLine("Paying with debit card: " + amount);
        }
        else if (type == "paypal")
        {
            Console.WriteLine("Paying with PayPal: " + amount);
        }
        else
        {
            Console.WriteLine("Invalid payment type");
        }
    }
}

class Program
{
    static void Main()
    {
        Payment payment= new();
        payment.pay("credit", 100.0);
        payment.pay("debit", 50.0);
        payment.pay("paypal", 25.0);
        payment.pay("cash", 10.0);
    }
}