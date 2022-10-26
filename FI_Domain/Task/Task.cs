using System.ComponentModel.DataAnnotations;

namespace FI_Domain;

    public class Task:Entity
    {
    public void CommitTo(ActionsDomainEvents actionsDomainEvents)
    {
        this.PublishEvent(new TaskCommitted(this, actionsDomainEvents));
    }
    [Key]
        public Guid TaskId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public String? TaskName { get; set; }

        public Boolean TaskState { get; set; }
    }







