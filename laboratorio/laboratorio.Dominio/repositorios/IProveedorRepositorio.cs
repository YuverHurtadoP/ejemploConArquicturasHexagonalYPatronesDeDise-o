using laboratorio.modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laboratorio.Dominio.repositorios
{
    public interface IProveedorRepositorio
    {
        IEnumerable<Proveedor> ObtenerTodos();
        Proveedor ObtenerPorId(int id);
        void Agregar(Proveedor proveedor);
        void Actualizar(Proveedor proveedor);
        void Eliminar(int id);
    }
}
