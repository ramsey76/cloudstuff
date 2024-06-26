﻿using Bogus;
using Bogus.Extensions.UnitedStates;
using SQLWriter.Models;

namespace SQLWriter;

public class Faker
{
    public static Faker<User> UserFaker(Bank bank)
    {
        var userIds = 0;
        return new Faker<User>("nl")
        .RuleFor(u => u.Id, f => userIds++)
        .RuleFor(u => u.FirstName, f => f.Person.FirstName)
        .RuleFor(u => u.LastName, f => f.Person.LastName)
        .RuleFor(u => u.Bsn, f => f.Person.Ssn().ToString())
        .RuleFor(u => u.Email, (f,u) => f.Internet.Email(u.FirstName, u.LastName))
        .RuleFor(u => u.StreetAddress, f => f.Address.StreetAddress())
        .RuleFor(u => u.City, f => f.Address.City())
        .RuleFor(u => u.Telephone, f => f.Phone.PhoneNumber())
        .RuleFor(u => u.Bank, (f,u) => u.Bank = bank)
        .RuleFor(u => u.BankId, (f,u) => u.BankId = bank.Id);
    }

    public static Faker<BankAccount> BankAccountFaker(Bank bank)
    {
        var banAccountIds = 0;
        return new Faker<BankAccount>("nl")
        .RuleFor(ba => ba.Id, f => banAccountIds++)
        .RuleFor(ba => ba.Type, f => f.PickRandom<BankAccountType>())
        .RuleFor(ba => ba.Bank, (f,ba) => ba.Bank = bank)
        .RuleFor(ba => ba.BankId, (f,ba) => ba.BankId = bank.Id)
        .RuleFor(ba => ba.CurrentAmount, f => f.Finance.Amount())
        .RuleFor(ba => ba.BankAccountNumber,f => f.Finance.Iban());
        ;
    }
}
