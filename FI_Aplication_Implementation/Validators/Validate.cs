using System.ComponentModel.DataAnnotations;

namespace FI_Aplication_Implementation;
public static class Validate
{
    public static void IsDataCorrect<T>(this T entity) where T : class, new()
    {
        var validatorContext = new ValidationContext(entity);
        var errors = new List<ValidationResult>();
        var responses = new List<string>();
        Validator.TryValidateObject(entity, validatorContext, errors, true);
        errors.ForEach(e => responses.Add(e.ErrorMessage));
        if (responses.Count > 0) throw new ArgumentException(String.Concat(responses));
    }
 

    public static void IsGUID(Guid value)
    {
        if ((Guid)value == Guid.Empty) throw new ArgumentException("This  value is incorrect", value.ToString());
    }
}




