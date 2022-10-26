
using FI_Aplication_Aggregate;
using FI_Aplication_Core;

namespace FI_Aplication_Implementation;

public class  UpdateTaskService : IUpdateTaskService
{
    private FI_Domain_Aggregate.IUpdateTask _updateTask;
 

    public  UpdateTaskService(FI_Domain_Aggregate.IUpdateTask  updateTask)
    {
        _updateTask =  updateTask;     

    }



    private void ValidateTask(TaskDTO taskDTO)
    {
        Validate.IsDataCorrect<TaskDTO>(taskDTO);
        Validate.IsGUID(taskDTO.TaskId);

    }
    public int  Invoque(TaskDTO taskDTO)
    {
        ValidateTask(taskDTO);
        FI_Domain.Task task = new FI_Domain.Task
        {
            TaskId = taskDTO.TaskId,
            TaskName = taskDTO.TaskName,
            TaskState = taskDTO.TaskState,
            
        };
        


        _updateTask.Update(task);
        task.CommitTo(FI_Domain.ActionsDomainEvents.UPDATE);
        return _updateTask.WorkUnit.Save();
    }


    




    public void Dispose()
    {
        _updateTask.Dispose();
    }


}