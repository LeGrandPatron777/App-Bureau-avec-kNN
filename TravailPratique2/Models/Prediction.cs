using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TravailPratique2.Models
{
    internal class Prediction : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private bool _m;
        public bool M { get { return _m; } set { if (_m != value) { _m = value; OnPropertyChanged(); } } }
        private bool _b;
        public bool B { get { return _b; } set { if (_b != value) { _b = value; OnPropertyChanged(); } } }
        public int PredictionId { get; set; }

        private float _radius_worst;
        public float radius_worst { get { return _radius_worst; } set { if (_radius_worst != value) { _radius_worst = value; OnPropertyChanged(); } } }


        private float _area_worst;
        public float area_worst { get { return _area_worst; } set { if (_area_worst != value) { _area_worst = value; OnPropertyChanged(); } } }


        private float _perimeter_worst;
        public float perimeter_worst { get { return _perimeter_worst; } set { if (_perimeter_worst != value) { _perimeter_worst = value; OnPropertyChanged(); } } }


        private float _points_worst;
        public float points_worst { get { return _points_worst; } set { if (_points_worst != value) { _points_worst = value; OnPropertyChanged(); } } }


        private float _points_mean;
        public float points_mean { get { return _points_mean; } set { if (_points_mean != value) { _points_mean = value; OnPropertyChanged(); } } }


        private float _perimeter_mean;
        public float perimeter_mean { get { return _perimeter_mean; } set { if (_perimeter_mean != value) { _perimeter_mean = value; OnPropertyChanged(); } } }

        private char _diagnosis;
        public char diagnosis { get { return _diagnosis; } set { if (_diagnosis != value) { _diagnosis = value; OnPropertyChanged(); } } }


        private string? _resultat;
        public string? resultat { get { return _resultat; } set { if (_resultat != value) { _resultat = value; OnPropertyChanged(); } } }

        public int PatientId { get; set; }
        public Patient? Patient { get; set; }

    }
}
