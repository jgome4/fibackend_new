namespace FI_Domain_Aggregate;

public interface IAddTask : IRepositoryBase<FI_Domain.Task>, IDisposable
{
    void  Add(FI_Domain.Task task);
}

