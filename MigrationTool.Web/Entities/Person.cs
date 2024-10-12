namespace MigrationTool.Web.Entities;

public class Person
{
    public Guid Uuid { get; set; }
    public string? GivenName { get; set; } = string.Empty;
    public string? FamilyName { get; set; } = string.Empty;
}