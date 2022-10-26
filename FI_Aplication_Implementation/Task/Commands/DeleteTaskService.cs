
using FI_Aplication_Aggregate;
using FI_Aplication_Core;

namespace FI_Aplication_Implementation;

public class DeleteTaskService : IDeleteTaskService
{
    private FI_Domain_Aggregate.IDeleteTask _deleteTask;
 

    public DeleteTaskService(FI_Domain_Aggregate.IDeleteTask deleteTask)
    {
        _deleteTask =deleteTask;     

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
            TaskState = taskDTO.TaskState
        };
        


        _deleteTask.Delete(task);
        task.CommitTo(FI_Domain.ActionsDomainEvents.DELETE);
        return _deleteTask.WorkUnit.Save();
    }


    




    public void Dispose()
    {
        _deleteTask.Dispose();
    }


}