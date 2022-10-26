using FI_Domain_Aggregate;
using Microsoft.EntityFrameworkCore;

namespace FI_Infra_Core;

    public interface IContextWorkUnit : IWorkUnit, IDisposable
        {
            DbSet<Entity> Set<Entity>() where Entity : class;       


        }

    