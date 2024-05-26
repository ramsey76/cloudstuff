using SQLWriter.Models;
using SQLWriter.Database;
using Microsoft.VisualBasic;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace SQLWriter;

public class SQLPublisher
{
    
    public BankContext bankContext;
        
    public SQLPublisher(BankContext bankContext)
    {
        this.bankContext = bankContext;
    }

    public void WriteEntities()
    {
        var startTime = DateTime.Now;

        Console.WriteLine($"StartTime {startTime.ToString()}");
        bankContext.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;        
        
        var bank = new Bank(){
            Name = "OldBank",
            Id = RandomNumberGenerator.GetInt32(1, 1000000)
        };
//        bankContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Banks ON");
        bankContext.SaveChanges();
        bankContext.Banks.Add(bank);
        bankContext.SaveChanges();
        
        var fakerBankAccounts = Faker.BankAccountFaker(bank);
        var fakeUser = Faker.UserFaker(bank);
        var fakeUsers = fakeUser.Generate(1000000);
        var fakeBankAccounts = fakerBankAccounts.Generate(750000);
        
        var usersChunks = fakeUsers.Chunk(10000).ToList();
        var bankAccountChunks  = fakeBankAccounts.Chunk(7500).ToList();
        
        Console.WriteLine("Generated Fake Data");
        
        for(int i =0;i< 100; i++)
        {
            bankContext.Users.AddRange(usersChunks[i]);
            bankContext.BankAccounts.AddRange(bankAccountChunks[i]);
            bankContext.SaveChanges();
            
            var bankAccountUsers = new List<BankAccountUser>();

            var count = 0;
            while(count < 5000)
            {
                bankAccountUsers.Add(new(){
                        UserId = usersChunks[i][count].Id,
                        BankAccountId = bankAccountChunks[i][count].Id
                    });
                count++;
            }
            
            while(count < 7500)
            {
                bankAccountUsers.Add(new(){
                        UserId = usersChunks[i][count].Id,
                        BankAccountId = bankAccountChunks[i][count].Id
                    });
                bankAccountUsers.Add(new(){
                        UserId = usersChunks[i][count+2500].Id,
                        BankAccountId = bankAccountChunks[i][count].Id
                    });                    
                count++;
            }
            bankContext.BankAccountUsers.AddRange(bankAccountUsers);
            bankContext.SaveChanges();

            Console.Write($"Written Part {i.ToString()}");
        }
        
        Console.WriteLine("Bank");
        
        bankContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Banks OFF");
        bankContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.BankAccounts OFF");
        bankContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Users OFF");

        var endTime = DateTime.Now;
        var totalTime = endTime - startTime;

        Console.WriteLine($"TotalTime: {totalTime}");
    }
}
