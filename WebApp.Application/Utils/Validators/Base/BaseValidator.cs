using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Application.Utils.Validators.Base
{
    public abstract class BaseValidator<T>
    {
        public abstract bool IsValid(T value, out string validationErrorMessage);
    }
}
