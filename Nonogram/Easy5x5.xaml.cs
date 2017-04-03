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
using Windows.UI.Text;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Nonogram
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Easy5x5 : Page
    {

        private int i, j; //licznik

        int[] wIlosc = new int[5]; //tablica 5 wymiarowa posiadajaca ilosc pol w kluczu w wierszu
        int[] kIlosc = new int[5]; //tablica 5 wymiarowa posiadajaca ilosc pol w kluczu w kolumnie
        private bool[,] btnKlikniety = new bool[5, 5]; //tablica reprezentujaca wszystkie przyciski (cos w stylu btn.isChecked())
        private bool[,] klucz = new bool[5, 5]; //tablica reprezentujaca klucz odpowiedzi bedacy rozwiazaniem


        private int ilosc = 0; //ilosc pol w kluczu odpowiedz
        private int iloscOdgadnietych = 0; //ilosc pol ktore odgadnal gracz

        private bool wygrana = false; //zmienna reprezentujaca stan rozgrywki
        private bool czyUlatwienieWlaczone = false; //zmienna bool mowiaca czy ulatwienie jet wlaczone
        private Visibility widocznoscTextGora = Visibility.Collapsed; //widocznosc podpowiedzi

        //kolory, aby łatwiej było operować na nich
        private SolidColorBrush bialy = new SolidColorBrush(Colors.White);
        private SolidColorBrush czarny = new SolidColorBrush(Colors.Black);
        private SolidColorBrush czerwony = new SolidColorBrush(Colors.Red);
        private SolidColorBrush zielony = new SolidColorBrush(Colors.Green);



        public Easy5x5()
        {
            this.InitializeComponent();
            textInfo.Text = "Aby rozpocząć wciśnij Nowa Gra!";

            //widocznosc dodatkowych pol podpowiedz
            textBox.Visibility = widocznoscTextGora; 
            textBox1.Visibility = widocznoscTextGora;

            
        }

        //obsluga klikniecia
        private void button_Click(object sender, RoutedEventArgs e)
        {
            //wyciagniecie wspolrzednych buttona z nazwy buttona
            Button btn = sender as Button;
            string nazwa = btn.Name;
            //oraz przypisanie ich do zmiennych
            string iS = nazwa.Substring(3, 1);
            int iL = Convert.ToInt32(iS);
            string jS = nazwa.Substring(4, 1);
            int jL = Convert.ToInt32(jS);

            //jesli nie jest klikniety to
            if(!btnKlikniety[iL,jL])
            {
                btnKlikniety[iL, jL] = true; //ustawiamy w tablicy na to ze jest klikniety
                btn.Background = czarny; //tlo na czarny
                if (klucz[iL, jL]) iloscOdgadnietych++; else iloscOdgadnietych--; //jesli zgadza sie z kluczem to dodajemy ilosc zgadnietych, jesli nie to odejmujemy
            }
            else
            {
                btnKlikniety[iL, jL] = false; //ustawiamy w tablicy na to ze jest nie klikniety
                btn.Background = bialy; //tlo na bialy
                if (klucz[iL, jL]) iloscOdgadnietych--; else iloscOdgadnietych++; //jesli zgadza sie z kluczem to odejmujemy ilosc zgadnietych, jesli nie to dodajemy
            }

            if (checkUlatwienie.IsChecked == true) ulatwienie();

            Sprawdz();
        }

        //sprawdzanie czy wygrana
        private void Sprawdz()
        {
            textBox.Text = iloscOdgadnietych.ToString();
            textInfo.Text = "Gra trwa!";
            #region Wygrana
            if (iloscOdgadnietych == ilosc)
            {
                wygrana = true;
                textInfo.FontSize = 40;
                textInfo.Foreground = zielony;
                textInfo.FontWeight = FontWeights.ExtraBold;
                textInfo.Text = "Wygrałeś!";

                #region wylaczanie buttonow
                    btn00.IsEnabled = false;
                    btn01.IsEnabled = false;
                    btn02.IsEnabled = false;
                    btn03.IsEnabled = false;
                    btn04.IsEnabled = false;

                    btn10.IsEnabled = false;
                    btn11.IsEnabled = false;
                    btn12.IsEnabled = false;
                    btn13.IsEnabled = false;
                    btn14.IsEnabled = false;

                    btn20.IsEnabled = false;
                    btn21.IsEnabled = false;
                    btn22.IsEnabled = false;
                    btn23.IsEnabled = false;
                    btn24.IsEnabled = false;

                    btn30.IsEnabled = false;
                    btn31.IsEnabled = false;
                    btn32.IsEnabled = false;
                    btn33.IsEnabled = false;
                    btn34.IsEnabled = false;

                    btn40.IsEnabled = false;
                    btn41.IsEnabled = false;
                    btn42.IsEnabled = false;
                    btn43.IsEnabled = false;
                    btn44.IsEnabled = false;
                #endregion

            }
            #endregion
        }

        //obsluga przycisku Nowa Gra
        private void btnNowaGra_Click(object sender, RoutedEventArgs e)
        {
            //czyszczenie tablic posiadajacych informacje o grze, ustawianie false
            for (i = 0; i < 5; i++)
            {
                for (j = 0; j < 5; j++)
                {
                    btnKlikniety[i, j] = false;
                    klucz[i, j] = false;
                }
            }
            #region CzyszczenieButtonow
                #region kolor bialy
                btn00.Background = bialy;
                btn01.Background = bialy;
                btn02.Background = bialy;
                btn03.Background = bialy;
                btn04.Background = bialy;

                btn10.Background = bialy;
                btn11.Background = bialy;
                btn12.Background = bialy;
                btn13.Background = bialy;
                btn14.Background = bialy;

                btn20.Background = bialy;
                btn21.Background = bialy;
                btn22.Background = bialy;
                btn23.Background = bialy;
                btn24.Background = bialy;

                btn30.Background = bialy;
                btn31.Background = bialy;
                btn32.Background = bialy;
                btn33.Background = bialy;
                btn34.Background = bialy;

                btn40.Background = bialy;
                btn41.Background = bialy;
                btn42.Background = bialy;
                btn43.Background = bialy;
                btn44.Background = bialy;
            #endregion

                #region wlaczenie buttonow
                btn00.IsEnabled = true;
                btn01.IsEnabled = true;
                btn02.IsEnabled = true;
                btn03.IsEnabled = true;
                btn04.IsEnabled = true;

                btn10.IsEnabled = true;
                btn11.IsEnabled = true;
                btn12.IsEnabled = true;
                btn13.IsEnabled = true;
                btn14.IsEnabled = true;

                btn20.IsEnabled = true;
                btn21.IsEnabled = true;
                btn22.IsEnabled = true;
                btn23.IsEnabled = true;
                btn24.IsEnabled = true;

                btn30.IsEnabled = true;
                btn31.IsEnabled = true;
                btn32.IsEnabled = true;
                btn33.IsEnabled = true;
                btn34.IsEnabled = true;

                btn40.IsEnabled = true;
                btn41.IsEnabled = true;
                btn42.IsEnabled = true;
                btn43.IsEnabled = true;
                btn44.IsEnabled = true;
            #endregion

                #region ulatwienie
                    #region czyszczenie
                    rctK0.Fill = bialy;
                    rctK1.Fill = bialy;
                    rctK2.Fill = bialy;
                    rctK3.Fill = bialy;
                    rctK4.Fill = bialy;

                    rctW0.Fill = bialy;
                    rctW1.Fill = bialy;
                    rctW2.Fill = bialy;
                    rctW3.Fill = bialy;
                    rctW4.Fill = bialy;
                    #endregion

                    #region ukrywanie
                    rctK0.Visibility = Visibility.Collapsed;
                    rctK1.Visibility = Visibility.Collapsed;
                    rctK2.Visibility = Visibility.Collapsed;
                    rctK3.Visibility = Visibility.Collapsed;
                    rctK4.Visibility = Visibility.Collapsed;

                    rctW0.Visibility = Visibility.Collapsed;
                    rctW1.Visibility = Visibility.Collapsed;
                    rctW2.Visibility = Visibility.Collapsed;
                    rctW3.Visibility = Visibility.Collapsed;
                    rctW4.Visibility = Visibility.Collapsed;
                    #endregion
                #endregion

            #endregion

            iloscOdgadnietych = 0; //czyszczenie ilosci odgadnietych przez gracza
            textInfo.FontSize = 40;
            textInfo.Foreground = czerwony;
            textInfo.FontWeight = FontWeights.Normal;
            textInfo.Text = "Gra trwa!";
            losowanie(); //losowanie klucza
            textBox1.Text = ilosc.ToString(); //podpowiedz
            kod_liter(); //wypelnianie opisu pol
            checkUlatwienie.IsEnabled = true;
            if (checkUlatwienie.IsChecked == true) checkUlatwienie.IsChecked = false; //wylaczanie ulatwienia
        }

        //losowanie klucza
        private void losowanie()
        {
            Random rnd = new Random();
            
            if (textIlosc.Text != "") //sprawdzamy czy uzytkownik wpisal liczbe, jesli nie jest losowana z przedzialu
            {
                int iloscGracza;
                iloscGracza = Convert.ToInt32(textIlosc.Text);
                if (iloscGracza < 5 || iloscGracza > 20) //sprawdzamy czy uzytkownik podal liczbe z przedzialu
                    ilosc = rnd.Next(10, 21); //jesli nie to ilosc pol w kluczu z przedzialu od 10(40%) do 20(80%)
                else
                    ilosc = iloscGracza; //jesli tak to ilosc gracza
            }
            else
            {
                ilosc = rnd.Next(10, 21); //ilosc pol w kluczu z przedzialu od 10(40%) do 20(80%)
            }
            i = 0;
            do
            {
                int randomI = rnd.Next(0, 5); 
                int randomJ = rnd.Next(0, 5);
                if (!klucz[randomI, randomJ]) //sprawdzamy czy pole nie zostalo juz wybrane
                {
                    klucz[randomI, randomJ] = true; //przypisujemy wartosc true, czyli znajduje sie w kluczu
                    i++;
                }
            } while (i < ilosc);
        }

        //kod pokazujacy opis wierszow i kolumn
        private void kod_liter()
        {
            string iloscWwierszu, iloscWkolumnie; //string odpowiadajace za opis wierszy i kolumn
            int liczW, liczK; //liczniki 
            string[] opis = new string[10]; //tablica reprezentujaca 10 opisow, 0-4 wiersze, 5-9 kolumny

            #region petla wypelniajaca opisy
            for (i=0;i<5;i++)
            {
                iloscWwierszu = "";
                iloscWkolumnie = "";
                liczK = 0;
                liczW = 0;
                for(j=0;j<5;j++)
                {
                    
                    if (klucz[i, j])
                        liczW++;
                    else
                    {
                        if (liczW > 0)
                        {
                            iloscWwierszu += liczW.ToString() + " ";
                            liczW = 0;
                        }
                    }

                    if (klucz[j,i])
                        liczK++;
                    else
                    {
                        if (liczK> 0)
                        {
                            iloscWkolumnie+=liczK.ToString() + Environment.NewLine;
                            liczK = 0;
                        }

                    }

                }
                if (liczW > 0)
                    iloscWwierszu += liczW.ToString() + " ";
                if (liczK > 0)
                    iloscWkolumnie += liczK.ToString() + Environment.NewLine;
                opis[i]=iloscWwierszu;
                opis[i + 5] = iloscWkolumnie;
            }
            #endregion

            #region wpisanie kodu liter do obramowania
            for (i = 0; i < 10; i++)
                if (opis[i] == "") opis[i] = "0";

            #region Kolor czcionki biały
            textW0.Foreground = bialy;
            textW1.Foreground = bialy;
            textW2.Foreground = bialy;
            textW3.Foreground = bialy;
            textW4.Foreground = bialy;

            textK0.Foreground = bialy;
            textK1.Foreground = bialy;
            textK2.Foreground = bialy;
            textK3.Foreground = bialy;
            textK4.Foreground = bialy;
            #endregion

            textW0.Text = opis[0];
            textW1.Text = opis[1];
            textW2.Text = opis[2];
            textW3.Text = opis[3];
            textW4.Text = opis[4];

            textK0.Text = opis[5];
            textK1.Text = opis[6];
            textK2.Text = opis[7];
            textK3.Text = opis[8];
            textK4.Text = opis[9];
            #endregion
        }

        //kod obslugi ulatwienia
        private void ulatwienie()
        {
            bool[] ulatwienieCzyOk = new bool[10]; //tablica reprezentujaca kwadraty przy ulatwieniach, 0-4 wiersze, 5-9 kolumny
            int kk, ww; //liczniki
            bool czyOkW, czyOkK; //zmienne sprawdzajace czy w rzedzie sie zgadzaja
            for (ww = 0; ww < 5; ww++)
            {
                //ustawienie wartosci bool na true
                czyOkW = true;
                czyOkK = true;
                ulatwienieCzyOk[ww] = true;
                ulatwienieCzyOk[ww + 5] = true;
                //podczas przejscia przez petla dowiadujemy sie czy w wierszu/kolumnie sa odpowiednie zaznaczone
                for (kk = 0; kk < 5; kk++)
                {
                    if (btnKlikniety[ww, kk] != klucz[ww, kk])
                    {
                        czyOkW = false;
                    }
                    if (btnKlikniety[kk, ww] != klucz[kk, ww])
                    {
                        czyOkK = false;
                    }
                }
                //ustawienie wartosci w tablicy zaleznie od tego czy sa odpowiednie zaznaczone
                if (czyOkW) ulatwienieCzyOk[ww] = true;
                    else ulatwienieCzyOk[ww] = false;
                if (czyOkK) ulatwienieCzyOk[ww + 5] = true;
                    else ulatwienieCzyOk[ww + 5] = false;
            }

            #region ulatwienie wypełnianie kolorem

            #region Wiersz 1
            if (ulatwienieCzyOk[0])
            {
                rctW0.Fill = zielony;
            }
            else
            {
                rctW0.Fill = bialy;
            }
            #endregion
            #region Wiersz 2
            if (ulatwienieCzyOk[1])
            {
                rctW1.Fill = zielony;
            }
            else
            {
                rctW1.Fill = bialy;
            }
            #endregion
            #region Wiersz 3
            if (ulatwienieCzyOk[2])
            {
                rctW2.Fill = zielony;
            }
            else
            {
                rctW2.Fill = bialy;
            }
            #endregion
            #region Wiersz 4
            if (ulatwienieCzyOk[3])
            {
                rctW3.Fill = zielony;
            }
            else
            {
                rctW3.Fill = bialy;
            }
            #endregion
            #region Wiersz 5
            if (ulatwienieCzyOk[4])
            {
                rctW4.Fill = zielony;
            }
            else
            {
                rctW4.Fill = bialy;
            }
            #endregion

            #region Kolumna 1
            if (ulatwienieCzyOk[5])
            {
                rctK0.Fill = zielony;
            }
            else
            {
                rctK0.Fill = bialy;
            }
            #endregion
            #region Kolumna 2
            if (ulatwienieCzyOk[6])
            {
                rctK1.Fill = zielony;
            }
            else
            {
                rctK1.Fill = bialy;
            }
            #endregion
            #region Kolumna 3
            if (ulatwienieCzyOk[7])
            {
                rctK2.Fill = zielony;
            }
            else
            {
                rctK2.Fill = bialy;
            }
            #endregion
            #region Kolumna 4
            if (ulatwienieCzyOk[8])
            {
                rctK3.Fill = zielony;
            }
            else
            {
                rctK3.Fill = bialy;
            }
            #endregion
            #region Kolumna 5
            if (ulatwienieCzyOk[9])
            {
                rctK4.Fill = zielony;
            }
            else
            {
                rctK4.Fill = bialy;
            }
            #endregion

            #endregion
        }

        //zaznaczenie ulatwienia
        private void checkUlatwienie_Checked(object sender, RoutedEventArgs e)
        {
            #region widocznosc podpowiedzi
            rctK0.Visibility = Visibility.Visible;
            rctK1.Visibility = Visibility.Visible;
            rctK2.Visibility = Visibility.Visible;
            rctK3.Visibility = Visibility.Visible;
            rctK4.Visibility = Visibility.Visible;
            rctW0.Visibility = Visibility.Visible;
            rctW1.Visibility = Visibility.Visible;
            rctW2.Visibility = Visibility.Visible;
            rctW3.Visibility = Visibility.Visible;
            rctW4.Visibility = Visibility.Visible;
            #endregion

            #region czy czyscimy podpowiedzi
            if (!czyUlatwienieWlaczone)
            {
                czyUlatwienieWlaczone = true;
                rctK0.Fill = bialy;
                rctK1.Fill = bialy;
                rctK2.Fill = bialy;
                rctK3.Fill = bialy;
                rctK4.Fill = bialy;
                rctW0.Fill = bialy;
                rctW1.Fill = bialy;
                rctW2.Fill = bialy;
                rctW3.Fill = bialy;
                rctW4.Fill = bialy;
            }
            #endregion
            
            #region liczymy ilosc odpowiedzi k kluczu w danych wierszu i kolumnie
            for (i = 0; i < 5; i++)
            {
                for (j = 0; j < 5; j++)
                {
                    if (klucz[i, j] == true)
                    {
                        wIlosc[i] += 1;
                        kIlosc[j] += 1;
                    }
                }
            }
            #endregion

            ulatwienie();
            
        }

        //odznaczenie ulatwienia
        private void checkUlatwienie_Unchecked(object sender, RoutedEventArgs e)
        {
            #region ukrywanie podpowiedzi
            rctK0.Visibility = Visibility.Collapsed;
            rctK1.Visibility = Visibility.Collapsed;
            rctK2.Visibility = Visibility.Collapsed;
            rctK3.Visibility = Visibility.Collapsed;
            rctK4.Visibility = Visibility.Collapsed;

            rctW0.Visibility = Visibility.Collapsed;
            rctW1.Visibility = Visibility.Collapsed;
            rctW2.Visibility = Visibility.Collapsed;
            rctW3.Visibility = Visibility.Collapsed;
            rctW4.Visibility = Visibility.Collapsed;
            #endregion
            
            #region wlaczanie buttonow
            if (!wygrana)
            {
                btn00.IsEnabled = true;
                btn01.IsEnabled = true;
                btn02.IsEnabled = true;
                btn03.IsEnabled = true;
                btn04.IsEnabled = true;

                btn10.IsEnabled = true;
                btn11.IsEnabled = true;
                btn12.IsEnabled = true;
                btn13.IsEnabled = true;
                btn14.IsEnabled = true;

                btn20.IsEnabled = true;
                btn21.IsEnabled = true;
                btn22.IsEnabled = true;
                btn23.IsEnabled = true;
                btn24.IsEnabled = true;

                btn30.IsEnabled = true;
                btn31.IsEnabled = true;
                btn32.IsEnabled = true;
                btn33.IsEnabled = true;
                btn34.IsEnabled = true;

                btn40.IsEnabled = true;
                btn41.IsEnabled = true;
                btn42.IsEnabled = true;
                btn43.IsEnabled = true;
                btn44.IsEnabled = true;
            }
            #endregion
        }

        //przycisk powrotu do menu
        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        //wyswietalnie okienka o ilosc
        private async void btnIlosc_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog msgbox;
            msgbox = new MessageDialog("Podaj liczbę z przedziału od 5 do 20!\nJeśli podasz z poza przedziału lub w ogóle nie podasz zostanie automatycznie wylosowana z przedziału (10~20)!");
            await msgbox.ShowAsync();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            musicMenu.Volume = Ustawienia.glosnosc;
            musicMenu.AutoPlay = Ustawienia.dzwiek;
        }
    }
}
