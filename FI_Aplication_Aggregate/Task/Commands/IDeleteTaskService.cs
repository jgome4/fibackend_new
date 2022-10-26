using FI_Aplication_Core;

namespace FI_Aplication_Aggregate;

public interface IDeleteTaskService :  IDisposable
{
    int Invoque(TaskDTO task);
}

