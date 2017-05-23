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
using System.Windows.Shapes;

namespace Bibliotheek.Client
{
    /// <summary>
    /// Interaction logic for GenreWindow.xaml
    /// </summary>
    public partial class GenreWindow : Window
    {
        public string Genre { get; set; }
        public GenreWindow(string windowTekst, string actieLabelTekst, string actieButtonTekst, string genre)
        {
            InitializeComponent();

            this.Title = windowTekst;
            lblGenreActie.Content = actieLabelTekst;
            btnGenreActie.Content = actieButtonTekst;
            txtGenre.Text = genre;
        }

        private void btnGenreActie_Click(object sender, RoutedEventArgs e)
        {
            if (txtGenre.Text != "")
            {
                Genre = txtGenre.Text;
                this.DialogResult = true;
                this.Close();
            }
        }

        private void btnGenreActieAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
