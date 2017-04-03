using System;
using System.Collections.Generic;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Nonogram
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Credits : Page
    {

        public Credits()
        {
            this.InitializeComponent();
            textBlockCreditsOtherNames1.Text = "* Krzyżowka japońska" + Environment.NewLine + 
                                               "* Obrazek logiczny" + Environment.NewLine + 
                                               "* Nonogram" + Environment.NewLine +
                                               "* Griddlers" + Environment.NewLine +
                                               "* Pixel Puzzles" + Environment.NewLine +
                                               "* Logic square";
            
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            musicMenu.Volume = Ustawienia.glosnosc;
            musicMenu.AutoPlay = Ustawienia.dzwiek;
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
