using SA_APP.Models;
using SA_APP.Models.FromApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SA_APP.ViewModels
{
    public class DepartmentsViewModel
    {
        public Action DisplayInvalidLoginPrompt;
        public Action DisplayValidLoginPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public ICommand DepartmentClickedCommand => new Command<string>(GoToLowerDepartments);

        public ShoppingUserAPP _shoppingUserAPP;

        public DepartmentsViewModel(ShoppingUserAPP shoppingUser)
        {
            _shoppingUserAPP = shoppingUser;
        }

        private void GoToLowerDepartments(string departmentName)
        {
            Department department = _shoppingUserAPP.homeStore.storeMap.Departments.FirstOrDefault(d => d.Name == departmentName);

        }
    }
}
