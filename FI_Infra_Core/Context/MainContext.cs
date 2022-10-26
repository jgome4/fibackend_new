using FI_Domain;
using Microsoft.EntityFrameworkCore;

namespace FI_Infra_Core;

public class MainContext : DbContext, IContextWorkUnit
{
    private readonly IDomainEventDispatcher _dispatcher;

    public MainContext(DbContextOptions<MainContext> options, IDomainEventDispatcher dispatcher)
        : base(options)
    {
        _dispatcher = dispatcher;
    }

    DbSet<FI_Domain.Task> _Task;


    public DbSet<FI_Domain.Task> Task
    {
        get { return _Task ?? (_Task = base.Set<FI_Domain.Task>()); }
    }

    public int Save()
    {
        _preSaveChanges().GetAwaiter().GetResult();
        return base.SaveChanges();
    }


    private async System.Threading.Tasks.Task _preSaveChanges()
    {
        await _dispatchDomainEvents();
    }

    private async System.Threading.Tasks.Task _dispatchDomainEvents()
    {
        var domainEventEntities = ChangeTracker.Entries<IEntity>()
           .Select(po => po.Entity)
           .Where(po => po.DomainEvents.Any())
           .ToArray();

        foreach (var entity in domainEventEntities)
        {
            IDomainEvent dev;
            while (entity.DomainEvents.TryTake(out dev))
                await _dispatcher.Dispatch(dev);
        }
    }




    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new TaskConfig());

    }
}

