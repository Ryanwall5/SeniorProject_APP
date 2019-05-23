using SA_APP.Models;
using SA_APP.Models.FromApi;
using SA_APP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace SA_APP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AislesPage : TabbedPage
    {
        private AislesViewModel _aislesViewModel;
        // departmentId -> Button or Label
        private Dictionary<int, Button> _aisleButtons = new Dictionary<int, Button>();

        public AislesPage()
        {

        }
        public AislesPage(ShoppingUserAPP shoppingUserAPP, LowerDepartment lowerDepartment, CheckedShoppingList shoppingList = null)
        {
            InitializeComponent();
            _aislesViewModel = new AislesViewModel(shoppingUserAPP, lowerDepartment, Navigation, shoppingList);
        }
        //during page close setting back to portrait
        protected override void OnDisappearing()
        {
            this.Children.Clear();
            _aisleButtons.Clear();
            base.OnDisappearing();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Send(this, "Landscape");

            ContentPage storeMapContent = new ContentPage { Title = _aislesViewModel._lowerDepartment.Name + " Lower Dept." };

            #region Grid Definitions

            //RowSpacing="0" ColumnSpacing="0"
            Grid aislesGrid = new Grid
            {
                RowSpacing = 0,
                ColumnSpacing = 0,
                BackgroundColor = Color.LightGray
            };

            aislesGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });
            aislesGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            aislesGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });

            int totalColumns = _aislesViewModel._lowerDepartment.AisleCount + _aislesViewModel._lowerDepartment.Aisles.Count + (_aislesViewModel._lowerDepartment.AisleCount - 1);

            for (int i = 0; i < _aislesViewModel._lowerDepartment.AisleCount; i++)
            {
                aislesGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });
                aislesGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                aislesGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(5) });
                aislesGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            aislesGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });

            #endregion

            int count = 0;
            int aisleCount = _aislesViewModel._lowerDepartment.AisleStart;
            bool buttonDivider = false;
            var aisles = _aislesViewModel._lowerDepartment.Aisles;

            Label label;
            int j = 0;

            for (j = 0; j < totalColumns; j++)
            {
                if (j % 4 == 0)
                {
                    label = new Label
                    {
                        Text = aisleCount.ToString(),
                        FontAttributes = FontAttributes.Bold,
                        FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                        TextColor = Color.SteelBlue,
                        HorizontalTextAlignment = TextAlignment.Center,
                        InputTransparent = true,
                        VerticalTextAlignment = TextAlignment.Center
                    };
                    aislesGrid.Children.Add(label, j, 1);
                    aisleCount++;
                    continue;
                }

                if (buttonDivider && ((j % 2) == 0))
                {
                    BoxView bv = new BoxView
                    {
                        BackgroundColor = Color.Black
                    };

                    aislesGrid.Children.Add(bv, j, 1);
                    buttonDivider = false;
                }
                else
                {
                    Aisle aisle1 = aisles[count];
                    Button button1 = new Button
                    {
                        Padding = 0,
                        CommandParameter = aisle1.Id,
                        BackgroundColor = Color.Gray,
                        Command = _aislesViewModel.AisleClickedCommand,
                    };

                    aislesGrid.Children.Add(button1, aisle1.Column, aisle1.Row);
                    _aisleButtons.Add(aisle1.Id, button1);
                    buttonDivider = true;
                    count++;
                }
            }


            #region ADD THE LAST BUTTON AND LABEL FOR THE LAST AISLE IF THE AISLE COUNT IS EVEN IF ODD DONT ADD
            Aisle aisle2 = null;

            if (count % 2 != 0)
            {
                aisle2 = aisles[count];
            }

           
            if(aisle2 != null)
            {
                Button button2 = new Button
                {
                    Padding = 0,
                    CommandParameter = 1,
                    BackgroundColor = Color.Gray,
                    Command = _aislesViewModel.AisleClickedCommand,
                };

                aislesGrid.Children.Add(button2, aisle2.Column, aisle2.Row);
                _aisleButtons.Add(aisle2.Id, button2);


                j++;
                label = new Label
                {
                    Text = aisleCount.ToString(),
                    FontAttributes = FontAttributes.Bold,
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                    TextColor = Color.SteelBlue,
                    HorizontalTextAlignment = TextAlignment.Center,
                    InputTransparent = true,
                    VerticalTextAlignment = TextAlignment.Center
                };
                aislesGrid.Children.Add(label, j, 1);
            }

            #endregion

            if (_aislesViewModel._shoppingList != null)
            {
                foreach (CheckedItem item in _aislesViewModel._shoppingList.CheckedShoppingItems)
                {
                    if (_aisleButtons.ContainsKey(item.lowerDepartmentId))
                    {
                        var button = _aisleButtons[item.lowerDepartmentId];
                        button.BackgroundColor = Color.Green;
                    }
                }
            }

            storeMapContent.Content = aislesGrid;
            this.Children.Add(storeMapContent);

            if (_aislesViewModel._shoppingList != null)
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
                        BackgroundColor = Color.Green,
                        HasShadow = true
                    };

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
                        TextColor = Color.White,
                        FontSize = 12,
                    };
                    itemNameLabel.SetBinding(Label.TextProperty, "name");
                    // column, row, columnspan, rowspan
                    shoppingListItemsGrid.Children.Add(itemNameLabel, 1, 0);
                    Grid.SetColumnSpan(itemNameLabel, 2);

                    Span aisleSpan1 = new Span { Text = "Aisle: " };
                    Span bindAisleSpan1 = new Span();
                    bindAisleSpan1.SetBinding(Span.TextProperty, "aisle");
                    FormattedString formattedStringaisle1 = new FormattedString();
                    formattedStringaisle1.Spans.Add(aisleSpan1);
                    formattedStringaisle1.Spans.Add(bindAisleSpan1);
                    Label aisleLabel = new Label
                    {
                        VerticalOptions = LayoutOptions.Center,
                        FontSize = 12,
                        FormattedText = formattedStringaisle1,
                        BackgroundColor = Color.Gold,
                        TextColor = Color.Red,
                        HorizontalOptions = LayoutOptions.Center
                    };
                    shoppingListItemsGrid.Children.Add(aisleLabel, 1, 1);
                    Grid.SetColumnSpan(aisleLabel, 2);

                    //Span qtySpan = new Span { Text = "Qty:" };
                    //Span bindedQtySpan = new Span();
                    //bindedQtySpan.SetBinding(Span.TextProperty, "itemQuantity");
                    //FormattedString formattedStringQty = new FormattedString();
                    //formattedStringQty.Spans.Add(qtySpan);
                    //formattedStringQty.Spans.Add(bindedQtySpan);
                    //Label itemQtyLabel = new Label
                    //{
                    //    VerticalOptions = LayoutOptions.Center,
                    //    FontSize = 12,
                    //    FormattedText = formattedStringQty,
                    //    BackgroundColor = Color.LightGray,
                    //    TextColor = Color.Red,
                    //    HorizontalOptions = LayoutOptions.Center
                    //};
                    //shoppingListItemsGrid.Children.Add(itemQtyLabel, 2, 1);

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
                    ItemsSource = _aislesViewModel._shoppingList.CheckedShoppingItems
                };
                listView.ItemTapped += ListView_ItemTapped;
                ContentPage shoppingListContent = new ContentPage { Title = _aislesViewModel._shoppingList.Name + " Shopping List" };

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
            var aisleId = selectedItem.aisleId;
            var action = await DisplayActionSheet("Got Item?", "Cancel", "Remove");

            if (action == "Remove")
            {
                _aislesViewModel._shoppingList.CheckedShoppingItems.Remove(selectedItem);

                if (_aislesViewModel._shoppingList.CheckedShoppingItems.FirstOrDefault(i => i.aisleId == aisleId) == null)
                {
                    _aisleButtons[aisleId].BackgroundColor = Color.Gray;
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