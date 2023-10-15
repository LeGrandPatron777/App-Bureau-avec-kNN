using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using TravailPratique2.Models;
using TravailPratique2.Views;
using static Azure.Core.HttpHeader;

namespace TravailPratique2.ViewModels
{
    class ConnexionViewModel
    {
        public Models.TravailPratique2EfCoreDbContext _context;
        private IConnexionMed _connexionMed { get; set; }
        public int selectedIdDoctor { get; set; }
        public string selectedDoctor { get; set; }
        public Models.Medecin Medecin { get; set; }
        public List<Models.Medecin> MesMedecins { get; set; }
        public List<string> Noms { get; set; }
        public List<int> Ids { get; set; }

        public ObservableCollection<string> ComboBoxValues { get; set; }

        public ConnexionViewModel(IConnexionMed connexionMed)
        {

            this._connexionMed = connexionMed;
            SeConnecterCommand = new RelayCommand(
               o => true,
               o => this.SeConnecter()
               );

            QuitterCommand = new RelayCommand(
                 o => true,
               o => this.Quitter()
               );

            CreerMedCommand = new RelayCommand(
                o => true,
               o => this.CreerMed()
                );
            TravailPratique2EfCoreDbContext _context = new TravailPratique2EfCoreDbContext();
            MesMedecins = _context.Medecins.ToList();
            Noms = MesMedecins.Select(d => d.Nom).ToList();
            Ids = MesMedecins.Select(d => d.MedecinId).ToList();

            ComboBoxValues = new ObservableCollection<string>();

            for (int i = 0; i < Noms.Count; i++)
            {
                string comboboxValue = $"{Ids[i]} - {Noms[i]}";
                ComboBoxValues.Add(comboboxValue);
            }
        }
        

        public ICommand SeConnecterCommand { get; set; }
        public ICommand CreerMedCommand { get; set; }
        public ICommand QuitterCommand { get; set; }

        private void SeConnecter()
        {
            if (!(selectedDoctor == null))
            {
                int parsedId = 0;
                selectedIdDoctor = 0;
                if (int.TryParse(selectedDoctor.Split(' ')[0], out parsedId))
                {
                    selectedIdDoctor = parsedId;

                }

                Views.IFenetrePrincipale fenetre2 = new Views.IFenetrePrincipale(selectedIdDoctor);
                _connexionMed.Close();
                fenetre2.Show();

            }
            else
            {
                MessageBox.Show("Veuillez choisir un medecin dans la liste", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CreerMed()
        {
            Views.ICreationProfilMed fenetre3 = new Views.ICreationProfilMed();
            _connexionMed.Close();
            fenetre3.Show();
        }

        private void Quitter()
        {
            Application.Current.Shutdown();
        }
    }
}
