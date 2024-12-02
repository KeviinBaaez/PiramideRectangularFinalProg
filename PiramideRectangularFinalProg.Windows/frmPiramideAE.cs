using PiramideRectangularFinalProg.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PiramideRectangularFinalProg.Windows
{
    public partial class frmPiramideAE : Form
    {
        private Piramide piramide;
        private bool filtro;
        private Colores colorSeleccionado;
        public frmPiramideAE(bool filtro)
        {
            InitializeComponent();
            CargarComboColores();

            this.filtro = filtro;

        }

        private void CargarComboColores()
        {
            cboColores.DataSource = Enum.GetValues(typeof(Colores));
            cboColores.SelectedIndex = 0;
        }

        public Piramide? GetPiramide()
        {
            return piramide;
        }

        public void SetPiramide(Piramide piramide)
        {
            this.piramide = piramide;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void frmPiramideAE_Load(object sender, EventArgs e)
        {
            if(filtro == false)
            {
                if(piramide != null)
                {
                    txtLado.Text = piramide.LadoBase.ToString();
                    txtAltura.Text = piramide.Altura.ToString();
                    nudCantidad.Value = piramide.CantidadLados;
                    cboColores.SelectedIndex = (int)piramide.Color;
                }
            }
            else
            {
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;

                txtLado.Enabled = false;
                txtAltura.Enabled = false;
                nudCantidad.Enabled = false;

                groupBox1.Enabled = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
        if(filtro == false)
            {
                if (ValidarDatos())
                {
                    if (piramide is null)
                    {
                        piramide = new Piramide();
                    }
                    piramide.LadoBase = int.Parse(txtLado.Text);
                    piramide.Altura = int.Parse(txtAltura.Text);
                    piramide.CantidadLados = (int)nudCantidad.Value;
                    piramide.Color = (Colores)(int)cboColores.SelectedValue!;
                    if (rbtMadera.Checked)
                    {
                        piramide.Material = Material.Madera;
                    }
                    else if (rbtPlastico.Checked)
                    {
                        piramide.Material = Material.Plastico;
                    }
                    else
                    {
                        piramide.Material = Material.Vidrio;
                    }

                    DialogResult = DialogResult.OK;
                }
            }
            else
            {
                colorSeleccionado = (Colores)(int)cboColores.SelectedValue!;
                DialogResult = DialogResult.OK;
            }

        }

        private bool ValidarDatos()
        {
            bool valido = true;
            int c;
            if(!int.TryParse(txtLado.Text,  out c))
            {
                valido = false;
            }
            if(!int.TryParse(txtAltura.Text, out c))
            {
                valido = false;
            }
            return valido;
        }

        public Colores? GetColor()
        {
            return colorSeleccionado;
        }
    }
}
