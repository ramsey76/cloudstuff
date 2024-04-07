namespace Target.Database.Models;

public class DepositorAccount
{
    public Guid AccountId { get; set; }
    public Account Account { get; set; }
    public Guid DepositorId { get; set; }
    public Depositor Depositor { get; set; }
    public int PercentageOwnership { get; set; }    
}
