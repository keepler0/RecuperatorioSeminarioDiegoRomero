using Bombones.Datos.Interfaces;
using Bombones.Entidades.Entidades;
using Bombones.Servicios.Intefaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bombones.Servicios.Servicios
{
    public class ServiciosProveedores : IServiciosProveedores
    {
        private readonly IRepositorioProveedores? _repositorio;
        private readonly string? _cadenaConexion;

        public ServiciosProveedores(IRepositorioProveedores? repositorio, string? cadenaConexion)
        {
            _repositorio = repositorio;
            _cadenaConexion = cadenaConexion;
        }

        public void Borrar(int ProveedorId)
        {
            using (var conexion=new SqlConnection(_cadenaConexion))
            {
                conexion.Open();
                using (var tran = conexion.BeginTransaction())
                {
                    try
                    {
                        _repositorio?.Borrar(ProveedorId, conexion, tran);
                        tran.Commit();
                    }
                    catch (Exception)
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<Proveedor> GetLista()
        {
            List<Proveedor> lista;
            using (var conexion=new SqlConnection(_cadenaConexion))
            {
                conexion.Open();
                lista=_repositorio.GetLista(conexion);
            }
            return lista;
        }

        public void Guardar(Proveedor proveedor)
        {
            using (var conexion=new SqlConnection(_cadenaConexion))
            {
                conexion.Open();
                using (var tran=conexion.BeginTransaction())
                {
                    try
                    {
                        if (proveedor.ProveedorId==0)
                        {
                            _repositorio?.Agregar(proveedor,conexion,tran);
                        }
                        else
                        {
                            _repositorio?.Editar(proveedor,conexion,tran);
                        }
                        tran.Commit();
                    }
                    catch (Exception)
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
