using FI_Domain_Aggregate;
using FI_Infra_Core;
using FI_Infra_Repository;

namespace FI_Infra_Implementation;

    public class GetTasksRepository : RepositoryBase<FI_Domain.Task>, IGetTask
{
        public GetTasksRepository(IContextWorkUnit contextWorkUnit) : base(contextWorkUnit) { }

    }

