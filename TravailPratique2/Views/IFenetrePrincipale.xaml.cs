using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TravailPratique2.Views
{
    /// <summary>
    /// Logique d'interaction pour IFenetrePrincipale.xaml
    /// </summary>
    public partial class IFenetrePrincipale : Window
    {
        public IFenetrePrincipale(int selectedIdDoctor)
        {
            InitializeComponent();
            DataContext = new ViewModels.FenetrePrincipaleViewModel(this, selectedIdDoctor);
        }

        private void PredictionHistoryDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void IntVerification(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+"); // N'autorise que les chiffres
            e.Handled = regex.IsMatch(e.Text);
        }
        private void IncrementButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(k.Text, out int value))
            {
                k.Text = (value + 1).ToString();
            }
        }

        private void DecrementButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(k.Text, out int value) && value > 1)
            {
                k.Text = (value - 1).ToString();
            }
        }
    }
}
