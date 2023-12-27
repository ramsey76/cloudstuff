using SQLWriter.Models;
using SQLWriter.Database;

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
        
        bankContext.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
        foreach(var peopleChunk in people.Chunk(10000))
        {
            var  users = new List<User>();
            foreach(var person in peopleChunk)
            {
                users.Add(new User(){
                    UserId = Guid.NewGuid(),
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Address = $"({person.Address1} {person.Address2})",
                    Telephone = person.MobilePhone,
                    Bsn = person.SSN,
                    City = person.City,
                    Email = person.Email
                });
            }

            bankContext.Users.AddRange(users);
            bankContext.SaveChanges();
        }
        var endTime = DateTime.Now;
        
        var totalTime = endTime - startTime;

        Console.WriteLine($"TotalTime: {totalTime}");
    }
}
