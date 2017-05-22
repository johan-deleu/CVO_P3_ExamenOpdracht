using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ServiceModel;
using Bibliotheek.DataModel;
using Bibliotheek.Service;

namespace Bibliotheek.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var binding = new BasicHttpBinding();
            var boekServiceEndpointAddress = new EndpointAddress("http://localhost:54398/BoekService.svc");
            var genreServiceEndpointAddress = new EndpointAddress("http://localhost:54398/GenreService.svc");

            var boekChannelFactory = new ChannelFactory<IBoekService>(binding, boekServiceEndpointAddress);
            var genreChannelFactory = new ChannelFactory<IGenreService>(binding, genreServiceEndpointAddress);
            var boekClient = boekChannelFactory.CreateChannel();
            var genreClient = genreChannelFactory.CreateChannel();

            //VulGenreListBox();
            ToonAlleBoekenAsync(boekClient);

        }
        private void WisAlleVelden()
        {
            txtTitel.Clear();
            txtAuteur.Clear();
            txtPaginas.Clear();
            lstGenre.SelectedItems.Clear();
            lstDatabase.SelectedItem = null;
        }

        private async void ToonAlleBoekenAsync(IBoekService boekClient)
        {
            lstDatabase.Items.Clear();
            //List<Boek> lstBoekenVanDb = new List<Boek>();
            Boek[] lstBoekenVanDb;
            //lstBoekenVanDb = dbAccess.OphalenBoeken();
            lstBoekenVanDb = await boekClient.OphalenBoekenAsync();

            foreach (var boek in lstBoekenVanDb)
            {
                lstDatabase.Items.Add(boek);
            }
        }


        private void VulGenreListBox()
        {
            //lstGenre.Items.Clear();
            //List<Genre> lstGenresVanDb = new List<Genre>();

            //lstGenresVanDb = dbAccess.OphalenGenres();
            //foreach (var genre in lstGenresVanDb)
            //{
            //    lstGenre.Items.Add(genre);
            //}
        }

        /// <summary>
        /// Voegt een boek toe aan de databank op basis van de ingevulde velden en geselecteerde genres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBoekToevoegen_Click(object sender, RoutedEventArgs e)
        {
            //var titel = txtTitel.Text;
            //var auteur = txtAuteur.Text;
            //var paginas = 0;

            //ICollection<Genre> genreLijst = new List<Genre>();

            //// Haal de lijst met geselecteerde genres uit de listbox en zet die klaar om een nieuw boek aan te maken
            //// genrelijst uit database halen
            //foreach (Genre item in lstGenre.SelectedItems)
            //{
            //    genreLijst.Add(dbAccess.OphalenGenre(item.Code));
            //}

            //try
            //{
            //    paginas = Convert.ToInt32(txtPaginas.Text);
            //    dbAccess.ToevoegenBoek(new Boek { Titel = titel, Auteur = auteur, Paginas = paginas, Genres = genreLijst });
            //}
            //catch (FormatException)
            //{
            //    MessageBox.Show("Fout: ongeldig aantal pagina's", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            //}

            //ToonAlleBoeken();
            //WisAlleVelden();
        }


        /// <summary>
        /// Ververst de tekstvelden bij een gewijzigde selectie in de boekenlijst
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstDatabase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (lstDatabase.SelectedItem != null)
            //{
            //    var geselecteerdBoek = dbAccess.OphalenBoek(((Boek)lstDatabase.SelectedItem).Code);

            //    txtTitel.Text = geselecteerdBoek.Titel;
            //    txtAuteur.Text = geselecteerdBoek.Auteur;
            //    txtPaginas.Text = Convert.ToString(geselecteerdBoek.Paginas);

            //    lstGenre.SelectedItems.Clear();
            //    foreach (Genre lstItem in lstGenre.Items)
            //    {
            //        foreach (Genre boekGenre in geselecteerdBoek.Genres)
            //        {
            //            if (boekGenre.Code == lstItem.Code)
            //            {
            //                lstGenre.SelectedItems.Add(lstItem);
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    // Velden leegmaken als er geen boek geselecteerd is
            //    txtTitel.Text = "";
            //    txtAuteur.Text = "";
            //    txtPaginas.Text = "";
            //    lstGenre.SelectedItems.Clear();
            //}
        }


        /// <summary>
        /// Verwijdert een boek uit de databank
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBoekVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            //if (lstDatabase.SelectedItem != null)
            //{

            //    dbAccess.VerwijderenBoek(((Boek)lstDatabase.SelectedItem).Code);

            //    VulGenreListBox();
            //    ToonAlleBoeken();
            //}
        }


        /// <summary>
        /// Werkt de database informatie bij van het geselecteerde boek
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBoekBewerken_Click(object sender, RoutedEventArgs e)
        {
            //var titel = txtTitel.Text;
            //var auteur = txtAuteur.Text;
            //var paginas = 0;

            //ICollection<Genre> genreLijst = new List<Genre>();

            //// Haal de lijst met geselecteerde genres uit de listbox en zet die klaar om de bestaande genrelijst te vervangen
            //// genrelijst uit database halen
            //foreach (Genre item in lstGenre.SelectedItems)
            //{
            //    genreLijst.Add(dbAccess.OphalenGenre(item.Code));
            //}

            //try
            //{
            //    paginas = Convert.ToInt32(txtPaginas.Text);
            //    dbAccess.BijwerkenBoek(((Boek)lstDatabase.SelectedItem).Code, new Boek { Titel = titel, Auteur = auteur, Paginas = paginas, Genres = genreLijst });
            //}
            //catch (FormatException)
            //{
            //    MessageBox.Show("Fout: ongeldig aantal pagina's", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            //}

            //ToonAlleBoeken();
            //WisAlleVelden();
        }


        /// <summary>
        /// GenreToevoegen roept een venster op om een nieuw genre aan de database toe te voegen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenreToevoegen_Click(object sender, RoutedEventArgs e)
        {
            //if (dbAccess.ToevoegenGenre() == true)
            //{
            //    VulGenreListBox();
            //}
            ////Weghalen boekselectie om geen verkeerde gegevens op het scherm te zien
            //lstDatabase.SelectedItem = null;
        }


        /// <summary>
        /// GenreVerwijderen verwijdert alle geselecteerde genres uit de database op voorwaarde dat er geen boeken aan die genres gekoppeld zijn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenreVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            //var genreSelectie = lstGenre.SelectedItems;

            //bool boekenGekoppeldAanGenre = false;
            //string foutmelding = "";

            //foreach (Genre genre in genreSelectie)
            //{
            //    // Selecteer alle boeken van dat genre, enkel indien er geen zijn mag het genre verwijderd worden

            //    if (!dbAccess.VerwijderenGenre(genre.Code))
            //    {
            //        // Er zijn nog boeken van dat genre: maak een lijstje met genres waar nog boeken aan gekoppeld zijn om daarna weer te geven in de foutmelding

            //        boekenGekoppeldAanGenre = true;
            //        foutmelding += "\n- " + genre.Omschrijving;
            //    }
            //}
            //if (boekenGekoppeldAanGenre == true)
            //{
            //    MessageBox.Show("Er zijn nog boeken van de volgende genres in de database:" + foutmelding, "Fout bij verwijderen genre", MessageBoxButton.OK, MessageBoxImage.Error);
            //}

            //VulGenreListBox();

            ////Weghalen boekselectie om geen verkeerde gegevens op het scherm te zien
            //lstDatabase.SelectedItem = null;
        }

        /// <summary>
        /// Wijzigt de omschrijving van het geselecteerde genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenreWijzigen_Click(object sender, RoutedEventArgs e)
        {
            //// De selectie mag slechts 1 genre bevatten
            //if (lstGenre.SelectedItems.Count == 1)
            //{
            //    var genreSelectie = (Genre)lstGenre.SelectedItem;

            //    if (dbAccess.BijwerkenGenre(genreSelectie.Code))
            //    {
            //        VulGenreListBox();
            //        //Weghalen boekselectie om geen verkeerde gegevens op het scherm te zien
            //        lstDatabase.SelectedItem = null;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Er zijn geen of meerdere genres geselecteerd.\n Selecteer 1 genre om te wijzigen.", "Fout bij wijzigen genre", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }

        private void rbtnFram_Checked(object sender, RoutedEventArgs e)
        {
            ////Bedoeling is bij het wisselen van framewerk technologie hier een 'DbAccess' object van de juiste technologie aan te maken
            ////Alle methodes in de 2 klassen kunnen dan dezelfde namen hebben en bij het oproepen moet er geen check van de radiobuttons meer gebeuren


            //if (rbtnEntFram.IsChecked == true)
            //{
            //    dbAccess = new DbAccessEF();
            //}
            //else
            //{
            //    dbAccess = new DbAccessADO();
            //}
        }
    }
}
