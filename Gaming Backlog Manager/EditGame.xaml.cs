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
    public sealed partial class EditGame : Page
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
        private string nowPlayingInput = "No";

        Game oldGame = new Game();
        Game game = new Game();
        ObservableCollection<Game> games = new ObservableCollection<Game>();

        public EditGame()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            oldGame = (Game)e.Parameter;
            game_title_textbox.Text = oldGame.GameTitle;
            system_combobox.ItemsSource = systems;
            system_combobox.SelectedItem = oldGame.System;
            system_combobox.PlaceholderText = oldGame.System;
            region_combobox.ItemsSource = regions;
            region_combobox.SelectedItem = oldGame.Region;
            region_combobox.PlaceholderText = oldGame.Region;
            ownership_combobox.ItemsSource = ownership;
            ownership_combobox.SelectedItem = oldGame.Ownership;
            ownership_combobox.PlaceholderText = oldGame.Ownership;
            distribution_combobox.ItemsSource = distribution;
            distribution_combobox.SelectedItem = oldGame.Distribution;
            distribution_combobox.PlaceholderText = oldGame.Distribution;
            if (oldGame.Status.Equals("Unplayed", StringComparison.Ordinal))
            {
                status_unplayed.IsChecked = true;
            }
            else if (oldGame.Status.Equals("Unfinished", StringComparison.Ordinal))
            {
                status_unfinished.IsChecked = true;
            }
            else if (oldGame.Status.Equals("Beaten", StringComparison.Ordinal))
            {
                status_beaten.IsChecked = true;
            }
            else if (oldGame.Status.Equals("Completed", StringComparison.Ordinal))
            {
                status_completed.IsChecked = true;
            }
            achievements1_textbox.Text = oldGame.Achievements1.ToString();
            achievements2_textbox.Text = oldGame.Achievements2.ToString();
            notes_textbox.Text = oldGame.Notes;
            if (oldGame.NowPlaying.Equals("Yes", StringComparison.Ordinal))
            {
                now_playing_checkbox.IsChecked = true;
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
            if (region_combobox.SelectedItem == null)
            {
                region_combobox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                region_combobox.BorderBrush = new SolidColorBrush(Colors.Silver);
                numberTrue++;
            }
            if (ownership_combobox.SelectedItem == null)
            {
                ownership_combobox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                ownership_combobox.BorderBrush = new SolidColorBrush(Colors.Silver);
                numberTrue++;
            }
            if (distribution_combobox.SelectedItem == null)
            {
                distribution_combobox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                distribution_combobox.BorderBrush = new SolidColorBrush(Colors.Silver);
                numberTrue++;
            }
            if (!(bool)status_unplayed.IsChecked && !(bool)status_unfinished.IsChecked && !(bool)status_beaten.IsChecked && !(bool)status_completed.IsChecked)
            {
                status_border.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                status_border.BorderBrush = new SolidColorBrush(Colors.White);
                numberTrue++;
            }
            return (numberTrue == 6);
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
                nowPlayingInput = "Yes";
            }
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
            StoreData();
        }

        private async void StoreData()
        {
            DataStorage ds = new DataStorage();
            await ds.DeserializeGameAsync();
            try
            {
                games = ds.Games;
                foreach (Game g in games)
                {
                    if (g.ID == game.ID)
                    {
                        games.Remove(g);
                        break;
                    }
                }
                games.Add(game);
                ds.Games = games;
                ds.SerializeGameAsync();
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

        private void Go_Back(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void SubmitMenu_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateEntries())
            {
                CreateGame();
                this.Frame.Navigate(typeof(MainPage));
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            game.ID = oldGame.ID;
            RemoveGame();
            this.Frame.Navigate(typeof(MainPage));
        }

        private async void RemoveGame()
        {
            DataStorage ds = new DataStorage();
            await ds.DeserializeGameAsync();
            try
            {
                games = ds.Games;
                foreach (Game g in games)
                {
                    if (g.ID == game.ID)
                    {
                        games.Remove(g);
                        break;
                    }
                }
                ds.Games = games;
                ds.SerializeGameAsync();
            }
            catch (Exception e)
            {
                //Debug.WriteLine(e.Message);
            }
        }
    }
}
