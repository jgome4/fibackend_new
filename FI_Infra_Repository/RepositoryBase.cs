using FI_Domain_Aggregate;
using FI_Infra_Core;

namespace FI_Infra_Repository;

public class RepositoryBase<Entity> : IRepositoryBase<Entity> where Entity : class
{
    private IContextWorkUnit _contextWorkUnit;

    public IWorkUnit WorkUnit
    {
        get { return _contextWorkUnit; }
    }
    public RepositoryBase(IContextWorkUnit contextWorkUnit)
    {
        _contextWorkUnit = contextWorkUnit;
    }


    public void Add(Entity entity)
    {

        _contextWorkUnit.Set<Entity>().Add(entity);

    }

    public void Update(Entity entity)
    {

        _contextWorkUnit.Set<Entity>().Update(entity);

    }

    public void Delete(Entity entity)
    {

        _contextWorkUnit.Set<Entity>().Remove(entity);

    }

    public IEnumerable<Entity> Get()
    {

        return _contextWorkUnit.Set<Entity>().AsEnumerable();

    }

    public void Dispose()
    {
        _contextWorkUnit.Dispose();
    }


}

