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
    public partial class FrmConsulta : Form, ICadForm
    {
        Consulta objConsulta;
        Conexao con;

        public void bloquearCampos()
        {
            txtID.ReadOnly = true;
            txtMotivo.ReadOnly = true;
            dgvConsulta.Enabled = false;
            txtDiagnostico.ReadOnly = true;
            txtReceita.ReadOnly = true;
            dgvRetorno.Enabled = false;
            txtMotivo.ReadOnly = true;

        }

        public void desbloquearCampos()
        {
            txtID.ReadOnly = false;
            txtMotivo.ReadOnly = false;
            dgvConsulta.Enabled = true;
            txtDiagnostico.ReadOnly = false;
            txtReceita.ReadOnly = false;
            dgvRetorno.Enabled = true;
            txtMotivo.ReadOnly = false;
        }

        public void lerDados()
        {
            objConsulta = new Consulta();
            objConsulta.idConsulta = int.Parse(txtID.Text.Trim());
            objConsulta.Motivo = txtMotivo.Text;
            objConsulta.dt_Consulta = dgvConsulta.Value;
            objConsulta.Diagnostico = txtDiagnostico.Text;
            objConsulta.Receita = txtReceita.Text;
            objConsulta.dt_Retorno = dgvRetorno.Value;
            objConsulta.Motivo_retorno = txtMotivoRetorno.Text;
            objConsulta.idPaciente = int.Parse(cbPaciente.Text.Trim());
            objConsulta.idDentista = int.Parse(cbPaciente.Text.Trim());
        }

        

        private void comboDentista()
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

            cbDentista.DataSource = null;
            cbDentista.DataSource = listDentista;
            cbDentista.ValueMember = "idDentista";
            cbDentista.DisplayMember = "nomeDentista";

        }

        public void limparDados()
        {
            txtID.Text = "";
            txtMotivo.Text = "";
            dgvConsulta.Text = "";
            dgvRetorno.Text = "";
            txtDiagnostico.Text = "";
            txtReceita.Text = "";
            txtMotivoRetorno.Text = "";
            cbDentista.Text = "";
            cbPaciente.Text = "";

        }
        

        private void comboPaciente()
        {
            List<Paciente> listPaciente = new List<Paciente>();
            con.conectar();
            SqlDataReader reader;
            reader = con.exeConsulta("select * from tb_paciente");

            if (reader.HasRows)
            {
                while (reader.Read())
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

            cbPaciente.DataSource = null;
            cbPaciente.DataSource = listPaciente;
            cbPaciente.DisplayMember = "nomePaciente";
            cbPaciente.ValueMember = "idPaciente";
        }

       

        public void atualizarGrid()
        {
            List<Consulta> listConsulta = new List<Consulta>();
            con.conectar();
            SqlDataReader reader;
            reader = con.exeConsulta("SELECT * FROM tb_consulta");

            if (reader.Read())
            {
                while (reader.Read())
                {
                    Consulta consulta = new Consulta();
                    consulta.idConsulta = reader.GetInt32(0);
                    consulta.Motivo = reader.GetString(1);
                    consulta.dt_Consulta = reader.GetDateTime(2);
                    consulta.Diagnostico = reader.GetString(3);
                    consulta.Receita = reader.GetString(4);
                    consulta.dt_Retorno = reader.GetDateTime(5);
                    consulta.Motivo_retorno = reader.GetString(6);
                    consulta.idPaciente = reader.GetInt32(7);
                    consulta.idDentista = reader.GetInt32(8);

                    listConsulta.Add(consulta);
                }
                reader.Close();
            }
            else
            {
                Console.WriteLine("Não retornou dados.");
            }
            dgvUsuario.DataSource = null;
            dgvUsuario.DataSource = listConsulta;
        }


        public FrmConsulta()
        {
            con = new Conexao();
            InitializeComponent();
        }

        private void FrmConsulta_Load(object sender, EventArgs e)
        {
            bloquearCampos();
            comboDentista();
            comboPaciente();
            atualizarGrid();
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

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            lerDados();
            String sql = "INSERT INTO tb_consulta VALUES (" + objConsulta.idConsulta + ",'" +
                objConsulta.Motivo + "','" +
                objConsulta.dt_Consulta + "','" +
                objConsulta.Diagnostico + "','" +
                objConsulta.Receita + "','" +
                objConsulta.dt_Retorno + "','" +
                objConsulta.Motivo_retorno +  "','" +
                objConsulta.idPaciente + "','" +
                objConsulta.idDentista + "')";

            if (con.executar(sql) == 1)
            {
                MessageBox.Show("Dados salvos com sucesso.");

            }
            else
            {
                MessageBox.Show("Dados não foram salvos.");
            }

            bloquearCampos();
            atualizarGrid();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza que deseja cancelar?", "Cancelando", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                this.Close();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            
        }

       
    }
}
