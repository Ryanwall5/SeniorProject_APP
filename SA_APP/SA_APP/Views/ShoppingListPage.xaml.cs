using Rg.Plugins.Popup.Services;
using SA_APP.Models.FromApi;
using SA_APP.Repository;
using SA_APP.Services;
using SA_APP.ViewModels;
using System;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SA_APP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    //public partial class ShoppingListPage : ContentPage
    public partial class ShoppingListPage : CarouselPage
    {

        private UsersShoppingListsViewModel _usersShoppingListsViewModel;

        public ICommand OnDeleteListCommand => new Command<int>(OnDeleteList);

        public ICommand OnCreateNewCommand => new Command(OnCreateNew);

        private ShoppingUserAPP _shoppingUser;

        //public ShoppingListPage()
        //{
        //    InitializeComponent();
        //    _usersShoppingListsViewModel = new UsersShoppingListsViewModel();
        //    this.BindingContext = _usersShoppingListsViewModel;
        //}

        public ShoppingListPage(ShoppingUserAPP shoppingUser)
        {
            try
            {
                InitializeComponent();
                _shoppingUser = shoppingUser;
                _usersShoppingListsViewModel = new UsersShoppingListsViewModel(_shoppingUser, Navigation);

                //addItemIcon.GestureRecognizers.Add(new TapGestureRecognizer
                //{
                //    Command = new Command(() => { PopupNavigation.Instance.PushAsync(new AddShoppingListPage(_shoppingUser)); }),
                //    NumberOfTapsRequired = 1
                //});


                this.BindingContext = _usersShoppingListsViewModel;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        //async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        //{
        //    if (e.Item == null)
        //        return;

        //    //Deselect Item
        //    ((ListView)sender).SelectedItem = null;
        //    var list = (ShoppingListAPP)e.Item;
        //    await Navigation.PushModalAsync(new ViewShoppingListPage(_shoppingUser, list.id));
        //}


        private void OnDeleteList(int listId)
        {
            var listDeleted = _usersShoppingListsViewModel.OnDeleteList(listId);
        }

        private void OnCreateNew()
        {          
             _usersShoppingListsViewModel.OnCreateNew();
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PopModalAsync();
            return base.OnBackButtonPressed();
        }



        protected override void OnAppearing()
        {
            base.OnAppearing();

            //items1.ItemsSource = InMemoryDatabase._items.Take(6);
            //items2.ItemsSource = InMemoryDatabase._items.Take(6);

            //_usersShoppingListsViewModel.RefreshList();
            _usersShoppingListsViewModel.RefreshList();
            this.SetBinding(Page.TitleProperty, "ShoppingListsCount");

            this.ItemTemplate = new DataTemplate(() =>
            {
                StackLayout stackLayoutVertical = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Margin = 0,
                    Padding = 0,
                };

                #region Shopping list name
                /*
                <StackLayout Margin="0, 10, 0, 10">
                    <Label Text="ShoppingList 1" TextColor="SteelBlue" FontAttributes="Bold" HorizontalOptions="Center" FontSize="22" LineBreakMode="WordWrap"/>
                </StackLayout>
                 */
                StackLayout stackLayoutShoppingListName = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Margin = new Thickness(0, 10, 0, 10)
                };

                Label shoppingListNameLabel = new Label
                {
                    TextColor = Color.SteelBlue,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.Center,
                    FontSize = 22,
                    LineBreakMode = LineBreakMode.WordWrap
                };
                shoppingListNameLabel.SetBinding(Label.TextProperty, "name");
                stackLayoutShoppingListName.Children.Add(shoppingListNameLabel);

                #endregion

                #region ShoppingList Buttons

                // <Grid Padding="0" RowSpacing="0" ColumnSpacing="0" Margin="0" VerticalOptions="FillAndExpand">
                Grid buttonsGrid = new Grid
                {
                    Padding = 0,
                    RowSpacing = 0,
                    ColumnSpacing = 0,
                    Margin = 0,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };

                buttonsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });

                buttonsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                buttonsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                buttonsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                Button deleteButton = new Button
                {
                    BackgroundColor = Color.Red,
                    Text = "Delete",
                    TextColor = Color.White,
                    Command = OnDeleteListCommand
                };
                deleteButton.SetBinding(Button.CommandParameterProperty, "id");

                Button renameButton = new Button
                {
                    BackgroundColor = Color.Blue,
                    Text = "Rename",
                    TextColor = Color.White,
                    Command = _usersShoppingListsViewModel.OnRenameCommand
                };
                renameButton.SetBinding(Button.CommandParameterProperty, "id");

                Button createNewButton = new Button
                {
                    BackgroundColor = Color.Green,
                    Text = "Create new",
                    TextColor = Color.White,
                    Command = OnCreateNewCommand
                };
                createNewButton.SetBinding(Button.CommandParameterProperty, "id");
                /*                
                <Button Grid.Column="0" Grid.Row="0" BackgroundColor="Red" TextColor="White" Text="Delete"/>
                <Button Grid.Column="1" Grid.Row="0" BackgroundColor="Green" TextColor="White" Text="Create New"/>
                <Button Grid.Column="2" Grid.Row="0" BackgroundColor="Blue" TextColor="White" Text="Rename"/>
                 */
                buttonsGrid.Children.Add(createNewButton, 0, 0);
                buttonsGrid.Children.Add(deleteButton, 1, 0);
                buttonsGrid.Children.Add(renameButton, 2, 0);

                #endregion

                #region ShoppingList Information

                Grid shoppingListGrid = new Grid
                {
                    Padding = 0,
                    RowSpacing = 0,
                    ColumnSpacing = 0,
                    Margin = 0,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };

                shoppingListGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });

                shoppingListGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                shoppingListGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                shoppingListGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                #region TotalCost Section
                /*
                     <Grid Padding="0" RowSpacing="0" ColumnSpacing="0" Margin="-5" VerticalOptions="FillAndExpand">
                        <Grid.RowDefinitions>
                            <RowDefinition Height="Auto"/>
                        </Grid.RowDefinitions>
                        <Grid.ColumnDefinitions>
                            <ColumnDefinition Width="*" />
                            <ColumnDefinition Width="*"/>
                            <ColumnDefinition Width="*"/>
                        </Grid.ColumnDefinitions>
                        <Frame Grid.Column="0" Grid.Row="0" BorderColor="Red"  Padding="0">
                            <StackLayout>
                                <Label HorizontalOptions="CenterAndExpand" Text="Total Cost" FontAttributes="Bold" />
                                <Label HorizontalOptions="CenterAndExpand">
                                    <Label.FormattedText>
                                        <FormattedString>
                                            <Span Text="$" FontAttributes="Bold"/>
                                            <Span Text="{Binding ShoppingList.totalCost}" FontAttributes="Bold" />
                                        </FormattedString>
                                    </Label.FormattedText>
                                </Label>
                            </StackLayout>
                        </Frame>
                    */

                // CODE FOR TOTAL COST INFORMATION

                Frame frameTotalCost = new Frame
                {
                    BorderColor = Color.Red,
                    Padding = 0
                };

                StackLayout stackLayout1 = new StackLayout
                {
                    Orientation = StackOrientation.Vertical
                };

                Span dollarSignSpan = new Span { Text = "$", FontAttributes = FontAttributes.Bold };
                Span totalCostSpan = new Span { FontAttributes = FontAttributes.Bold };
                totalCostSpan.SetBinding(Span.TextProperty, "totalCost");
                FormattedString formattedStringCost = new FormattedString();
                formattedStringCost.Spans.Add(dollarSignSpan);
                formattedStringCost.Spans.Add(totalCostSpan);
                Label totalCost = new Label
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = "Total Cost",
                    FontAttributes = FontAttributes.Bold
                };
                Label bindedTotalCost = new Label
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    FormattedText = formattedStringCost
                };
                stackLayout1.Children.Add(totalCost);
                stackLayout1.Children.Add(bindedTotalCost);
                frameTotalCost.Content = stackLayout1;
                shoppingListGrid.Children.Add(frameTotalCost, 0, 0);

                #endregion

                #region Total Items Section

                /*
                 
                 <Frame Grid.Column="1" Grid.Row="0" BorderColor="Red" Padding="0">
                    <StackLayout>
                        <Label HorizontalOptions="CenterAndExpand" Text="Total Items" FontAttributes="Bold" />
                        <Label HorizontalOptions="CenterAndExpand" Text="{Binding TotalItems}"/>
                    </StackLayout>
                </Frame>
                 */

                // CODE FOR TOTALITEMS INFORMATION
                Frame frameTotalItems = new Frame
                {
                    BorderColor = Color.Red,
                    Padding = 0
                };

                StackLayout stackLayout2 = new StackLayout
                {
                    Orientation = StackOrientation.Vertical
                };

                Label totalItems = new Label
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = "Total Items",
                    FontAttributes = FontAttributes.Bold
                };
                Label bindedTotalItems = new Label
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    FontAttributes = FontAttributes.Bold
                };
                bindedTotalItems.SetBinding(Label.TextProperty, "totalItems");

                stackLayout2.Children.Add(totalItems);
                stackLayout2.Children.Add(bindedTotalItems);
                frameTotalItems.Content = stackLayout2;
                shoppingListGrid.Children.Add(frameTotalItems, 1, 0);

                #endregion

                #region Time of Creation Section

                /*
                 <Frame Grid.Column="2" Grid.Row="0" BorderColor="Red" Padding="0">
                    <StackLayout>
                        <Label HorizontalOptions="CenterAndExpand" Text="Created On" FontAttributes="Bold"/>
                        <Label HorizontalOptions="CenterAndExpand">
                            <Label.FormattedText>
                                <FormattedString>
                                    <Span Text="5" FontAttributes="Bold" />
                                </FormattedString>
                            </Label.FormattedText>
                        </Label>
                    </StackLayout>
                </Frame>
                 */

                Frame frameTimeOfCreation = new Frame
                {
                    BorderColor = Color.Red,
                    Padding = 0
                };

                StackLayout stackLayout3 = new StackLayout
                {
                    Orientation = StackOrientation.Vertical
                };

                Label timeOfCreation = new Label
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = "Date Created",
                    FontAttributes = FontAttributes.Bold
                };

                Label bindedTimeOfCreation = new Label
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    FontAttributes = FontAttributes.Bold
                };
                bindedTimeOfCreation.SetBinding(Label.TextProperty, "timeOfCreation");

                stackLayout3.Children.Add(timeOfCreation);
                stackLayout3.Children.Add(bindedTimeOfCreation);
                frameTimeOfCreation.Content = stackLayout3;
                shoppingListGrid.Children.Add(frameTimeOfCreation, 2, 0);

                #endregion

                #endregion

                ScrollView scrollView = new ScrollView();


                /*
                <ListView x:Name="items1">
                    <ListView.ItemTemplate>
                        <DataTemplate>
                            <ViewCell>
                                <Grid Padding="0" RowSpacing="0" ColumnSpacing="0">
                                    <Grid.RowDefinitions>
                                        <RowDefinition Height="*"/>
                                    </Grid.RowDefinitions>
                                    <Grid.ColumnDefinitions>
                                        <ColumnDefinition Width="5*" />
                                        <ColumnDefinition Width="2*"/>
                                        <ColumnDefinition Width="3*"/>
                                    </Grid.ColumnDefinitions>
                                    <Label LineBreakMode="WordWrap" Text="{Binding name}"  Grid.Column="0" FontSize="Medium" VerticalOptions="Center"/>
                                    <Label FontSize="Medium" Grid.Column="1" VerticalOptions="Center">
                                        <Label.FormattedText>
                                            <FormattedString>
                                                <Span Text="$"/>
                                                <Span Text="{Binding price}"/>
                                            </FormattedString>
                                        </Label.FormattedText>
                                    </Label>
                                    <Button Grid.Column="2" x:Name="deleteItemIcon" Image="minus_square.jpg" WidthRequest="40" BackgroundColor="White" HeightRequest="46"  HorizontalOptions="EndAndExpand" Clicked="OnDelete_Clicked" VerticalOptions="Center" CommandParameter="{Binding id}"/>

                                </Grid>
                            </ViewCell>
                        </DataTemplate>
                    </ListView.ItemTemplate>
                 </ListView>
                 */

                var shoppingListItemsDataTemplate = new DataTemplate(() =>
                {

                    SwipeGestureRecognizer swipeRight = new SwipeGestureRecognizer
                    {
                        Direction = SwipeDirection.Right,
                        Command = _usersShoppingListsViewModel.OnDeleteItemCommand,
                    };

                    swipeRight.SetBinding(SwipeGestureRecognizer.CommandParameterProperty, "linkId");

                    Frame listItemFrame = new Frame
                    {
                        Padding = 2,
                        Margin = new Thickness(0, 3, 0, 3),
                        BackgroundColor = Color.White,
                        HasShadow = true,
                        InputTransparent = true,
                    };
                    listItemFrame.GestureRecognizers.Add(swipeRight);


                    Grid shoppingListItemsGrid = new Grid
                    {
                        Padding = 0,
                        RowSpacing = 0,
                        ColumnSpacing = 0,
                        Margin = 0
                    };

                    shoppingListItemsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });
                    shoppingListItemsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });

                    shoppingListItemsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(.25, GridUnitType.Star) });
                    shoppingListItemsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(.05, GridUnitType.Star) });
                    shoppingListItemsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(.30, GridUnitType.Star) });
                    shoppingListItemsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(.30, GridUnitType.Star) });
                    shoppingListItemsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(.10, GridUnitType.Star) });

                    Image itemImage = new Image
                    {
                        WidthRequest = 30,
                        HeightRequest = 30,
                    };
                    itemImage.SetBinding(Image.SourceProperty, "image");
                    shoppingListItemsGrid.Children.Add(itemImage, 0, 0);
                    Grid.SetRowSpan(itemImage, 2);


                    Label itemNameLabel = new Label
                    {
                        LineBreakMode = LineBreakMode.TailTruncation,
                        MaxLines = 2,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        HorizontalTextAlignment = TextAlignment.Start,
                        VerticalTextAlignment = TextAlignment.Center,
                        TextColor = Color.RoyalBlue,
                        FontSize = 14,
                    };
                    itemNameLabel.SetBinding(Label.TextProperty, "name");
                    // column, row, columnspan, rowspan
                    shoppingListItemsGrid.Children.Add(itemNameLabel, 2, 0);
                    Grid.SetColumnSpan(itemNameLabel, 2);

                    Span dollarSignSpan1 = new Span { Text = "$" };
                    Span totalCostSpan1 = new Span();
                    totalCostSpan1.SetBinding(Span.TextProperty, "price");
                    FormattedString formattedStringCost1 = new FormattedString();
                    formattedStringCost1.Spans.Add(dollarSignSpan1);
                    formattedStringCost1.Spans.Add(totalCostSpan1);
                    Label itempriceLabel = new Label
                    {
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Start,
                        FontSize = 14,
                        FormattedText = formattedStringCost1,
                        BackgroundColor = Color.Gold,
                        TextColor = Color.Red,
                    };
                    shoppingListItemsGrid.Children.Add(itempriceLabel, 2, 1);

                    Span qtySpan = new Span { Text = "Qty:" };
                    Span bindedQtySpan = new Span();
                    bindedQtySpan.SetBinding(Span.TextProperty, "itemQuantity");
                    FormattedString formattedStringQty = new FormattedString();
                    formattedStringQty.Spans.Add(qtySpan);
                    formattedStringQty.Spans.Add(bindedQtySpan);
                    Label itemQtyLabel = new Label
                    {
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Start,
                        FontSize = 14,
                        FormattedText = formattedStringQty,
                        BackgroundColor = Color.LimeGreen,
                        TextColor = Color.Black,
                    };
                    shoppingListItemsGrid.Children.Add(itemQtyLabel, 3, 1);


                    Image arrowImage = new Image
                    {
                        WidthRequest = 30,
                        HeightRequest = 30,
                        Source = "arrow.jpg"
                    };
                    shoppingListItemsGrid.Children.Add(arrowImage, 4, 0);
                    Grid.SetRowSpan(arrowImage, 2);


                    //Span locationSpan = new Span { Text = "Location: " };
                    //Span bindedDeptSpan = new Span();
                    //bindedDeptSpan.SetBinding(Span.TextProperty, "department");
                    //Span Arrow = new Span { Text = "->" };
                    //Span bindedLowerDeptSpan = new Span();
                    //bindedLowerDeptSpan.SetBinding(Span.TextProperty, "lowerDepartment");
                    //Span bindedAisleSpan = new Span();
                    //Span aisleArrow = new Span { Text = "->Aisle: " };
                    //bindedAisleSpan.SetBinding(Span.TextProperty, "aisle");
                    //Span bindedShelfSpan = new Span();
                    //Span shelfArrow = new Span { Text = "->Shelf: " };
                    //bindedShelfSpan.SetBinding(Span.TextProperty, "shelf");
                    //FormattedString formattedStringLocation = new FormattedString();
                    //formattedStringLocation.Spans.Add(locationSpan);
                    //formattedStringLocation.Spans.Add(bindedDeptSpan);
                    //formattedStringLocation.Spans.Add(Arrow);
                    //formattedStringLocation.Spans.Add(bindedLowerDeptSpan);
                    //formattedStringLocation.Spans.Add(aisleArrow);
                    //formattedStringLocation.Spans.Add(bindedAisleSpan);
                    //formattedStringLocation.Spans.Add(shelfArrow);
                    //formattedStringLocation.Spans.Add(bindedAisleSpan);
                    //Label locationLabel = new Label
                    //{
                    //    VerticalOptions = LayoutOptions.Center,
                    //    FontSize = 12,
                    //    FormattedText = formattedStringLocation,
                    //    TextColor = Color.SteelBlue,
                    //    HorizontalOptions = LayoutOptions.Center,
                    //    LineBreakMode = LineBreakMode.WordWrap
                    //};
                    //shoppingListItemsGrid.Children.Add(locationLabel, 0, 1);
                    //Grid.SetColumnSpan(locationLabel, 3);


                    // <Button Grid.Column = "2" x: Name = "deleteItemIcon" Image = "minus_square.jpg" 
                    // WidthRequest = "40" BackgroundColor = "White" HeightRequest = "46"  HorizontalOptions = "EndAndExpand" 
                    // Clicked = "OnDelete_Clicked" VerticalOptions = "Center" CommandParameter = "{Binding id}" />
                    //Button deleteBtn = new Button
                    //{
                    //    Image = "minus_square.jpg",
                    //    WidthRequest = 40,
                    //    HeightRequest = 46,
                    //    BackgroundColor = Color.Yellow,
                    //    HorizontalOptions = LayoutOptions.Center,
                    //    VerticalOptions = LayoutOptions.Center,
                    //    Command = _usersShoppingListsViewModel.OnDeleteItemCommand,
                    //};
                    //deleteBtn.SetBinding(Button.CommandParameterProperty, "linkId");
                    //shoppingListItemsGrid.Children.Add(deleteBtn, 3, 0);
                    //Grid.SetRowSpan(deleteBtn, 2);
                    listItemFrame.Content = shoppingListItemsGrid;
                    return new ViewCell { View = listItemFrame };
                });

                ListView listView = new ListView
                {
                    ItemTemplate = shoppingListItemsDataTemplate,
                    HasUnevenRows = true
                };
                listView.SetBinding(ListView.ItemsSourceProperty, "items");
                listView.ItemTapped += ListView_ItemTapped;

                /*
                  <Image x:Name="addItemIcon1" Source="plus_square.jpg" HorizontalOptions="StartAndExpand"/>
                  <Button HorizontalOptions="FillAndExpand" BackgroundColor="Orange"  TextColor="White" Text="View In Store"/>
                */


                //Image imageAddItem = new Image
                //{
                //    HorizontalOptions = LayoutOptions.End,
                //    Source = "plus_square.jpg",
                //    InputTransparent = true
                //};


                Button viewInStoreBtn = new Button
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Text = "View In Store",
                    Command = _usersShoppingListsViewModel.OnViewStoreCommand,
                    BackgroundColor = Color.Orange,
                    TextColor = Color.White
                };
                viewInStoreBtn.SetBinding(Button.CommandParameterProperty, "id");

                scrollView.Content = listView;
                stackLayoutVertical.Children.Add(stackLayoutShoppingListName);
                stackLayoutVertical.Children.Add(buttonsGrid);
                stackLayoutVertical.Children.Add(shoppingListGrid);
                stackLayoutVertical.Children.Add(listView);
                //stackLayoutVertical.Children.Add(imageAddItem);
                stackLayoutVertical.Children.Add(viewInStoreBtn);


                ContentPage cp = new ContentPage
                {
                    Content = stackLayoutVertical
                };

                return cp;
            });

            this.ItemsSource = _usersShoppingListsViewModel.ShoppingLists;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
            _usersShoppingListsViewModel.OnViewItemCommand.Execute((ShoppingItemAPP)e.Item);
        }


        //foreach (ShoppingListAPP shoppinglist in _usersShoppingListsViewModel.ShoppingLists)
        //{
        //    StackLayout stackLayoutVertical = new StackLayout
        //    {
        //        Orientation = StackOrientation.Vertical,
        //        Margin = 0,
        //        Padding = 0
        //    };

        //    #region Shopping list name
        //    /*
        //    <StackLayout Margin="0, 10, 0, 10">
        //        <Label Text="ShoppingList 1" TextColor="SteelBlue" FontAttributes="Bold" HorizontalOptions="Center" FontSize="22" LineBreakMode="WordWrap"/>
        //    </StackLayout>
        //     */
        //    StackLayout stackLayoutShoppingListName = new StackLayout
        //    {
        //        Orientation = StackOrientation.Vertical,
        //        Margin = new Thickness(0, 10, 0, 10)
        //    };

        //    Label shoppingListNameLabel = new Label
        //    {
        //        TextColor = Color.SteelBlue,
        //        FontAttributes = FontAttributes.Bold,
        //        HorizontalOptions = LayoutOptions.Center,
        //        FontSize = 22,
        //        LineBreakMode = LineBreakMode.WordWrap,
        //        Text = shoppinglist.name                  
        //    };
        //    stackLayoutShoppingListName.Children.Add(shoppingListNameLabel);

        //    #endregion

        //    #region ShoppingList Buttons

        //    // <Grid Padding="0" RowSpacing="0" ColumnSpacing="0" Margin="0" VerticalOptions="FillAndExpand">
        //    Grid buttonsGrid = new Grid
        //    {
        //        Padding = 0,
        //        RowSpacing = 0,
        //        ColumnSpacing = 0,
        //        Margin = 0,
        //        VerticalOptions = LayoutOptions.FillAndExpand
        //    };

        //    buttonsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });

        //    buttonsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        //    buttonsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        //    buttonsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

        //    Button deleteButton = new Button
        //    {
        //        BackgroundColor = Color.Red,
        //        Text = "Delete",
        //        TextColor = Color.White,
        //        Command = _usersShoppingListsViewModel.OnDeleteListCommand,
        //        CommandParameter = shoppinglist.id
        //    };

        //    Button renameButton = new Button
        //    {
        //        BackgroundColor = Color.Blue,
        //        Text = "Rename",
        //        TextColor = Color.White,
        //        Command = _usersShoppingListsViewModel.OnRenameCommand,
        //        CommandParameter = shoppinglist.id
        //    };

        //    Button createNewButton = new Button
        //    {
        //        BackgroundColor = Color.Green,
        //        Text = "Rename",
        //        TextColor = Color.White,
        //        Command = _usersShoppingListsViewModel.OnCreateNewCommand,
        //        CommandParameter = shoppinglist.id
        //    };

        //    /*                
        //    <Button Grid.Column="0" Grid.Row="0" BackgroundColor="Red" TextColor="White" Text="Delete"/>
        //    <Button Grid.Column="1" Grid.Row="0" BackgroundColor="Green" TextColor="White" Text="Create New"/>
        //    <Button Grid.Column="2" Grid.Row="0" BackgroundColor="Blue" TextColor="White" Text="Rename"/>
        //     */
        //    buttonsGrid.Children.Add(createNewButton, 0, 0);
        //    buttonsGrid.Children.Add(deleteButton, 1, 0);
        //    buttonsGrid.Children.Add(renameButton, 2, 0);

        //    #endregion

        //    #region ShoppingList Information

        //    Grid shoppingListGrid = new Grid
        //    {
        //        Padding = 0,
        //        RowSpacing = 0,
        //        ColumnSpacing = 0,
        //        Margin = 0,
        //        VerticalOptions = LayoutOptions.FillAndExpand
        //    };

        //    shoppingListGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });

        //    shoppingListGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        //    shoppingListGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        //    shoppingListGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

        //    #region TotalCost Section
        //    /*
        //         <Grid Padding="0" RowSpacing="0" ColumnSpacing="0" Margin="-5" VerticalOptions="FillAndExpand">
        //            <Grid.RowDefinitions>
        //                <RowDefinition Height="Auto"/>
        //            </Grid.RowDefinitions>
        //            <Grid.ColumnDefinitions>
        //                <ColumnDefinition Width="*" />
        //                <ColumnDefinition Width="*"/>
        //                <ColumnDefinition Width="*"/>
        //            </Grid.ColumnDefinitions>
        //            <Frame Grid.Column="0" Grid.Row="0" BorderColor="Red"  Padding="0">
        //                <StackLayout>
        //                    <Label HorizontalOptions="CenterAndExpand" Text="Total Cost" FontAttributes="Bold" />
        //                    <Label HorizontalOptions="CenterAndExpand">
        //                        <Label.FormattedText>
        //                            <FormattedString>
        //                                <Span Text="$" FontAttributes="Bold"/>
        //                                <Span Text="{Binding ShoppingList.totalCost}" FontAttributes="Bold" />
        //                            </FormattedString>
        //                        </Label.FormattedText>
        //                    </Label>
        //                </StackLayout>
        //            </Frame>
        //        */

        //    // CODE FOR TOTAL COST INFORMATION

        //    Frame frameTotalCost = new Frame
        //    {
        //        BorderColor = Color.Red,
        //        Padding = 0
        //    };

        //    StackLayout stackLayout1 = new StackLayout
        //    {
        //        Orientation = StackOrientation.Vertical
        //    };

        //    Span dollarSignSpan = new Span { Text = "$", FontAttributes = FontAttributes.Bold };
        //    Span totalCostSpan = new Span { FontAttributes = FontAttributes.Bold };
        //    totalCostSpan.SetBinding(Span.TextProperty, "totalCost");
        //    FormattedString formattedStringCost = new FormattedString();
        //    formattedStringCost.Spans.Add(dollarSignSpan);
        //    formattedStringCost.Spans.Add(totalCostSpan);
        //    Label totalCost = new Label
        //    {
        //        HorizontalOptions = LayoutOptions.CenterAndExpand,
        //        Text = "Total Cost",
        //        FontAttributes = FontAttributes.Bold
        //    };
        //    Label bindedTotalCost = new Label
        //    {
        //        HorizontalOptions = LayoutOptions.CenterAndExpand,
        //        FormattedText = formattedStringCost
        //    };
        //    stackLayout1.Children.Add(totalCost);
        //    stackLayout1.Children.Add(bindedTotalCost);
        //    frameTotalCost.Content = stackLayout1;
        //    shoppingListGrid.Children.Add(frameTotalCost, 0, 0);

        //    #endregion

        //    #region Total Items Section

        //    /*

        //     <Frame Grid.Column="1" Grid.Row="0" BorderColor="Red" Padding="0">
        //        <StackLayout>
        //            <Label HorizontalOptions="CenterAndExpand" Text="Total Items" FontAttributes="Bold" />
        //            <Label HorizontalOptions="CenterAndExpand" Text="{Binding TotalItems}"/>
        //        </StackLayout>
        //    </Frame>
        //     */

        //    // CODE FOR TOTALITEMS INFORMATION
        //    Frame frameTotalItems = new Frame
        //    {
        //        BorderColor = Color.Red,
        //        Padding = 0
        //    };

        //    StackLayout stackLayout2 = new StackLayout
        //    {
        //        Orientation = StackOrientation.Vertical
        //    };

        //    Label totalItems = new Label
        //    {
        //        HorizontalOptions = LayoutOptions.CenterAndExpand,
        //        Text = "Total Items",
        //        FontAttributes = FontAttributes.Bold
        //    };
        //    Label bindedTotalItems = new Label
        //    {
        //        HorizontalOptions = LayoutOptions.CenterAndExpand,
        //        FontAttributes = FontAttributes.Bold
        //    };
        //    bindedTotalItems.SetBinding(Label.TextProperty, shoppinglist.totalItems.ToString());

        //    stackLayout2.Children.Add(totalItems);
        //    stackLayout2.Children.Add(bindedTotalItems);
        //    frameTotalItems.Content = stackLayout2;
        //    shoppingListGrid.Children.Add(frameTotalItems, 1, 0);

        //    #endregion

        //    #region Time of Creation Section

        //    /*
        //     <Frame Grid.Column="2" Grid.Row="0" BorderColor="Red" Padding="0">
        //        <StackLayout>
        //            <Label HorizontalOptions="CenterAndExpand" Text="Created On" FontAttributes="Bold"/>
        //            <Label HorizontalOptions="CenterAndExpand">
        //                <Label.FormattedText>
        //                    <FormattedString>
        //                        <Span Text="5" FontAttributes="Bold" />
        //                    </FormattedString>
        //                </Label.FormattedText>
        //            </Label>
        //        </StackLayout>
        //    </Frame>
        //     */

        //    Frame frameTimeOfCreation = new Frame
        //    {
        //        BorderColor = Color.Red,
        //        Padding = 0
        //    };

        //    StackLayout stackLayout3 = new StackLayout
        //    {
        //        Orientation = StackOrientation.Vertical
        //    };

        //    Label timeOfCreation = new Label
        //    {
        //        HorizontalOptions = LayoutOptions.CenterAndExpand,
        //        Text = "Total Cost",
        //        FontAttributes = FontAttributes.Bold
        //    };

        //    Label bindedTimeOfCreation = new Label
        //    {
        //        HorizontalOptions = LayoutOptions.CenterAndExpand,
        //        FontAttributes = FontAttributes.Bold
        //    };
        //    bindedTimeOfCreation.SetBinding(Label.TextProperty, shoppinglist.timeOfCreation.ToString());

        //    stackLayout3.Children.Add(timeOfCreation);
        //    stackLayout3.Children.Add(bindedTimeOfCreation);
        //    frameTimeOfCreation.Content = stackLayout3;
        //    shoppingListGrid.Children.Add(frameTimeOfCreation, 2, 0);

        //    #endregion

        //    #endregion

        //    ScrollView scrollView = new ScrollView();


        //    /*
        //    <ListView x:Name="items1">
        //        <ListView.ItemTemplate>
        //            <DataTemplate>
        //                <ViewCell>
        //                    <Grid Padding="0" RowSpacing="0" ColumnSpacing="0">
        //                        <Grid.RowDefinitions>
        //                            <RowDefinition Height="*"/>
        //                        </Grid.RowDefinitions>
        //                        <Grid.ColumnDefinitions>
        //                            <ColumnDefinition Width="5*" />
        //                            <ColumnDefinition Width="2*"/>
        //                            <ColumnDefinition Width="3*"/>
        //                        </Grid.ColumnDefinitions>
        //                        <Label LineBreakMode="WordWrap" Text="{Binding name}"  Grid.Column="0" FontSize="Medium" VerticalOptions="Center"/>
        //                        <Label FontSize="Medium" Grid.Column="1" VerticalOptions="Center">
        //                            <Label.FormattedText>
        //                                <FormattedString>
        //                                    <Span Text="$"/>
        //                                    <Span Text="{Binding price}"/>
        //                                </FormattedString>
        //                            </Label.FormattedText>
        //                        </Label>
        //                        <Button Grid.Column="2" x:Name="deleteItemIcon" Image="minus_square.jpg" WidthRequest="40" BackgroundColor="White" HeightRequest="46"  HorizontalOptions="EndAndExpand" Clicked="OnDelete_Clicked" VerticalOptions="Center" CommandParameter="{Binding id}"/>

        //                    </Grid>
        //                </ViewCell>
        //            </DataTemplate>
        //        </ListView.ItemTemplate>
        //     </ListView>
        //     */

        //    var shoppingListItemsDataTemplate = new DataTemplate(() => 
        //    {
        //        Grid shoppingListItemsGrid = new Grid
        //        {
        //            Padding = 0,
        //            RowSpacing = 0,
        //            ColumnSpacing = 0,
        //            Margin = 0
        //        };

        //        shoppingListItemsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

        //        shoppingListItemsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) });
        //        shoppingListItemsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
        //        shoppingListItemsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });

        //        Label itemNameLabel = new Label
        //        {
        //            LineBreakMode = LineBreakMode.WordWrap,
        //            VerticalOptions = LayoutOptions.Center,
        //            TextColor = Color.RoyalBlue,
        //            FontSize = 10,

        //        };
        //        itemNameLabel.SetBinding(Label.TextProperty, "name");
        //        shoppingListItemsGrid.Children.Add(itemNameLabel, 0, 0);

        //        Span dollarSignSpan1 = new Span { Text = "$" };
        //        Span totalCostSpan1 = new Span();
        //        totalCostSpan1.SetBinding(Span.TextProperty, "price");
        //        FormattedString formattedStringCost1 = new FormattedString();
        //        formattedStringCost1.Spans.Add(dollarSignSpan1);
        //        formattedStringCost1.Spans.Add(totalCostSpan1);
        //        Label itempriceLabel = new Label
        //        {
        //            VerticalOptions = LayoutOptions.Center,
        //            FontSize = 10,
        //            FormattedText = formattedStringCost1,
        //            TextColor = Color.Red,
        //            HorizontalOptions = LayoutOptions.Center
        //        };
        //        shoppingListItemsGrid.Children.Add(itempriceLabel, 1, 0);
        //        // <Button Grid.Column = "2" x: Name = "deleteItemIcon" Image = "minus_square.jpg" 
        //        // WidthRequest = "40" BackgroundColor = "White" HeightRequest = "46"  HorizontalOptions = "EndAndExpand" 
        //        // Clicked = "OnDelete_Clicked" VerticalOptions = "Center" CommandParameter = "{Binding id}" />
        //        Button deleteBtn = new Button
        //        {
        //            Image = "minus_square.jpg",
        //            WidthRequest = 40,
        //            HeightRequest = 46,
        //            BackgroundColor = Color.White,
        //            HorizontalOptions = LayoutOptions.EndAndExpand,
        //            VerticalOptions = LayoutOptions.Center,
        //            Command = _usersShoppingListsViewModel.OnDeleteItemCommand,
        //        };
        //        deleteBtn.SetBinding(Button.CommandParameterProperty, "linkId");
        //        shoppingListItemsGrid.Children.Add(deleteBtn, 2, 0);

        //        return new ViewCell { View = shoppingListItemsGrid };
        //    });

        //    ListView listView = new ListView
        //    {
        //        ItemTemplate = shoppingListItemsDataTemplate,
        //        ItemsSource = shoppinglist.items
        //    };

        //    /*
        //      <Image x:Name="addItemIcon1" Source="plus_square.jpg" HorizontalOptions="StartAndExpand"/>
        //      <Button HorizontalOptions="FillAndExpand" BackgroundColor="Orange"  TextColor="White" Text="View In Store"/>
        //    */

        //    StackLayout stackLayoutAddItem = new StackLayout
        //    {
        //        Orientation = StackOrientation.Horizontal,
        //        BackgroundColor = Color.Transparent
        //    };
        //    Image imageAddItem = new Image
        //    {
        //        HorizontalOptions = LayoutOptions.End,
        //        Source = "plus_square.jpg"
        //    };
        //    stackLayoutAddItem.Children.Add(imageAddItem);

        //    StackLayout stackLayoutViewInStore = new StackLayout
        //    {
        //        Orientation = StackOrientation.Horizontal,
        //    };
        //    Button viewInStoreBtn = new Button
        //    {
        //        HorizontalOptions = LayoutOptions.FillAndExpand,
        //        Text = "View In Store",
        //        Command = _usersShoppingListsViewModel.OnViewStoreCommand,
        //        BackgroundColor = Color.Orange,
        //        TextColor = Color.White
        //    };
        //    viewInStoreBtn.SetBinding(Button.CommandParameterProperty, shoppinglist.id.ToString());
        //    stackLayoutViewInStore.Children.Add(viewInStoreBtn);

        //    scrollView.Content = listView;
        //    stackLayoutVertical.Children.Add(stackLayoutShoppingListName);
        //    stackLayoutVertical.Children.Add(buttonsGrid);
        //    stackLayoutVertical.Children.Add(shoppingListGrid);
        //    stackLayoutVertical.Children.Add(listView);


        //    ContentPage cp = new ContentPage
        //    {
        //        Title = shoppinglist.name,
        //        Content = stackLayoutVertical
        //    };

        //    this.Children.Add(cp);
        //}
        //var shoppingLists = _shoppingListService.GetShoppingUsersList(_shoppingUser.token);
        //_lists = new ObservableCollection<ShoppingList>();
        //foreach (var list in shoppingLists)
        //{
        //    _lists.Add(list);
        //}


        //MyListView.ItemsSource = _lists;
    }
}
