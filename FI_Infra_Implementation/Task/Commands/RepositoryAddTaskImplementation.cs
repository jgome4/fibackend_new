using FI_Domain_Aggregate;
using FI_Infra_Core;
using FI_Infra_Repository;

namespace FI_Infra_Implementation;

    public class TaskRepository : RepositoryBase<FI_Domain.Task>, IAddTask
    {
        public TaskRepository(IContextWorkUnit contextWorkUnit) : base(contextWorkUnit) { }

    }

