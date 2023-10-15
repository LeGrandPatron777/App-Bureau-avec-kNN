using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TravailPratique2.Models;
using TravailPratique2.Views;

namespace TravailPratique2.ViewModels
{
    class PatientViewModel
    {
        public Models.Medecin selectedMedecin { get; set; }
        private ICreationProfilPat _creationProfilPat;
        public Models.TravailPratique2EfCoreDbContext _context;
        public Models.Patient Patient { get; set; }
        public ObservableCollection<string> Villes { get; set; }
        private int _selectedIdDoctor { get; set; }
        public string selectedOption
        {
            get
            {
                if (Patient.Homme)
                {
                    return "Homme";
                }
                else if (Patient.Femme)
                {
                    return "Femme";
                }
                else
                {
                    return "Autre";
                }
            }
        }
        public PatientViewModel(ICreationProfilPat creationProfilPat , int selectedIdDoctor) 
        {
            
            this._context = new Models.TravailPratique2EfCoreDbContext();
            this.selectedMedecin = _context.Medecins.Find(this._selectedIdDoctor);
            this._selectedIdDoctor= selectedIdDoctor;
            _creationProfilPat = creationProfilPat;
            this.Patient = new Models.Patient();
            Villes = new ObservableCollection<string>() { "Rimouski", "Lévis", "Québec", "Montreal" };

            CreationPatCommand = new RelayCommand(
               o => Patient.IsValid,
               o => this.CreationPat()
               );

            AnnulerCommand = new RelayCommand(
               o => true,
               o => this.Annuler()
               );
        }


        public ICommand CreationPatCommand { get; set; }
        public ICommand AnnulerCommand { get; set; }
        private void Annuler()
        {
            IFenetrePrincipale fenetre2 = new IFenetrePrincipale(this._selectedIdDoctor);
            fenetre2.Show();
            _creationProfilPat.Close();
        }
        private void CreationPat()
        {
            Models.Patient patient = new Models.Patient();
            patient.Nom = Patient.Nom;
            patient.Prenom= Patient.Prenom;
            patient.Email= Patient.Email;
            patient.Ville = Patient.Ville;
            patient.Genre = selectedOption;
            patient.DateDeNaissance= Patient.DateDeNaissance;
            patient.Medecin= selectedMedecin;
            patient.MedecinId = _selectedIdDoctor;
            _context.Patients.Add(patient);
            _context.SaveChanges();
            Views.IFenetrePrincipale fenetre2 = new Views.IFenetrePrincipale(_selectedIdDoctor);
            MessageBox.Show($"Votre compte a été créé avec succès!", "Compte Créé", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            fenetre2.Show();
            _creationProfilPat.Close();
        }
    }
}
