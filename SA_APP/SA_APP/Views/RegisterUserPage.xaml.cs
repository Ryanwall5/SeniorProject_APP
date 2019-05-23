using SA_APP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SA_APP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterUserPage : ContentPage
    {
        private RegisterUser _registerUser;
        public RegisterUserPage()
        {
            _registerUser = new RegisterUser(Navigation);
            this.BindingContext = _registerUser;
            _registerUser.DisplayInvalidRegisterPrompt += () => DisplayAlert("Error", "Invalid Registration, try again", "OK");
            _registerUser.DisplayValidRegisterPrompt += () => DisplayAlert("Success", "Register, Successful", "OK");

            InitializeComponent();

            firstNameEntry.Completed += (object sender, EventArgs e) =>
            {
                lastNameEntry.Focus();
            };

            lastNameEntry.Completed += (object sender, EventArgs e) =>
            {
                emailEntry.Focus();
            };

            emailEntry.Completed += (object sender, EventArgs e) =>
            {
                passwordEntry.Focus();
            };

            passwordEntry.Completed += (object sender, EventArgs e) =>
            {
                confirmPasswordEntry.Focus();
            };

            confirmPasswordEntry.Completed += (object sender, EventArgs e) =>
            {
                homeStorePicker.Focus();
            };
        }

        //private async void Button_Clicked(object sender, EventArgs e)
        //{
        //    _registerUser.SubmitCommand.Execute(null);

        //    if (_registerUser._registerSuccessful)
        //    {
        //        _registerUser.DisplayValidRegisterPrompt();
        //        await Navigation.PopModalAsync();
        //    }
        //}
    }
}