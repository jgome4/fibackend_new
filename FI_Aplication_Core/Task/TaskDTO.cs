using System.ComponentModel.DataAnnotations;

namespace FI_Aplication_Core;

    public record TaskDTO   
    {

    [Key]
    public Guid TaskId { get; set; }

    [Required(AllowEmptyStrings = false)]
    [DisplayFormat(ConvertEmptyStringToNull = false)]
    public String? TaskName { get; set; }

    public Boolean TaskState { get; set; }
}

