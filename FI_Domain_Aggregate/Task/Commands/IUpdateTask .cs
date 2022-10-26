namespace FI_Domain_Aggregate;

public interface IUpdateTask : IRepositoryBase<FI_Domain.Task>, IDisposable
{
    void  Update(FI_Domain.Task task);
}

