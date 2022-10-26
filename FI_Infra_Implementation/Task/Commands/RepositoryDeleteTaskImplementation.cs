using FI_Domain_Aggregate;
using FI_Infra_Core;
using FI_Infra_Repository;

namespace FI_Infra_Implementation;

    public class DeleteTaskRepository : RepositoryBase<FI_Domain.Task>, IDeleteTask
{
        public DeleteTaskRepository(IContextWorkUnit contextWorkUnit) : base(contextWorkUnit) { }

    }

