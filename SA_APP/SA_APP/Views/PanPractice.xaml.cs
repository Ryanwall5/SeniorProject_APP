using Rg.Plugins.Popup.Services;
using SA_APP.Models.FromApi;
using SA_APP.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.PlatformConfiguration;


namespace SA_APP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PanPractice : ContentPage
    {
        double x = 0, y = 0;
        double afterX = 0;
        double afterY = 0;
        double imageContentOpacity;
        bool first = true;
        double shoppingListFinishLine;
        private CutoffConverter _cutoffConverter;
        private List<Item> _items;
        private Image _newImage;
        private ContentView _newView;
        private ShoppingUserAPP _shoppingUserApp;


        public PanPractice()
        {
            _shoppingUserApp = new ShoppingUserAPP();

                InitializeComponent();

            //TapGestureRecognizer onimagetapped = new TapGestureRecognizer()
            //{
            //    NumberOfTapsRequired = 1,
            //    Command = new Command(
            //                execute: () =>
            //                {
            //                    absoluteView.RaiseChild(new BoxView()
            //                    {
                                   
            //                    });
            //                })
            //};

           // imageContent.GestureRecognizers.Add(onimagetapped);
            _cutoffConverter = new CutoffConverter();
            _items = new List<Item>();

            for (int i = 0; i < 10; i++)
            {
                Item item = new Item
                {
                    Id = i,
                    Name = $"Item{i}",
                    Price = 5.0m,
                    storeId = 1,
                    Image = "image.jpg",
                    InStock = true,
                    StockAmount = 30,
                    SlotId = 1,
                };
                _items.Add(item);
            }

            //shoppingListFinishLine = finishLine.Y;
           //MyListView.ItemsSource = _items;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();


            ///*


            //    <DataTrigger x:Name="finishLineDataTrigger2"  TargetType="BoxView"  Binding="{Binding Source={x:Reference imageContent1}, Path=TranslationY,
            //                       Converter={StaticResource TranslationYConverter}}" Value="true">
            //        <DataTrigger.Setters>
            //            <Setter Property="BackgroundColor" Value="Transparent"/>
            //        </DataTrigger.Setters>
            //    </DataTrigger>

            //    <ListView x:Name="MyListView"
            //        ItemTapped="Handle_ItemTapped"
            //        CachingStrategy="RecycleElement"
            //          Margin="20"
            //          HasUnevenRows="True">

            //        <ListView.ItemTemplate>
            //            <DataTemplate>
            //                <ViewCell>
            //                    <Grid Margin="5">
            //                        <Grid.ColumnDefinitions>
            //                            <ColumnDefinition  Width="Auto"/>
            //                            <ColumnDefinition  />
            //                        </Grid.ColumnDefinitions>
            //                        <Grid.RowDefinitions>
            //                            <RowDefinition Height="Auto"/>
            //                            <RowDefinition />
            //                        </Grid.RowDefinitions>

            //                        <ContentView x:Name="invisibleContentView1" BackgroundColor="Gray">
            //                            <ContentView.GestureRecognizers>
            //                                <PanGestureRecognizer PanUpdated="PanGestureRecognizer_PanUpdated"/>
            //                            </ContentView.GestureRecognizers>
            //                            <Image x:Name="imageContent1" Source="{Binding Image}" HorizontalOptions="Start" VerticalOptions="Start" WidthRequest="50" HeightRequest="50" Grid.RowSpan="2"  Aspect="AspectFill" />
            //                        </ContentView>

            //                        <Label Grid.Column="1" Grid.Row="0" FontSize="Small" Text="{Binding Name}" />
            //                        <Label VerticalTextAlignment="Start" VerticalOptions="Start" Grid.Column="1" Grid.Row="1" FontSize="Small" Text="{Binding Price}" />
            //                    </Grid>

            //                </ViewCell>
            //            </DataTemplate>
            //        </ListView.ItemTemplate>
            //    </ListView>
            // */
            //// Add in a trigger foreach item we have added.
            //foreach (Item item in _items)
            //{

            //    Setter s = new Setter
            //    {
            //        Property = BackgroundColorProperty,
            //        Value = Color.Transparent
            //    };

            //    DataTrigger d = new DataTrigger(typeof(BoxView));

            //    d.SetBinding();
            //    d.Setters.Add(s);

            //    finishLine.Triggers.Add(d);
            //}

            ////searchListItem


            //var searchedItemsDataTemplate = new DataTemplate(() =>
            //    {
            //        Frame listItemFrame = new Frame
            //        {
            //            Padding = 2,
            //            Margin = new Thickness(0, 3, 0, 3),
            //            HasShadow = true
            //        };

            //        Grid shoppingListItemsGrid = new Grid
            //        {
            //            Padding = 0,
            //            RowSpacing = 0,
            //            ColumnSpacing = 0,
            //            Margin = 0
            //        };

            //        shoppingListItemsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            //        shoppingListItemsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            //        shoppingListItemsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
            //        shoppingListItemsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });


            //        Image itemImage = new Image
            //        {
            //            WidthRequest = 40,
            //            HeightRequest = 46,
            //            Source = "image.jpg"
            //        };
            //        shoppingListItemsGrid.Children.Add(itemImage, 0, 0);
            //        Grid.SetRowSpan(itemImage, 2);

            //        Label itemNameLabel = new Label
            //        {
            //            LineBreakMode = LineBreakMode.TailTruncation,
            //            VerticalOptions = LayoutOptions.Center,
            //            FontSize = 12,

            //        };
            //        itemNameLabel.SetBinding(Label.TextProperty, "name");
            //        itemNameLabel.SetBinding(Label.TextColorProperty, "LabelColor");
            //    // column, row, columnspan, rowspan
            //    shoppingListItemsGrid.Children.Add(itemNameLabel, 1, 0);
            //        Grid.SetColumnSpan(itemNameLabel, 2);

            //        Span departmentSpan1 = new Span { Text = "Department: " };
            //        Span bindDepartmentSpan1 = new Span();
            //        bindDepartmentSpan1.SetBinding(Span.TextProperty, "department");
            //        FormattedString formattedStringD1 = new FormattedString();
            //        formattedStringD1.Spans.Add(departmentSpan1);
            //        formattedStringD1.Spans.Add(bindDepartmentSpan1);
            //        Label itemDeptLabel = new Label
            //        {
            //            VerticalOptions = LayoutOptions.Center,
            //            FontSize = 12,
            //            FormattedText = formattedStringD1,
            //            BackgroundColor = Color.Gold,
            //            TextColor = Color.Red,
            //            HorizontalOptions = LayoutOptions.Start
            //        };
            //        shoppingListItemsGrid.Children.Add(itemDeptLabel, 1, 1);

            //        Span qtySpan = new Span { Text = "Qty:" };
            //        Span bindedQtySpan = new Span();
            //        bindedQtySpan.SetBinding(Span.TextProperty, "itemQuantity");
            //        FormattedString formattedStringQty = new FormattedString();
            //        formattedStringQty.Spans.Add(qtySpan);
            //        formattedStringQty.Spans.Add(bindedQtySpan);
            //        Label itemQtyLabel = new Label
            //        {
            //            VerticalOptions = LayoutOptions.Center,
            //            FontSize = 12,
            //            FormattedText = formattedStringQty,
            //            BackgroundColor = Color.LightGray,
            //            TextColor = Color.Red,
            //            HorizontalOptions = LayoutOptions.Start
            //        };
            //        shoppingListItemsGrid.Children.Add(itemQtyLabel, 2, 1);

            //        listItemFrame.Content = shoppingListItemsGrid;
            //        ViewCell viewCell = new ViewCell
            //        {
            //            View = listItemFrame
            //        };

            //        return viewCell;
            //    });

            //ListView listView = new ListView
            //{
            //    ItemTemplate = shoppingListItemsDataTemplate,
            //    HasUnevenRows = true,
            //    ItemsSource = _shoppingList.CheckedShoppingItems
            //};
            //listView.ItemTapped += ListView_ItemTapped;


        }

        //private void OnTapped(string obj)
        //{
        //    finishLine.SetBinding();
        //}

        private async void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            Grid firstGrid = grid1; 
            ContentView contentViewWrapper = (ContentView)sender;
            Grid mygrid = contentViewWrapper.Parent as Grid;
            Image image = (Image)contentViewWrapper.Content;
            //image.GetElevation();
            imageContentOpacity = image.Opacity;
            //Grid grid = (Grid)contentViewWrapper.Parent;
            
            if (e.StatusType.Equals(GestureStatus.Started))
            {
                mystacklayout.RaiseChild(mygrid);
                //var list = MyListView.ItemsSource;
                //foreach(var item in list)
                //{
                //    var dataItem = (Item)item;
                //}
                //itemsscrollview.IsEnabled = false;
                //listViewWrapper.RaiseChild(image);
                //MyListView.IsEnabled = false;
                //ContentView contentViewWrapper = (ContentView)sender;

                //Image image = (Image)contentViewWrapper.Content;
                //imageContentOpacity = image.Opacity;


                ///*
                // <ContentView x:Name="invisibleContentView1" BackgroundColor="Gray">
                //                <ContentView.GestureRecognizers>
                //                    <PanGestureRecognizer PanUpdated="PanGestureRecognizer_PanUpdated"/>
                //                </ContentView.GestureRecognizers>
                //                <Image  x:Name="imageContent1"  Source="https://spoonacular.com/productImages/29260-312x231.jpg" HorizontalOptions="Start" VerticalOptions="Start" WidthRequest="50" HeightRequest="50" Grid.RowSpan="2"  Aspect="AspectFill" />

                // </ContentView>

                // */
                //_newImage = new Image();
                //_newImage.Source = image.Source;
                //_newImage.Layout(image.Bounds);

                //_newView = new ContentView();

                //// Layout bounds x,y,width,height
                //_newView.Layout(contentViewWrapper.Bounds);
                //_newView.Content = _newImage;

                //absoluteView.RaiseChild(_newView);
            }
            else if (e.StatusType.Equals(GestureStatus.Running))
            {
                x = image.X + e.TotalX;
                y = image.Y + e.TotalY;

                await image.TranslateTo(x, y, 1);
                //x = _newImage.X + e.TotalX;
                //y = _newImage.Y + e.TotalY;

                //await _newImage.TranslateTo(x, y, 1);
            }
            else if (e.StatusType.Equals(GestureStatus.Completed))
            {

                if(myscrollview.ScrollY > 0)
                {
                   y = y + mygrid.Y - myscrollview.ScrollY;
                }
                else
                {
                    y = y + mygrid.Y;
                }

                Console.WriteLine($"Current ScrollY position: {myscrollview.ScrollY}");
                Console.WriteLine($"Height phone: {Xamarin.Forms.Application.Current.MainPage.Height}");
                Console.WriteLine($"Width phone: {Xamarin.Forms.Application.Current.MainPage.Width}");
                Console.WriteLine($"First Grid Location: {firstGrid.Y}");
                Console.WriteLine($"Grid that is being used Location: {mygrid.Y}");
                Console.WriteLine($"Total y Location: {x}, {y}");
                Console.WriteLine($"BoxView Location: {dropzoneBoxView.X} , {dropzoneBoxView.Y} ");
                Console.WriteLine($"Grid Location: {dropZoneGrid.X} , {dropZoneGrid.Y} ");
                //y = y - myscrollview.ScrollY + mygrid.Y;

                //y = mygrid.Y + y;
                //finishLineDataTrigger.SetBinding(image, "TranslationY", BindingMode.Default, _cutoffConverter);
                await FadeIn(image);
                if (y >= dropZoneGrid.Y && x > dropzoneBoxView.X)
                {
                    await DisplayActionSheet("Add item to shopping list", "cancel", null, new string[] { "Add Item", "ANother button" });
                    //await PopupNavigation.Instance.PushAsync(new AddItemToListPopup(_items[0], _shoppingUserApp));
                }
                await image.TranslateTo(image.X, image.Y, 1);
                image.Opacity = imageContentOpacity;
                //await FadeIn(_newImage);
                //if (y >= 400)
                //{
                //    //await PopupNavigation.Instance.PushAsync(new AddItemToListPopup(slot.Item, _shoppingUserApp));
                //}
                //await _newImage.TranslateTo(_newImage.X, _newImage.Y, 1);
                //_newImage.Opacity = imageContentOpacity;
                //MyListView.IsEnabled = true;
            }
        }

        private void ItemSearchBar_SearchButtonPressed(object sender, EventArgs e)
        {

        }

        private void MyListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }


        private void tapped(Image image)
        {
            //absoluteView.RaiseChild(image);
        }


        //private async Task AnimateImage(double xtrans, double ytrans)
        //{
        //    if (first)
        //    {
        //        await imageContent.TranslateTo(xtrans, ytrans);
        //        imageContentOpacity = imageContent.Opacity;
        //        first = false;
        //    }
        //    else
        //    {
        //        await imageContent.TranslateTo(xtrans, ytrans);
        //        imageContent.Opacity = imageContentOpacity;
        //        first = true;
        //    }

        //}
        private async Task FadeIn(Image image)
        {
            await image.FadeTo(0, 500);
        }
    }



    public class CutoffConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((double)value) > 400;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((double)value) < 500;
        }
    }
}