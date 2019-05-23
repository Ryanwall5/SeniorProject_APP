using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SA_APP.Models.FromApi;
using SA_APP.Models;
using SA_APP.Repository;

namespace SA_APP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreMapPage : TabbedPage
    {
        private ShoppingUserAPP _shoppingUserAPP;
        private StoreMap _storeMap;
        private CheckedShoppingList _shoppingList;
        private Command OnDepartmentClicked => new Command<int>(OnDeptClicked);

        // Button and Label dictionary that point to linkId in the _shoppingList.CheckedItems
        private Dictionary<int, Button> _departmentButtons = new Dictionary<int, Button>();
        private Dictionary<int, Label> _departmentLabels = new Dictionary<int, Label>();

        private Dictionary<int, Color> _departmentColors = new Dictionary<int, Color>();
        private Dictionary<int, Frame> _listViewFrames = new Dictionary<int, Frame>();


        private Dictionary<string, GridUnitType> _gridLengths = new Dictionary<string, GridUnitType>
        {
            { "*", GridUnitType.Star },
            { "Auto", GridUnitType.Auto }
        };

        public StoreMapPage()
        {

        }
        public StoreMapPage(ShoppingUserAPP shoppingUser, StoreMap storeMap)
        {
            InitializeComponent();
            _shoppingUserAPP = shoppingUser;
            _storeMap = storeMap;
            _shoppingList = null;
        }
        public StoreMapPage(ShoppingUserAPP shoppingUser, StoreMap storeMap, CheckedShoppingList shoppingList)
        {
            InitializeComponent();
            _shoppingUserAPP = shoppingUser;
            _storeMap = storeMap;
            _shoppingList = shoppingList;
        }

        ////during page close setting back to portrait
        protected override void OnDisappearing()
        {
            this.Children.Clear();
            _departmentButtons.Clear();
            _departmentLabels.Clear();
            _departmentColors.Clear();
            base.OnDisappearing();
        }

        private async void OnDeptClicked(int departmentId)
        {
            Department department = _storeMap.Departments.FirstOrDefault(d => d.Id == departmentId);
            await Navigation.PushModalAsync(new LowerDepartmentsPage(_shoppingUserAPP, department, _shoppingList));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Send(this, "Landscape");
            ContentPage storeMapContent = new ContentPage { Title = _shoppingUserAPP.homeStore.name + " Map" };

            #region Grid Definitions

            //RowSpacing="0" ColumnSpacing="0"
            Grid storeGrid = new Grid
            {
                RowSpacing = 0,
                ColumnSpacing = 0,
                BackgroundColor = Color.LightGray
            };

            foreach (string part in _storeMap.ColumnsDefinitions.Split(','))
            {
                if (_gridLengths.ContainsKey(part))
                {
                    storeGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, _gridLengths[part]) });
                }
                else
                {
                    storeGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(Convert.ToDouble(part), GridUnitType.Absolute) });
                }
            }

            foreach (string part in _storeMap.RowDefinitions.Split(','))
            {

                if (_gridLengths.ContainsKey(part))
                {
                    storeGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, _gridLengths[part]) });
                }
                else
                {
                    storeGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(Convert.ToDouble(part), GridUnitType.Absolute) });
                }
            }

            #endregion

            /*
             * 
             *  <Button Clicked="Button_Clicked" BackgroundColor="Green" CommandParameter="1" Grid.Row="0" Grid.RowSpan="1" Grid.Column="0" Grid.ColumnSpan="2"/>
                <Label Text="Paint/Kitchen/bathroom" FontAttributes="Bold" 
                       TextColor="Red" HorizontalTextAlignment="Center" 
                       VerticalOptions="CenterAndExpand" 
                       InputTransparent="True" Grid.Row="0" Grid.RowSpan="1" Grid.Column="0" Grid.ColumnSpan="2"
                        />
             */
            foreach (Department department in _storeMap.Departments)
            {
                Button departmentBtn = new Button
                {
                    Command = OnDepartmentClicked,
                    CommandParameter = department.Id,
                    Margin = 3,
                    BackgroundColor = Color.Gray
                };
                _departmentButtons.Add(department.Id, departmentBtn);
                _departmentColors.Add(department.Id, Color.Gray);

                if (department.ColumnsDefinitions == "" && department.RowDefinitions == "")
                {
                    departmentBtn.IsEnabled = false;
                }

                Label departmentLabel = new Label
                {
                    Text = department.Name,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    InputTransparent = true,
                    TextColor = Color.Black
                };
                _departmentLabels.Add(department.Id, departmentLabel);

                // row,column,rowspan,columnspan
                var mapLocations = department.MapLocation.Split(',');
                int row = Convert.ToInt32(mapLocations[0]);
                int column = Convert.ToInt32(mapLocations[1]);
                int rowSpan = Convert.ToInt32(mapLocations[2]);
                int columnSpan = Convert.ToInt32(mapLocations[3]);

                storeGrid.Children.Add(departmentBtn, column, row);
                Grid.SetRowSpan(departmentBtn, rowSpan);
                Grid.SetColumnSpan(departmentBtn, columnSpan);

                storeGrid.Children.Add(departmentLabel, column, row);
                Grid.SetRowSpan(departmentLabel, rowSpan);
                Grid.SetColumnSpan(departmentLabel, columnSpan);
            }

            int buttonColor = 0;
            if (_shoppingList != null)
            {
                foreach (CheckedItem item in _shoppingList.CheckedShoppingItems)
                {
                    if (_departmentButtons.ContainsKey(item.departmentId))
                    {
                        if (_departmentButtons[item.departmentId].BackgroundColor == Color.Gray)
                        {
                            var button = _departmentButtons[item.departmentId];
                            button.BackgroundColor = InMemoryDatabase.colorsByInt[buttonColor];

                            var Label = _departmentLabels[item.departmentId];
                            Label.TextColor = Color.White;
                            buttonColor++;
                        }

                        item.FrameBackGroundColor = _departmentButtons[item.departmentId].BackgroundColor;
                        item.LabelColor = Color.White;
                    }
                }
            }


            storeMapContent.Content = storeGrid;
            this.Children.Add(storeMapContent);

            if (_shoppingList != null)
            {
                var shoppingListItemsDataTemplate = new DataTemplate(() =>
                {
                    Frame listItemFrame = new Frame
                    {
                        Padding = 2,
                        Margin = new Thickness(0, 3, 0, 3),
                        HasShadow = true
                    };
                    listItemFrame.SetBinding(BackgroundColorProperty, "FrameBackGroundColor");

                    Grid shoppingListItemsGrid = new Grid
                    {
                        Padding = 0,
                        RowSpacing = 0,
                        ColumnSpacing = 0,
                        Margin = 0
                    };

                    shoppingListItemsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                    shoppingListItemsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });

                    shoppingListItemsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                    shoppingListItemsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    shoppingListItemsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                    Image itemImage = new Image
                    {
                        WidthRequest = 40,
                        HeightRequest = 46,
                    };
                    itemImage.SetBinding(Image.SourceProperty, "image");
                    shoppingListItemsGrid.Children.Add(itemImage, 0, 0);
                    Grid.SetRowSpan(itemImage, 2);

                    Label itemNameLabel = new Label
                    {
                        LineBreakMode = LineBreakMode.TailTruncation,
                        VerticalOptions = LayoutOptions.Center,
                        FontSize = 12,

                    };
                    itemNameLabel.SetBinding(Label.TextProperty, "name");
                    itemNameLabel.SetBinding(Label.TextColorProperty, "LabelColor");
                    // column, row, columnspan, rowspan
                    shoppingListItemsGrid.Children.Add(itemNameLabel, 1, 0);
                    Grid.SetColumnSpan(itemNameLabel, 2);

                    Span departmentSpan1 = new Span { Text = "Department: " };
                    Span bindDepartmentSpan1 = new Span();
                    bindDepartmentSpan1.SetBinding(Span.TextProperty, "department");
                    FormattedString formattedStringD1 = new FormattedString();
                    formattedStringD1.Spans.Add(departmentSpan1);
                    formattedStringD1.Spans.Add(bindDepartmentSpan1);
                    Label itemDeptLabel = new Label
                    {
                        VerticalOptions = LayoutOptions.Center,
                        FontSize = 12,
                        FormattedText = formattedStringD1,
                        BackgroundColor = Color.Gold,
                        TextColor = Color.Red,
                        HorizontalOptions = LayoutOptions.Start
                    };
                    shoppingListItemsGrid.Children.Add(itemDeptLabel, 1, 1);

                    Span qtySpan = new Span { Text = "Qty:" };
                    Span bindedQtySpan = new Span();
                    bindedQtySpan.SetBinding(Span.TextProperty, "itemQuantity");
                    FormattedString formattedStringQty = new FormattedString();
                    formattedStringQty.Spans.Add(qtySpan);
                    formattedStringQty.Spans.Add(bindedQtySpan);
                    Label itemQtyLabel = new Label
                    {
                        VerticalOptions = LayoutOptions.Center,
                        FontSize = 12,
                        FormattedText = formattedStringQty,
                        BackgroundColor = Color.LightGray,
                        TextColor = Color.Red,
                        HorizontalOptions = LayoutOptions.Start
                    };
                    shoppingListItemsGrid.Children.Add(itemQtyLabel, 2, 1);

                    listItemFrame.Content = shoppingListItemsGrid;
                    ViewCell viewCell = new ViewCell
                    {
                        View = listItemFrame
                    };

                    return viewCell;
                });

                ListView listView = new ListView
                {
                    ItemTemplate = shoppingListItemsDataTemplate,
                    HasUnevenRows = true,
                    ItemsSource = _shoppingList.CheckedShoppingItems
                };
                listView.ItemTapped += ListView_ItemTapped;

                ContentPage shoppingListContent = new ContentPage { Title = _shoppingList.Name + " Shopping List" };

                StackLayout stackLayout = new StackLayout();
                stackLayout.Children.Add(listView);

                shoppingListContent.Content = stackLayout;
                this.Children.Add(shoppingListContent);
            }
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            //Deselect Item
            ((ListView)sender).SelectedItem = null;

            var selectedItem = (CheckedItem)e.Item;
            var departmentId = selectedItem.departmentId;
            var action = await DisplayActionSheet("Got Item?", "Cancel", "Remove");

            if (action == "Remove")
            {
                _shoppingList.CheckedShoppingItems.Remove(selectedItem);

                if (_shoppingList.CheckedShoppingItems.FirstOrDefault(i => i.departmentId == departmentId) == null)
                {
                    _departmentButtons[departmentId].BackgroundColor = Color.Gray;
                    _departmentLabels[departmentId].TextColor = Color.Black;
                }
            }

            //_usersShoppingListsViewModel.OnViewItemCommand.Execute((CheckedItem)e.Item);
        }

        protected override bool OnBackButtonPressed()
        {
            MessagingCenter.Send(this, "Unspecified");
            RemoveModal();
            return true;
        }

        private async void RemoveModal()
        {
            await Navigation.PopModalAsync();
        }

    }
}