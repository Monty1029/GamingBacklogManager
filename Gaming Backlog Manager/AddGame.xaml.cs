using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                "PlayStation 4", "PC", "3DS", "Nintendo Switch", "Xbox One", "iOS", "Wii U", "Android", "PlayStation Vita", "Windows Mobile"
            };

        private ObservableCollection<string> regions = new ObservableCollection<string>
            {
                "North America", "Europe", "Japan", "Korea", "China"
            };

        private ObservableCollection<string> ownership = new ObservableCollection<string>
            {
                "Owned", "Subscription", "Borrowed/Rented"
            };

        public AddGame()
        {   
            this.InitializeComponent();

        }
    }
}
