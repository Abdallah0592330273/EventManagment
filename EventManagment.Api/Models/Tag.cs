namespace EventManagment.Api.Models;

public sealed class Tag
{
    private Tag()
    {
        // Required by EF Core when materializing a Tag from the database.
    }

    public Tag(string name, string description)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
    }


    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }

}
