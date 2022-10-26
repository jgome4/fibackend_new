
using System.Collections.Concurrent;

namespace FI_Domain;
public interface IEntity
    {
        IProducerConsumerCollection<IDomainEvent> DomainEvents { get; }
    }

