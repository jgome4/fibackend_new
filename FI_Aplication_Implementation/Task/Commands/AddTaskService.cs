
using FI_Aplication_Aggregate;
using FI_Aplication_Core;

namespace FI_Aplication_Implementation;

public class AddTaskService : IAddTaskService
{
    private FI_Domain_Aggregate.IAddTask _addTask;
 

    public AddTaskService(FI_Domain_Aggregate.IAddTask addTask)
    {
        _addTask = addTask;     

    }



    private void ValidateTask(TaskDTO taskDTO)
    {
        Validate.IsDataCorrect<TaskDTO>(taskDTO);
       

    }
    public int  Invoque(TaskDTO taskDTO)
    {
        taskDTO.TaskId = new Guid();
        ValidateTask(taskDTO);
        FI_Domain.Task task = new FI_Domain.Task
        {
            TaskId = taskDTO.TaskId != System.Guid.Empty ? taskDTO.TaskId : new Guid() ,
            TaskName = taskDTO.TaskName,
            TaskState = taskDTO.TaskState
        };
        


        _addTask.Add(task);
        task.CommitTo(FI_Domain.ActionsDomainEvents.ADD);
        return _addTask.WorkUnit.Save();
    }


    




    public void Dispose()
    {
        _addTask.Dispose();
    }


}