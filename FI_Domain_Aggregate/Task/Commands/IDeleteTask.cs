namespace FI_Domain_Aggregate;

public interface IDeleteTask : IRepositoryBase<FI_Domain.Task>, IDisposable
{
    void  Delete(FI_Domain.Task task);
}

