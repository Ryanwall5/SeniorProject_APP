using SA_APP.Models;
using SA_APP.Models.FromApi;
using SA_APP.Repository;
using SA_APP.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SA_APP.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        private ItemService _itemService;
        private Login _login;

		public LoginPage ()
		{
            _login = new Login(Navigation);
            this.BindingContext = _login;
            _login.DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Invalid Login, try again", "OK");
            _login.DisplayValidLoginPrompt += () => DisplayAlert("Success", "Login, Successful", "OK");
            _itemService = new ItemService();
            this.Title = "Login";
            InitializeComponent();

            Email.Focus();
            Email.Completed += (object sender, EventArgs e) =>
            {
                Password.Focus();
            };

            Password.Completed += (object sender, EventArgs e) =>
            {
                //login.SubmitCommand.Execute(null);

                //if(login._loginSuccessful)
                //{
                //   if(login._storeUser != null)
                //    {
                //        Navigation.PushModalAsync(new StoreUserHomePage(login._storeUser));
                //    }
                //    else if (login._shoppingUser != null)
                //    {
                //        try
                //        {
                //            Navigation.PushModalAsync(new UserHome(login._shoppingUser));
                //        }   
                //        catch(Exception ex)
                //        {

                //        }

                //    }
                //    else if (login._adminUser != null)
                //    {
                //        Navigation.PushModalAsync(new AdminUserHomePage(login._adminUser));
                //    }
                //}
            };




        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Email.Text = "";
            Password.Text = "";
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {

             _login.SubmitCommand.Execute(null);

            if (_login._loginSuccessful)
            {
                if (_login._storeUser != null)
                {
                    await Navigation.PushModalAsync(new StoreUserHomePage(_login._storeUser));
                }
                else if (_login._shoppingUser != null)
                {
                    try
                    {
                        //Navigation.InsertPageBefore(new UserHome(_login._shoppingUser), this);
                        await Navigation.PushModalAsync(new UserHome(_login._shoppingUser));
                        foreach (var page in Navigation.NavigationStack)
                        {
                            if(page.Title == "Login")
                            {
                                await Navigation.PopAsync();
                            }
                          
                        }
                    }
                    catch (Exception ex)
                    {
                        
                    }

                }
                else if (_login._adminUser != null)
                {
                    await Navigation.PushModalAsync(new AdminUserHomePage(_login._adminUser));
                }
                //}
            }
        }


        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new RegisterUserPage());
        }

        //private void View_Page_Test_Clicked(object sender, EventArgs e)
        //{
        //    //ShoppingUserAPP shoppingUser = new ShoppingUserAPP();
        //    //shoppingUser.email = "shopper@SeniorProject.local";
        //    //shoppingUser.fullName = "Ryan, wall";
        //    //shoppingUser.role = "Shopping";
        //    //shoppingUser.token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIzYjY1YjM1OS0xMmNiLTQ3Y2MtYmQ3ZC0wOGQ2ODBkYTg5MTgiLCJ1bmlxdWVfbmFtZSI6IlNob3BwaW5nIn0.c6jWVaXTXcVT5AzeBdQeNABvMET1xIbw0TwpTGBuBVU";
        //    //shoppingUser.shoppingLists = new ObservableCollection<ShoppingListAPP>();

        //    //Address address = new Address
        //    //{
        //    //    street = "1041 SE 1st Ave",
        //    //    city = "Canby",
        //    //    state = "Oregon",
        //    //    zip = 97013,
        //    //    longitude = "-122.6767",
        //    //    latitude = "45.2664"
        //    //};

        //    //var items = _itemService.GetInMemoryItems();
        //    //Dictionary<int, List<ShelfSlot>> slots = new Dictionary<int, List<ShelfSlot>>();

        //    //var rand = new Random();

        //    //for(int i = 1; i < 10 + 1; i++)
        //    //{
        //    //    List<ShelfSlot> ss = new List<ShelfSlot>();
        //    //    foreach (Item item in items.OrderBy(x => rand.Next()).Take(3))
        //    //    {
        //    //        ShelfSlot shelfSlot = new ShelfSlot
        //    //        {
        //    //            Id = item.slotId,
        //    //            Item = item
        //    //        };


        //    //        ss.Add(shelfSlot);
        //    //    }
        //    //    slots.Add(i, ss);
        //    //}

        //    //List<Shelf> shelvesCannned = new List<Shelf>
        //    //{
        //    //    new Shelf
        //    //    {
        //    //        Id = 1,
        //    //        ShelfNumber = 1,
        //    //        Slots = slots[1]
        //    //    },
        //    //    new Shelf
        //    //    {
        //    //        Id = 2,
        //    //        ShelfNumber = 2,
        //    //        Slots = slots[2]
        //    //    },
        //    //    new Shelf
        //    //    {
        //    //        Id = 3,
        //    //        ShelfNumber = 3,
        //    //        Slots = slots[3]
        //    //    },
        //    //    new Shelf
        //    //    {
        //    //        Id = 4,
        //    //        ShelfNumber = 1,
        //    //        Slots = slots[4]
        //    //    },
        //    //    new Shelf
        //    //    {
        //    //        Id = 5,
        //    //        ShelfNumber = 2,
        //    //        Slots = slots[5]
        //    //    },
        //    //    new Shelf
        //    //    {
        //    //        Id = 6,
        //    //        ShelfNumber = 3,
        //    //        Slots = slots[6]
        //    //    },
        //    //    new Shelf
        //    //    {
        //    //        Id = 7,
        //    //        ShelfNumber = 1,
        //    //        Slots = slots[7]
        //    //    },
        //    //    new Shelf
        //    //    {
        //    //        Id = 8,
        //    //        ShelfNumber = 2,
        //    //        Slots = slots[8]
        //    //    },
        //    //    new Shelf
        //    //    {
        //    //        Id = 9,
        //    //        ShelfNumber = 3,
        //    //        Slots = slots[9]
        //    //    },
        //    //};

        //    //List<Section> sectionsCanned = new List<Section>
        //    //{
        //    //    new Section
        //    //    {
        //    //        Id = 1, 
        //    //        Name = "Canned1",
        //    //        ItemsPerShelf = 3,
        //    //        Shelves = shelvesCannned.Take(3).ToList()
        //    //    },
        //    //    new Section
        //    //    {
        //    //        Id = 2,
        //    //        Name = "Canned2",
        //    //        ItemsPerShelf = 3,
        //    //        Shelves = shelvesCannned.Skip(3).Take(3).ToList()
        //    //    },
        //    //    new Section
        //    //    {
        //    //        Id = 3,
        //    //        Name = "Canned3",
        //    //        ItemsPerShelf = 3,
        //    //        Shelves = shelvesCannned.Skip(6).Take(3).ToList()
        //    //    },
        //    //};

        //    //List<Aisle> aislesCanned = new List<Aisle>
        //    //{
        //    //    new Aisle
        //    //    {
        //    //        Id = 1,
        //    //        Name = "1",
        //    //        SideOfAisle = "Right",
        //    //        Row = 1,
        //    //        Column = 1,
        //    //        Sections = sectionsCanned
        //    //    },
        //    //    new Aisle
        //    //    {
        //    //        Id = 2,
        //    //        Name = "2",
        //    //        SideOfAisle = "left",
        //    //        Row = 1,
        //    //        Column = 3,
        //    //        Sections = sectionsCanned
        //    //    },
        //    //    new Aisle
        //    //    {
        //    //        Id = 3,
        //    //        Name = "2",
        //    //        SideOfAisle = "right",
        //    //        Row = 1,
        //    //        Column = 5,
        //    //        Sections = sectionsCanned
        //    //    },
        //    //    new Aisle
        //    //    {
        //    //        Id = 4,
        //    //        Name = "3",
        //    //        SideOfAisle = "left",
        //    //        Row = 1,
        //    //        Column = 7,
        //    //        Sections = sectionsCanned
        //    //    },
        //    //    new Aisle
        //    //    {
        //    //        Id = 5,
        //    //        Name = "3",
        //    //        SideOfAisle = "right",
        //    //        Row = 1,
        //    //        Column = 9,
        //    //        Sections = sectionsCanned
        //    //    },
        //    //     new Aisle
        //    //    {
        //    //        Id = 6,
        //    //        Name = "4",
        //    //        SideOfAisle = "left",
        //    //        Row = 1,
        //    //        Column = 11,
        //    //        Sections = sectionsCanned
        //    //    },
        //    //};


        //    //List<LowerDepartment> lowerDepartmentsGrocery = new List<LowerDepartment>
        //    //{
        //    //    new LowerDepartment
        //    //    {
        //    //        Id = 1,
        //    //        Name = "Canned/Pasta",
        //    //        Row = 0,
        //    //        Column = 0,
        //    //        AisleCount = 3,
        //    //        Aisles = aislesCanned
        //    //    },
        //    //    new LowerDepartment
        //    //    {
        //    //        Id = 2,
        //    //        Name = "Coffee/Drinks/Crackers",
        //    //        Row = 0,
        //    //        Column = 1,
        //    //        AisleCount = 3,
        //    //        Aisles = aislesCanned
        //    //    },
        //    //    new LowerDepartment
        //    //    {
        //    //        Id = 3,
        //    //        Name = "Healthy Back",
        //    //        Row = 0,
        //    //        Column = 2,
        //    //        AisleCount = 3,
        //    //        Aisles = aislesCanned
        //    //    },
        //    //    new LowerDepartment
        //    //    {
        //    //        Id = 4,
        //    //        Name = "Cooking/Asian/Mexican",
        //    //        Row = 1,
        //    //        Column = 0,
        //    //        AisleCount = 3,
        //    //        Aisles = aislesCanned
        //    //    },
        //    //    new LowerDepartment
        //    //    {
        //    //        Id = 5,
        //    //        Name = "Sancks/Candy",
        //    //        Row = 1,
        //    //        Column = 1,
        //    //        AisleCount = 3,
        //    //        Aisles = aislesCanned
        //    //    },
        //    //    new LowerDepartment
        //    //    {
        //    //        Id = 5,
        //    //        Name = "Healthy front",
        //    //        Row = 1,
        //    //        Column = 2,
        //    //        AisleCount = 3,
        //    //        Aisles = aislesCanned
        //    //    }
        //    //};

        //    //List<Department> departments = new List<Department>
        //    //{
        //    //    new Department
        //    //    {
        //    //        Id = 1,
        //    //        Name = "Grocery",
        //    //        Rows = 2,
        //    //        Columns = 3,
        //    //        LowerDepartments = lowerDepartmentsGrocery
        //    //    },
        //    //    new Department
        //    //    {
        //    //        Id = 2,
        //    //        Name = "Electronics",
        //    //        Rows = 2,
        //    //        Columns = 3,
        //    //        LowerDepartments = lowerDepartmentsGrocery
        //    //    },
        //    //    new Department
        //    //    {
        //    //        Id = 3,
        //    //        Name = "Tools",
        //    //        Rows = 2,
        //    //        Columns = 3,
        //    //        LowerDepartments = lowerDepartmentsGrocery
        //    //    },
        //    //    new Department
        //    //    {
        //    //        Id = 4,
        //    //        Name = "Sports",
        //    //        Rows = 2,
        //    //        Columns = 3,
        //    //        LowerDepartments = lowerDepartmentsGrocery
        //    //    },
        //    //    new Department
        //    //    {
        //    //        Id = 5,
        //    //        Name = "Housing",
        //    //        Rows = 2,
        //    //        Columns = 3,
        //    //        LowerDepartments = lowerDepartmentsGrocery
        //    //    }
        //    //};

        //    //StoreMap storeMap = new StoreMap()
        //    //{
        //    //    Id = 1,
        //    //    Departments = departments
        //    //};


        //    //Store store = new Store
        //    //{
        //    //    id = 1,
        //    //    address = address,
        //    //    name = "Fred Meyer",
        //    //    phoneNumber = "5032634100",
        //    //    website = "http://www.fredmeyer.com",
        //    //    items = items,
        //    //    storeMap = storeMap          
        //    //};

        //    //shoppingUser.homeStore = store;

        //    //ShoppingListAPP shoppingList1 = new ShoppingListAPP
        //    //{
        //    //    id = 1,
        //    //    name = "Christmas",
        //    //    store = store,
        //    //    timeOfCreation = DateTime.Now,
        //    //    items = new ObservableCollection<ShoppingItemAPP>(),
        //    //    totalCost = 0,
        //    //    totalItems = 0
        //    //};

        //    //ShoppingListAPP shoppingList2 = new ShoppingListAPP
        //    //{
        //    //    id = 2,
        //    //    name = "Weekend",
        //    //    store = store,
        //    //    timeOfCreation = DateTime.Now,
        //    //    items = new ObservableCollection<ShoppingItemAPP>(),
        //    //    totalCost = 0,
        //    //    totalItems = 0
        //    //};

        //    //shoppingUser.shoppingLists.Add(shoppingList1);
        //    //shoppingUser.shoppingLists.Add(shoppingList2);

        //    //Console.Write("Hello");
        //    //var user = InMemoryDatabase._shoppingUser;
        //    Navigation.PushModalAsync(new PanPractice());
        //}


    }
}