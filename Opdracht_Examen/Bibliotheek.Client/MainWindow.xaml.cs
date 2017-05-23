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
        private IBoekService _boekClient;
        private IGenreService _genreClient;

        public MainWindow()
        {
            InitializeComponent();

            var binding = new BasicHttpBinding();
            var boekServiceEndpointAddress = new EndpointAddress("http://localhost:54398/BoekService.svc");
            var genreServiceEndpointAddress = new EndpointAddress("http://localhost:54398/GenreService.svc");

            var boekChannelFactory = new ChannelFactory<IBoekService>(binding, boekServiceEndpointAddress);
            var genreChannelFactory = new ChannelFactory<IGenreService>(binding, genreServiceEndpointAddress);
            _boekClient = boekChannelFactory.CreateChannel();
            _genreClient = genreChannelFactory.CreateChannel();

            //VulGenreListBox();
            ToonAlleGenresAsync(_genreClient);
            ToonAlleBoekenAsync(_boekClient);

        }
        private void WisAlleVelden()
        {
            txtTitel.Clear();
            txtAuteur.Clear();
            txtPaginas.Clear();
            lstGenre.SelectedItems.Clear();
            lstDatabase.SelectedItem = null;
        }


        private async void ToonAlleGenresAsync(IGenreService genreClient)
        {
            lstGenre.Items.Clear();
            List<Genre> lstGenresVanDb = new List<Genre>();

            lstGenresVanDb = await genreClient.OphalenGenresAsync();
            foreach (var genre in lstGenresVanDb)
            {
                lstGenre.Items.Add(genre);
            }
        }
        private async void ToonAlleBoekenAsync(IBoekService boekClient)
        {
            lstDatabase.Items.Clear();
            List<Boek> lstBoekenVanDb = new List<Boek>();

            lstBoekenVanDb= await boekClient.OphalenBoekenAsync();
            foreach (var boek in lstBoekenVanDb)
            {
                lstDatabase.Items.Add(boek);
            }
        }
        private async Task<Genre> OphalenGenreAsync(IGenreService genreClient, Genre genre)
        {
            return await genreClient.OphalenGenreAsync(genre);
        }
        private async Task<Int32?> ToevoegenGenreAsync(IGenreService genreClient, Genre genre)
        {
            return await genreClient.ToevoegenGenreAsync(genre);
        }

        private async Task<Int32?> VerwijderenGenreAsync(IGenreService genreClient, Genre genre)
        {
            return await genreClient.VerwijderenGenreAsync(genre);   
        }

        private async Task<List<Genre>> VerwijderenGenreLijstAsync(IGenreService genreClient, List<Genre> genreLijst)
        {
            return await genreClient.VerwijderenGenreLijstAsync(genreLijst);
        }

        private async Task<Int32?> WijzigenGenreAsync(IGenreService genreClient, Genre bestaandGenre, Genre bijgewerktGenre)
        {
            return await genreClient.WijzigenGenreAsync(bestaandGenre, bijgewerktGenre);
        }

        private async Task<Boek> OphalenBoekMetGenreAsync(IBoekService boekClient, Boek boek)
        {
            return await boekClient.OphalenBoekMetGenreAsync(boek);
        }

        private async Task<Int32?> ToevoegenBoekAsync(IBoekService boekClient, Boek boek)
        {
            return await boekClient.ToevoegenBoekAsync(boek);
        }
        private async Task<Int32?> VerwijderenBoekAsync(IBoekService boekClient, Boek boek)
        {
            return await boekClient.VerwijderenBoekAsync(boek);
        }

        private async Task<Int32?> BewerkenBoekAsync(IBoekService boekClient, Boek teBewerkenBoek, Boek bewerktBoek)
        {
            return await boekClient.BewerkenBoekAsync(teBewerkenBoek, bewerktBoek);
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

            ICollection<Genre> genreLijst = new List<Genre>();

            // Haal de lijst met geselecteerde genres uit de listbox en zet die klaar om een nieuw boek aan te maken
            // genrelijst uit database halen
            foreach (Genre item in lstGenre.SelectedItems)
            {
                genreLijst.Add(await OphalenGenreAsync(_genreClient,item));
            }

            try
            {
                paginas = Convert.ToInt32(txtPaginas.Text);
                await ToevoegenBoekAsync(_boekClient,new Boek { Titel = titel, Auteur = auteur, Paginas = paginas, Genres = genreLijst });
            }
            catch (FormatException)
            {
                MessageBox.Show("Fout: ongeldig aantal pagina's", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            ToonAlleGenresAsync(_genreClient);
            ToonAlleBoekenAsync(_boekClient);
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
                var geselecteerdBoek = await OphalenBoekMetGenreAsync(_boekClient,(Boek)lstDatabase.SelectedItem);

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
                // Velden leegmaken als er geen boek geselecteerd is
                txtTitel.Text = "";
                txtAuteur.Text = "";
                txtPaginas.Text = "";
                lstGenre.SelectedItems.Clear();
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

                await VerwijderenBoekAsync(_boekClient,(Boek)lstDatabase.SelectedItem);

                ToonAlleGenresAsync(_genreClient);
                ToonAlleBoekenAsync(_boekClient);
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
                genreLijst.Add(await OphalenGenreAsync(_genreClient, item));
            }

            try
            {
                paginas = Convert.ToInt32(txtPaginas.Text);
                await BewerkenBoekAsync(_boekClient,teBewerkenBoek ,new Boek { Titel = titel, Auteur = auteur, Paginas = paginas, Genres = genreLijst });
            }
            catch (FormatException)
            {
               MessageBox.Show("Fout: ongeldig aantal pagina's", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            ToonAlleGenresAsync(_genreClient);
            ToonAlleBoekenAsync(_boekClient);
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
                var code = await ToevoegenGenreAsync(_genreClient, new Genre(genreWindow.Genre));
                if (code!= null)
                {
                    ToonAlleGenresAsync(_genreClient);
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
                var keyVerwijderdGenre = await VerwijderenGenreAsync(_genreClient, genre);
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
            ToonAlleGenresAsync(_genreClient);

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
                    var pkGewijzigdGenre = await WijzigenGenreAsync(_genreClient, geselecteerdGenre, bijgewerktGenre);
                    if (pkGewijzigdGenre != null)
                    {
                        ToonAlleGenresAsync(_genreClient);
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
