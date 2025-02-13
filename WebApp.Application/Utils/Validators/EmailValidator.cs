using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Application.Utils.Validators.Base;

namespace WebApp.Application.Utils.Validators
{
    public class EmailValidator : BaseValidator<string>
    {
        public override bool IsValid(string value, out string validationErrorMessage)
        {
            throw new NotImplementedException();
        }
    }
}
