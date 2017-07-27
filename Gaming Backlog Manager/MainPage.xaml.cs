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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Gaming_Backlog_Manager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Game> games = new ObservableCollection<Game>();
        private ObservableCollectionView<Game> view;

        public MainPage()
        {
            this.InitializeComponent();
            view = new ObservableCollectionView<Game>(games);
            view.Order = g => g.GameTitle;
            getSave();
        }

        private async void getSave()
        {
            DataStorage ds = new DataStorage();
            await ds.DeserializeGameAsync();
            games = ds.Games;
            List<Game> sortedByGameTitle = new List<Game>(games);
            sortedByGameTitle.Sort((x, y) => string.Compare(x.GameTitle, y.GameTitle));
            games = new ObservableCollection<Game>(sortedByGameTitle);
        }

        private void Add_Game(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddGame));
        }        
    }
}
