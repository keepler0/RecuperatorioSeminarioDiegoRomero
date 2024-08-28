using Bombones.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bombones.Datos.Interfaces
{
    public interface IRepositorioProveedores
    {
        void Agregar(Proveedor proveedor, SqlConnection conexion, SqlTransaction tran);
        void Borrar(int proveedorId, SqlConnection conexion, SqlTransaction tran);
        void Editar(Proveedor proveedor, SqlConnection conexion, SqlTransaction tran);
        List<Proveedor> GetLista(SqlConnection conexion);
    }
}
