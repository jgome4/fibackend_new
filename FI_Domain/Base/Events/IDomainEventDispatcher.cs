

namespace FI_Domain;
public interface IDomainEventDispatcher
    {
    System.Threading.Tasks.Task Dispatch(IDomainEvent devent);
    }

