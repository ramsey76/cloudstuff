using System.ComponentModel.DataAnnotations;

namespace SQLWriter.Models;

public enum BankAccountType {
        Current,
        Savings,
        Mortgage
}

public class BankAccount
{
        public Guid BankAccountId {get;set;}
        public required string AccountNumber {get;set;}
        public required Bank Bank {get;set;}
        public Guid BankId {get;set;}
        public Decimal CurrentAmount {get;set;}    
        public BankAccountType Type {get;set;}

        public List<BankAccountUsers> BankAccountUsers {get;set;}
}
