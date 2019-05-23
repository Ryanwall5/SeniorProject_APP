using Rg.Plugins.Popup.Services;
using SA_APP.CustomControls;
using SA_APP.Models.FromApi;
using SA_APP.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SA_APP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchItemsPage : ContentPage
    {
        private ShoppingUserAPP _shoppingUser;
        private ItemService _itemService;
        public ObservableCollection<Item> _items { get; set; }

        public ICommand ViewItemCommand => new Command<Item>(ViewItem);



        private double _imageContentOpacity;
        private double x = 0;
        private double y = 0;


        public SearchItemsPage(ShoppingUserAPP shoppingUser)
        {
            InitializeComponent();
            _shoppingUser = shoppingUser;
            _itemService = new ItemService();
            _items = new ObservableCollection<Item>();
            //_items = _itemService.GetInMemoryItems();      
        }

        private async void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            dropZoneGrid.IsVisible = true;
            var searchedItem = itemSearchBar.Text;
            itemSearchBar.Text = "";
            //MyListView.IsRefreshing = true;
            var items = _itemService.SearchItems(searchedItem, _shoppingUser.homeStore.id);
            //var items = _itemService.SearchItemsInMemory(searchedItem, _shoppingUser.HomeStore.Id);
            //var items = _items.Where(i => i.storeId == _shoppingUser.homeStore.id).ToList();
            if (items == null || items.Count == 0)
            {
                await DisplayAlert("Error", $"{_shoppingUser.homeStore.name} does not contain any of those items", "OK");
            }
            else
            {
                foreach (var item in items)
                {
                    _items.Add(item);
                }
            }
            // MyListView.IsRefreshing = false;


            /*
             * <Grid HorizontalOptions="Fill" x:Name="grid1" HeightRequest="100" BackgroundColor="Red">
                    <Grid.RowDefinitions>
                        <RowDefinition Height="25"/>
                        <RowDefinition Height="25"/>
                    </Grid.RowDefinitions>
                    <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="50"/>
                        <ColumnDefinition Width="10"/>
                        <ColumnDefinition Width="*"/>
                    </Grid.ColumnDefinitions>
                    <custom:PanContentView Grid.Row="0" Grid.RowSpan="2" Grid.Column="0" BackgroundColor="Green" >
                        <ContentView.GestureRecognizers>
                            <PanGestureRecognizer PanUpdated="PanGestureRecognizer_PanUpdated"/>
                        </ContentView.GestureRecognizers>
                        <Image x:Name="ImageContent" HorizontalOptions="Start" Source="image.jpg"  Aspect="AspectFill" />
                    </custom:PanContentView>

                        <Label Text="Item 1" Grid.Row="0" Grid.Column="2"/>
                        <Label Text="Item 1" Grid.Row="1" Grid.Column="2"/>
                    </Grid>
             */

            foreach (var item in _items)
            {

                Grid itemGrid = new Grid
                {
                    HorizontalOptions = LayoutOptions.Fill,
                    HeightRequest = 100,
                    BackgroundColor = Color.White
                };
                itemGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(25) });
                itemGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(25) });

                itemGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });
                itemGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10) });
                itemGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                itemGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });

                PanContentView panContentView = new PanContentView { BackgroundColor = Color.Silver };

                PanGestureRecognizer panGesture = new PanGestureRecognizer();
                panGesture.PanUpdated += PanGestureRecognizer_PanUpdated;

                panContentView.GestureRecognizers.Add(panGesture);

                Image itemImage = new Image
                {
                    Source = item.Image,
                    HorizontalOptions = LayoutOptions.Start,
                    Aspect = Aspect.AspectFill,
                    ClassId = item.Id.ToString()
                };

                panContentView.Content = itemImage;
                itemGrid.Children.Add(panContentView, 0, 0);
                Grid.SetRowSpan(panContentView, 2);


                /*
                 * <Label Grid.Column="1" Grid.Row="0" FontSize="Small" Text="{Binding Name}" />
                   <Label VerticalTextAlignment="Start" VerticalOptions="Start" Grid.Column="1" Grid.Row="1" FontSize="Small" Text="{Binding Price}" />
                 */
                Label itemName = new Label
                {
                    Text = item.Name,
                    FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                    LineBreakMode = LineBreakMode.TailTruncation
                };
                itemGrid.Children.Add(itemName, 2, 0);


                Label itemPrice = new Label
                {
                    Text = item.Price.ToString(),
                    FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))

                };
                itemGrid.Children.Add(itemPrice, 2, 1);

                Button viewItem = new Button
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.Center,
                    Image = "rightarrow.png",
                    CommandParameter = item,
                    Command = ViewItemCommand
                };
                itemGrid.Children.Add(viewItem, 3, 0);
                Grid.SetRowSpan(viewItem, 2);

                mystacklayout.Children.Add(itemGrid);
            }
        }

        private async void ViewItem(Item item)
        {
            await Navigation.PushModalAsync(new ViewStoreItemPage(item, _shoppingUser));
        }

        private async void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            ContentView contentViewWrapper = (ContentView)sender;
            Grid mygrid = contentViewWrapper.Parent as Grid;
            Image image = (Image)contentViewWrapper.Content;
            //image.GetElevation();
            _imageContentOpacity = image.Opacity;
            //Grid grid = (Grid)contentViewWrapper.Parent;

            if (e.StatusType.Equals(GestureStatus.Started))
            {
                mystacklayout.RaiseChild(mygrid);
            }
            else if (e.StatusType.Equals(GestureStatus.Running))
            {
                x = image.X + e.TotalX;
                y = image.Y + e.TotalY;

                await image.TranslateTo(x, y, 1);
            }
            else if (e.StatusType.Equals(GestureStatus.Completed))
            {

                Console.WriteLine($"itemSearchBar.height {itemSearchBar.Height}");

                if (myscrollview.ScrollY > 0)
                {
                    y = y + mygrid.Y - myscrollview.ScrollY + itemSearchBar.Height;
                }
                else
                {
                    y = y + mygrid.Y + itemSearchBar.Height;
                }

                Console.WriteLine($"Current ScrollY position: {myscrollview.ScrollY}");
                Console.WriteLine($"Height phone: {Xamarin.Forms.Application.Current.MainPage.Height}");
                Console.WriteLine($"Width phone: {Xamarin.Forms.Application.Current.MainPage.Width}");
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
                    //await DisplayActionSheet("Add item to shopping list", "cancel", null, new string[] { "Add Item", "ANother button" });
                    int item_id = Convert.ToInt32(image.ClassId);
                    Item itemToAdd = _items.FirstOrDefault(i => i.Id == item_id);
                    await PopupNavigation.Instance.PushAsync(new AddItemToListPopup(itemToAdd, _shoppingUser));
                }
                await image.TranslateTo(image.X, image.Y, 1);
                image.Opacity = _imageContentOpacity;
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


        private async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            //Deselect Item
            ((ListView)sender).SelectedItem = null;

            await Navigation.PushModalAsync(new ViewStoreItemPage((Item)e.Item, _shoppingUser));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            itemSearchBar.Focus();
        }
    }
}