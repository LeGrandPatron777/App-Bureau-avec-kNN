using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TravailPratique2.Models
{
    class Medecin : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public int MedecinId { get; set; }

        private bool _homme;
        public bool Homme { get { return _homme; } set { if (_homme != value) { _homme = value;OnPropertyChanged(); } } }
        private bool _femme;
        public bool Femme { get { return _femme; } set { if (_femme != value) { _femme = value;  OnPropertyChanged(); } } }
        private bool _autre;
        public bool Autre { get { return _autre; } set { if (_autre != value) { _autre = value; OnPropertyChanged(); } } }

        private string? _nom;
        public string? Nom { get { return _nom ;} set { if (_nom != value) { _nom = value; SetIsValid(); OnPropertyChanged(); } } }

        private string? _prenom;
        public string? Prenom { get { return _prenom; } set { if (_prenom != value) { _prenom = value; SetIsValid(); OnPropertyChanged(); } } }

        private string? _ville;
        public string? Ville { get { return _ville; } set { if (_ville != value) { _ville = value; SetIsValid(); OnPropertyChanged(); } } }

        private string? _email;
        public string? Email { get { return _email; } set { if (_email != value) { _email = value; SetIsValid(); OnPropertyChanged(); } } }

        private string? _genre;
        public string? Genre { get { return _genre; } set { if (_genre != value) { _genre = value; SetIsValid(); OnPropertyChanged(); } } }

        private DateTime? _dateDeNaissance;
        public DateTime? DateDeNaissance { get { return _dateDeNaissance; } set { if (_dateDeNaissance != value) { _dateDeNaissance = value; SetIsValid(); OnPropertyChanged(); } } }

        private bool _isValid;
        public bool IsValid { get { return _isValid; } }

        private void SetIsValid()
        {
            _isValid = !string.IsNullOrEmpty(Nom) && !string.IsNullOrEmpty(Prenom) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Ville) && DateDeNaissance.HasValue && DateDeNaissance.Value.Date < DateTime.Now.Date;
        }
    }
}
