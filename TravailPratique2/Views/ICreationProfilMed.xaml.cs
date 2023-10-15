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

namespace TravailPratique2.Views
{
    /// <summary>
    /// Logique d'interaction pour ICreationProfilMed.xaml
    /// </summary>
    public partial class ICreationProfilMed : Window
    {
        public ICreationProfilMed()
        {
            InitializeComponent();
            DataContext = new ViewModels.MedecinViewModel(this);
        }
    }
}
