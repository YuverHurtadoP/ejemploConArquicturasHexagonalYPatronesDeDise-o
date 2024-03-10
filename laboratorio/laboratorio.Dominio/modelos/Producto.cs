using System;
using System.Collections.Generic;

namespace laboratorio.modelos;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string? Nombre { get; set; }

    public decimal? Precio { get; set; }

    public int? IdProveedor { get; set; }

    public virtual Proveedor? IdProveedorNavigation { get; set; }
}
