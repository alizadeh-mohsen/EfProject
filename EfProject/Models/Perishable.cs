using EfProject.Models;

public class Perishable : Product
{
    public int? ExpirationDays { get; set; }
    public bool? Refrigerated { get; set; }
}