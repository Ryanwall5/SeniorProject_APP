using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Forms;

namespace SA_APP.Models.FromApi
{
    [DataContract]
    //public class Address
    //{
    //    [DataMember]
    //    public string street { get; set; }
    //    [DataMember]
    //    public string city { get; set; }
    //    [DataMember]
    //    public string state { get; set; }
    //    [DataMember]
    //    public int zip { get; set; }
    //    [DataMember]
    //    public string longitude { get; set; }
    //    [DataMember]
    //    public string latitude { get; set; }
    //}


    //[DataContract]
    //public class Address2
    //{
    //    [DataMember]
    //    public string street { get; set; }
    //    [DataMember]
    //    public string city { get; set; }
    //    [DataMember]
    //    public string state { get; set; }
    //    [DataMember]
    //    public int zip { get; set; }
    //    [DataMember]
    //    public string longitude { get; set; }
    //    [DataMember]
    //    public string latitude { get; set; }
    //}
    ////[DataContract]
    ////public class Store
    ////{
    ////    [DataMember]
    ////    public int id { get; set; }
    ////    [DataMember]
    ////    public string name { get; set; }
    ////    [DataMember]
    ////    public string phoneNumber { get; set; }
    ////    [DataMember]
    ////    public string website { get; set; }
    ////    [DataMember]
    ////    public Address2 address { get; set; }
    ////}


    // This is used when the link gets returned from updated shopping item link
    // when changing the Shopping Item Quantity
    public class UpdatedLink
    {
        public int id { get; set; }
        public int shoppingListId { get; set; }
        public object shoppingList { get; set; }
        public int itemId { get; set; }
        public object item { get; set; }
        public int itemQuantity { get; set; }
    }

    public class ShoppingItemAPP : BaseModel
    {
        private int _stockAmount;
        private int _itemQuantity;
        private string _name;
        public int linkId { get; set; }
        public int slotId { get; set; }
        public int shelfId { get; set; }
        public int aisleId { get; set; }
        public int sectionId { get; set; }
        public int departmentId { get; set; }
        public int lowerDepartmentId { get; set; }

        public string shelf { get; set; }
        public string aisle { get; set; }
        public string section { get; set; }
        public string department { get; set; }
        public string lowerDepartment { get; set; }
        public string slot { get; set; }

        public string image { get; set; }

        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public double price { get; set; }

        public bool inStock { get; set; }

        public int stockAmount
        {
            get
            {
                return _stockAmount;
            }
            set
            {
                _stockAmount = value;
                OnPropertyChanged();
            }
        }

        public int itemQuantity
        {
            get
            {
                return _itemQuantity;
            }
            set
            {
                _itemQuantity = value;
                OnPropertyChanged();
            }
        }
    }

    public class ShoppingItem
    {

        public int linkId { get; set; }

        public string image { get; set; }

        public string name { get; set; }

        public double price { get; set; }

        public bool inStock { get; set; }

        public int stockAmount { get; set; }

        public int slotId { get; set; }

        public int shelfId { get; set; }

        public int aisleId { get; set; }

        public int sectionId { get; set; }

        public int departmentId { get; set; }

        public int lowerDepartmentId { get; set; }

        public string shelf { get; set; }
        public string aisle { get; set; }
        public string section { get; set; }
        public string department { get; set; }
        public string lowerDepartment { get; set; }
        public string slot { get; set; }

        public int itemQuantity { get; set; }
    }

    public class Item
    {
        public int Id { get; set; }

        public int storeId { get; set; }

        public int LinkId { get; set; }

        public string Image { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public bool InStock { get; set; }

        public int StockAmount { get; set; }

        public int SlotId { get; set; }

        public int ShelfId { get; set; }

        public int AisleId { get; set; }

        public int SectionId { get; set; }

        public int DepartmentId { get; set; }

        public int LowerDepartmentId { get; set; }

        public string shelf { get; set; }
        public string aisle { get; set; }
        public string section { get; set; }
        public string department { get; set; }
        public string lowerDepartment { get; set; }
        public string slot { get; set; }

        public HtmlWebViewSource NutritionWidget { get; set; }

        public SpoonProductInformation ProductInformation { get; set; }
    }

    public class SpoonProductInformation
    {
        public int id { get; set; }

        public string title { get; set; }

        public decimal price { get; set; }

        public decimal likes { get; set; }

        public string[] badges { get; set; }

        public string[] important_badges { get; set; }

        public string nutrition_widget { get; set; }

        public string serving_size { get; set; }
        public string number_of_servings { get; set; }

        public decimal spoonacular_score { get; set; }

        public string[] breadcrumbs { get; set; }

        public string generated_text { get; set; }

        public decimal ingredientCount { get; set; }

        public string[] images { get; set; }

    }
    public class SpoonInfo
    {
        public int Id { get; set; }

        public HtmlWebViewSource NutritionWidget { get; set; }

        public string ServingSize { get; set; }

        public string GeneratedText { get; set; }

    }

    public class ShoppingListAPP : BaseModel
    {
        private decimal _totalPrice;
        private int _totalItemCount;
        private string _name;
        private DateTime _timeOfCreation;

        public int id { get; set; }

        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public DateTime timeOfCreation
        {
            get
            {
                return _timeOfCreation;
            }
            set
            {
                _timeOfCreation = value;
                OnPropertyChanged();
            }
        }

        public decimal totalCost
        {
            get
            {
                return _totalPrice;
            }
            set
            {
                _totalPrice = value;
                OnPropertyChanged();
            }
        }

        public int totalItems
        {
            get
            {
                return _totalItemCount;
            }
            set
            {
                _totalItemCount = value;
                OnPropertyChanged();
            }
        }


        //public Store store { get; set; }


        public ObservableCollection<ShoppingItemAPP> items { get; set; }
    }

    [DataContract]
    public class ShoppingUserAPP : BaseUser
    {
        public ObservableCollection<ShoppingListAPP> shoppingLists { get; set; }
    }


    public class Address : BaseModel
    {
        private string _street;
        private string _city;
        private string _state;
        private string _longitude;
        private string _latitude;
        private int _zip;

        public string street
        {
            get
            {
                return _street;
            }
            set
            {
                _street = value;
                OnPropertyChanged();
            }
        }

        public string city
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
                OnPropertyChanged();
            }
        }
        public string state
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
                OnPropertyChanged();
            }
        }
        public int zip
        {
            get
            {
                return _zip;
            }
            set
            {
                _zip = value;
                OnPropertyChanged();
            }
        }
        public string longitude
        {
            get
            {
                return _longitude;
            }
            set
            {
                _longitude = value;
                OnPropertyChanged();
            }
        }

        public string latitude
        {
            get
            {
                return _latitude;
            }
            set
            {
                _latitude = value;
                OnPropertyChanged();
            }
        }
    }

    public class HomeStore : BaseModel
    {
        private string _name;
        private string _phoneNumber;
        private string _website;
        public int id { get; set; }

        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string phoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged();
            }
        }
        public string website
        {
            get
            {
                return _website;
            }
            set
            {
                _website = value;
                OnPropertyChanged();
            }
        }
        public Address address { get; set; }
        public StoreMap storeMap { get; set; }
    }

    public class Address2
    {
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int zip { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
    }

    //public class Store
    //{
    //    public int id { get; set; }
    //    public string name { get; set; }
    //    public string phoneNumber { get; set; }
    //    public string website { get; set; }
    //    public Address address { get; set; }
    //}

    public class ShoppingList
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime timeOfCreation { get; set; }
        public Store store { get; set; }
        public List<ShoppingItem> items { get; set; }
        public decimal totalCost { get; set; }
        public int totalItems { get; set; }
    }

    public class ShoppingUserAPI : BaseUser
    {
        public List<ShoppingList> shoppingLists { get; set; }
    }

}
