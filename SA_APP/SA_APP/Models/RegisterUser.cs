using Rg.Plugins.Popup.Services;
using SA_APP.Models.FromApi;
using SA_APP.Models.ToApi;
using SA_APP.Services;
using SA_APP.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SA_APP.Models
{
    public class RegisterUser
    {
        public Action DisplayInvalidRegisterPrompt;
        public Action DisplayValidRegisterPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public ICommand SubmitCommand { protected set; get; }

        private string _firstName;
        private string _lastName;
        private int _homeStoreId;
        private string _email;
        private string _password;
        private string _comfirmPassword;
        private bool _areYouAStoreUser;
        private List<Store> _storeList = new List<Store>();
        private Store _selectedStore;
        private StoreService _storeService;
        private INavigation _navigation;
        private UserService _userService;

        public bool _registerSuccessful { get; private set; }
        public RegisterUser(INavigation nav)
        {
            //_storeList = new List<Store>
            //{
            //    new Store{ id = 3, address = new Address{ }, name = "Fred Meyer", phoneNumber = "5032634100", website = "https://www.fredmeyer.com" },
            //    new Store{  id = 2, address = new Address{ }, name = "Safeway", phoneNumber = "5032665890", website = "https://www.fredmeyer.com" },
            //};
            _userService = new UserService();
            _storeService = new StoreService();
            _registerSuccessful = false;
            init();

            SubmitCommand = new Command(async () => {

                if(VerifyUserInformation())
                {
                    APIUser apiUser = new APIUser
                    {
                        Email = _email,
                        Password = _password,
                        FirstName = _firstName,
                        LastName = _lastName,
                        HomeStoreId = SelectedStore.id
                    };
                    await PopupNavigation.Instance.PushAsync(new LoadingPopup("Registering New User"));
                    var userRegistered = await _userService.RegisterUser(apiUser);
                    await PopupNavigation.Instance.PopAsync();
                    if (userRegistered)
                    {
                        DisplayValidRegisterPrompt();
                        await _navigation.PopModalAsync();
                    }
                    else
                    {
                        DisplayInvalidRegisterPrompt();
                    }
                }
                else
                {
                    DisplayInvalidRegisterPrompt();
                }
               
            }); 

        }

        public  void init()
        {
            StoreList = _storeService.GetStores();
        }

        public List<Store> StoreList
        {
            get { return _storeList; }
            set
            {
                _storeList = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(StoreList)));
            }
        }

        public Store SelectedStore
        {
            get { return _selectedStore; }
            set
            {
                _selectedStore = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedStore)));
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Email)));
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Password)));
            }
        }

        public string ConfirmPassword
        {
            get { return _comfirmPassword; }
            set
            {
                _comfirmPassword = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(ConfirmPassword)));
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(LastName)));
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(FirstName)));
            }
        }


        public int HomeStoreId
        {
            get { return _homeStoreId; }
            set
            {
                _homeStoreId = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(HomeStoreId)));
            }
        }


        public bool VerifyUserInformation()
        {
            if(Password != ConfirmPassword)
            {
                return false;
            }
            
            if(FirstName == "" || FirstName == null)
            {
                return false;
            }

            return true;
        }
    }
}
