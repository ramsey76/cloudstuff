namespace Function2;

public class Calculate
{
    public BankAccount DollarAmountCurrentAccount(BankAccount bankAccount)
    {
        
        bankAccount.DollarAmount = bankAccount.CurrentAmount * 1.08m;
        return bankAccount;
    }   
}
