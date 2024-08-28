using Bombones.Entidades.Entidades;
using Bombones.Servicios.Intefaces;
using Bombones.Windows.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bombones.Windows.Formularios
{
    public partial class frmProveedores : Form
    {
        private readonly IServiceProvider? _serviceProvider;
        private readonly IServiciosProveedores? _servicios;
        private List<Proveedor>? lista;
        public frmProveedores(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _servicios = _serviceProvider.GetService<IServiciosProveedores>();
        }

        private void frmProveedores_Load(object sender, EventArgs e)
        {
            lista = _servicios?.GetLista();
            LlenarGrilla();
        }

        private void LlenarGrilla()
        {
            GridHelper.LimpiarGrilla(dgvDatos);
            foreach (var item in lista)
            {
                var r = GridHelper.ConstruirFila(dgvDatos);
                GridHelper.SetearFila(r, item);
                GridHelper.AgregarFila(r, dgvDatos);
            }
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            var frm = new frmProveedoresAE() { Text = "Agregando proveedor" };
            var dr = frm.ShowDialog();
            if (dr == DialogResult.Cancel) return;
            try
            {
                //voy a omitir la  verificacion de existencia
                var nuevoProveedor = frm.GetProveedor();
                _servicios.Guardar(nuevoProveedor);
                var r = GridHelper.ConstruirFila(dgvDatos);
                GridHelper.SetearFila(r, nuevoProveedor);
                GridHelper.AgregarFila(r, dgvDatos);
                MessageBox.Show("proveedor agregado");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
        }

        private void tsbBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0) return;
            var r = dgvDatos.SelectedRows[0];
            var pr = r.Tag as Proveedor;
            try
            {
                _servicios.Borrar(pr.ProveedorId);
                GridHelper.QuitarFila(r);
                MessageBox.Show("Registro eliminado");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0) return;
            var r = dgvDatos.SelectedRows[0];
            var pr = r.Tag as Proveedor;
            try
            {
                var frm = new frmProveedoresAE(){Text="Editando proveedor" };
                frm.SetProveedor(pr);
                var dr = frm.ShowDialog();
                if (dr == DialogResult.Cancel) return;
                var prEditado = frm.GetProveedor();
                _servicios.Guardar(prEditado);
                GridHelper.SetearFila(r, prEditado);
                MessageBox.Show("Registro editado");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
