namespace FI_Domain_Aggregate;

public interface IGetTask : IRepositoryBase<FI_Domain.Task>, IDisposable
{
    IEnumerable<FI_Domain.Task> Get();
}

