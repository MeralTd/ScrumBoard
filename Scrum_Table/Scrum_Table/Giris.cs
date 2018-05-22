using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Scrum_Table
{
    public partial class Giris : MetroFramework.Forms.MetroForm
    {
        public Giris()
        {
            InitializeComponent();
        }

        private void Giris_Load(object sender, EventArgs e)
        {

        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\toshıba\Desktop\Scrum_Table\Login.accdb");
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("select * from Login where kullaniciAdi='" + txtUsername.Text + "' and sifre='" + txtPassword.Text + "'", baglanti);
            OleDbDataReader dr = komut.ExecuteReader();

            if (dr.Read())
            {
                this.Hide();
                ScrumTable form2 = new ScrumTable();
                
                form2.Show();
            }
            else
            {
                MessageBox.Show("Kullanıcı Adı ve Parolayı kontrol ediniz.");
            }
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
