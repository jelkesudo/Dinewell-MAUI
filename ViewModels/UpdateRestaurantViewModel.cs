using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Ispitpredaja.Bussiness;
using Ispitpredaja.Common;
using Ispitpredaja.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ispitpredaja.ViewModels
{
    public class UpdateRestaurantViewModel : BaseViewModel
    {
        private OneRestaurantsService _service;
        public UpdateRestaurantViewModel()
        {
        }
        public UpdateRestaurantViewModel(int restaurantId)
        {
            UpdateServiceCommand = new Command(UpdateService);

            Name.OnValueChange = Validate;
            Description.OnValueChange = Validate;
            Address.OnValueChange = Validate;
            AddressNumber.OnValueChange = Validate;
            WorkFrom.OnValueChange = Validate;
            WorkTo.OnValueChange = Validate;

            _service = new OneRestaurantsService();
            var gotIt = _service.GetRestaurant(restaurantId);

            if (string.IsNullOrEmpty(gotIt.Name))
            {
                Application.Current.MainPage = new HomePage();
                return;
            }

            Id.Value = restaurantId;
            Name.Value = gotIt.Name;
            Description.Value = gotIt.Description;
            Address.Value = gotIt.AddressName;
            AddressNumber.Value = gotIt.AddressNumber;
            WorkFrom.Value = gotIt.WorkFrom;
            WorkTo.Value = gotIt.WorkTo;

            IsUpdateButtonEnabled = false;

            Name.Error = "";
            Description.Error = "";
            Address.Error = "";
            AddressNumber.Error = "";
            WorkFrom.Error = "";
            WorkTo.Error = "";
        }
        public MProp<int> Id { get; set; } = new MProp<int>();
        public MProp<string> Name { get; set; } = new MProp<string>();
        public MProp<string> Description { get; set; } = new MProp<string>();
        public MProp<string> Address { get; set; } = new MProp<string>();
        public MProp<int> AddressNumber { get; set; } = new MProp<int>();
        public MProp<int> WorkFrom { get; set; } = new MProp<int>();
        public MProp<int> WorkTo { get; set; } = new MProp<int>();

        public bool IsUpdateButtonEnabled { get; set; }

        private void Validate()
        {
            var validator = new UpdateRestaurantViewModelValidator();

            var result = validator.Validate(this);

            if (!result.IsValid)
            {
                IsUpdateButtonEnabled = false;

                var passwordError = result.Errors.FirstOrDefault(x => x.PropertyName.Contains("Name"));
                var emailError = result.Errors.FirstOrDefault(x => x.PropertyName.Contains("Description"));
                var usernameError = result.Errors.FirstOrDefault(x => x.PropertyName.Contains("Address"));
                var firstNameError = result.Errors.FirstOrDefault(x => x.PropertyName.Contains("AddressNumber"));
                var lastNameError = result.Errors.FirstOrDefault(x => x.PropertyName.Contains("WorkFrom"));
                var addressNumberError = result.Errors.FirstOrDefault(x => x.PropertyName.Contains("WorkTo"));


                ErrorPrint(passwordError, Name);
                ErrorPrint(emailError, Description);
                ErrorPrint(usernameError, Address);
                ErrorPrint(firstNameError, AddressNumber);
                ErrorPrint(lastNameError, WorkFrom);
                ErrorPrint(addressNumberError, WorkTo);
            }
            else
            {
                IsUpdateButtonEnabled = true;
                Name.Error = "";
                Description.Error = "";
                Address.Error = "";
                AddressNumber.Error = "";
                WorkFrom.Error = "";
                WorkTo.Error = "";
            }

            OnPropertyChanged(nameof(IsUpdateButtonEnabled));
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

        public ICommand UpdateServiceCommand { get; set; }

        public async void UpdateService()
        {
            RestaurantsService registerService = new RestaurantsService();
            UpdateRestaurantDTO registerDto = new UpdateRestaurantDTO
            {
                Name = this.Name.Value,
                Description = this.Description.Value,
                Address = this.Address.Value,
                AddressNumber = this.AddressNumber.Value,
                WorkFrom = this.WorkFrom.Value,
                WorkTo = this.WorkTo.Value
            };

            bool isRegistred = registerService.UpdateRestaurant(registerDto, Id.Value);
            if (isRegistred)
            {
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

                var snackbarOptions = new SnackbarOptions
                {
                    BackgroundColor = Colors.Green,
                    TextColor = Colors.White,
                    ActionButtonTextColor = Colors.Gray,
                    CornerRadius = new CornerRadius(10),
                };

                string text = "Successful update";
                string actionButtonText = "Close";
                TimeSpan duration = TimeSpan.FromSeconds(5);

                var snackbar = Snackbar.Make(text, null, actionButtonText, duration, snackbarOptions);

                await snackbar.Show(cancellationTokenSource.Token);
                Application.Current.MainPage = new HomePage();
            }
        }
    }
}
