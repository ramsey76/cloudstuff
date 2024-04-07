using Target.Database.Models;

namespace Target.Database;

public class Delivery
{
    public Guid Id {get;set;}
    public DateOnly DeliveryDate {get;set;} 
    public Guid InstitutionId {get;set;}
    public Institution? Institution {get;set;} = null;
    public ICollection<Account> Accounts {get;set;} = [];
    public ICollection<Depositor> Depositors {get;set;} = [];
}
