using Bombones.Datos.Interfaces;
using Bombones.Entidades.Entidades;
using Dapper;
using System.Data.SqlClient;

namespace Bombones.Datos.Repositorios
{
    public class RepositorioProveedores : IRepositorioProveedores
    {
        public void Agregar(Proveedor proveedor, SqlConnection conexion, SqlTransaction tran)
        {
            string InsertQuery = @"INSERT INTO Proveedores(NombreProveedor,Telefono,Email) 
                                   VALUES (@NombreProveedor,@Telefono,@Email);
                                   SELECT SCOPE_IDENTITY()";
            int id = conexion.QuerySingle<int>(InsertQuery, proveedor, tran);
            proveedor.ProveedorId = id == 0 ? throw new Exception("No ha sido posible agregar el registro") : id;
        }

        public void Borrar(int proveedorId, SqlConnection conexion, SqlTransaction tran)
        {
            string DeleteQuery = "DELETE FROM Proveedores WHERE ProveedorId=@ProveedorId";
            int row = conexion.Execute(DeleteQuery,new { @ProveedorId=proveedorId}, tran);
            if (row == 0) throw new Exception("No ha sido posible eliminar el registro");
        }

        public void Editar(Proveedor proveedor, SqlConnection conexion, SqlTransaction tran)
        {
            string UpdateQuery = "UPDATE Proveedores SET NombreProveedor=@NombreProveedor,Telefono=@Telefono,Email=@Email";
            int row = conexion.Execute(UpdateQuery, proveedor, tran);
        }

        public List<Proveedor> GetLista(SqlConnection conexion)
        {
            string SelectQuery = "SELECT NombreProveedor,Telefono,Email FROM Proveedores";
            return conexion.Query<Proveedor>(SelectQuery).ToList();
        }
    }
}
