using SQLWriter.Models;
using SQLWriter.Database;
using Microsoft.VisualBasic;
using System.Security.Cryptography;

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
        bankContext.Banks.Add(bank);
        
        var fakerBankAccounts = Faker.BankAccountFaker(bank);
        var fakeUser = Faker.UserFaker();
        var fakeUsers = fakeUser.Generate(1000000);
        var fakeBankAccounts = fakerBankAccounts.Generate(750000);
        
        var usersChunks = fakeUsers.Chunk(10000).ToList();
        var bankAccountChunks  = fakeBankAccounts.Chunk(7500).ToList();
        
        Console.WriteLine("Generated Fake Data");
        
        for(int i =0;i< 100; i++)
        {
            bankContext.Users.AddRange(usersChunks[i]);
            bankContext.BankAccounts.AddRange(bankAccountChunks[i]);

            var bankAccountUsers = new List<BankAccountUser>();
            
            var count = 0;
            while(count < 5000)
            {
                bankAccountUsers.Add(new(){
                        User = usersChunks[i][count],
                        BankAccount = bankAccountChunks[i][count]
                    });
                count++;
            }
            
            while(count < 7500)
            {
                bankAccountUsers.Add(new(){
                        User = usersChunks[i][count],
                        BankAccount = bankAccountChunks[i][count]
                    });
                bankAccountUsers.Add(new(){
                        User = usersChunks[i][count+2500],
                        BankAccount = bankAccountChunks[i][count]
                    });                    
                count++;
            }
            bankContext.BankAccountUsers.AddRange(bankAccountUsers);
            bankContext.SaveChanges();

            Console.Write($"Written Part {i.ToString()}");
        }
        
        Console.WriteLine("Bank");
        
        

        var endTime = DateTime.Now;
        var totalTime = endTime - startTime;

        Console.WriteLine($"TotalTime: {totalTime}");
    }
}
