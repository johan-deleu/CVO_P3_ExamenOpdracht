using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        private IBoekService _boekClient;
        private IGenreService _genreClient;

        public MainWindow()
        {
            InitializeComponent();

            //Vul de listboxen van Genres en Boeken
            ToonAlleGenresAsync();
            ToonAlleBoekenAsync();
        }

        private void CreateWCFClients()
        {            
            _boekClient = new BoekWcfClient();
            _genreClient = new GenreWcfClient();
        }

        private void CreateWebApiClients()
        {
            _boekClient = new BoekWebApiClient();
            _genreClient = new GenreWebApiClient();
        }
        private void WisAlleVelden()
        {
            txtTitel.Clear();
            txtAuteur.Clear();
            txtPaginas.Clear();
            lstGenre.SelectedItems.Clear();
            lstDatabase.SelectedItem = null;
        }

        private async void ToonAlleGenresAsync()
        {
            lstGenre.Items.Clear();
            List<Genre> lstGenresVanDb = new List<Genre>();

            lstGenresVanDb = await _genreClient.OphalenGenresAsync();
            foreach (var genre in lstGenresVanDb)
            {
                lstGenre.Items.Add(genre);
            }
        }
        
        private async void ToonAlleBoekenAsync()
        {
            lstDatabase.Items.Clear();
            List<Boek> lstBoekenVanDb = new List<Boek>();

            lstBoekenVanDb = await _boekClient.OphalenBoekenAsync();
            foreach (var boek in lstBoekenVanDb)
            {
                lstDatabase.Items.Add(boek);
            }
        }
        /// <summary>
        /// Voegt een boek toe aan de databank op basis van de ingevulde velden en geselecteerde genres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnBoekToevoegen_Click(object sender, RoutedEventArgs e)
        {
            var titel = txtTitel.Text;
            var auteur = txtAuteur.Text;
            var paginas = 0;

            // Haal de lijst met geselecteerde genres uit de listbox en zet die klaar om een nieuw boek aan te maken
            // genrelijst uit database halen
            ICollection<Genre> genreLijst = new List<Genre>();
            foreach (Genre item in lstGenre.SelectedItems)
            {
                genreLijst.Add(await _genreClient.OphalenGenreAsync(item));
            }

            try
            {
                paginas = Convert.ToInt32(txtPaginas.Text);
                await _boekClient.ToevoegenBoekAsync(new Boek { Titel = titel, Auteur = auteur, Paginas = paginas, Genres = genreLijst });
            }
            catch (FormatException)
            {
                MessageBox.Show("Fout: ongeldig aantal pagina's", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            ToonAlleGenresAsync();
            ToonAlleBoekenAsync();
        }

        /// <summary>
        /// Ververst de tekstvelden bij een gewijzigde selectie in de boekenlijst
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void lstDatabase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstDatabase.SelectedItem != null)
            {
                //Steeds de gegevens terug uit de databank ophalen, ze kunnen immers gewijzigd zijn               
                var geselecteerdBoek = await _boekClient.OphalenBoekAsync(((Boek)lstDatabase.SelectedItem).Code);

                //Mogelijk is zelfs het boek intussen uit de databank
                if(geselecteerdBoek != null)
                {
                    txtTitel.Text = geselecteerdBoek.Titel;
                    txtAuteur.Text = geselecteerdBoek.Auteur;
                    txtPaginas.Text = Convert.ToString(geselecteerdBoek.Paginas);

                    lstGenre.SelectedItems.Clear();
                    foreach (Genre lstItem in lstGenre.Items)
                    {
                        foreach (Genre boekGenre in geselecteerdBoek.Genres)
                        {
                            if (boekGenre.Code == lstItem.Code)
                            {
                                lstGenre.SelectedItems.Add(lstItem);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Fout: dit boek werd niet gevonden in de databank", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);                   
                }             
            }
            else
            {
                // Velden leegmaken als er geen boek geselecteerd is
                WisAlleVelden();
            }
        }

        /// <summary>
        /// Verwijdert een boek uit de databank
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnBoekVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            if (lstDatabase.SelectedItem != null)
            {
                await _boekClient.VerwijderenBoekAsync((Boek)lstDatabase.SelectedItem);

                ToonAlleGenresAsync();
                ToonAlleBoekenAsync();
            }
        }


        /// <summary>
        /// Werkt de database informatie bij van het geselecteerde boek
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnBoekBewerken_Click(object sender, RoutedEventArgs e)
        {

            Boek teBewerkenBoek = (Boek)lstDatabase.SelectedItem;

            var titel = txtTitel.Text;
            var auteur = txtAuteur.Text;
            var paginas = 0;

            ICollection<Genre> genreLijst = new List<Genre>();

            //// Haal de lijst met geselecteerde genres uit de listbox en zet die klaar om de bestaande genrelijst te vervangen
            //// genrelijst uit database halen
            foreach (Genre item in lstGenre.SelectedItems)
            {
                genreLijst.Add(await _genreClient.OphalenGenreAsync(item));
            }

            try
            {
                paginas = Convert.ToInt32(txtPaginas.Text);
                await _boekClient.BewerkenBoekAsync(teBewerkenBoek.Code ,new Boek { Titel = titel, Auteur = auteur, Paginas = paginas, Genres = genreLijst });
            }
            catch (FormatException)
            {
               MessageBox.Show("Fout: ongeldig aantal pagina's", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            ToonAlleGenresAsync();
            ToonAlleBoekenAsync();
        }


        /// <summary>
        /// GenreToevoegen roept een venster op om een nieuw genre aan de database toe te voegen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnGenreToevoegen_Click(object sender, RoutedEventArgs e)
        {

            GenreWindow genreWindow = new GenreWindow("Genre toevoegen", "Geef de omschrijving van het nieuwe genre: ", "Genre toevoegen", "");
            genreWindow.ShowDialog();

            if (genreWindow.DialogResult == true)
            {
                var code = await _genreClient.ToevoegenGenreAsync(new Genre(genreWindow.Genre));
                if (code!= null)
                {
                    ToonAlleGenresAsync();
                }   
            }
            //Weghalen boekselectie om geen verkeerde gegevens op het scherm te zien
            lstDatabase.SelectedItem = null;
        }

        /// <summary>
        /// GenreVerwijderen verwijdert alle geselecteerde genres uit de database op voorwaarde dat er geen boeken aan die genres gekoppeld zijn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnGenreVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            var genreSelectie = lstGenre.SelectedItems;

            bool boekenGekoppeldAanGenre = false;
            string foutmelding = "";

            foreach (Genre genre in genreSelectie)
            {
                // Selecteer alle boeken van dat genre, enkel indien er geen zijn mag het genre verwijderd worden
                var keyVerwijderdGenre = await _genreClient.VerwijderenGenreAsync(genre);
                if (keyVerwijderdGenre == null)
                {
                    // Dit genre kan niet verwijderd worden
                    // Er zijn nog boeken van dat genre: maak een lijstje met genres waar nog boeken aan gekoppeld zijn om daarna weer te geven in de foutmelding

                    boekenGekoppeldAanGenre = true;
                    foutmelding += "\n- " + genre.Omschrijving;
                }
            }
            if (boekenGekoppeldAanGenre == true)
            {
                MessageBox.Show("Er zijn nog boeken van de volgende genres in de database:" + foutmelding, "Fout bij verwijderen genre", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            //Refresh van de genreListBox
            ToonAlleGenresAsync();

            //Weghalen boekselectie om geen verkeerde gegevens op het scherm te zien
            lstDatabase.SelectedItem = null;
        }

        /// <summary>
        /// Wijzigt de omschrijving van het geselecteerde genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnGenreWijzigen_Click(object sender, RoutedEventArgs e)
        {
            // De selectie mag slechts 1 genre bevatten
            if (lstGenre.SelectedItems.Count == 1)
            {
                var geselecteerdGenre = (Genre)lstGenre.SelectedItem;
                GenreWindow genreWindow = new GenreWindow("Genre  wijzigen", "Geef de nieuwe omschrijving van het genre: ", "Genre wijzigen", "");
                genreWindow.txtGenre.Text = geselecteerdGenre.Omschrijving;
                genreWindow.ShowDialog();

                if (genreWindow.DialogResult == true)
                {
                    var bijgewerktGenre = new Genre(genreWindow.txtGenre.Text);
                    var pkGewijzigdGenre = await _genreClient.WijzigenGenreAsync(geselecteerdGenre, bijgewerktGenre);
                    if (pkGewijzigdGenre != null)
                    {
                        ToonAlleGenresAsync();
                        //Weghalen boekselectie om geen verkeerde gegevens op het scherm te zien
                        lstDatabase.SelectedItem = null;
                    }
                }
            }
            else
            {
                MessageBox.Show("Er zijn geen of meerdere genres geselecteerd.\n Selecteer 1 genre om te wijzigen.", "Fout bij wijzigen genre", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void rbtnService_Checked(object sender, RoutedEventArgs e)
        {
            //Bedoeling is bij het wisselen van framewerk technologie hier een Client object van de juiste technologie aan te maken:
            //- WCF
            //- Web API
            //Alle methodes in de 2 klassen kunnen dan dezelfde namen hebben en bij het oproepen moet er geen check van de radiobuttons meer gebeuren


            if (rbtnWCF.IsChecked == true)
            {
                CreateWCFClients();
            }
            else
            {
                CreateWebApiClients();
            }
        }       
    }
}
