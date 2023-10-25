namespace Bookify.Domain.Abstractions;
public abstract class Entity<TEntityId> : IEntity
{
    private readonly List<IDomainEvent> _domainEvents = new();
    protected Entity(TEntityId id)
    {
        Id = id;
    }

    protected Entity() { }

    public TEntityId Id { get; init; }

    public override bool Equals(object obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj is Entity<TEntityId> otherEntity)
        {
            return otherEntity.Id.Equals(Id);
        }

        return false;
    }

    public IReadOnlyList<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents.ToList();
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
