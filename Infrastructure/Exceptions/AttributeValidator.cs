using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Exceptions
{
    public class AttributeValidator<T>
    {
        public AttributeValidator(T argo)
        {
            var validationContext = new ValidationContext(argo, null, null);
            var results = new List<ValidationResult>();
            var p = Validator.TryValidateObject(argo, validationContext, results, true);
            if (!p) throw new AppException(results[0].ErrorMessage ?? string.Empty);
        }
    }
}
