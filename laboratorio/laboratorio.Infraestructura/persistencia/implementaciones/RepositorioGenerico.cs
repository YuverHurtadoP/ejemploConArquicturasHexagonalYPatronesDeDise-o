using laboratorio.Dominio.repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laboratorio.Infraestructura.persistencia.implementaciones
{
    public class RepositorioGenerico<T> : IRepositorioGenerico<T> where T : class
    {
        private readonly LaboratorioContext _context;

        public RepositorioGenerico(LaboratorioContext context)
        {
            _context = context;
        }
        public void Agregar(T entidad)
        {
            if (entidad == null)
                throw new ArgumentNullException(nameof(entidad));

            _context.Set<T>().Add(entidad);
            _context.SaveChanges();
        }

        public void Actualizar(T entidad)
        {
            if (entidad == null)
                throw new ArgumentNullException(nameof(entidad));

            _context.Set<T>().Update(entidad);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var entidad = _context.Set<T>().Find(id);
            if (entidad == null)
                return;

            _context.Set<T>().Remove(entidad);
            _context.SaveChanges();
        }

        public T ObtenerPorId(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> ObtenerTodos()
        {
            return _context.Set<T>().ToList();
        }
    }
}
