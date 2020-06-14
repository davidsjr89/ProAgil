using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class Repositorio : IRepository
    {
        public ProAgilContext _context { get; set; }
        public Repositorio(ProAgilContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        //GERAL
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
        //EVENTO
        public async Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
                                        .Include(c => c.Lotes)
                                        .Include(c => c.RedesSociais);
            if(includePalestrantes)
            {
                query = query.Include(pe => pe.PalestranteEventos)
                             .Include(p => p.Palestrantes);
            }
            query = query.AsNoTracking()
                            .OrderByDescending(p => p.DataEvento)
                            .Where(t => t.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }
        public async Task<Evento[]> GetAllEventoAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                                        .Include(c => c.Lotes)
                                        .Include(c => c.RedesSociais);
            if(includePalestrantes)
            {
                query = query
                        .Include(pe => pe.PalestranteEventos)
                        .Include(p => p.Palestrantes);
            }
            query = query.AsNoTracking()
                            .OrderByDescending(c => c.DataEvento);
            return await query.ToArrayAsync();
        }
        
        public async Task<Evento[]> GetEventoAsyncByTema(int EventoId, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
                                        .Include(c => c.Lotes)
                                        .Include(c => c.RedesSociais);
            if(includePalestrantes)
            {
                query = query.Include(pe => pe.PalestranteEventos)
                             .Include(p => p.Palestrantes);
            }
            query = query.OrderByDescending(p => p.DataEvento)
                            .Where(t => t.Id == EventoId);

            return await query.ToArrayAsync();
        }


        public async Task<Evento> GetEventosAsyncById(int EventoId, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
                                        .Include(c => c.Lotes)
                                        .Include(c => c.RedesSociais);
            if(includePalestrantes)
            {
                query = query.Include(pe => pe.PalestranteEventos)
                             .Include(p => p.Palestrantes);
            }
            query = query.AsNoTracking()
                            .OrderByDescending(p => p.DataEvento)
                            .Where(t => t.Id == EventoId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsyncByName(string name, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                                        .Include(c => c.RedesSociais);
            if(includeEventos)
            {
                query = query.Include(pe => pe.PalestranteEventos)
                             .Include(e => e.Eventos);
            }
            query = query.AsNoTracking()
                            .Where(p => p.Nome.ToLower().Contains(name.ToLower()));
            
            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteAsync(int PalestrantesoId, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                                        .Include(c => c.RedesSociais);
            if(includeEventos)
            {
                query = query.Include(pe => pe.PalestranteEventos)
                             .Include(e => e.Eventos);
            }
            query = query.OrderBy(p => p.Nome)
                        .Where(p => p.Id == PalestrantesoId);
            
            return await query.FirstOrDefaultAsync();
        }
    }
}