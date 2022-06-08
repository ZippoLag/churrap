using Churrap.Models;
using Xamarin.Forms.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Churrap.Services
{
    public class MockChurrerxService : IChurrerxService
    {
        readonly List<Churrerx> _churrerxs;

        public MockChurrerxService()
        {
            _churrerxs = new List<Churrerx>()
            {
                new Churrerx { Nombre = "Turgut", Posicion=new Position(-32.958961, -60.659928) },
                new Churrerx { Nombre = "Jorge", Posicion=new Position(-32.9588, -60.6598) },
                new Churrerx { Nombre = "Juan", Posicion=new Position(-32.9587, -60.6597) },
                new Churrerx { Nombre = "Martina", Posicion=new Position(-32.959, -60.655) },
                new Churrerx { Nombre = "Junior", Posicion=new Position(-32.9584, -60.659) },
                new Churrerx { Nombre = "Fran", Posicion=new Position(-32.9589, -60.653) }
            };
        }

        public async Task<bool> AgregarChurrerxAsync(Churrerx churrerx)
        {
            _churrerxs.Add(churrerx);

            return await Task.FromResult(true);
        }

        public async Task<bool> ActualizarPosicionChurrerxAsync(string nombre, Position nuevaPosicion)
        {
            Churrerx churrerx = await GetChurrerxAsync(nombre);
            churrerx.Posicion = nuevaPosicion;

            return await Task.FromResult(true);
        }

        public async Task<bool> BorrarChurrerxAsync(string nombre)
        {
            Churrerx churrerxARemover = await GetChurrerxAsync(nombre);
            bool success = _churrerxs.Remove(churrerxARemover);

            return await Task.FromResult(success);
        }

        public async Task<Churrerx> GetChurrerxAsync(string nombre)
        {
            return await Task.FromResult(_churrerxs.FirstOrDefault((Churrerx c) => c.Nombre == nombre));
        }

        public async Task<IEnumerable<Churrerx>> GetChurrerxsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(_churrerxs);
        }
    }
}