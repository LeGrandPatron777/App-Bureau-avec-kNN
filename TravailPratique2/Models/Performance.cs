using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TravailPratique2.Models
{
    internal class Performance : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public int PerformanceId { get; set; }

        private int _k;
        public int K { get { return _k; } set { if (_k != value) { _k = value; OnPropertyChanged(); } } }

        private float _tauxDeReconnaissance;
        public float TauxDeReconnaissance { get { return _tauxDeReconnaissance; } set { if (_tauxDeReconnaissance != value) { _tauxDeReconnaissance = value; OnPropertyChanged(); } } }

    }
}
