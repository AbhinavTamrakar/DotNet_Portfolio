namespace Portfolio.Domain.Entities;

public class ProjectTechnology
{
    public long ProjectId { get; set; }

    public Project Project { get; set; } = null!;

    public long TechnologyId { get; set; }

    public Technology Technology { get; set; } = null!;
}
