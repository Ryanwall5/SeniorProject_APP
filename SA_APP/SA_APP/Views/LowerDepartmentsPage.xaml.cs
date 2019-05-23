using SA_APP.Models;
using SA_APP.Models.FromApi;
using SA_APP.Repository;
using SA_APP.ViewModels;
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
    public partial class LowerDepartmentsPage : TabbedPage
    {
        private LowerDepartmentsViewModel _lowerDepartmentsViewModel;
        private Dictionary<string, GridUnitType> _gridLengths = new Dictionary<string, GridUnitType>
        {
            { "*", GridUnitType.Star },
            { "Auto", GridUnitType.Auto }
        };

        // departmentId -> Button or Label
        private Dictionary<int, Button> _lowerDepartmentButtons = new Dictionary<int, Button>();
        private Dictionary<int, Label> _lowerDepartmentLabels = new Dictionary<int, Label>();
        private Dictionary<int, Color> _lowerDepartmentColors = new Dictionary<int, Color>();


        public LowerDepartmentsPage()
        {

        }
        public LowerDepartmentsPage(ShoppingUserAPP shoppingUserAPP, Department department, CheckedShoppingList shoppingList = null)
        {
            InitializeComponent();
            _lowerDepartmentsViewModel = new LowerDepartmentsViewModel(shoppingUserAPP, department, Navigation, shoppingList);
            this.BindingContext = _lowerDepartmentsViewModel;
        }

        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //    var button = (Button)sender;
        //    var lowerDepartmentId = int.Parse(button.CommandParameter.ToString());
        //    LowerDepartment lowerDepartment = _department.LowerDepartments.FirstOrDefault(ld => ld.Id == lowerDepartmentId);

        //    Navigation.PushAsync(new AislesPage(_shoppingUserAPP, lowerDepartment));
        //}

        protected override void OnDisappearing()
        {
            this.Children.Clear();
            _lowerDepartmentButtons.Clear();
            _lowerDepartmentLabels.Clear();
            _lowerDepartmentColors.Clear();
            base.OnDisappearing();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Send(this, "Landscape");
            ContentPage storeMapContent = new ContentPage { Title = _lowerDepartmentsViewModel._department.Name + " Department" };

            #region Grid Definitions

            //RowSpacing="0" ColumnSpacing="0"
            Grid lowerDepartmentGrid = new Grid
            {
                RowSpacing = 20,
                ColumnSpacing = 20,
                BackgroundColor = Color.LightGray
            };

            foreach (string part in _lowerDepartmentsViewModel._department.ColumnsDefinitions.Split(','))
            {
                if (_gridLengths.ContainsKey(part))
                {
                    lowerDepartmentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, _gridLengths[part]) });
                }
                else
                {
                    lowerDepartmentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(Convert.ToDouble(part), GridUnitType.Absolute) });
                }
            }

            foreach (string part in _lowerDepartmentsViewModel._department.RowDefinitions.Split(','))
            {

                if (_gridLengths.ContainsKey(part))
                {
                    lowerDepartmentGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, _gridLengths[part]) });
                }
                else
                {
                    lowerDepartmentGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(Convert.ToDouble(part), GridUnitType.Absolute) });
                }
            }

            #endregion
            //TODO: FIX THIS PART WITH MAP LOCATIONS
            //for (int i = 0; i < _lowerDepartmentsViewModel._department.Rows; i++)
            //{
            //    lowerDepartmentGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star)});
            //}

            //for (int i = 0; i < _lowerDepartmentsViewModel._department.Columns; i++)
            //{
            //    lowerDepartmentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            //}

            foreach (LowerDepartment ld in _lowerDepartmentsViewModel._department.LowerDepartments)
            {

                //< Button Clicked = "Button_Clicked" Padding = "0" CommandParameter = "1" Grid.Row = "0" Grid.RowSpan = "1" Grid.Column = "0" Grid.ColumnSpan = "1" />

                //< Label Text = "Canned/Pasta" FontAttributes = "Bold"
                //        TextColor = "Red" HorizontalTextAlignment = "Center"
                //        VerticalOptions = "CenterAndExpand"
                //        InputTransparent = "True"
                //Grid.Row = "0" Grid.RowSpan = "1" Grid.Column = "0" Grid.ColumnSpan = "1" />

                Button button = new Button
                {
                    Padding = 0,
                    CommandParameter = ld.Id,
                    Command = _lowerDepartmentsViewModel.LowerDepartmentClickedCommand,
                    BackgroundColor = Color.Gray
                };
                lowerDepartmentGrid.Children.Add(button, ld.Column, ld.Row);
                _lowerDepartmentButtons.Add(ld.Id, button);
                _lowerDepartmentColors.Add(ld.Id, Color.Gray);

                Label label = new Label
                {
                    Text = ld.Name,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.Black,
                    HorizontalTextAlignment = TextAlignment.Center,
                    InputTransparent = true,
                    VerticalOptions = LayoutOptions.CenterAndExpand
                };
                lowerDepartmentGrid.Children.Add(label, ld.Column, ld.Row);
                _lowerDepartmentLabels.Add(ld.Id, label);

            }

            int buttonColor = 0;
            if (_lowerDepartmentsViewModel._shoppingList != null)
            {
                foreach (CheckedItem item in _lowerDepartmentsViewModel._shoppingList.CheckedShoppingItems)
                {
                    if (_lowerDepartmentButtons.ContainsKey(item.lowerDepartmentId))
                    {
                        if (_lowerDepartmentButtons[item.lowerDepartmentId].BackgroundColor == Color.Gray)
                        {
                            var button = _lowerDepartmentButtons[item.lowerDepartmentId];
                            button.BackgroundColor = InMemoryDatabase.colorsByInt[buttonColor];

                            var Label = _lowerDepartmentButtons[item.lowerDepartmentId];
                            Label.TextColor = Color.White;
                            buttonColor++;
                        }

                        item.FrameBackGroundColor = _lowerDepartmentButtons[item.lowerDepartmentId].BackgroundColor;
                        item.LabelColor = Color.White;
                    }
                }
            }

            storeMapContent.Content = lowerDepartmentGrid;
            this.Children.Add(storeMapContent);

            if (_lowerDepartmentsViewModel._shoppingList != null)
            {
                var shoppingListItemsDataTemplate = new DataTemplate(() =>
                {
                    //SwipeGestureRecognizer swipeRightGestureRecognizer = new SwipeGestureRecognizer
                    //{
                    //    Direction = SwipeDirection.Right,
                    //    Command = OnItemSwipedRightCommand
                    //};
                    //swipeRightGestureRecognizer.SetBinding(SwipeGestureRecognizer.CommandParameterProperty, ".");


                    //SwipeGestureRecognizer swipeLeftGestureRecognizer = new SwipeGestureRecognizer
                    //{
                    //    Direction = SwipeDirection.Left,
                    //    Command = OnItemSwipedLeftCommand,
                    //};
                    //swipeLeftGestureRecognizer.SetBinding(SwipeGestureRecognizer.CommandParameterProperty, ".");

                    Frame listItemFrame = new Frame
                    {
                        Padding = 2,
                        Margin = new Thickness(0, 3, 0, 3),
                        HasShadow = true
                    };
                    listItemFrame.SetBinding(BackgroundColorProperty, "FrameBackGroundColor");
                    //listItemFrame.GestureRecognizers.Add(swipeRightGestureRecognizer);
                    //listItemFrame.GestureRecognizers.Add(swipeLeftGestureRecognizer);

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

                    Span lowerDeptSpan1 = new Span { Text = "Lower Department: " };
                    Span bindLowerDeptSpan1 = new Span();
                    bindLowerDeptSpan1.SetBinding(Span.TextProperty, "lowerDepartment");
                    FormattedString formattedStringld1 = new FormattedString();
                    formattedStringld1.Spans.Add(lowerDeptSpan1);
                    formattedStringld1.Spans.Add(bindLowerDeptSpan1);
                    Label lowerDepartmentLabel = new Label
                    {
                        VerticalOptions = LayoutOptions.Center,
                        FontSize = 12,
                        FormattedText = formattedStringld1,
                        BackgroundColor = Color.Gold,
                        TextColor = Color.Red,
                        HorizontalOptions = LayoutOptions.Start
                    };
                    shoppingListItemsGrid.Children.Add(lowerDepartmentLabel, 1, 1);

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
                    ItemsSource = _lowerDepartmentsViewModel._shoppingList.CheckedShoppingItems
                };
                listView.ItemTapped += ListView_ItemTapped; ;
                ContentPage shoppingListContent = new ContentPage { Title = _lowerDepartmentsViewModel._shoppingList.Name + " Shopping List" };

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
            var ldId = selectedItem.lowerDepartmentId;
            var action = await DisplayActionSheet("Got Item?", "Cancel", "Remove");

            if (action == "Remove")
            {
                _lowerDepartmentsViewModel._shoppingList.CheckedShoppingItems.Remove(selectedItem);

                if (_lowerDepartmentsViewModel._shoppingList.CheckedShoppingItems.FirstOrDefault(i => i.lowerDepartmentId == ldId) == null)
                {
                    _lowerDepartmentButtons[ldId].BackgroundColor = Color.Gray;
                    _lowerDepartmentLabels[ldId].TextColor = Color.Black;
                }
            }
        }

        protected override bool OnBackButtonPressed()
        {
            RemoveModal();
            return true;
        }

        private async void RemoveModal()
        {
            await Navigation.PopModalAsync();
        }
    }
}