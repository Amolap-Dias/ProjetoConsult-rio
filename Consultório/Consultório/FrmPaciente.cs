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
    public partial class FrmPaciente : Form, ICadForm
    {  
        Paciente objPaciente;
        Conexao con;

        public FrmPaciente()
        {
            con = new Conexao();
            InitializeComponent();
        }

      

        public void atualizarGrid()
        {
            List<Paciente> listPaciente = new List<Paciente>();
            con.conectar();
            SqlDataReader reader;
            reader = con.exeConsulta("select * from tb_paciente");

          if (reader.HasRows)
            {
                while(reader.Read())
                {
                    Paciente paciente = new Paciente();
                    paciente.idPaciente = reader.GetInt32(0);
                    paciente.Nome = reader.GetString(1);
                    paciente.CPF = reader.GetString(2);
                    paciente.Endereco = reader.GetString(3);
                    paciente.Telefone = reader.GetString(4);
                    paciente.dtpPaciente = reader.GetDateTime(5);


                    listPaciente.Add(paciente);
                }

                reader.Close();
                reader.Dispose();
                
            }
            else
            {
                Console.WriteLine("Não retornou dados.");
            }

            dgvPaciente.DataSource = null;
            dgvPaciente.DataSource = listPaciente;
            // 

        }

        public void lerDados()
        {

            objPaciente = new Paciente();
            objPaciente.idPaciente = int.Parse(txtID.Text.Trim());
            objPaciente.Nome = txtNome.Text;
            objPaciente.CPF = txtCPF.Text;
            objPaciente.Telefone = txtTelefone.Text;
            objPaciente.Endereco = txtEndereco.Text;
            objPaciente.dtpPaciente = dtpPaciente.Value;

        }

        private void Paciente_Load(object sender, EventArgs e)
        {
            atualizarGrid();
            bloquearCampos();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            desbloquearCampos();
            limparDados();
        }


        public void bloquearCampos()
        {
            txtID.ReadOnly = true;
            txtNome.ReadOnly = true;
            txtCPF.ReadOnly = true;
            txtEndereco.ReadOnly = true;
            txtTelefone.ReadOnly = true;
            dtpPaciente.Enabled = false;
        }

        public void desbloquearCampos()
        {
            txtID.ReadOnly = false;
            txtNome.ReadOnly = false;
            txtCPF.ReadOnly = false;
            txtEndereco.ReadOnly = false;
            txtTelefone.ReadOnly = false;
            dtpPaciente.Enabled = true;
        }



        public void limparDados()
        {
            txtID.Text = "";
            txtNome.Text = "";
            txtCPF.Text = "";
            txtEndereco.Text = "";
            txtTelefone.Text = "";
            dtpPaciente.Text = "";
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            lerDados();
            String sql = "INSERT INTO tb_paciente VALUES (" + objPaciente.idPaciente + ",'" +
                objPaciente.Nome + "','" +
                objPaciente.CPF + "','" +
                objPaciente.Endereco + "','" +
                objPaciente.Telefone + "','" +
                objPaciente.dtpPaciente +"')";

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

        private void btnExcluir_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza que deseja cancelar?", "Cancelando", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                this.Close();
            }
        }

        private void dgvPaciente_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           txtID.Text = dgvPaciente.CurrentRow.Cells[0].Value.ToString();
           txtNome.Text = dgvPaciente.CurrentRow.Cells[1].Value.ToString();
           txtCPF.Text = dgvPaciente.CurrentRow.Cells[2].Value.ToString();
           txtEndereco.Text = dgvPaciente.CurrentRow.Cells[3].Value.ToString();
           txtTelefone.Text = dgvPaciente.CurrentRow.Cells[4].Value.ToString();
           dtpPaciente.Text = dgvPaciente.CurrentRow.Cells[5].Value.ToString();
           

        }
    }
}
