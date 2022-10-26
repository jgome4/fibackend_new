namespace FI_Domain_Aggregate;

    public interface IRepositoryBase<Entity> : IDisposable
    {

        IWorkUnit WorkUnit { get; }    


    }

