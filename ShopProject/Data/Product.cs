using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Shop.Data
{
    public class Product : INotifyPropertyChanged
    {
        public string Name { get; set; }
        private int _quantity;
        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Quantity should be positive!");
                }
                else
                {
                    _quantity = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool IsAvaliable
        {
            get
            {
                return Quantity > 0;
            }
        }
        public int Price { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
