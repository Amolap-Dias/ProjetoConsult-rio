using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Consultório
{
    public partial class FrmDentista : Form, ICadForm
    {

        Dentista objDentista;
        Conexao con;

        public void bloquearBotoes()
        {
            btnSalvar.Enabled = false;
            btnExcluir.Enabled = false;
        }

        public void desbloquearBotoes()
        {
            btnSalvar.Enabled = true;
            btnExcluir.Enabled = true;
        }

        public FrmDentista()
        {
            InitializeComponent();
            con = new Conexao();
        }

        public void atualizarGrid()
        {

            List<Dentista> listDentista = new List<Dentista>();
            con.conectar();
            SqlDataReader reader;
            reader = con.exeConsulta("select * from tb_dentista");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Dentista dentista = new Dentista();
                    dentista.idDentista = reader.GetInt32(0);
                    dentista.Nome = reader.GetString(1);
                    dentista.Cro = reader.GetString(2);
                    dentista.Sexo = reader.GetValue(3) == null ? "" : reader.GetValue(3).ToString();
                    dentista.Linkedin = reader.GetValue(4).ToString() == "true" ? 1 : 0;
                    dentista.Instagram = reader.GetValue(5).ToString() == "true" ? 1 : 0;
                    dentista.Facebook = reader.GetValue(6).ToString() == "true" ? 1 : 0;
                    dentista.Twitter = reader.GetValue(7).ToString() == "true" ? 1 : 0;

                    listDentista.Add(dentista);
                }
                reader.Close();
            }
            else
            {
                Console.WriteLine("Não retornou dados. ");
            }

            dgvDados.DataSource = null;
            dgvDados.DataSource = listDentista;

        }




        private void btnNovo_Click(object sender, EventArgs e)
        {
            bloquearBotoes();
            desbloquearCampos();
            limparDados();
        }

        private void Dentista_Load(object sender, EventArgs e)
        {
            atualizarGrid();
            bloquearCampos();
        }

        public void desbloquearCampos()
        {
            txtID.ReadOnly = false;
            txtNome.ReadOnly = false;
            txtCRO.ReadOnly = false;
        }

        public void bloquearCampos()
        {
            txtID.ReadOnly = true;
            txtNome.ReadOnly = true;
            txtCRO.ReadOnly = true;

        }

        public void lerDados()
        {
            objDentista = new Dentista();
            objDentista.idDentista = int.Parse(txtID.Text.Trim());
            objDentista.Nome = txtNome.Text;
            objDentista.Cro = txtCRO.Text;

            objDentista.Instagram = chbInstagram.Checked ? 1 : 0;
            objDentista.Twitter = chbTwitter.Checked ? 1 : 0;
            objDentista.Facebook = chbFacebook.Checked ? 1 : 0;
            objDentista.Linkedin = chbLinkedin.Checked ? 1 : 0;

            if (rbFeminino.Checked)

            {
                objDentista.Sexo = "F";

            }
            else
            {
                objDentista.Sexo = "M";

            }
        }

        public void limparDados()
        {
            txtID.Text = "";
            txtCRO.Text = "";
            txtNome.Text = "";

        }

        private void dgvDados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dgvDados.CurrentRow.Cells[0].Value.ToString();
            txtNome.Text = dgvDados.CurrentRow.Cells[1].Value.ToString();
            txtCRO.Text = dgvDados.CurrentRow.Cells[2].Value.ToString();

            rbFeminino.Checked = dgvDados.CurrentRow.Cells[3].Value.Equals(1) ? true : false;
            rbMasculino.Checked = dgvDados.CurrentRow.Cells[4].Value.Equals(1) ? true : false;

            chbFacebook.Checked = dgvDados.CurrentRow.Cells[5].Value.Equals(1);
            chbInstagram.Checked = dgvDados.CurrentRow.Cells[6].Value.Equals(1);
            chbLinkedin.Checked = dgvDados.CurrentRow.Cells[7].Value.Equals(1);
            chbTwitter.Checked = dgvDados.CurrentRow.Cells[8].Value.Equals(1);

            // Também pode fazer assim: chbFacebook.Checked = dgvDados.CurrentRow.Cells[4].Value.Equals(1) ? true : false;


        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            lerDados();
            String sql = "INSERT INTO tb_dentista VALUES (" + objDentista.idDentista + ", '" +
                objDentista.Nome + "','" +
                objDentista.Cro + "','" +
            objDentista.Sexo + "','" +
            objDentista.Instagram + "','" +
            objDentista.Facebook + "','" +
            objDentista.Twitter + "','" +
            objDentista.Linkedin + "')";

            if (con.executar(sql) == 1)
            {
                MessageBox.Show("Dados salvos com sucesso!");
            }

            else
            {
                MessageBox.Show("Dados não foram salvos!");
            }

            bloquearCampos();
            atualizarGrid();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limparDados();
            desbloquearBotoes();
            /*    if (MessageBox.Show("Tem certeza que deseja cancelar?", "Cancelando",MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {

                     this.Close();
                 }*/
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            con.conectar();
            DialogResult op = MessageBox.Show("Tem certeza que deseja excluir?", "Excluir?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (op == DialogResult.Yes)
            {
                Dentista dentista = new Dentista();
                dentista.idDentista = int.Parse(txtID.Text.ToString());
                String sql = "DELETE FROM tb_dentista WHERE id_dentista =" + dentista.idDentista;

                if(con.executar(sql)==1)
                {
                    MessageBox.Show("Excluindo");

                }
                else
                {
                    MessageBox.Show("Não foi excluido");
                }
                atualizarGrid();
            }

            
        }

        private void btnpesquisar_Click(object sender, EventArgs e)
        {
            Pesquisar pesquisar = new Pesquisar();
            pesquisar.Show();
        }
    }
}

