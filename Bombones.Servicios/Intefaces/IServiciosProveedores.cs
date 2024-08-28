using Bombones.Entidades.Entidades;

namespace Bombones.Servicios.Intefaces
{
    public interface IServiciosProveedores
    {
        void Guardar(Proveedor? proveedor);
        void Borrar(int ProveedorId);
        List<Proveedor> GetLista();
    }
}
