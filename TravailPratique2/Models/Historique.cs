using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TravailPratique2.Models
{
    internal class Historique : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public int HistoriqueId { get; set; }

        private string? _nomDuPatient;
        public string? NomDuPatient { get { return _nomDuPatient; } set { if (_nomDuPatient != value) { _nomDuPatient = value;  OnPropertyChanged(); } } }


        private string? _resultat;
        public string? Resultat { get { return _resultat; } set { if (_resultat != value) { _resultat = value; OnPropertyChanged(); } } }

        private float? _k;
        public float? K { get { return _k; } set { if (_k != value) { _k = value; OnPropertyChanged(); } } }
        public int MedecinId { get; set; }

        public Medecin? Medecin { get; set; }


        private bool _isValid;
        public bool IsValid { get { return _isValid; } }

        private void SetIsValid()
        {
            _isValid = !string.IsNullOrEmpty(NomDuPatient) && !string.IsNullOrEmpty(Resultat);
        }

    }
}
