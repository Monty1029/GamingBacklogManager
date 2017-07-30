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
    public sealed partial class Wishlist : Page
    {
        private ObservableCollection<string> systems = new ObservableCollection<string>
            {
                "3DO", "3DS", "Amiga", "Android", "Atari 2600", "Atari 5200", "Atari 7800", "ColecoVision", "Commodore 64", "Dreamcast", "DS", "Game Boy",
                "Game Boy Color", "Game Advance", "GameCube", "iOS", "Jaguar", "NES", "NeoGeo Pocket Color", "Nintendo 64", "Nintendo Switch", "PC", "PlayStation",
                "PlayStation 2", "PlayStation 3", "PlayStation 4", "PlayStation Vita", "PSP", "Sega Genesis", "Sega Genesis/Mega Drive", "Sega Master System",
                "Sega Saturn", "Super Nintendo", "Turbo Grafx-16", "Wii U", "Wii", "Windows Mobile", "WonderSwan Color", "WonderSwan", "Xbox", "Xbox 360", "Xbox One"
            };

        WishlistGame game = new WishlistGame();
        private ObservableCollection<WishlistGame> games;
        private List<WishlistGame> sortedGames;

        private string systemText;

        public Wishlist()
        {
            GetSave();
            this.InitializeComponent();
        }

        private async void GetSave()
        {
            DataStorage ds = new DataStorage();
            await ds.DeserializeWishlistGameAsync();
            games = ds.WishlistGames;
            if (games != null)
            {
                sortedGames = new List<WishlistGame>(games);
                sortedGames.Sort((x, y) => string.Compare(x.GameTitle, y.GameTitle));
                games = new ObservableCollection<WishlistGame>(sortedGames);
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

        private void Sort_GameTitle(object sender, RoutedEventArgs e)
        {
            if (games != null)
            {
                games.Clear();
                sortedGames.Sort((x, y) => string.Compare(x.GameTitle, y.GameTitle));
                foreach (WishlistGame g in sortedGames)
                {
                    games.Add(g);
                }
            }
        }

        private void Sort_ReleaseDate(object sender, RoutedEventArgs e)
        {
            if (games != null)
            {
                games.Clear();
                sortedGames.Sort((x, y) => string.Compare(x.ReleaseDate, y.ReleaseDate));
                foreach (WishlistGame g in sortedGames)
                {
                    games.Add(g);
                }
            }
        }

        private void Sort_System(object sender, RoutedEventArgs e)
        {
            if (games != null)
            {
                games.Clear();
                sortedGames.Sort((x, y) => string.Compare(x.System, y.System));
                foreach (WishlistGame g in sortedGames)
                {
                    games.Add(g);
                }
            }
        }

        private void Add_Game(object sender, RoutedEventArgs e)
        {
            if (ValidateEntries())
            {
                CreateGame();                
            }            
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

        private void CreateGame()
        {
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
                games.Add(game);
                ds.WishlistGames = games;
                ds.SerializeWishlistGameAsync();
                GetSave();
                game_title_textbox.Text = "";
                system_combobox.SelectedItem = null;
                datePicker.Date = null;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}
