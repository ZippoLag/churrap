using Churrap.Models;
using Churrap.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
                onChanged?.Invoke();
                OnPropertyChanged(propertyName);
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
