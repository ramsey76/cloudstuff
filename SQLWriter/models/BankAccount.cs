﻿using System.ComponentModel.DataAnnotations;

namespace SQLWriter.Models;

public enum BankAccountType {
        Current,
        Savings,
        Mortgage
}

public class BankAccount
{
        public Guid Id {get;set;}
        public string AccountNumber {get;set;}
        public Bank Bank {get;set;}
        public Guid BankId {get;set;}
        public Decimal CurrentAmount {get;set;}    
        public Decimal DollarAmount {get;set;}
        public BankAccountType Type {get;set;}

        public List<BankAccountUser> BankAccountUsers {get;set;}
}
