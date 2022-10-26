namespace FI_Aplication_Aggregate;

public interface IAddTaskService : IDisposable
{
    int Invoque(FI_Aplication_Core.TaskDTO task);
}
