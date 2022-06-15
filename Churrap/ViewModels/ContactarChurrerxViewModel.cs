using Churrap.Models;
using Churrap.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;
using System.Threading;

namespace Churrap.ViewModels
{
    [QueryProperty(nameof(Nombre), nameof(Nombre))]
    public class ContactarChurrerxViewModel : BaseViewModel
    {
        private string _nombre;
        private Churrerx _selectedChurrerx;
        public Churrerx SelectedChurrerx { get => _selectedChurrerx; private set => SetProperty(ref _selectedChurrerx, value); }

        public ContactarChurrerxViewModel()
        {
            
        }

        public void OnAppearing()
        {
            Title = $"Contactar a {_selectedChurrerx?.Nombre}?";
        }
        public void OnDisappearing()
        {
        }

        public string Nombre
        {
            get => _nombre;
            set
            {
                _nombre = value;
                CargarChurrerxAsync(value);
            }
        }

        private async void CargarChurrerxAsync(string nombre)
        {
            try
            {
                SelectedChurrerx = await ChurrerxService.GetChurrerxAsync(nombre);
            }
            catch (Exception)
            {
                Debug.WriteLine($"No se pudo cargar al Churrerx {nombre}");
            }
        }
    }
}