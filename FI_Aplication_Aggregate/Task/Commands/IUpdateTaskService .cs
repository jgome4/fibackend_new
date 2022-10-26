using FI_Aplication_Core;

namespace FI_Aplication_Aggregate;

public interface IUpdateTaskService : IDisposable
{
    int Invoque(TaskDTO task);
}

