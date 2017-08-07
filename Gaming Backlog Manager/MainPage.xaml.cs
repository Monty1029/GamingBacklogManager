using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MyToolkit.Controls;
using System.Collections.ObjectModel;
using MyToolkit.Collections;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Gaming_Backlog_Manager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Game> games;
        private ObservableCollection<Game> gamesTemp;
        private List<Game> sortedGames;
        private Game gameClicked = new Game();

        public MainPage()
        {
            games = new ObservableCollection<Game>();
            this.InitializeComponent();
        }

        async protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            int gs = await GetSave();
        }

        private async Task<int> GetSave()
        {            
            DataStorage ds = new DataStorage();
            await ds.DeserializeGameAsync();
            gamesTemp = ds.Games;
            if (games != null)
            {
                sortedGames = new List<Game>(gamesTemp);
                sortedGames.Sort((x, y) => string.Compare(x.GameTitle, y.GameTitle));
                games.Clear();
                foreach (Game g in sortedGames)
                {
                    games.Add(g);
                }
            }
            return 0;
        }        

        private void Sort_GameTitle(object sender, RoutedEventArgs e)
        {
            if (games != null)
            {
                games.Clear();
                sortedGames.Sort((x, y) => string.Compare(x.GameTitle, y.GameTitle));
                foreach (Game g in sortedGames)
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
                foreach (Game g in sortedGames)
                {
                    games.Add(g);
                }
            }
        }

        private void Sort_Status(object sender, RoutedEventArgs e)
        {
            if (games != null)
            {
                games.Clear();
                sortedGames.Sort((x, y) => string.Compare(x.Status, y.Status));
                foreach (Game g in sortedGames)
                {
                    games.Add(g);
                }
            }
        }

        private void Sort_NowPlaying(object sender, RoutedEventArgs e)
        {
            if (games != null)
            {
                games.Clear();
                sortedGames.Sort((x, y) => string.Compare(x.NowPlaying, y.NowPlaying));
                foreach (Game g in sortedGames)
                {
                    games.Add(g);
                }
            }
        }

        private void Add_Game(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddGame));
        }

        private void Wishlist_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Wishlist));
        }

        private void MySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            gameClicked = (Game)e.AddedItems[0];
            this.Frame.Navigate(typeof(EditGame), gameClicked);
        }
    }
}
