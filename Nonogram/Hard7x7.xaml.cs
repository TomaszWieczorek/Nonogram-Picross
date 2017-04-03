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
    public sealed partial class Hard7x7 : Page
    {
        private int i, j; //licznik

        int[] wIlosc = new int[7]; //tablica 5 wymiarowa posiadajaca ilosc pol w kluczu w wierszu
        int[] kIlosc = new int[7]; //tablica 5 wymiarowa posiadajaca ilosc pol w kluczu w kolumnie
        private bool[,] btnKlikniety = new bool[7, 7]; //tablica reprezentujaca wszystkie przyciski (cos w stylu btn.isChecked())
        private bool[,] klucz = new bool[7, 7]; //tablica reprezentujaca klucz odpowiedzi bedacy rozwiazaniem


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



        public Hard7x7()
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
            if (!btnKlikniety[iL, jL])
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
                textInfo.Foreground = zielony;
                textInfo.FontWeight = FontWeights.ExtraBold;
                textInfo.Text = "Wygrałeś!";
                wygrana = true;
                #region wylaczanie buttonow
                btn00.IsEnabled = false;
                btn01.IsEnabled = false;
                btn02.IsEnabled = false;
                btn03.IsEnabled = false;
                btn04.IsEnabled = false;
                btn05.IsEnabled = false;
                btn06.IsEnabled = false;

                btn10.IsEnabled = false;
                btn11.IsEnabled = false;
                btn12.IsEnabled = false;
                btn13.IsEnabled = false;
                btn14.IsEnabled = false;
                btn15.IsEnabled = false;
                btn16.IsEnabled = false;

                btn20.IsEnabled = false;
                btn21.IsEnabled = false;
                btn22.IsEnabled = false;
                btn23.IsEnabled = false;
                btn24.IsEnabled = false;
                btn25.IsEnabled = false;
                btn26.IsEnabled = false;

                btn30.IsEnabled = false;
                btn31.IsEnabled = false;
                btn32.IsEnabled = false;
                btn33.IsEnabled = false;
                btn34.IsEnabled = false;
                btn35.IsEnabled = false;
                btn36.IsEnabled = false;

                btn40.IsEnabled = false;
                btn41.IsEnabled = false;
                btn42.IsEnabled = false;
                btn43.IsEnabled = false;
                btn44.IsEnabled = false;
                btn45.IsEnabled = false;
                btn46.IsEnabled = false;

                btn50.IsEnabled = false;
                btn51.IsEnabled = false;
                btn52.IsEnabled = false;
                btn53.IsEnabled = false;
                btn54.IsEnabled = false;
                btn55.IsEnabled = false;
                btn56.IsEnabled = false;

                btn60.IsEnabled = false;
                btn61.IsEnabled = false;
                btn62.IsEnabled = false;
                btn63.IsEnabled = false;
                btn64.IsEnabled = false;
                btn65.IsEnabled = false;
                btn66.IsEnabled = false;
                #endregion

            }
            #endregion
        }

        //obsluga przycisku Nowa Gra
        private void btnNowaGra_Click(object sender, RoutedEventArgs e)
        {
            //czyszczenie tablic posiadajacych informacje o grze, ustawianie false
            for (i = 0; i < 7; i++)
            {
                for (j = 0; j < 7; j++)
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
            btn05.Background = bialy;
            btn06.Background = bialy;

            btn10.Background = bialy;
            btn11.Background = bialy;
            btn12.Background = bialy;
            btn13.Background = bialy;
            btn14.Background = bialy;
            btn15.Background = bialy;
            btn16.Background = bialy;

            btn20.Background = bialy;
            btn21.Background = bialy;
            btn22.Background = bialy;
            btn23.Background = bialy;
            btn24.Background = bialy;
            btn25.Background = bialy;
            btn26.Background = bialy;

            btn30.Background = bialy;
            btn31.Background = bialy;
            btn32.Background = bialy;
            btn33.Background = bialy;
            btn34.Background = bialy;
            btn35.Background = bialy;
            btn36.Background = bialy;

            btn40.Background = bialy;
            btn41.Background = bialy;
            btn42.Background = bialy;
            btn43.Background = bialy;
            btn44.Background = bialy;
            btn45.Background = bialy;
            btn46.Background = bialy;

            btn50.Background = bialy;
            btn51.Background = bialy;
            btn52.Background = bialy;
            btn53.Background = bialy;
            btn54.Background = bialy;
            btn55.Background = bialy;
            btn56.Background = bialy;

            btn60.Background = bialy;
            btn61.Background = bialy;
            btn62.Background = bialy;
            btn63.Background = bialy;
            btn64.Background = bialy;
            btn65.Background = bialy;
            btn66.Background = bialy;
            #endregion

            #region wlaczenie buttonow
            btn00.IsEnabled = true;
            btn01.IsEnabled = true;
            btn02.IsEnabled = true;
            btn03.IsEnabled = true;
            btn04.IsEnabled = true;
            btn05.IsEnabled = true;
            btn06.IsEnabled = true;

            btn10.IsEnabled = true;
            btn11.IsEnabled = true;
            btn12.IsEnabled = true;
            btn13.IsEnabled = true;
            btn14.IsEnabled = true;
            btn15.IsEnabled = true;
            btn16.IsEnabled = true;

            btn20.IsEnabled = true;
            btn21.IsEnabled = true;
            btn22.IsEnabled = true;
            btn23.IsEnabled = true;
            btn24.IsEnabled = true;
            btn25.IsEnabled = true;
            btn26.IsEnabled = true;

            btn30.IsEnabled = true;
            btn31.IsEnabled = true;
            btn32.IsEnabled = true;
            btn33.IsEnabled = true;
            btn34.IsEnabled = true;
            btn35.IsEnabled = true;
            btn36.IsEnabled = true;

            btn40.IsEnabled = true;
            btn41.IsEnabled = true;
            btn42.IsEnabled = true;
            btn43.IsEnabled = true;
            btn44.IsEnabled = true;
            btn45.IsEnabled = true;
            btn46.IsEnabled = true;

            btn50.IsEnabled = true;
            btn51.IsEnabled = true;
            btn52.IsEnabled = true;
            btn53.IsEnabled = true;
            btn54.IsEnabled = true;
            btn55.IsEnabled = true;
            btn56.IsEnabled = true;

            btn60.IsEnabled = true;
            btn61.IsEnabled = true;
            btn62.IsEnabled = true;
            btn63.IsEnabled = true;
            btn64.IsEnabled = true;
            btn65.IsEnabled = true;
            btn66.IsEnabled = true;
            #endregion

            #region ulatwienie
            #region czyszczenie
            rctK0.Fill = bialy;
            rctK1.Fill = bialy;
            rctK2.Fill = bialy;
            rctK3.Fill = bialy;
            rctK4.Fill = bialy;
            rctK5.Fill = bialy;
            rctK6.Fill = bialy;

            rctW0.Fill = bialy;
            rctW1.Fill = bialy;
            rctW2.Fill = bialy;
            rctW3.Fill = bialy;
            rctW4.Fill = bialy;
            rctW5.Fill = bialy;
            rctW6.Fill = bialy;
            #endregion

            #region ukrywanie
            rctK0.Visibility = Visibility.Collapsed;
            rctK1.Visibility = Visibility.Collapsed;
            rctK2.Visibility = Visibility.Collapsed;
            rctK3.Visibility = Visibility.Collapsed;
            rctK4.Visibility = Visibility.Collapsed;
            rctK5.Visibility = Visibility.Collapsed;
            rctK6.Visibility = Visibility.Collapsed;

            rctW0.Visibility = Visibility.Collapsed;
            rctW1.Visibility = Visibility.Collapsed;
            rctW2.Visibility = Visibility.Collapsed;
            rctW3.Visibility = Visibility.Collapsed;
            rctW4.Visibility = Visibility.Collapsed;
            rctW5.Visibility = Visibility.Collapsed;
            rctW6.Visibility = Visibility.Collapsed;
            #endregion
            #endregion

            #endregion

            iloscOdgadnietych = 0; //czyszczenie ilosci odgadnietych przez gracza
            textInfo.Foreground = czerwony;
            textInfo.FontSize = 40;
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
                if (iloscGracza < 20 || iloscGracza > 40) //sprawdzamy czy uzytkownik podal liczbe z przedzialu
                    ilosc = rnd.Next(30, 40); //jesli nie to ilosc pol w kluczu z przedzialu od 30(60%) do 40(80%)
                else
                    ilosc = iloscGracza; //jesli tak to ilosc gracza
            }
            else
            {
                ilosc = rnd.Next(30, 40); //ilosc pol w kluczu z przedzialu od 30(60%) do 40(80%)
            }
            i = 0;
            do
            {
                int randomI = rnd.Next(0, 7);
                int randomJ = rnd.Next(0, 7);
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
            string[] opis = new string[14]; //tablica reprezentujaca 12 opisow, 0-5 wiersze, 6-11 kolumny

            #region petla wypelniajaca opisy
            for (i = 0; i < 7; i++)
            {
                iloscWwierszu = "";
                iloscWkolumnie = "";
                liczK = 0;
                liczW = 0;
                for (j = 0; j < 7; j++)
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

                    if (klucz[j, i])
                        liczK++;
                    else
                    {
                        if (liczK > 0)
                        {
                            iloscWkolumnie += liczK.ToString() + Environment.NewLine;
                            liczK = 0;
                        }

                    }

                }
                if (liczW > 0)
                    iloscWwierszu += liczW.ToString() + " ";
                if (liczK > 0)
                    iloscWkolumnie += liczK.ToString() + Environment.NewLine;
                opis[i] = iloscWwierszu;
                opis[i + 7] = iloscWkolumnie;
            }
            #endregion

            #region wpisanie kodu liter do obramowania
            for (i = 0; i < 14; i++)
                if (opis[i] == "") opis[i] = "0";

            #region Kolor czcionki biały
            textW0.Foreground = bialy;
            textW1.Foreground = bialy;
            textW2.Foreground = bialy;
            textW3.Foreground = bialy;
            textW4.Foreground = bialy;
            textW5.Foreground = bialy;
            textW6.Foreground = bialy;

            textK0.Foreground = bialy;
            textK1.Foreground = bialy;
            textK2.Foreground = bialy;
            textK3.Foreground = bialy;
            textK4.Foreground = bialy;
            textK5.Foreground = bialy;
            textK6.Foreground = bialy;
            #endregion

            textW0.Text = opis[0];
            textW1.Text = opis[1];
            textW2.Text = opis[2];
            textW3.Text = opis[3];
            textW4.Text = opis[4];
            textW5.Text = opis[5];
            textW6.Text = opis[6];

            textK0.Text = opis[7];
            textK1.Text = opis[8];
            textK2.Text = opis[9];
            textK3.Text = opis[10];
            textK4.Text = opis[11];
            textK5.Text = opis[12];
            textK6.Text = opis[13];
            #endregion
        }

        //kod obslugi ulatwienia
        private void ulatwienie()
        {
            bool[] ulatwienieCzyOk = new bool[14]; //tablica reprezentujaca kwadraty przy ulatwieniach, 0-5 wiersze, 6-11 kolumny
            int kk, ww; //liczniki
            bool czyOkW, czyOkK; //zmienne sprawdzajace czy w rzedzie sie zgadzaja
            for (ww = 0; ww < 7; ww++)
            {
                //ustawienie wartosci bool na true
                czyOkW = true;
                czyOkK = true;
                ulatwienieCzyOk[ww] = true;
                ulatwienieCzyOk[ww + 7] = true;
                //podczas przejscia przez petla dowiadujemy sie czy w wierszu/kolumnie sa odpowiednie zaznaczone
                for (kk = 0; kk < 7; kk++)
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
                if (czyOkK) ulatwienieCzyOk[ww + 7] = true;
                else ulatwienieCzyOk[ww + 7] = false;
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
            #region Wiersz 6
            if (ulatwienieCzyOk[5])
            {
                rctW5.Fill = zielony;
            }
            else
            {
                rctW5.Fill = bialy;
            }
            #endregion
            #region Wiersz 7
            if (ulatwienieCzyOk[6])
            {
                rctW6.Fill = zielony;
            }
            else
            {
                rctW6.Fill = bialy;
            }
            #endregion

            #region Kolumna 1
            if (ulatwienieCzyOk[7])
            {
                rctK0.Fill = zielony;
            }
            else
            {
                rctK0.Fill = bialy;
            }
            #endregion
            #region Kolumna 2
            if (ulatwienieCzyOk[8])
            {
                rctK1.Fill = zielony;
            }
            else
            {
                rctK1.Fill = bialy;
            }
            #endregion
            #region Kolumna 3
            if (ulatwienieCzyOk[9])
            {
                rctK2.Fill = zielony;
            }
            else
            {
                rctK2.Fill = bialy;
            }
            #endregion
            #region Kolumna 4
            if (ulatwienieCzyOk[10])
            {
                rctK3.Fill = zielony;
            }
            else
            {
                rctK3.Fill = bialy;
            }
            #endregion
            #region Kolumna 5
            if (ulatwienieCzyOk[11])
            {
                rctK4.Fill = zielony;
            }
            else
            {
                rctK4.Fill = bialy;
            }
            #endregion
            #region Kolumna 6
            if (ulatwienieCzyOk[12])
            {
                rctK5.Fill = zielony;
            }
            else
            {
                rctK5.Fill = bialy;
            }
            #endregion
            #region Kolumna 7
            if (ulatwienieCzyOk[13])
            {
                rctK6.Fill = zielony;
            }
            else
            {
                rctK6.Fill = bialy;
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
            rctK5.Visibility = Visibility.Visible;
            rctK6.Visibility = Visibility.Visible;

            rctW0.Visibility = Visibility.Visible;
            rctW1.Visibility = Visibility.Visible;
            rctW2.Visibility = Visibility.Visible;
            rctW3.Visibility = Visibility.Visible;
            rctW4.Visibility = Visibility.Visible;
            rctW5.Visibility = Visibility.Visible;
            rctW6.Visibility = Visibility.Visible;
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
                rctK5.Fill = bialy;
                rctK6.Fill = bialy;

                rctW0.Fill = bialy;
                rctW1.Fill = bialy;
                rctW2.Fill = bialy;
                rctW3.Fill = bialy;
                rctW4.Fill = bialy;
                rctW5.Fill = bialy;
                rctW6.Fill = bialy;
            }
            #endregion

            #region liczymy ilosc odpowiedzi k kluczu w danych wierszu i kolumnie
            for (i = 0; i < 7; i++)
            {
                for (j = 0; j < 7; j++)
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
            rctK5.Visibility = Visibility.Collapsed;
            rctK6.Visibility = Visibility.Collapsed;

            rctW0.Visibility = Visibility.Collapsed;
            rctW1.Visibility = Visibility.Collapsed;
            rctW2.Visibility = Visibility.Collapsed;
            rctW3.Visibility = Visibility.Collapsed;
            rctW4.Visibility = Visibility.Collapsed;
            rctW5.Visibility = Visibility.Collapsed;
            rctW6.Visibility = Visibility.Collapsed;
            #endregion

            #region wlaczanie buttonow
            if (!wygrana)
            {
                btn00.IsEnabled = true;
                btn01.IsEnabled = true;
                btn02.IsEnabled = true;
                btn03.IsEnabled = true;
                btn04.IsEnabled = true;
                btn05.IsEnabled = true;
                btn06.IsEnabled = true;

                btn10.IsEnabled = true;
                btn11.IsEnabled = true;
                btn12.IsEnabled = true;
                btn13.IsEnabled = true;
                btn14.IsEnabled = true;
                btn15.IsEnabled = true;
                btn16.IsEnabled = true;

                btn20.IsEnabled = true;
                btn21.IsEnabled = true;
                btn22.IsEnabled = true;
                btn23.IsEnabled = true;
                btn24.IsEnabled = true;
                btn25.IsEnabled = true;
                btn26.IsEnabled = true;

                btn30.IsEnabled = true;
                btn31.IsEnabled = true;
                btn32.IsEnabled = true;
                btn33.IsEnabled = true;
                btn34.IsEnabled = true;
                btn35.IsEnabled = true;
                btn36.IsEnabled = true;

                btn40.IsEnabled = true;
                btn41.IsEnabled = true;
                btn42.IsEnabled = true;
                btn43.IsEnabled = true;
                btn44.IsEnabled = true;
                btn45.IsEnabled = true;
                btn46.IsEnabled = true;

                btn50.IsEnabled = true;
                btn51.IsEnabled = true;
                btn52.IsEnabled = true;
                btn53.IsEnabled = true;
                btn54.IsEnabled = true;
                btn55.IsEnabled = true;
                btn56.IsEnabled = true;

                btn60.IsEnabled = true;
                btn61.IsEnabled = true;
                btn62.IsEnabled = true;
                btn63.IsEnabled = true;
                btn64.IsEnabled = true;
                btn65.IsEnabled = true;
                btn66.IsEnabled = true;
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
            msgbox = new MessageDialog("Podaj liczbę z przedziału od 20 do 49!\nJeśli podasz z poza przedziału lub w ogóle nie podasz zostanie automatycznie wylosowana z przedziału (30~40)!");
            await msgbox.ShowAsync();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            musicMenu.Volume = Ustawienia.glosnosc;
            musicMenu.AutoPlay = Ustawienia.dzwiek;
        }
    }
}
