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
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Nonogram
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Options : Page
    {
        private SolidColorBrush kolorTak = new SolidColorBrush(Colors.Black);
        private SolidColorBrush kolorNie = new SolidColorBrush(Colors.White);

        private double tymczasowaGlosnosc=1;

        public Options()
        {
            this.InitializeComponent();
            
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (Ustawienia.dzwiek)
            {
                btnWlaczDzwiek.Background = kolorNie;
                btnWlaczDzwiek.Foreground = kolorTak;
                btnWylaczDzwiek.Background = kolorTak;
                btnWylaczDzwiek.Foreground = kolorNie;

                sliderGlosnosc.Value = Ustawienia.glosnosc * 100;
                musicMenu.Volume = Ustawienia.glosnosc;
                musicMenu.AutoPlay = true;
            }
            else
            {
                btnWlaczDzwiek.Background = kolorTak;
                btnWlaczDzwiek.Foreground = kolorNie;
                btnWylaczDzwiek.Background = kolorNie;
                btnWylaczDzwiek.Foreground = kolorTak;
                musicMenu.AutoPlay = false;
            }
        }

        private void btnCredits_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Credits));
        }
        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void btnWlaczDzwiek_Click(object sender, RoutedEventArgs e)
        {
            Ustawienia.dzwiek = true;
            btnWlaczDzwiek.Background = kolorNie;
            btnWlaczDzwiek.Foreground = kolorTak;
            btnWylaczDzwiek.Background = kolorTak;
            btnWylaczDzwiek.Foreground = kolorNie;
            textBlockGlosnosc.Text = Ustawienia.glosnosc.ToString();
            sliderGlosnosc.Value = tymczasowaGlosnosc*100;
            Ustawienia.glosnosc = tymczasowaGlosnosc;
            musicMenu.Play();

        }

        private void btnWylaczDzwiek_Click(object sender, RoutedEventArgs e)
        {
            Ustawienia.dzwiek = false;
            btnWlaczDzwiek.Background = kolorTak;
            btnWlaczDzwiek.Foreground = kolorNie;
            btnWylaczDzwiek.Background = kolorNie;
            btnWylaczDzwiek.Foreground = kolorTak;
            tymczasowaGlosnosc = sliderGlosnosc.Value;
            sliderGlosnosc.Value = 0;
            textBlockGlosnosc.Text = "0";
            musicMenu.Pause();
        }

        private async void btnPomoc_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog msgbox;
            msgbox = new MessageDialog("Liczby z lewej strony każdego wiersza określają, ile grup czarnych pól jest w danym rzędzie i "  
                                     + "ile czarnych pól jest w każdej grupie. \n\nDla przykładu liczby „1 4 2” oznaczają trzy grupy: "
                                     + "\n* pierwsza jest złożona z jednego, \n * druga z czterech, \n* trzecia z dwóch czarnych pól. \n\nWyodrębnienie 3 kolejnych " 
                                     + "liczb świadczy o tym, że pomiędzy grupami czarnych pól występuje przynajmniej jedno wolne (białe) pole. "
                                     + "\n\nAnalogicznie jest z liczbami u góry diagramu.", "Instrukcje");
            await msgbox.ShowAsync();
        }

        private void sliderGlosnosc_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            double zero = 0;
            if (sliderGlosnosc.Value == zero)
            {
                Ustawienia.dzwiek = false;
                btnWlaczDzwiek.Background = kolorTak;
                btnWlaczDzwiek.Foreground = kolorNie;
                btnWylaczDzwiek.Background = kolorNie;
                btnWylaczDzwiek.Foreground = kolorTak;
            }
            else
            {
                Ustawienia.dzwiek = true;
                btnWlaczDzwiek.Background = kolorNie;
                btnWlaczDzwiek.Foreground = kolorTak;
                btnWylaczDzwiek.Background = kolorTak;
                btnWylaczDzwiek.Foreground = kolorNie;

                textBlockGlosnosc.Text = sliderGlosnosc.Value.ToString();
                Ustawienia.glosnosc = sliderGlosnosc.Value/100;
                musicMenu.Volume = Ustawienia.glosnosc;
            }
        }
    }
}

