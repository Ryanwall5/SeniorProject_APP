using Rg.Plugins.Popup.Services;
using SA_APP.CustomControls;
using SA_APP.Models.FromApi;
using SA_APP.Services;
using SA_APP.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SA_APP.Models
{
    public class Login
    {
        public Action DisplayInvalidLoginPrompt;
        public Action DisplayValidLoginPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public ICommand SubmitCommand { protected set; get; }
        private string _email;
        private string _password;
        private UserService _userService;
        public ShoppingUserAPP _shoppingUser { get; set; }
        public StoreUser _storeUser { get; set; }
        public AdminUser _adminUser { get; set; }

        public bool _loginSuccessful { get; private set; }
        private INavigation _navigation { get; set; }

        public Login(INavigation navigation)
        {
            _navigation = navigation;
            _userService = new UserService();
            _loginSuccessful = false;

            SubmitCommand = new Command(async () => 
            {
                await PopupNavigation.Instance.PushAsync(new LoadingPopup("Logging In"));

                Tuple<BaseUser, string> user = await _userService.LoginUser(_email, _password);

           
                if (user == null)
                {
                    DependencyService.Get<IAndroidToast>().MakeLongToast("Login not successful. Try Again!", "error");
                    await PopupNavigation.Instance.PopAsync();

                    _loginSuccessful = false;
                }
                else if (user.Item2 == "Shopping")
                {
                    _shoppingUser = new ShoppingUserAPP();

                    //DisplayValidLoginPrompt();
                    _loginSuccessful = true;
                    DependencyService.Get<IAndroidToast>().MakeLongToast("Login Successful", "submit");
                    await PopupNavigation.Instance.PopAsync();
                    ShoppingUserAPI shoppingUserAPI = user.Item1 as ShoppingUserAPI;
                    _shoppingUser.email = shoppingUserAPI.email;
                    _shoppingUser.fullName = shoppingUserAPI.fullName;
                    _shoppingUser.homeStore = shoppingUserAPI.homeStore;
                    _shoppingUser.role = shoppingUserAPI.role;
                    _shoppingUser.token = shoppingUserAPI.token;
                    _shoppingUser.shoppingLists = new ObservableCollection<ShoppingListAPP>();
                    foreach (ShoppingList list in shoppingUserAPI.shoppingLists)
                    {
                        ShoppingListAPP listAPP = new ShoppingListAPP
                        {
                            id = list.id,
                            name = list.name,
                            timeOfCreation = list.timeOfCreation,
                            totalCost = list.totalCost,
                            totalItems = list.totalItems,
                            items = new ObservableCollection<ShoppingItemAPP>()
                        };

                        foreach (ShoppingItem item in list.items)
                        {
                            ShoppingItemAPP shoppingItemAPP = new ShoppingItemAPP
                            {
                                linkId = item.linkId,
                                image = item.image,
                                inStock = item.inStock,
                                price = item.price,
                                stockAmount = item.stockAmount,
                                itemQuantity = item.itemQuantity,
                                name = item.name,
                                departmentId = item.departmentId,
                                department = item.department,
                                lowerDepartment = item.lowerDepartment,
                                lowerDepartmentId = item.lowerDepartmentId,
                                aisle = item.aisle,
                                aisleId = item.aisleId,
                                section = item.section,
                                sectionId = item.sectionId,
                                shelf = item.shelf,
                                shelfId = item.shelfId,
                                slot = item.slot,
                                slotId = item.slotId,
                            };

                            listAPP.items.Add(shoppingItemAPP);
                        }

                        _shoppingUser.shoppingLists.Add(listAPP);

                    }
                    await _navigation.PushModalAsync(new UserHome(_shoppingUser));
                    foreach (var page in _navigation.NavigationStack)
                    {
                        if (page.Title == "Login")
                        {
                            await _navigation.PopAsync();
                        }

                    }
                }
               
            });


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

        public async Task OnSubmit()
        {
            Tuple<BaseUser, string> user = await _userService.LoginUser(_email, _password);
            if (user == null)
            {
                DisplayInvalidLoginPrompt();
                _loginSuccessful = false;
            }
            else if (user.Item2 == "Store")
            {
                _storeUser = new StoreUser();
                DisplayValidLoginPrompt();
                _loginSuccessful = true;
                _storeUser = user.Item1 as StoreUser;
            }
            else if (user.Item2 == "Admin")
            {
                _adminUser = new AdminUser();
                DisplayValidLoginPrompt();
                _loginSuccessful = true;
                _adminUser = user.Item1 as AdminUser;
            }
            else if (user.Item2 == "Shopping")
            {
                _shoppingUser = new ShoppingUserAPP();
                DisplayValidLoginPrompt();
                _loginSuccessful = true;
                ShoppingUserAPI shoppingUserAPI = user.Item1 as ShoppingUserAPI;
                _shoppingUser.email = shoppingUserAPI.email;
                _shoppingUser.fullName = shoppingUserAPI.fullName;
                _shoppingUser.homeStore = shoppingUserAPI.homeStore;
                _shoppingUser.role = shoppingUserAPI.role;
                _shoppingUser.token = shoppingUserAPI.token;
                _shoppingUser.shoppingLists = new ObservableCollection<ShoppingListAPP>();
                foreach(ShoppingList list in shoppingUserAPI.shoppingLists)
                {
                    ShoppingListAPP listAPP = new ShoppingListAPP
                    {
                        id = list.id,
                        name = list.name,
                        timeOfCreation = list.timeOfCreation,
                        totalCost = list.totalCost,
                        totalItems = list.totalItems,
                        items = new ObservableCollection<ShoppingItemAPP>()
                    };
                               
                    foreach(ShoppingItem item in list.items)
                    {
                        ShoppingItemAPP shoppingItemAPP = new ShoppingItemAPP
                        {
                            linkId = item.linkId,
                            image = item.image,
                            inStock = item.inStock,
                            price = item.price,
                            stockAmount = item.stockAmount,
                            itemQuantity = item.itemQuantity,
                            name = item.name,
                            departmentId = item.departmentId,
                            department = item.department,
                            lowerDepartment = item.lowerDepartment,
                            lowerDepartmentId = item.lowerDepartmentId,
                            aisle = item.aisle,
                            aisleId = item.aisleId, 
                            section = item.section,
                            sectionId = item.sectionId,
                            shelf = item.shelf,
                            shelfId = item.shelfId,
                            slot =item.slot,
                            slotId = item.slotId,
                        };

                        listAPP.items.Add(shoppingItemAPP);
                    }

                    _shoppingUser.shoppingLists.Add(listAPP);
                    
                }

            }
        }
    }
}
