using Churrap.Models;
using Churrap.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Churrap.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IChurrerxService ChurrerxService => DependencyService.Get<IChurrerxService>();

        private bool isBusy = false;
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }
        public bool NotIsBusy {get => !isBusy;}

        private string title = string.Empty;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            bool hasPropertyChanged = !EqualityComparer<T>.Default.Equals(backingStore, value);

            if (hasPropertyChanged)
            {
                backingStore = value;
                
                //Nota: puede que sea overkill que todos los cambios triggereados en properties impacten sobre el hilo principal, pero ¿por qué alguna debería de NO hacerlo? De esta forma estamos cubiertos ante cambios desencadenados en propiedades desde otros hilos, y al cabo que para eso está este helper base: para hacernos la vida más simple
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    onChanged?.Invoke();
                    OnPropertyChanged(propertyName);
                });
            }
            
            return hasPropertyChanged;
        }

        #region INotifyPropertyChanged Event Handler
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler changeHandler = PropertyChanged;
            if (PropertyChanged != null)
            {
                changeHandler.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
