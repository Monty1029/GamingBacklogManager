using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class AddGame : Page
    {
        private ObservableCollection<string> systems = new ObservableCollection<string>
            {
                "3DO", "3DS", "Amiga", "Android", "Atari 2600", "Atari 5200", "Atari 7800", "ColecoVision", "Commodore 64", "Dreamcast", "DS", "Game Boy",
                "Game Boy Color", "Game Advance", "GameCube", "iOS", "Jaguar", "NES", "NeoGeo Pocket Color", "Nintendo 64", "Nintendo Switch", "PC", "PlayStation",
                "PlayStation 2", "PlayStation 3", "PlayStation 4", "PlayStation Vita", "PSP", "Sega Genesis", "Sega Genesis/Mega Drive", "Sega Master System",
                "Sega Saturn", "Super Nintendo", "Turbo Grafx-16", "Wii U", "Wii", "Windows Mobile", "WonderSwan Color", "WonderSwan", "Xbox", "Xbox 360", "Xbox One"
            };

        private ObservableCollection<string> regions = new ObservableCollection<string>
            {
                "North America", "Europe", "Japan", "Korea", "China"
            };

        private ObservableCollection<string> ownership = new ObservableCollection<string>
            {
                "Owned", "Subscription", "Borrowed/Rented"
            };

        private ObservableCollection<string> distribution = new ObservableCollection<string>
            {
                "Digital", "Physical"
            };

        private string systemText;
        private string regionText;
        private string ownershipText;
        private string distributionText;

        private string statusText;
        private bool nowPlayingInput = false;

        Game game = new Game();
        List<Game> games = new List<Game>();

        public AddGame()
        {   
            this.InitializeComponent();
        }

        private void ValidateEntries()
        {
            if (game_title_textbox.Text.Equals("", StringComparison.Ordinal))
            {
                game_title_textbox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                game_title_textbox.BorderBrush = new SolidColorBrush(Colors.Silver);
            }
            if (system_combobox.SelectedItem == null)
            {
                system_combobox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                system_combobox.BorderBrush = new SolidColorBrush(Colors.Silver);
            }
            if (region_combobox.SelectedItem == null)
            {
                region_combobox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                region_combobox.BorderBrush = new SolidColorBrush(Colors.Silver);
            }
            if (ownership_combobox.SelectedItem == null)
            {
                ownership_combobox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                ownership_combobox.BorderBrush = new SolidColorBrush(Colors.Silver);
            }
            if (distribution_combobox.SelectedItem == null)
            {
                distribution_combobox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                distribution_combobox.BorderBrush = new SolidColorBrush(Colors.Silver);
            }
            if (!(bool)status_unplayed.IsChecked && !(bool)status_unfinished.IsChecked && !(bool)status_beaten.IsChecked && !(bool)status_completed.IsChecked)
            {
                status_border.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                status_border.BorderBrush = new SolidColorBrush(Colors.White);
            }
        }

        private void CheckSelectedStatus()
        {
            if ((bool)status_unplayed.IsChecked)
            {
                statusText = (string)status_unplayed.Content;
            }
            else if ((bool)status_unfinished.IsChecked)
            {
                statusText = (string)status_unfinished.Content;
            }
            else if ((bool)status_beaten.IsChecked)
            {
                statusText = (string)status_beaten.Content;
            }
            else if ((bool)status_completed.IsChecked)
            {
                statusText = (string)status_completed.Content;
            }
        }

        private void CheckNowPlaying()
        {
            if ((bool)now_playing_checkbox.IsChecked)
            {
                nowPlayingInput = true;
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            ValidateEntries();            
            game.GameTitle = game_title_textbox.Text;
            game.System = systemText;
            game.Region = regionText;
            game.Ownership = ownershipText;
            game.Distribution = distributionText;

            CheckSelectedStatus();
            game.Status = statusText;
            if (!achievements1_textbox.Text.Equals("", StringComparison.Ordinal) && !achievements2_textbox.Text.Equals("", StringComparison.Ordinal))
            {
                game.Achievements1 = Int32.Parse(achievements1_textbox.Text);
                game.Achievements2 = Int32.Parse(achievements2_textbox.Text);
            }            
            game.Notes = notes_textbox.Text;
            CheckNowPlaying();
            game.NowPlaying = nowPlayingInput;
            storeData();            
            this.Frame.Navigate(typeof(MainPage));
        }

        private async void storeData()
        {
            DataStorage ds = new DataStorage();
            await ds.DeserializeGameAsync();
            games = ds.Games;
            games.Add(game);
            ds.Games = games;
            ds.SerializeGameAsync();
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

        private void Region_combobox_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            if (region_combobox.SelectedItem == null)
            {
                return;
            }
            else
            {
                regionText = (string)region_combobox.SelectedItem;
            }
        }

        private void Ownership_combobox_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ownership_combobox.SelectedItem == null)
            {
                return;
            }
            else
            {
                ownershipText = (string)ownership_combobox.SelectedItem;
            }
        }

        private void Distribution_combobox_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            if (distribution_combobox.SelectedItem == null)
            {
                return;
            }
            else
            {
                distributionText = (string)distribution_combobox.SelectedItem;
            }
        }
    }
}
