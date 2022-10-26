namespace FI_Domain;
public class TaskCommitted : IDomainEvent
    {
 
    public Guid TaskId { get; set; }

   
    public String? TaskName { get; set; }

    public Boolean TaskState { get; set; }

    public ActionsDomainEvents ActionsDomainEvents { get; set; }
    private TaskCommitted() { }
        public TaskCommitted(FI_Domain.Task task, ActionsDomainEvents actionsDomainEvents)
        {
            this.TaskId = task.TaskId;
            this.TaskName = task.TaskName;
            this.TaskState = task.TaskState;
            this.ActionsDomainEvents = actionsDomainEvents;
            
        }
    }

