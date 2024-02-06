using Ispitpredaja.Common;
using Ispitpredaja.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ispitpredaja.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        public RegisterViewModel()
        {
            Email.OnValueChange = Validate;
            Password.OnValueChange = Validate;
            Username.OnValueChange = Validate;
            FirstName.OnValueChange = Validate;
            LastName.OnValueChange = Validate;

            IsRegisterButtonEnabled = false;

            Email.Value = "";
            Password.Value = "";
            Username.Value = "";
            FirstName.Value = "";
            LastName.Value = "";
            Address.Value = "";

            Email.Error = "";
            Password.Error = "";
            Username.Error = "";
            FirstName.Error = "";
            LastName.Error = "";
            Address.Error = "";
            AddressNumber.Error = "";
        }
        public MProp<string> Username { get; set; } = new MProp<string>();
        public MProp<string> Email { get; set; } = new MProp<string>();
        public MProp<string> FirstName { get; set; } = new MProp<string>();
        public MProp<string> LastName { get; set; } = new MProp<string>();
        public MProp<string> Password { get; set; } = new MProp<string>();
        public MProp<string> Address { get; set; } = new MProp<string>();
        public MProp<int> AddressNumber { get; set; } = new MProp<int>();

        public bool IsRegisterButtonEnabled { get; set; }

        private void Validate()
        {
            var validator = new RegisterViewModelValidator();

            var result = validator.Validate(this);

            if (!result.IsValid)
            {
                IsRegisterButtonEnabled = false;

                var passwordError = result.Errors.FirstOrDefault(x => x.PropertyName.Contains("Password"));
                var emailError = result.Errors.FirstOrDefault(x => x.PropertyName.Contains("Email"));
                var usernameError = result.Errors.FirstOrDefault(x => x.PropertyName.Contains("Username"));
                var firstNameError = result.Errors.FirstOrDefault(x => x.PropertyName.Contains("FirstName"));
                var lastNameError = result.Errors.FirstOrDefault(x => x.PropertyName.Contains("LastName"));
                var addressError = result.Errors.FirstOrDefault(x => x.PropertyName.Contains("Address"));
                var addressNumberError = result.Errors.FirstOrDefault(x => x.PropertyName.Contains("AddressNumber"));


                ErrorPrint(passwordError, Password);
                ErrorPrint(emailError, Email);
                ErrorPrint(usernameError, Username);
                ErrorPrint(firstNameError, FirstName);
                ErrorPrint(lastNameError, LastName);
                ErrorPrint(addressError, Address);
                ErrorPrint(addressNumberError, AddressNumber);
            }
            else
            {
                IsRegisterButtonEnabled = true;
                Email.Error = "";
                Password.Error = "";
                FirstName.Error = "";
                LastName.Error = "";
                Username.Error = "";
            }

            OnPropertyChanged(nameof(IsRegisterButtonEnabled));
        }

        public void ErrorPrint(FluentValidation.Results.ValidationFailure checkError, MProp<string> printError)
        {
            if (checkError != null)
            {
                printError.Error = checkError.ErrorMessage;
            }
            else
            {
                printError.Error = "";
            }
        }

        public void ErrorPrint(FluentValidation.Results.ValidationFailure checkError, MProp<int> printError)
        {
            if (checkError != null)
            {
                printError.Error = checkError.ErrorMessage;
            }
            else
            {
                printError.Error = "";
            }
        }
    }
}
