using SA_APP.Models;
using SA_APP.Models.FromApi;
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
    //public partial class SectionPage : TabbedPage
    public partial class SectionPage : TabbedPage
    {
        private SectionViewModel _sectionViewModel;
        private Dictionary<int, Frame> _slotFrames = new Dictionary<int, Frame>();

        public SectionPage()
        {

        }
        public SectionPage(ShoppingUserAPP shoppingUser, Aisle aisle, CheckedShoppingList shoppingList = null)
        {
            InitializeComponent();
            _sectionViewModel = new SectionViewModel(aisle, shoppingUser, Navigation, shoppingList);
        }

        protected override void OnDisappearing()
        {
            this.Children.Clear();
            _slotFrames.Clear();
            base.OnDisappearing();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Send(this, "Unspecified");
            foreach (Section s in _sectionViewModel._aisle.Sections)
            {
                //x:Name="storeGrid1" RowSpacing="0" Margin="0" Padding="0" ColumnSpacing="0" BackgroundColor="white" HorizontalOptions="FillAndExpand" VerticalOptions="Fill"
                Grid sectionGrid = new Grid()
                {
                    Padding = 0,
                    BackgroundColor = Color.White,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.Fill
                };

                for (int i = 0; i < s.Shelves.Count; i++)
                {
                    sectionGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    sectionGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(10) });
                }

                for (int i = 0; i < s.ItemsPerShelf; i++)
                {
                    sectionGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                }

                int shelfBottom = 1;
                int shelfNum = 0;
                string[] actionList = null;

                if(_sectionViewModel._shoppingList == null)
                {
                    actionList = new string[] { "Add To List", "View Item" };
                }
                else
                {
                    actionList = new string[] { "Add To List", "View Item", "Got Item?" };

                }

                foreach (Shelf shelf in s.Shelves)
                {
                    int count = 0;
                    foreach (ShelfSlot sl in shelf.Slots)
                    {
                        TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer
                        {
                            NumberOfTapsRequired = 1,
                            Command = new Command(async () => 
                            {
                               var action = await DisplayActionSheet(sl.Item.Name, "Cancel", null, actionList);
                                if (action == "Add To List")
                                {
                                    _sectionViewModel.AddToListCommand.Execute($"{s.Id},{shelf.Id},{sl.Id}");
                                }
                                else if (action == "View Item")
                                {
                                    _sectionViewModel.ViewItemCommand.Execute($"{s.Id},{shelf.Id},{sl.Id}");
                                }
                                else if (action == "Got Item?")
                                {
                                    var frame = _slotFrames[sl.Id];
                                    frame.BackgroundColor = Color.White;

                                    _sectionViewModel.RemoveItem(sl.Item); 
                                }

                            }),
                        };

                        Image itemImage = new Image
                        {
                            HeightRequest = 60,
                            WidthRequest = 60,
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center,
                            Source = sl.Item.Image
                        };
                        itemImage.GestureRecognizers.Add(tapGestureRecognizer);

                        Label itemNameLabel = new Label
                        {
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalTextAlignment = TextAlignment.Center,
                            LineBreakMode = LineBreakMode.TailTruncation,
                            MaxLines = 2,
                            Text = sl.Item.Name.ToString(),
                            TextColor = Color.Black
                        };


                        Label itemPriceLabel = new Label
                        {
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center,
                            Text = "$" + sl.Item.Price.ToString(),
                            BackgroundColor = Color.Gold,
                            TextColor = Color.Red
                        };

                        //HeightRequest="38" BackgroundColor="Red" TextColor="White" Text="View Item"
                        //Button viewItemDetails = new Button
                        //{
                        //    Padding = 0,
                        //    FontSize = 10,
                        //    HeightRequest = 32,
                        //    BackgroundColor = Color.Red,
                        //    TextColor = Color.White,
                        //    CommandParameter = $"{s.Id},{shelf.Id},{sl.Id}",
                        //    Command = _sectionViewModel.ViewItemCommand,
                        //    Text = "View Item"
                        //};

                        //Button addToList = new Button
                        //{
                        //    Padding = 0,
                        //    FontSize = 10,
                        //    HeightRequest = 32,
                        //    BackgroundColor = Color.CadetBlue,
                        //    TextColor = Color.White,
                        //    CommandParameter = $"{s.Id},{shelf.Id},{sl.Id}",
                        //    Command = _sectionViewModel.AddToListCommand,
                        //    Text = "Add To List"
                        //};

                        Frame frame1 = new Frame
                        {
                            HasShadow = true,
                            Padding = 0,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            Content = new StackLayout
                            {
                                Orientation = StackOrientation.Vertical,
                                Children =
                                {
                                    itemImage,
                                    itemNameLabel,
                                    itemPriceLabel
                                }
                            }
                        };

                        sectionGrid.Children.Add(frame1, count, shelfNum);
                        _slotFrames.Add(sl.Id, frame1);
                        count++;
                    }

                    Frame frame2 = new Frame
                    {
                        BackgroundColor = Color.Black,
                        HasShadow = true
                    };

                    sectionGrid.Children.Add(frame2, 0, shelfBottom);
                    Grid.SetColumnSpan(frame2, 3);

                    shelfBottom += 2;
                    shelfNum += 2;
                }



                ContentPage cp = new ContentPage
                {
                    Title = s.Name,
                    Content = sectionGrid
                };


                this.Children.Add(cp);
            }

            if (_sectionViewModel._shoppingList != null)
            {
                foreach (CheckedItem item in _sectionViewModel._shoppingList.CheckedShoppingItems)
                {
                    if (_slotFrames.ContainsKey(item.slotId))
                    {
                        var frame = _slotFrames[item.slotId];
                        frame.BackgroundColor = Color.Green;
                    }
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
