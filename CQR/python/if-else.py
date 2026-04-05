class Payment:
    def pay(self,type,amount):
        if type == "credit":
            print("Paying with credit card: ", amount)
        elif type == "debit":
            print("Paying with debit card: ", amount)
        elif type == "paypal":
            print("Paying with PayPal: ", amount)
        else:
            print("Invalid payment type")

# Usage
payment = Payment()
payment.pay("credit", 100)
payment.pay("debit", 50)
payment.pay("paypal", 75)
payment.pay("cash", 20)  # Invalid payment type

# problems:
# 1. The Payment class has multiple responsibilities (handling different payment types), which violates the Single Responsibility Principle.
# 2. Adding new payment types requires modifying the existing code, which violates the Open/Closed Principle.
# 3. The code is not easily extensible, as adding new payment types would require changing the existing logic and potentially introducing bugs.
# 4. The code is not easily testable, as the payment logic is tightly coupled with the payment types, making it difficult to isolate and test individual components.'
        