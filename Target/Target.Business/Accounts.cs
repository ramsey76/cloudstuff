using Microsoft.EntityFrameworkCore;
using Target.Database;

namespace Target.Business;

public class Accounts
{
    private readonly DatabaseContext _context;
    public Accounts(DatabaseContext context)
    {
        _context = context;
    }

    public void CountAllAccounts()
    {
        var count = _context.Institutions.Include(i => i.Accounts).SelectMany(i => i.Accounts).Count();
        Console.WriteLine($"There are {count} accounts in the database.");
    }
}
