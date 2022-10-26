
using FI_Aplication_Aggregate;
using FI_Aplication_Core;

namespace FI_Aplication_Implementation;

public class GetTasksService : IGetTasksService
{
    private FI_Domain_Aggregate.IGetTask _GetTask;
 

    public GetTasksService(FI_Domain_Aggregate.IGetTask GetTask)
    {
        _GetTask = GetTask;     

    }



    private void ValidateTask(TaskDTO taskDTO)
    {
        Validate.IsDataCorrect<TaskDTO>(taskDTO);
        Validate.IsGUID(taskDTO.TaskId);

    }
    public IEnumerable<TaskDTO>  Invoque()
    {
        return _GetTask.Get().Select(x=> new TaskDTO
        {
            TaskId = x.TaskId,
             TaskName = x.TaskName,
              TaskState = x.TaskState
        });
        
    }


    




    public void Dispose()
    {
        _GetTask.Dispose();
    }


}