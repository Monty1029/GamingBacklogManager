using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Gaming_Backlog_Manager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditWishlistGame : Page
    {
        public EditWishlistGame()
        {
            this.InitializeComponent();
        }

        private ObservableCollection<string> systems = new ObservableCollection<string>
            {
                "3DO", "3DS", "Amiga", "Android", "Atari 2600", "Atari 5200", "Atari 7800", "ColecoVision", "Commodore 64", "Dreamcast", "DS", "Game Boy",
                "Game Boy Color", "Game Advance", "GameCube", "iOS", "Jaguar", "NES", "NeoGeo Pocket Color", "Nintendo 64", "Nintendo Switch", "PC", "PlayStation",
                "PlayStation 2", "PlayStation 3", "PlayStation 4", "PlayStation Vita", "PSP", "Sega Genesis", "Sega Genesis/Mega Drive", "Sega Master System",
                "Sega Saturn", "Super Nintendo", "Turbo Grafx-16", "Wii U", "Wii", "Windows Mobile", "WonderSwan Color", "WonderSwan", "Xbox", "Xbox 360", "Xbox One"
            };

        private string systemText;        

        WishlistGame oldGame = new WishlistGame();
        WishlistGame game = new WishlistGame();
        ObservableCollection<WishlistGame> games = new ObservableCollection<WishlistGame>();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            oldGame = (WishlistGame)e.Parameter;
            game_title_textbox.Text = oldGame.GameTitle;
            system_combobox.ItemsSource = systems;
            system_combobox.SelectedItem = oldGame.System;
            system_combobox.PlaceholderText = oldGame.System;
            datePicker.Date = Convert.ToDateTime(oldGame.ReleaseDate);
        }

        private bool ValidateEntries()
        {
            int numberTrue = 0;
            if (game_title_textbox.Text.Equals("", StringComparison.Ordinal))
            {
                game_title_textbox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                game_title_textbox.BorderBrush = new SolidColorBrush(Colors.Silver);
                numberTrue++;
            }
            if (system_combobox.SelectedItem == null)
            {
                system_combobox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                system_combobox.BorderBrush = new SolidColorBrush(Colors.Silver);
                numberTrue++;
            }
            return (numberTrue == 2);
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateEntries())
            {
                CreateGame();
                this.Frame.Navigate(typeof(MainPage));
            }
        }

        private void CreateGame()
        {
            game.ID = oldGame.ID;
            game.GameTitle = game_title_textbox.Text;
            game.System = systemText;
            if (datePicker.Date != null)
            {
                game.ReleaseDate = datePicker.Date.Value.Date.ToString("d");
            }
            StoreData();
        }

        private async void StoreData()
        {
            DataStorage ds = new DataStorage();
            await ds.DeserializeWishlistGameAsync();
            try
            {
                games = ds.WishlistGames;
                foreach (WishlistGame g in games)
                {
                    if (g.ID == game.ID)
                    {
                        games.Remove(g);
                        break;
                    }
                }
                games.Add(game);
                ds.WishlistGames = games;
                ds.SerializeWishlistGameAsync();
            }
            catch (Exception e)
            {
                //Debug.WriteLine(e.Message);
            }
        }

        private void System_combobox_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            if (system_combobox.SelectedItem == null)
            {
                return;
            }
            else
            {
                systemText = (string)system_combobox.SelectedItem;
            }
        }

        private void Go_Back(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void SubmitMenu_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateEntries())
            {
                CreateGame();
                this.Frame.Navigate(typeof(Wishlist));
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            game.ID = oldGame.ID;
            RemoveGame();
            this.Frame.Navigate(typeof(Wishlist));
        }

        private async void RemoveGame()
        {
            DataStorage ds = new DataStorage();
            await ds.DeserializeWishlistGameAsync();
            try
            {
                games = ds.WishlistGames;
                foreach (WishlistGame g in games)
                {
                    if (g.ID == game.ID)
                    {
                        games.Remove(g);
                        break;
                    }
                }
                ds.WishlistGames = games;
                ds.SerializeWishlistGameAsync();
            }
            catch (Exception e)
            {
                //Debug.WriteLine(e.Message);
            }
        }
    }
}
