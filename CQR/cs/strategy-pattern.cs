interface IPaymentStrategy
{
    void Pay(decimal amount);
}

class CreditCardPayment : IPaymentStrategy
{
    public void Pay(decimal amount)
    {
        Console.WriteLine("Paying with credit card: " + amount);
    }
}

class DebitCardPayment : IPaymentStrategy
{
    public void Pay(decimal amount)
    {
        Console.WriteLine("Paying with debit card: " + amount);
    }
}

class PayPalPayment : IPaymentStrategy
{
    public void Pay(decimal amount)
    {
        Console.WriteLine("Paying with PayPal: " + amount);
    }
}

class Payment
{
    private IPaymentStrategy _paymentStrategy;
    public Payment(IPaymentStrategy paymentStrategy)
    {
        _paymentStrategy = paymentStrategy;
    }
    public void Pay(decimal amount)
    {
        _paymentStrategy.Pay(amount);
    }
}

class Program
{
    static void Main()
    {
        Payment creditPayment = new(new CreditCardPayment());
        creditPayment.Pay(100.0m);

        Payment debitPayment = new(new DebitCardPayment());
        debitPayment.Pay(50.0m);

        Payment paypalPayment = new(new PayPalPayment());
        paypalPayment.Pay(25.0m);
    }
}