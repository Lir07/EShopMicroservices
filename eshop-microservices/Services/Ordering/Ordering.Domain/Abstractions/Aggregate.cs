﻿namespace Ordering.Domain.Abstractions;
public abstract class Aggregate<TId> : Entity<TId>, IAggregate<TId>
{
	public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
	private readonly List<IDomainEvent> _domainEvents = new();

	public void AddDomainEvent(IDomainEvent domainEvent)
	{
		_domainEvents.Add(domainEvent);
	}
	public IDomainEvent[] ClearDomainEvents()
	{
		IDomainEvent[] dequedEvents = _domainEvents.ToArray();
		_domainEvents.Clear();
		return dequedEvents;
	}
}
