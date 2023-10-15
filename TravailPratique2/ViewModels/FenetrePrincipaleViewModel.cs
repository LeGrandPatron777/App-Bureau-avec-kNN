using KnnLibrary;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TravailPratique2.Models;
using TravailPratique2.Views;

namespace TravailPratique2.ViewModels
{

    class FenetrePrincipaleViewModel : INotifyPropertyChanged
    {
        public Models.Patient PatientSforR { get; set; }
        public Models.Historique HistoriqueSforR { get; set; }
        public string PatientSelectione { get; set; }
        public int IdPatientSelectione { get; set; }
        public List<Historique> Historiques { get; set; }
        KNN_Breast _KNN_Breast { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string _TrainFilePath;
        public string TrainFilePath
        {
            get { return _TrainFilePath; }
            set
            {
                _TrainFilePath = value;
                OnPropertyChanged(nameof(TrainFilePath));
            }
        }

        private string _TestFilePath;
        public string TestFilePath
        {
            get { return _TestFilePath; }
            set
            {
                _TestFilePath = value;
                OnPropertyChanged(nameof(TestFilePath));
            }
        }
        private Dictionary<char, int[]> _confusionMatrix;

        public Dictionary<char, int[]> ConfusionMatrix
        {
            get { return _confusionMatrix; }
            set
            {
                _confusionMatrix = value;
                OnPropertyChanged(nameof(ConfusionMatrix));
            }
        }
        public Models.Performance Performance { get; set; }
        public ObservableCollection<string> Villes { get; set; }
        public List<Patient> Patients { get; set; }
        public List<string> NomsPatients { get; set; }
        public List<int> IdPatient { get; set; }
        public ObservableCollection<string> ComboBoxValuesPatient { get; set; }

        public Models.Medecin Medecin { get; set; }
        public Models.Medecin selectedMedecin { get; set; }
        public Models.Prediction prediction { get; set; }
        private IFenetrePrincipale _fenetrePrincipale { get; set; }
        private int _selectedIdDoctor { get; set; }
        public char selectedOption
        {
            get
            {
                if (prediction.M)
                {

                    return 'M';
                }
                else
                {
                    return 'B';
                }
            }
        }
        public FenetrePrincipaleViewModel(IFenetrePrincipale fenetrePrincipale, int selectedIdDoctor)
        {

            this.Historiques = new List<Historique>();
            this.prediction = new Prediction();
            this._KNN_Breast = new KNN_Breast();
            this.Performance = new Performance();
            this.Villes = new ObservableCollection<string>() { "Rimouski", "Lévis", "Québec", "Montreal" };
            this._selectedIdDoctor = selectedIdDoctor;
            this._fenetrePrincipale = fenetrePrincipale;
            this.Medecin = new Medecin();
            TravailPratique2EfCoreDbContext _context = new TravailPratique2EfCoreDbContext();
            selectedMedecin = _context.Medecins.Find(this._selectedIdDoctor);
            Patients = _context.Patients.Where(p => p.MedecinId == _selectedIdDoctor).ToList();
            Historiques = _context.Historiques.Where(c => c.MedecinId == _selectedIdDoctor).ToList();

            this.ComboBoxValuesPatient = new ObservableCollection<string>();
            this.IdPatient = new List<int>();
            this.NomsPatients = new List<string>();
            NomsPatients = Patients.Select(d => d.Nom).ToList();
            for (int i = 0; i < NomsPatients.Count; i++)
            {
                string comboboxValue = $"{NomsPatients[i]}";
                ComboBoxValuesPatient.Add(comboboxValue);
            }


            AjouterPatientCommand = new RelayCommand(
                o => true,
                o => this.AjouterPatient()
                );

            QuitterCommand = new RelayCommand(
                o => true,
                o => this.Quitter()
                );

            MettreAJourCommand = new RelayCommand(
                 o => true,
                o => this.MettreAJour()
                );
            TrainFileCommand = new RelayCommand(
                o => true,
                o => this.TrainFile()
                );
            TestFileCommand = new RelayCommand(
               o => true,
               o => this.TestFile()
               );
            TauxDeReconnaissanceCommand = new RelayCommand(
                o => true,
                o => this.TauxDeReconnaissance()
                );

            PredictCommand = new RelayCommand(
                o => true,
                o => this.Predict()
                );

            SupprimerPatientCommand = new RelayCommand(
                o => true,
                o => this.SupprimerPat()
                );
            SupprimerHistoriqueCommand = new RelayCommand(
                o => true,
                o => this.SupprimerHist()
                );
        }

        public ICommand AjouterPatientCommand { get; set; }
        public ICommand QuitterCommand { get; set; }
        public ICommand MettreAJourCommand { get; set; }
        public ICommand TrainFileCommand { get; }
        public ICommand TestFileCommand { get; }
        public ICommand TauxDeReconnaissanceCommand { get; set; }
        public ICommand PredictCommand { get; set; }
        public ICommand SupprimerPatientCommand { get; set; }
        public ICommand SupprimerHistoriqueCommand { get; set; }

        private void AjouterPatient()
        {
            Views.ICreationProfilPat fenetre4 = new Views.ICreationProfilPat(this._selectedIdDoctor);
            _fenetrePrincipale.Close();
            fenetre4.Show();
        }

        private void Quitter()
        {
            IConnexionMed fenetre1 = new IConnexionMed();
            fenetre1.Show();
            _fenetrePrincipale.Close();

        }

        private void MettreAJour()
        {

            TravailPratique2EfCoreDbContext _context = new TravailPratique2EfCoreDbContext();
            Medecin medecin = _context.Medecins.Find(this._selectedIdDoctor);
            medecin.Nom = this.selectedMedecin.Nom;
            medecin.Prenom = this.selectedMedecin.Prenom;
            medecin.Email = this.selectedMedecin.Email;
            medecin.Ville = this.selectedMedecin.Ville;
            medecin.DateDeNaissance = this.selectedMedecin.DateDeNaissance;
            _context.SaveChanges();
            MessageBox.Show($"Vos modificatins ont été apportées avec succès!", "Mise A Jour", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void TrainFile()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                TrainFilePath = openFileDialog.FileName;
                // Traiter le fichier ici
            }
        }
        private void TestFile()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                TestFilePath = openFileDialog.FileName;
                // Traiter le fichier ici
            }
        }
        private void TauxDeReconnaissance()
        {
            if (!(Performance.K == 0))
            {
                if (!(TestFilePath == null && TrainFilePath == null))
                {

                    _KNN_Breast.Train(TrainFilePath, (int)Performance.K);
                    Performance.TauxDeReconnaissance = _KNN_Breast.Evaluate(TestFilePath);
                    ConfusionMatrix = _KNN_Breast.confusion_matrix;
                }
                else
                {
                    MessageBox.Show("Assurez vous d'avoir importer le fichier train et test !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Veillez rentrez unz valeur valide de K", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Predict()
        {
            if (!(Performance.K == 0))
            {
                if (!(TestFilePath == null && TrainFilePath == null))
                {
                    List<Breast> breast_samples = new List<Breast>
            {
                new Breast() { AreaWorst=this.prediction.area_worst,
                    RadiusWorst=this.prediction.radius_worst,
                    ConcavePointsMean=this.prediction.points_mean,
                    ConcavePointsWorst=this.prediction.points_worst,
                    Diagnosis=selectedOption,
                    PerimeterMean=this.prediction.perimeter_mean,
                    PerimeterWorst=this.prediction.perimeter_worst  }
            };

                    if (!(PatientSelectione == null))
                    {
                        prediction.resultat = $"Prediction of sample is -> {_KNN_Breast.Predict(breast_samples[0])} (expert -> {breast_samples[0].Label})";
                        this.prediction.PatientId = this.IdPatientSelectione;
                        this.prediction.resultat = $"Prediction of sample is -> {_KNN_Breast.Predict(breast_samples[0])} (expert -> {breast_samples[0].Label})";
                        Models.Historique historique = new Models.Historique();
                        historique.Resultat = this.prediction.resultat;
                        historique.NomDuPatient = PatientSelectione;
                        historique.MedecinId = this._selectedIdDoctor;
                        historique.K = Performance.K;
                        TravailPratique2EfCoreDbContext context = new TravailPratique2EfCoreDbContext();
                        context.Historiques.Add(historique);
                        context.SaveChanges();
                        MessageBox.Show($"{prediction.resultat}", "Resultat de la prediction", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        Views.IFenetrePrincipale fenetre2 = new Views.IFenetrePrincipale(_selectedIdDoctor);
                        _fenetrePrincipale.Close();
                        fenetre2.Show();
                    }
                    else
                    {
                        MessageBox.Show("Veuillez choisir un patient", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Assurez vous d'avoir importer le fichier train et test !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Veillez rentrez une valeur valide de K", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SupprimerPat()
        {
            Models.TravailPratique2EfCoreDbContext context = new TravailPratique2EfCoreDbContext();
            int Id = PatientSforR.PatientId;
            Models.Patient patient = context.Patients.Find(Id);
            context.Patients.Remove(patient);
            context.SaveChanges();
            MessageBox.Show($"Le patient a ete supprimé avec succès", "Suppression", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            Views.IFenetrePrincipale fenetre2 = new Views.IFenetrePrincipale(_selectedIdDoctor);
            _fenetrePrincipale.Close();
            fenetre2.Show();
        }
        private void SupprimerHist()
        {
            Models.TravailPratique2EfCoreDbContext context = new TravailPratique2EfCoreDbContext();
            int Id = HistoriqueSforR.HistoriqueId;
            Models.Historique historique = context.Historiques.Find(Id);
            context.Historiques.Remove(historique);
            context.SaveChanges();
            MessageBox.Show($"La prediction a ete supprimé avec succès", "Suppression", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            Views.IFenetrePrincipale fenetre2 = new Views.IFenetrePrincipale(_selectedIdDoctor);
            _fenetrePrincipale.Close();
            fenetre2.Show();
        }
    }
}
