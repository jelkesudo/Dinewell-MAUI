using Ispitpredaja.Common;
using Ispitpredaja.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ispitpredaja.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            Email.OnValueChange = Validate;
            Password.OnValueChange = Validate;

            Email.Value = "jana@gmail.com";
            Password.Value = "Probaacc123#";

            Email.Error = "";
            Password.Error = "";
            IsLoginButtonEnabled = false;
        }
        public MProp<string> Email { get; set; } = new MProp<string>();
        public MProp<string> Password { get; set; } = new MProp<string>();

        public bool IsLoginButtonEnabled { get; set; }

        private void Validate() //Action
        {
            var validator = new LoginViewModelValidator();

            var result = validator.Validate(this);

            if (!result.IsValid)
            {
                IsLoginButtonEnabled = false;
                var passwordError = result.Errors.FirstOrDefault(x => x.PropertyName.Contains("Password"));

                if (passwordError != null)
                {
                    Password.Error = passwordError.ErrorMessage;
                }
                else
                {
                    Password.Error = "";
                }

                var emailError = result.Errors.FirstOrDefault(x => x.PropertyName.Contains("Email"));

                if (emailError != null)
                {
                    Email.Error = emailError.ErrorMessage;
                }
                else
                {
                    Email.Error = "";
                }
            }
            else
            {
                IsLoginButtonEnabled = true;
                Email.Error = "";
                Password.Error = "";
            }

            OnPropertyChanged(nameof(IsLoginButtonEnabled));
        }

    }
}
