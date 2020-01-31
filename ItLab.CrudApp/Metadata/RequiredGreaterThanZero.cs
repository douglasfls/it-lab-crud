using System.ComponentModel.DataAnnotations;

namespace ItLab.CrudApp.Metadata
{
    public class RequiredGreaterThanZero : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value != null && int.TryParse(value.ToString(), out int i) && i > 0;
        }
    }

}
