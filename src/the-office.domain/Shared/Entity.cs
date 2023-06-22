namespace the_office.domain.Shared;

public class Entity
{
    protected Entity()
    {
        Code = Guid.NewGuid();
    }
    public int Id { get; set; }

    public Guid Code { get; set; }
}