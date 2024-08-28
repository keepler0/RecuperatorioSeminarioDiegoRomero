using Bombones.Entidades.Entidades;

namespace Bombones.Windows.Formularios
{
    public partial class frmProveedoresAE : Form
    {
        private Proveedor? proveedor;
        public frmProveedoresAE()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (proveedor is not null)
            {
                txtProveedor.Text = proveedor.NombreProveedor;
                txtTelefono.Text = proveedor.Telefono;
                txtMail.Text = proveedor.Email;
            }
        }
        public Proveedor? GetProveedor() => proveedor;
        public void SetProveedor(Proveedor proveedor)
        {
            this.proveedor = proveedor;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                if (proveedor is null)
                {
                    proveedor = new Proveedor();
                }
                proveedor.NombreProveedor = txtProveedor.Text;
                proveedor.Telefono = txtTelefono.Text == string.Empty ? "Sin número telefonico" : txtTelefono.Text;
                proveedor.Email = txtMail.Text == string.Empty ? "Sin Email" : txtMail.Text;
                DialogResult = DialogResult.OK;
            }
        }

        private bool Validar()
        {
            if (string.IsNullOrEmpty(txtProveedor.Text.Trim()))
            {
                errorProvider1.SetError(txtProveedor, "Debe ingresar un nombre");
                return false;
            }
            return true;
        }
    }
}
