using SA_APP.Models;
using SA_APP.Models.FromApi;
using SA_APP.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace SA_APP.Repository
{
    public static class InMemoryDatabase
    {
        public static Store _store = new Store();
        public static ObservableCollection<ShoppingItemAPP> _shoppingItems = new ObservableCollection<ShoppingItemAPP>();
        public static List<Item> _items = new List<Item>();

        public static ObservableCollection<ShoppingList> _shoppingLists = new ObservableCollection<ShoppingList>();
        public static List<Address> _addresses = new List<Address>();
        public static ShoppingUserAPP _shoppingUser = new ShoppingUserAPP();


        public static List<Department> _departments = new List<Department>();
        public static Address _address = new Address();
        public static List<Aisle> _aislesCanned = new List<Aisle>();
        public static StoreMap _storeMap = new StoreMap();
        public static List<LowerDepartment> _lowerDepartmentsGrocery = new List<LowerDepartment>();
        public static List<Section> _sectionsCanned = new List<Section>();
        public static List<Shelf> shelvesCannned = new List<Shelf>();
        public static Dictionary<int, List<ShelfSlot>> slots = new Dictionary<int, List<ShelfSlot>>();
        public static int _shoppingListIdCount;
        public static ItemService _itemService;
        public static Dictionary<string, Color> colorsByName = new Dictionary<string, Color>()
        {
            { "red", Color.FromRgb(204,0,0) },
            { "green", Color.FromRgb(0,204,0) },
            { "orangish", Color.FromRgb(204,102,0) },
            { "yellowish", Color.FromRgb(204,204,0) },
            { "green/yellow", Color.FromRgb(153,204,0) },
            { "blue/green", Color.FromRgb(0,204,153) },
            { "lightblue", Color.FromRgb(0,153,204) },
            { "blue", Color.FromRgb(0,0,204) },
            { "purple", Color.FromRgb(153,0,204) },
            { "pinkish", Color.FromRgb(204,0,204) },
            { "red/pink", Color.FromRgb(204,0,153) },
        };

        public static Dictionary<int, Color> colorsByInt = new Dictionary<int, Color>()
        {
            { 0, Color.FromRgb(204,0,0) },
            { 1, Color.FromRgb(0,204,0) },
            { 2, Color.FromRgb(204,102,0) },
            { 3, Color.FromRgb(204,204,0) },
            { 4, Color.FromRgb(153,204,0) },
            { 5, Color.FromRgb(0,204,153) },
            { 7, Color.FromRgb(0,153,204) },
            { 8, Color.FromRgb(0,0,204) },
            { 9, Color.FromRgb(153,0,204) },
            { 10, Color.FromRgb(204,0,204) },
            { 11, Color.FromRgb(204,0,153) },
        };




        static InMemoryDatabase()
        {
            _itemService = new ItemService();
            AddStuff();
            //AddAddresses();
            //AddStores();
            //AddStoreItems();
            //AddShoppingItems();
            //AddShoppingLists();
            //AddShoppingUsers();
        }

        private static void AddStuff()
        {
            _shoppingUser = new ShoppingUserAPP();
            _shoppingUser.email = "shopper@SeniorProject.local";
            _shoppingUser.fullName = "Ryan wall";
            _shoppingUser.role = "Shopping";
            _shoppingUser.token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIzYjY1YjM1OS0xMmNiLTQ3Y2MtYmQ3ZC0wOGQ2ODBkYTg5MTgiLCJ1bmlxdWVfbmFtZSI6IlNob3BwaW5nIn0.c6jWVaXTXcVT5AzeBdQeNABvMET1xIbw0TwpTGBuBVU";
            _shoppingUser.shoppingLists = new ObservableCollection<ShoppingListAPP>();

            _address = new Address
            {
                street = "1041 SE 1st Ave",
                city = "Canby",
                state = "Oregon",
                zip = 97013,
                longitude = "-122.6767",
                latitude = "45.2664"
            };

            _items = _itemService.GetInMemoryItems();

            var rand = new Random();

            int count = 0;
            for (int i = 1; i < 10 + 1; i++)
            {
                List<ShelfSlot> ss = new List<ShelfSlot>();
                foreach (Item item in _items.OrderBy(x => rand.Next()).Take(3))
                {
                    count += 1;
                    ShelfSlot shelfSlot = new ShelfSlot
                    {
                        Id = count,
                        SlotOnShelf = item.SlotId,
                        Item = item
                    };

                    ss.Add(shelfSlot);
                }
                slots.Add(i, ss);
            }

            #region Shelves

            shelvesCannned = new List<Shelf>
            {
                new Shelf
                {
                    Id = 1,
                    ShelfNumber = 1,
                    Slots = slots[1]
                },
                new Shelf
                {
                    Id = 2,
                    ShelfNumber = 2,
                    Slots = slots[2]
                },
                new Shelf
                {
                    Id = 3,
                    ShelfNumber = 3,
                    Slots = slots[3]
                },
                new Shelf
                {
                    Id = 4,
                    ShelfNumber = 1,
                    Slots = slots[4]
                },
                new Shelf
                {
                    Id = 5,
                    ShelfNumber = 2,
                    Slots = slots[5]
                },
                new Shelf
                {
                    Id = 6,
                    ShelfNumber = 3,
                    Slots = slots[6]
                },
                new Shelf
                {
                    Id = 7,
                    ShelfNumber = 1,
                    Slots = slots[7]
                },
                new Shelf
                {
                    Id = 8,
                    ShelfNumber = 2,
                    Slots = slots[8]
                },
                new Shelf
                {
                    Id = 9,
                    ShelfNumber = 3,
                    Slots = slots[9]
                },
            };

            #endregion

            #region Sections

            List<Section> milkSections = new List<Section>
            {
                new Section
                {
                    Id = 1,
                    Name = "Milk1",
                    ItemsPerShelf = 3,
                    Shelves = shelvesCannned.Take(3).ToList()
                },
                new Section
                {
                    Id = 2,
                    Name = "Milk1",
                    ItemsPerShelf = 3,
                    Shelves = shelvesCannned.Skip(3).Take(3).ToList()
                },
                new Section
                {
                    Id = 3,
                    Name = "Milk1",
                    ItemsPerShelf = 3,
                    Shelves = shelvesCannned.Skip(6).Take(3).ToList()
                },
            };

            List<Section> coffee_hotcocoa = new List<Section>
            {
                new Section
                {
                    Id = 4,
                    Name = "Coffee1",
                    ItemsPerShelf = 3,
                    Shelves = shelvesCannned.Take(3).ToList()
                },
                new Section
                {
                    Id = 5,
                    Name = "Coffee2",
                    ItemsPerShelf = 3,
                    Shelves = shelvesCannned.Skip(3).Take(3).ToList()
                },
                new Section
                {
                    Id = 6,
                    Name = "Hot Cocoa",
                    ItemsPerShelf = 3,
                    Shelves = shelvesCannned.Skip(6).Take(3).ToList()
                },
            };

            #endregion

            #region Aisles

            _aislesCanned = new List<Aisle>
            {
                new Aisle
                {
                    Id = 1,
                    Name = "1",
                    SideOfAisle = "Right",
                    Row = 1,
                    Column = 1,
                    Sections = milkSections
                },
                new Aisle
                {
                    Id = 2,
                    Name = "2",
                    SideOfAisle = "left",
                    Row = 1,
                    Column = 3,
                    Sections = milkSections
                },
                new Aisle
                {
                    Id = 3,
                    Name = "2",
                    SideOfAisle = "right",
                    Row = 1,
                    Column = 5,
                    Sections = milkSections
                },
                new Aisle
                {
                    Id = 4,
                    Name = "3",
                    SideOfAisle = "left",
                    Row = 1,
                    Column = 7,
                    Sections = milkSections
                },
                new Aisle
                {
                    Id = 5,
                    Name = "3",
                    SideOfAisle = "right",
                    Row = 1,
                    Column = 9,
                    Sections = milkSections
                },
                 new Aisle
                {
                    Id = 6,
                    Name = "4",
                    SideOfAisle = "left",
                    Row = 1,
                    Column = 11,
                    Sections = milkSections
                },
            };



            List<Aisle> coffee_drinks_crackers = new List<Aisle>
            {
                new Aisle
                {
                    Id = 7,
                    Name = "4",
                    SideOfAisle = "Right",
                    Row = 1,
                    Column = 1,
                    Sections = milkSections
                },
                new Aisle
                {
                    Id = 8,
                    Name = "5",
                    SideOfAisle = "left",
                    Row = 1,
                    Column = 3,
                    Sections = milkSections
                },
                new Aisle
                {
                    Id = 9,
                    Name = "5",
                    SideOfAisle = "right",
                    Row = 1,
                    Column = 5,
                    Sections = milkSections
                },
                new Aisle
                {
                    Id = 10,
                    Name = "6",
                    SideOfAisle = "left",
                    Row = 1,
                    Column = 7,
                    Sections = milkSections
                },
                new Aisle
                {
                    Id = 11,
                    Name = "6",
                    SideOfAisle = "right",
                    Row = 1,
                    Column = 9,
                    Sections = milkSections
                },
                 new Aisle
                {
                    Id = 12,
                    Name = "7",
                    SideOfAisle = "left",
                    Row = 1,
                    Column = 11,
                    Sections = milkSections
                },
            };

            List<Aisle> healthy_back_aisles = new List<Aisle>
            {
                new Aisle
                {
                    Id = 13,
                    Name = "7",
                    SideOfAisle = "Right",
                    Row = 1,
                    Column = 1,
                    Sections = milkSections
                },
                new Aisle
                {
                    Id = 14,
                    Name = "8",
                    SideOfAisle = "left",
                    Row = 1,
                    Column = 3,
                    Sections = milkSections
                },
                new Aisle
                {
                    Id = 15,
                    Name = "8",
                    SideOfAisle = "right",
                    Row = 1,
                    Column = 5,
                    Sections = milkSections
                },
                new Aisle
                {
                    Id = 16,
                    Name = "9",
                    SideOfAisle = "left",
                    Row = 1,
                    Column = 7,
                    Sections = milkSections
                },
                new Aisle
                {
                    Id = 17,
                    Name = "9",
                    SideOfAisle = "right",
                    Row = 1,
                    Column = 9,
                    Sections = milkSections
                },
                 new Aisle
                {
                    Id = 18,
                    Name = "10",
                    SideOfAisle = "left",
                    Row = 1,
                    Column = 11,
                    Sections = milkSections
                },
            };

            List<Aisle> milkAisle = new List<Aisle>
            {
                new Aisle
                {
                    Id = 18,
                    Name = "1",
                    SideOfAisle = "Backwall",
                    Row = 1,
                    Column = 1,
                    Sections = milkSections
                },

            };

            #endregion

            #region Lower Departments

            _lowerDepartmentsGrocery = new List<LowerDepartment>
            {
                new LowerDepartment
                {
                    Id = 1,
                    Name = "Canned/Pasta",
                    Row = 0,
                    Column = 0,
                    AisleCount = 3,
                    AisleStart = 1,
                    Aisles = _aislesCanned
                },
                new LowerDepartment
                {
                    Id = 2,
                    Name = "Coffee/Drinks/Crackers",
                    Row = 0,
                    Column = 1,
                    AisleCount = 3,
                    AisleStart = 4,
                    Aisles = coffee_drinks_crackers
                },
                new LowerDepartment
                {
                    Id = 3,
                    Name = "Healthy Back",
                    Row = 0,
                    Column = 2,
                    AisleCount = 3,
                    AisleStart = 7,
                    Aisles = healthy_back_aisles
                },
                new LowerDepartment
                {
                    Id = 4,
                    Name = "Cooking/Asian/Mexican",
                    Row = 1,
                    Column = 0,
                    AisleCount = 3,
                    AisleStart = 10,
                    Aisles = _aislesCanned
                },
                new LowerDepartment
                {
                    Id = 5,
                    Name = "Chips/Cereal",
                    Row = 1,
                    Column = 1,
                    AisleCount = 3,
                    AisleStart = 13,
                    Aisles = _aislesCanned
                },
                new LowerDepartment
                {
                    Id = 6,
                    Name = "Healthy front",
                    Row = 1,
                    Column = 2,
                    AisleCount = 3,
                    AisleStart = 16,
                    Aisles = _aislesCanned
                }
            };

            List<LowerDepartment> lowerDepartmentsPKB = new List<LowerDepartment>
            {
                new LowerDepartment
                {
                    Id = 7,
                    Name = "Paint",
                    Row = 0,
                    Column = 0,
                    AisleCount = 3,
                    AisleStart = 1,
                    Aisles = _aislesCanned
                },
                new LowerDepartment
                {
                    Id = 8,
                    Name = "Bathroom",
                    Row = 0,
                    Column = 1,
                    AisleCount = 3,
                    AisleStart = 4,
                    Aisles = _aislesCanned
                },
                new LowerDepartment
                {
                    Id = 9,
                    Name = "Kitchen",
                    Row = 0,
                    Column = 2,
                    AisleCount = 3,
                    AisleStart = 7,
                    Aisles = _aislesCanned
                },
            };


            List<LowerDepartment> dairyLowerDepartment = new List<LowerDepartment>
            {
                new LowerDepartment
                {
                    Id = 10,
                    Name = "Milk and Yogurt",
                    Row = 0,
                    Column = 0,
                    AisleCount = 1,
                    Aisles = milkAisle
                },
                new LowerDepartment
                {
                    Id = 11,
                    Name = "Cheese",
                    Row = 0,
                    Column = 1,
                    AisleCount = 1,
                    Aisles = _aislesCanned
                },
            };

            List<LowerDepartment> meatLowerDepartment = new List<LowerDepartment>
            {
                new LowerDepartment
                {
                    Id = 12,
                    Name = "Sausage",
                    Row = 0,
                    Column = 0,
                    AisleCount = 1,
                    Aisles = _aislesCanned
                },
                new LowerDepartment
                {
                    Id = 13,
                    Name = "Pork and Steak",
                    Row = 0,
                    Column = 1,
                    AisleCount = 1,
                    Aisles = _aislesCanned
                },
                new LowerDepartment
                {
                    Id = 14,
                    Name = "Chicken",
                    Row = 0,
                    Column = 2,
                    AisleCount = 1,
                    Aisles = _aislesCanned
                },
                new LowerDepartment
                {
                    Id = 15,
                    Name = "Fish",
                    Row = 0,
                    Column = 3,
                    AisleCount = 1,
                    Aisles = _aislesCanned
                }
            };

            List<LowerDepartment> elec_sports_tools_lowerDepartment = new List<LowerDepartment>
            {
                new LowerDepartment
                {
                    Id = 16,
                    Name = "Electronics",
                    Row = 0,
                    Column = 0,
                    AisleCount = 2,
                    AisleStart = 1,
                    Aisles = _aislesCanned.Take(4).ToList()
                },
                new LowerDepartment
                {
                    Id = 17,
                    Name = "Sports",
                    Row = 0,
                    Column = 1,
                    AisleCount = 3,
                    AisleStart = 3,
                    Aisles = _aislesCanned
                },
                new LowerDepartment
                {
                    Id = 18,
                    Name = "Tools",
                    Row = 0,
                    Column = 2,
                    AisleCount = 3,
                    AisleStart = 6,
                    Aisles = _aislesCanned
                }
            };

            List<LowerDepartment> house_furniture_lowerDepartment = new List<LowerDepartment>
            {
                new LowerDepartment
                {
                    Id = 16,
                    Name = "Furniture",
                    Row = 0,
                    Column = 0,
                    AisleCount = 1,
                    Aisles = _aislesCanned
                },
                new LowerDepartment
                {
                    Id = 17,
                    Name = "Lighting and Candles",
                    Row = 0,
                    Column = 1,
                    AisleCount = 3,
                    Aisles = _aislesCanned
                },
                new LowerDepartment
                {
                    Id = 18,
                    Name = "Wall Decor",
                    Row = 0,
                    Column = 2,
                    AisleCount = 3,
                    Aisles = _aislesCanned
                }
            };

            #endregion

            #region Departments

            _departments = new List<Department>
            {
                new Department
                {
                    Id = 1,
                    Name = "Paint/Kitchen/bathroom",
                    MapLocation = "0,0,1,2",
                    LowerDepartments = lowerDepartmentsPKB,
                    ColumnsDefinitions = "*,*,*",
                    RowDefinitions = "*"
                },
                new Department
                {
                    Id = 2,
                    Name = "Dairy",
                    MapLocation = "0,2,1,2",
                    LowerDepartments = dairyLowerDepartment,
                    ColumnsDefinitions = "*,*",
                    RowDefinitions = "*"
                },
                new Department
                {
                    Id = 3,
                    Name = "Meat",
                    MapLocation = "0,4,1,1",
                    LowerDepartments = meatLowerDepartment,
                    ColumnsDefinitions = "*,*,*,*",
                    RowDefinitions = "*"
                },
                new Department
                {
                    Id = 4,
                    Name = "Elect./Sports/Tools",
                    MapLocation = "1,0,5,1",
                    LowerDepartments = elec_sports_tools_lowerDepartment,
                    ColumnsDefinitions = "*,*,*",
                    RowDefinitions = "*"
                },
                new Department
                {
                    Id = 5,
                    Name = "House/Furniture",
                    MapLocation = "1,1,5,1",
                    LowerDepartments = house_furniture_lowerDepartment,
                    ColumnsDefinitions = "*,*,*",
                    RowDefinitions = "*"
                },
                new Department
                {
                    Id = 6,
                    Name = "Grocery",
                    MapLocation = "1,2,5,2",
                    LowerDepartments = _lowerDepartmentsGrocery,
                    ColumnsDefinitions = "*,*,*",
                    RowDefinitions = "*,*"
                },
                new Department
                {
                    Id = 7,
                    Name = "Alcohol",
                    MapLocation = "1,4,2,1",
                    LowerDepartments = new List<LowerDepartment>(),
                    ColumnsDefinitions = "",
                    RowDefinitions = ""
                },
                new Department
                {
                    Id = 8,
                    Name = "Produce",
                    MapLocation = "3,4,3,1",
                    LowerDepartments = new List<LowerDepartment>(),
                    ColumnsDefinitions = "",
                    RowDefinitions = ""
                },
                 new Department
                {
                    Id = 9,
                    Name = "Apparel",
                    MapLocation = "0,5,6,1",
                    LowerDepartments = new List<LowerDepartment>(),
                    ColumnsDefinitions = "",
                    RowDefinitions = ""
                },
                new Department
                {
                    Id = 10,
                    Name = "Cash Registers",
                    MapLocation = "6,2,1,2",
                    LowerDepartments = new List<LowerDepartment>(),
                    ColumnsDefinitions = "",
                    RowDefinitions = ""
                }
            };

            #endregion

            #region Store Map

            _storeMap = new StoreMap()
            {
                Id = 1,
                Departments = _departments,
                ColumnsDefinitions = "60,70,*,*,*",
                RowDefinitions = "*,*,*,*,*,*,*",
            };

            #endregion

            _store = new Store
            {
                id = 1,
                address = _address,
                name = "Fred Meyer",
                phoneNumber = "5032634100",
                website = "http://www.fredmeyer.com",
                items = _items,
                storeMap = _storeMap
            };

            _shoppingUser.homeStore = _store;

            foreach(var item in _items.Take(3))
            {
                // setting location information just for now
                _shoppingItems.Add(
                    new ShoppingItemAPP
                    {
                        linkId = item.LinkId,
                        slotId = 1,
                        departmentId = 2,
                        aisleId = 18,
                        sectionId = 1, 
                        shelfId = 1,
                        lowerDepartmentId = 10,
                        department = "Dairy",
                        lowerDepartment = "Milk and Yogurt",
                        shelf = "1",
                        aisle = "1",
                        section = "Milk1",
                        name = item.Name,
                        itemQuantity = 3,
                        inStock = item.InStock,
                        stockAmount = item.StockAmount, 
                        image = "https://spoonacular.com/productImages/46136-312x231.jpg",
                        price = Convert.ToDouble(item.Price)
                    });
            }

            _shoppingItems.First().departmentId = 6;
            _shoppingItems.First().department = "Grocery";

            ShoppingListAPP shoppingList1 = new ShoppingListAPP
            {
                id = 1,
                name = "Christmas",
                //store = _store,
                timeOfCreation = DateTime.Now,
                items = _shoppingItems,
                totalCost = Convert.ToDecimal(_shoppingItems.Select(i => i.itemQuantity * i.price).Sum()),
                totalItems = _shoppingItems.Select(i => i.itemQuantity).Sum()
            };

            ShoppingListAPP shoppingList2 = new ShoppingListAPP
            {
                id = 2,
                name = "Weekend",
                //store = _store,
                timeOfCreation = DateTime.Now,
                items = _shoppingItems,
                totalCost = Convert.ToDecimal(_shoppingItems.Select(i => i.itemQuantity * i.price).Sum()),
                totalItems = _shoppingItems.Select(i => i.itemQuantity).Sum()
            };

            _shoppingUser.shoppingLists.Add(shoppingList1);
            _shoppingUser.shoppingLists.Add(shoppingList2);
        }
            


        //private static void AddStoreItems()
        //{
        //    Store store = _stores.First();

        //    List<Item> items = new List<Item>
        //    {
        //       new Item
        //        {
        //            id = 1,
        //            image = "none",
        //           name = "test item 1",
        //            price = 2.99,
        //            inStock = true,
        //            stockAmount = 100,
        //            slotId = 
        //            storeId = 1
        //        },
        //        new Item
        //        {
        //            Id = 2,
        //            Image = "none",
        //            Name = "test item 2",
        //            Price = 2.99,
        //            InStock = true,
        //            StockAmount = 100,
        //            Aisle = "2",
        //            Section = "test section",
        //            StoreId = 1
        //        },
        //        new Item
        //        {
        //            Id = 3,
        //            Image = "none",
        //            Name = "test item 3",
        //            Price = 2.99,
        //            InStock = true,
        //            StockAmount = 100,
        //            Aisle = "1",
        //            Section = "test section",
        //            StoreId = 1
        //        },
        //        new Item
        //        {
        //            Id = 4,
        //            Image = "none",
        //            Name = "test item 4",
        //            Price = 2.99,
        //            InStock = true,
        //            StockAmount = 100,
        //            Aisle = "3",
        //            Section = "test section",
        //            StoreId = 1
        //        },
        //    };

        //    foreach (Item item in items)
        //    {
        //        store.Items.Add(item);
        //    }
        //}

        //public static ShoppingList GetShoppingList()
        //{
        //    return _user1ListofShoppingLists._shoppingLists.First();
        //}

        //public static void UpdateShoppingItem(string token, int listId, ShoppingItem shoppingItem)
        //{
        //    ShoppingUser user = _shoppingUsers.FirstOrDefault(u => u.Token == token);

        //    ShoppingList list = user.ShoppingLists._shoppingLists.FirstOrDefault(l => l.Id == listId);
        //    ShoppingItem item = list.Items.FirstOrDefault(i => i.Id == shoppingItem.Id);

        //    item.ItemQuantity = shoppingItem.ItemQuantity;
        //}

        //public static ShoppingItem GetItemById(int itemId)
        //{
        //    ShoppingItem item = _shoppingItems.FirstOrDefault(i => i.Id == itemId);
        //    return item;
        //}

        //public static ListofShoppingLists GetShoppingLists()
        //{
        //    return _user1ListofShoppingLists;
        //}

        //public static BaseUser LoginUser(string email, string password)
        //{

        //    ShoppingUser user = _shoppingUsers.FirstOrDefault(u => u.Email == email);

        //    if (user == null)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        return user;
        //    }
        //}

        //public static List<Item> SearchItems(string search, int storeId)
        //{
        //    Store store = _stores.FirstOrDefault(s => s.Id == storeId);

        //    if (store == null)
        //    {
        //        return null;
        //    }

        //    var foundItems = store.Items.Where(i => i.Name.Contains(search)).ToList();

        //    return foundItems;
        //}


        //public static bool CreateShoppingList(string token, string name)
        //{
        //    if (_shoppingLists.FirstOrDefault(sl => sl.Name == name) != null)
        //    {
        //        return false;
        //    }

        //    ShoppingUser user = _shoppingUsers.FirstOrDefault(u => u.Token == token);

        //    _shoppingListIdCount += 1;
        //    ShoppingList shoppingList = new ShoppingList
        //    {
        //        Id = _shoppingListIdCount,
        //        Name = name,
        //        Items = new ObservableCollection<ShoppingItem>(),
        //        Store = _stores.First(),
        //        TimeOfCreation = DateTime.Now,
        //        TotalItemCount = 0,
        //        TotalItemQTY = 0,
        //        TotalPrice = 0,
        //    };
        //    user.ShoppingLists._shoppingLists.Add(shoppingList);
        //    user.ShoppingLists._shoppingListsCount += 1;

        //    return true;
        //}

        //private static void AddShoppingLists()
        //{
        //    List<ShoppingList> shoppingLists1 = new List<ShoppingList>
        //    {
        //        new ShoppingList {
        //            Name = "Holidays",
        //            Id = 1,
        //            TimeOfCreation = DateTime.Now,
        //            TotalItemQTY =  _shoppingItems.Select(i => i.ItemQuantity).Sum(),
        //            TotalPrice = _shoppingItems.Select(i => Convert.ToDecimal(i.Price)).Sum(),
        //            TotalItemCount = _shoppingItems.Count,
        //            Store = _stores.First(),
        //            Items = _shoppingItems },
        //        new ShoppingList {
        //            Name = "Thanks Giving",
        //            Id = 2,
        //            TimeOfCreation = DateTime.Now,
        //            TotalItemQTY = _shoppingItems.Select(i => i.ItemQuantity).Sum(),
        //            TotalPrice = _shoppingItems.Select(i => Convert.ToDecimal(i.Price)).Sum(),
        //            TotalItemCount = _shoppingItems.Count,
        //            Store = _stores.First(),
        //            Items = _shoppingItems },
        //        new ShoppingList {
        //            Name = "Christmas",
        //            Id = 3,
        //            TimeOfCreation = DateTime.Now,
        //            TotalItemQTY = _shoppingItems.Select(i => i.ItemQuantity).Sum(),
        //            TotalPrice = _shoppingItems.Select(i => Convert.ToDecimal(i.Price)).Sum(),
        //            TotalItemCount = _shoppingItems.Count,
        //            Store = _stores.First(),
        //            Items = _shoppingItems },
        //        new ShoppingList {
        //            Name = "Weekly",
        //            Id = 4,
        //            TimeOfCreation = DateTime.Now,
        //            TotalItemQTY = _shoppingItems.Select(i => i.ItemQuantity).Sum(),
        //            TotalPrice = _shoppingItems.Select(i => Convert.ToDecimal(i.Price)).Sum(),
        //            TotalItemCount = _shoppingItems.Count,
        //            Store = _stores.First(),
        //            Items = _shoppingItems }

        //    };

        //    List<ShoppingList> shoppingLists2 = new List<ShoppingList>
        //    {
        //        new ShoppingList {
        //            Name = "Holidays",
        //            Id = 1,
        //            TimeOfCreation = DateTime.Now,
        //            TotalItemQTY =  _shoppingItems.Select(i => i.ItemQuantity).Sum(),
        //            TotalPrice = _shoppingItems.Select(i => Convert.ToDecimal(i.Price)).Sum(),
        //            TotalItemCount = _shoppingItems.Count,
        //            Store = _stores.First(),
        //            Items = _shoppingItems },
        //        new ShoppingList {
        //            Name = "Thanks Giving",
        //            Id = 2,
        //            TimeOfCreation = DateTime.Now,
        //            TotalItemQTY = _shoppingItems.Select(i => i.ItemQuantity).Sum(),
        //            TotalPrice = _shoppingItems.Select(i => Convert.ToDecimal(i.Price)).Sum(),
        //            TotalItemCount = _shoppingItems.Count,
        //            Store = _stores.First(),
        //            Items = _shoppingItems },
        //        new ShoppingList {
        //            Name = "Christmas",
        //            Id = 3,
        //            TimeOfCreation = DateTime.Now,
        //            TotalItemQTY = _shoppingItems.Select(i => i.ItemQuantity).Sum(),
        //            TotalPrice = _shoppingItems.Select(i => Convert.ToDecimal(i.Price)).Sum(),
        //            TotalItemCount = _shoppingItems.Count,
        //            Store = _stores.First(),
        //            Items = _shoppingItems },
        //        new ShoppingList {
        //            Name = "Weekly",
        //            Id = 4,
        //            TimeOfCreation = DateTime.Now,
        //            TotalItemQTY = _shoppingItems.Select(i => i.ItemQuantity).Sum(),
        //            TotalPrice = _shoppingItems.Select(i => Convert.ToDecimal(i.Price)).Sum(),
        //            TotalItemCount = _shoppingItems.Count,
        //            Store = _stores.First(),
        //            Items = _shoppingItems }

        //    };



        //    foreach (ShoppingList list in shoppingLists1)
        //    {
        //        _user1ListofShoppingLists._shoppingLists.Add(list);

        //    }

        //    foreach (ShoppingList list in shoppingLists2)
        //    {
        //        _user2ListofShoppingLists._shoppingLists.Add(list);
        //    }
        //}

        //private static void AddAddresses()
        //{
        //    List<Address> addresses = new List<Address>
        //    {
        //        new Address
        //        {
        //            Id = 1,
        //            street = "1041 SE 1st Ave",
        //            city = "Canby",
        //            state = "Oregon",
        //            zip = 97013,
        //            longitude = "-122.6767",
        //            latitude = "45.2664"
        //        },
        //        new Address
        //        {
        //            Id = 2,
        //            street = "1051 SW 1st Ave",
        //            city = "Canby",
        //            state = "Oregon",
        //            zip = 97013,
        //            longitude = "-122.7026",
        //            latitude = "45.2554"
        //        }
        //    };
        //    foreach (Address address in addresses)
        //    {
        //        _addresses.Add(address);
        //    }
        //}

        //private static void AddStores()
        //{
        //    List<Store> stores = new List<Store>
        //    {
        //        new Store
        //        {
        //            Id = 1,
        //            Address = _addresses.FirstOrDefault(a => a.Id == 1),
        //            Name = "Fred Meyer",
        //            PhoneNumber = "5032634100",
        //            Website = "www.fredmeyer.com"
        //        },
        //        new Store
        //        {
        //            Id = 2,
        //            Address = _addresses.FirstOrDefault(a => a.Id == 2),
        //            Name = "Safeway",
        //            PhoneNumber = "5032665890",
        //            Website = "www.safeway.com"
        //        }
        //    };
        //    foreach (Store store in stores)
        //    {
        //        _stores.Add(store);
        //    }

        //}

        //private static void AddShoppingUsers()
        //{
        //    List<ShoppingUser> shoppingUsers = new List<ShoppingUser>
        //    {
        //        new ShoppingUser
        //        {
        //            Email = "ShoppingUser1@gmail.com",
        //            FullName = "Shopping User One",
        //            Token = "useronetoken",
        //            Role = "Shopping",
        //            HomeStore = _stores.FirstOrDefault(s => s.Id == 1),
        //            ShoppingLists = _user1ListofShoppingLists
        //        },
        //        new ShoppingUser
        //        {
        //            Email = "ShoppingUser2@gmail.com",
        //            FullName = "Shopping User Two",
        //            Token = "usertwotoken",
        //            Role = "Shopping",
        //            HomeStore = _stores.FirstOrDefault(s => s.Id == 2),
        //            ShoppingLists = _user2ListofShoppingLists
        //        }
        //    };

        //    foreach (ShoppingUser user in shoppingUsers)
        //    {
        //        _shoppingUsers.Add(user);
        //    }

        //}

        //private static void AddShoppingItems()
        //{
        //    List<ShoppingItem> items = new List<ShoppingItem>
        //    {
        //        new ShoppingItem
        //        {
        //            Id = 1,
        //            Image = "none",
        //            Name = "test item 1",
        //            Price = 2.99,
        //            InStock = true,
        //            StockAmount = 100,
        //            Aisle = "1",
        //            Section = "test section",
        //            ItemQuantity = 1
        //        },
        //        new ShoppingItem
        //        {
        //            Id = 2,
        //            Image = "none",
        //            Name = "test item 2",
        //            Price = 2.99,
        //            InStock = true,
        //            StockAmount = 100,
        //            Aisle = "2",
        //            Section = "test section",
        //            ItemQuantity = 1
        //        },
        //        new ShoppingItem
        //        {
        //            Id = 3,
        //            Image = "none",
        //            Name = "test item 3",
        //            Price = 2.99,
        //            InStock = true,
        //            StockAmount = 100,
        //            Aisle = "1",
        //            Section = "test section",
        //            ItemQuantity = 1
        //        },
        //        new ShoppingItem
        //        {
        //            Id = 4,
        //            Image = "none",
        //            Name = "test item 4",
        //            Price = 2.99,
        //            InStock = true,
        //            StockAmount = 100,
        //            Aisle = "3",
        //            Section = "test section",
        //            ItemQuantity = 1
        //        },

        //    };

        //    foreach (ShoppingItem i in items)
        //    {
        //        _shoppingItems.Add(i);
        //    }
        //}

    }
}
