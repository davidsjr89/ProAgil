using System.Threading.Tasks;
using Domain;

namespace Repository
{
    public interface IRepository
    {
         void Add<T>(T entity) where T : class;
         void Update<T>(T entity) where T : class;
         void Delete<T>(T entity) where T : class;
         Task<bool> SaveChangesAsync();

         //EVENTOS
         Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrantes);
         Task<Evento[]> GetAllEventoAsync(bool includePalestrantes);
         Task<Evento[]> GetEventoAsyncByTema(int EventoId, bool includePalestrantes);
         Task<Evento> GetEventosAsyncById(int EventoId, bool includePalestrantes);

         //PALESTRANTE
         
         Task<Palestrante[]> GetAllPalestrantesAsyncByName(string name, bool includeEventos);
         Task<Palestrante> GetPalestranteAsync(int PalestrantesoId, bool includeEventos);
    }
}