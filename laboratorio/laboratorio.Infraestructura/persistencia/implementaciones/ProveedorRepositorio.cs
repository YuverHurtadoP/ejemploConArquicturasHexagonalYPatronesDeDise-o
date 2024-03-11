using laboratorio.Dominio.repositorios;
using laboratorio.modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laboratorio.Infraestructura.persistencia.implementaciones
{
    public class ProveedorRepositorio : IProveedorRepositorio
    {
        private readonly LaboratorioContext _context;

        public ProveedorRepositorio(LaboratorioContext context)
        {
            _context = context;
        }

        public void Actualizar(Proveedor proveedor)
        {
            _context.Proveedors.Update(proveedor);
            _context.SaveChanges();
        }

        public void Agregar(Proveedor proveedor)
        {
            if (proveedor == null)
                throw new ArgumentNullException(nameof(proveedor));

            _context.Proveedors.Add(proveedor);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var proveedor = _context.Proveedors.Find(id);
            if (proveedor == null)
                return;

            _context.Proveedors.Remove(proveedor);
            _context.SaveChanges();
        }

        public Proveedor ObtenerPorId(int id)
        {
            return _context.Proveedors.FirstOrDefault(p => p.IdProveedor == id);
        }

        public IEnumerable<Proveedor> ObtenerTodos()
        {
            return _context.Proveedors.ToList();
        }
    }
}
