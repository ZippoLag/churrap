using Churrap.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace Churrap.Services
{
    public interface IChurrerxService
    {
        Task<bool> AgregarChurrerxAsync(Churrerx churrerx);
        Task<bool> ActualizarPosicionChurrerxAsync(string nombre, Position nuevaPosicion);
        Task<bool> BorrarChurrerxAsync(string nombre);
        Task<Churrerx> GetChurrerxAsync(string nombre);
        //TODO: refactorear o agregar otro que tome posicion y distancia como parametros
        Task<IEnumerable<Churrerx>> GetChurrerxsAsync(bool forceRefresh = false);
    }
}
