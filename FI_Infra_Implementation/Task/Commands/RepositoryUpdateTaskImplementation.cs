using FI_Domain_Aggregate;
using FI_Infra_Core;
using FI_Infra_Repository;

namespace FI_Infra_Implementation;

    public class UpdateTaskRepository : RepositoryBase<FI_Domain.Task>, IUpdateTask
{
        public UpdateTaskRepository(IContextWorkUnit contextWorkUnit) : base(contextWorkUnit) { }

    }

