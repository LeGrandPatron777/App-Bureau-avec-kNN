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
using TravailPratique2.ViewModels;

namespace TravailPratique2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class IConnexionMed : Window
    {
        public IConnexionMed()
        {
            InitializeComponent();
            DataContext = new ViewModels.ConnexionViewModel(this);
        }
    }
}
