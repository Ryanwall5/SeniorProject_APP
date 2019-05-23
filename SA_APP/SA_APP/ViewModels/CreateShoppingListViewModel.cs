using SA_APP.Models.FromApi;
using SA_APP.Services;

using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace SA_APP.ViewModels
{
    public class CreateShoppingListViewModel : BaseViewModel
    {
        public Action DisplayInvalidShoppingListPrompt;
        public Action DisplayValidShoppingListPrompt;
        public ICommand SubmitCommand { protected set; get; }
        private string _shoppingListName;
        public string _token { get; private set; }
        public bool _createdSuccessful { get; private set; }

        private ShoppingListService _shoppingListService;
        private UsersShoppingListsViewModel _usersShoppingListsViewModel;
        private ShoppingUserAPP _shoppingUser;

        public CreateShoppingListViewModel(ShoppingUserAPP shoppingUser, UsersShoppingListsViewModel usersShoppingListsViewModel)
        {
            _shoppingUser = shoppingUser;
            _usersShoppingListsViewModel = usersShoppingListsViewModel;
            _shoppingListService = new ShoppingListService();
            SubmitCommand = new Command(OnSubmit);
            _createdSuccessful = false;
        }

        public CreateShoppingListViewModel()
        {
            _shoppingListService = new ShoppingListService();
            SubmitCommand = new Command(OnSubmit);
            _createdSuccessful = false;
        }

        public string ShoppingListName
        {
            get { return _shoppingListName; }
            set
            {
                _shoppingListName = value;
                OnPropertyChanged();
            }
        }

        public void OnSubmit()
        {
            //var listCreated = _shoppingListService.CreateShoppingList(_shoppingListName, _token);
            var listCreated = _shoppingListService.CreateShoppingList(_shoppingUser.token, _shoppingListName);
            if(listCreated != null)
            {
                _shoppingUser.shoppingLists.Add(listCreated);
                DisplayValidShoppingListPrompt();
                _createdSuccessful = true;
            }
            else
            {
                DisplayInvalidShoppingListPrompt();
                _createdSuccessful = false;
            }
        }
    }
}
