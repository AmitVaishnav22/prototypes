class paymentStrategy:
    def pay(self, amount):
        pass

class CreditCardPayment(paymentStrategy):
    def pay(self, amount):
        print("Paying with credit card: ", amount)

class DebitCardPayment(paymentStrategy):
    def pay(self, amount):
        print("Paying with debit card: ", amount)

class PayPalPayment(paymentStrategy):
    def pay(self, amount):
        print("Paying with PayPal: ", amount)

class Payment:
    def __init__(self,strategy):
        self.strategy = strategy
    def pay(self, amount):
        self.strategy.pay(amount)

# Usage
payment = Payment(CreditCardPayment())
payment.pay(100)
payment = Payment(DebitCardPayment())
payment.pay(50)
payment = Payment(PayPalPayment())
payment.pay(75)



# 1. The Payment class is now focused on a single responsibility (handling payment), which adheres to the Single Responsibility Principle.
# 2. Adding new payment types can be done by creating new classes that implement the paymentStrategy interface, without modifying existing code, which adheres to the Open/Closed Principle.
# 3. The code is easily extensible, as adding new payment types only requires creating new classes that implement the paymentStrategy interface, without changing existing logic.
# 4. The code is easily testable, as the payment logic is decoupled from the payment types, allowing for isolated testing of individual components.
