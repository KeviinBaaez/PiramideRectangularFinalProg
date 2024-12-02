using PiramideRectangularFinalProg.Datos;
using PiramideRectangularFinalProg.Entidades;
using System.Reflection.Emit;

namespace PiramideRectangularFinalProg.Windows
{
    public partial class frmPiramides : Form
    {
        private Repositorio repositorio;
        private bool filtro = false;
        public frmPiramides()
        {
            InitializeComponent();
            repositorio = new Repositorio();
            TotalPages();
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmPiramideAE frm = new frmPiramideAE(filtro) { Text = "nueva piramide" };
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.Cancel) return;
            Piramide? piramide = frm.GetPiramide();
            if (piramide is null) return;
            var resultado = repositorio.AgregarPiramide(piramide);
            if (resultado.exito)
            {
                MessageBox.Show($"{resultado.mensaje}",
                    "Mensaje",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                LoadData();
                TotalPages();
            }
            else
            {
                MessageBox.Show($"{resultado.mensaje}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            dgvDatos.Rows.Clear();
            foreach (var item in repositorio.lista)
            {
                DataGridViewRow r = ConstruirFila(dgvDatos);
                SetearFila(r, item);
                AgregarFila(r);
            }
        }

        private void AgregarFila(DataGridViewRow r)
        {
            dgvDatos.Rows.Add(r);
        }

        private void SetearFila(DataGridViewRow r, Piramide item)
        {
            r.Cells[colLado.Index].Value = item.LadoBase;
            r.Cells[colAltura.Index].Value = item.Altura;
            r.Cells[colCantidad.Index].Value = item.CantidadLados;
            r.Cells[colMaterial.Index].Value = item.Material;
            r.Cells[colColor.Index].Value = item.Color;
            r.Cells[colVolumen.Index].Value = item.GetVolumen();
        }

        private DataGridViewRow ConstruirFila(DataGridView dgvDatos)
        {
            DataGridViewRow r = new DataGridViewRow();
            r.CreateCells(dgvDatos);
            return r;
        }

        private void tsbBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0) return;
            var r = dgvDatos.SelectedRows[0];

            int lado = (int)r.Cells[0].Value;
            int altura = (int)r.Cells[1].Value;
            int cantidadLados = (int)r.Cells[2].Value;
            Material material = (Material)(int)r.Cells[3].Value;
            Colores color = (Colores)(int)r.Cells[4].Value;

            Piramide piramide = new Piramide(lado, altura, cantidadLados, color, material);

            DialogResult dr = MessageBox.Show($"Deseas borrar la piramide:\n {piramide.ToString()}?",
                "Mensaje",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dr == DialogResult.No) return;
            var resultado = repositorio.EliminarPiramide(piramide);
            if (resultado.exito)
            {
                MessageBox.Show($"{resultado.mensaje}",
                    "Mensaje",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                QuitarFila(r);
                TotalPages();
            }
            else
            {
                MessageBox.Show($"{resultado.mensaje}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void QuitarFila(DataGridViewRow r)
        {
            dgvDatos.Rows.Remove(r);
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0) return;
            var r = dgvDatos.SelectedRows[0];

            int lado = (int)r.Cells[0].Value;
            int altura = (int)r.Cells[1].Value;
            int cantidadLados = (int)r.Cells[2].Value;
            Material material = (Material)(int)r.Cells[3].Value;
            Colores color = (Colores)(int)r.Cells[4].Value;

            Piramide piramide = new Piramide(lado, altura, cantidadLados, color, material);
            Piramide? piramideEditada = new Piramide(lado, altura, cantidadLados, color, material);

            frmPiramideAE frm = new frmPiramideAE(filtro) { Text = "Editar piramide" };
            frm.SetPiramide(piramide);
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.Cancel) return;
            piramideEditada = frm.GetPiramide();
            if (piramideEditada is null) return;

            var resultado = repositorio.EditarPiramide(piramide, piramideEditada);
            if (resultado.exito)
            {
                MessageBox.Show($"{resultado.mensaje}",
                    "Mensaje",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                SetearFila(r, piramideEditada);
            }
            else
            {
                MessageBox.Show($"{resultado.mensaje}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void tsbActualizar_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void tsbSalir_Click(object sender, EventArgs e)
        {
            repositorio.GuardarDatos();
            Application.Exit();
        }

        private void frmPiramides_Load(object sender, EventArgs e)
        {
            LoadData();
            TotalPages();
        }

        private void TotalPages()
        {
            var total = repositorio.GetCantidad();
            txtCantidad.Text = total.ToString();
        }

        private void lado09ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgvDatos.Rows.Clear();
            foreach (var item in repositorio.lista.OrderBy(p => p.LadoBase))
            {
                DataGridViewRow r = ConstruirFila(dgvDatos);
                SetearFila(r, item);
                AgregarFila(r);
            }
        }

        private void lado90ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgvDatos.Rows.Clear();
            foreach (var item in repositorio.lista.OrderByDescending(p => p.LadoBase))
            {
                DataGridViewRow r = ConstruirFila(dgvDatos);
                SetearFila(r, item);
                AgregarFila(r);
            }
        }

        private void tsbFiltrar_Click(object sender, EventArgs e)
        {

        }

        private void bordeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filtro = true;
            frmPiramideAE frm = new frmPiramideAE(filtro) { Text = "Filtro color" };
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.Cancel) return;
            Colores? colorSeleccionado = frm.GetColor();
            if (colorSeleccionado == null) return;
            var resultado = repositorio.GetListaColor((Colores)colorSeleccionado);
            if (resultado.exito)
            {
                dgvDatos.Rows.Clear();
                foreach (var item in repositorio.listaColor)
                {
                    DataGridViewRow r = ConstruirFila(dgvDatos);
                    SetearFila(r, item);
                    AgregarFila(r);
                    TotalPages();
                }
            }
            else
            {
                MessageBox.Show($"No se encontraron datos de ese color",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }
    }
}
