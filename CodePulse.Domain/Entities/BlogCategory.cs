namespace CodePulse.Domain.Entities;

public class BlogCategory : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string UrlHandle { get; set; } = string.Empty;
}
