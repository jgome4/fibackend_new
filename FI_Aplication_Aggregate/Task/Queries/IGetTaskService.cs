using FI_Aplication_Core;

namespace FI_Aplication_Aggregate;

public interface IGetTasksService : IDisposable
{
    IEnumerable<TaskDTO> Invoque();
}