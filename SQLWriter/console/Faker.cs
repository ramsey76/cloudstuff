using Bogus;
using SQLWriter.Models;

namespace SQLWriter;

public class Faker
{
    public static Faker<User> BankAccountUserFaker()
    {
        return new Faker<User>("nl")
        .RuleFor(u => u.FirstName, f => f.Person.FirstName)
        .RuleFor(u => u.LastName, f => f.Person.LastName)
        .RuleFor(u => u.Email, (f,u) => f.Internet.Email(u.FirstName, u.LastName))
        ;
    }
}
