using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TravailPratique2.Views;
using TravailPratique2;

namespace TravailPratique2.ViewModels
{
    class MedecinViewModel
    {
        public Models.Medecin Medecin { get; set; }

        public ObservableCollection<string> Villes { get; set; }

        public Models.TravailPratique2EfCoreDbContext _context;

        private ICreationProfilMed _creationProfilMed;

        public string selectedOption
        {
            get
            {
                if (Medecin.Homme)
                {
                    Medecin.Homme = true;
                    return "Homme";
                }
                else if (Medecin.Femme)
                {
                    Medecin.Femme = true;
                    return "Femme";
                }
                else
                {
                    Medecin.Autre=true;
                    return "Autre";
                }
            }
        }

        public MedecinViewModel(Views.ICreationProfilMed creationProfilMed) 
        {
    
            this._creationProfilMed = creationProfilMed;
            this._context = new Models.TravailPratique2EfCoreDbContext();
            this.Medecin = new Models.Medecin();
            this.Villes = new ObservableCollection<string>() {"Rimouski", "Lévis", "Québec", "Montreal"  };

            CreationMedCommand = new RelayCommand(
                o => this.Medecin.IsValid,
                o => this.CreationMed()
                );

            AnnulerCommand = new RelayCommand(
                o => true,
                o => this.Annuler()
                );
        }

        public ICommand CreationMedCommand { get; set; }
        public ICommand AnnulerCommand { get; set; }

        private void CreationMed()
        {

            Models.Medecin medecin = new Models.Medecin();
            medecin.Nom = Medecin.Nom;
            medecin.Prenom = Medecin.Prenom;
            medecin.Email = Medecin.Email;
            medecin.Ville = Medecin.Ville;
            medecin.Genre = selectedOption;
            medecin.DateDeNaissance = Medecin.DateDeNaissance;
            _context.Medecins.Add(medecin);
            _context.SaveChanges();
            IConnexionMed connexionMed = new IConnexionMed();
            MessageBox.Show($"Votre compte a été créé avec succès!", "Compte Créé", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            connexionMed.Show();
            _creationProfilMed.Close();
        }
        private void Annuler()
        {
            IConnexionMed fenetre1 = new IConnexionMed();
            fenetre1.Show();
            _creationProfilMed.Close();
        }
    }
}
