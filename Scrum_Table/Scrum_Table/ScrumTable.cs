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
    public partial class ScrumTable : MetroFramework.Forms.MetroForm
    {
        public ScrumTable()
        {
            InitializeComponent();
            panel = new Panel[] { pnlNotStarted, pnlInProgress, pnlDone };
            curren_panel = 0;


        }
       
        private void label_MouseDown(object sender, MouseEventArgs e)
        {
            p = e.Location;
            if (e.Button == MouseButtons.Left)
                DoDragDrop(sender, DragDropEffects.All);
        }
        private void panel_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void panel_DragDrop(object sender, DragEventArgs e)
        {
            Panel pnl = sender as Panel;
            Label src = e.Data.GetData(typeof(Label)) as Label;
            src.Location = pnl.PointToClient(new Point(e.X, e.Y));
            pnl.Controls.Add(src);
           
        }

        private void ScrumTable_Load(object sender, EventArgs e)
        {
            ControlExtension.Draggable(pnlNotStarted, true);
            ControlExtension.Draggable(pnlInProgress, true);
            ControlExtension.Draggable(pnlDone, true);

        }
        private Point p;
        int curren_panel;

        ColorDialog MyDialog = new ColorDialog();
        Panel[] panel;
        Label label1;
        Random renk = new Random();
        int Sayac = 0;
        int Sayac1 = 0;
        int Sayac2 = 0;
        int Sayac3 = 0;

        OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\toshıba\Desktop\Scrum_Table\kayıtEkle.accdb");
        OleDbCommand komut = new OleDbCommand();
        OleDbDataAdapter da = new OleDbDataAdapter();
        DataSet ds = new DataSet();
         
    

        public void Read()
        {
            baglanti.Open();
            OleDbCommand cmd = new OleDbCommand("SELECT TaskSorumlusu,TaskBaslamaTarihi,TaskBitisTarihi,Task FROM KayıtEkle", baglanti);
            OleDbDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                label1.Text = dr["TaskSorumlusu"].ToString() + Environment.NewLine + dr["TaskBaslamaTarihi"].ToString() + Environment.NewLine + dr["TaskBitisTarihi"].ToString() + Environment.NewLine + dr["Task"].ToString();

            }
            baglanti.Close();
        }
        
        public void LabelOlustur()
        {
            
            label1 = new Label();
            label1.AllowDrop = true;
            label1.Size = new System.Drawing.Size(100, 20);
            label1.Width = 250;
            label1.Height = 100;
            label1.BackColor = Color.FromArgb(renk.Next(0, 255), renk.Next(0, 255), renk.Next(0, 255)); 
            TxtTaskAdSoyad.Text = "";
            txtTask.Text = "";
            rdbtnNotStarted.Checked = false;
            rdbtnInProgress.Checked = false;
            rdbtnDone.Checked = false;
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label_MouseDown);


        }


        private void btnStoryEkle_Click(object sender, EventArgs e)
        {
            if (txtStoryAdSoyad.Text != "" && txtStory.Text != "")
            {
                komut.Connection = baglanti;
                komut.CommandText = "INSERT INTO KayıtEkle(StoryYazanKisi,BaslamaTarihi,BitisTarihi,Story) VALUES('" + txtStoryAdSoyad.Text + "','" + dTBaslangicTarihi.Text + "','" + dTBitisTarihi.Text + "','" + txtStory.Text + "')";
                baglanti.Open();
                komut.ExecuteNonQuery();
                komut.Dispose();
                baglanti.Close();
                MessageBox.Show(" Kayıt tamamlandı. ");


            }
            else
            {
                MessageBox.Show(" Boş Alan Bırakmayınız. ");
            }

            label1 = new Label();
            label1.Size = new System.Drawing.Size(100, 20);
            label1.Width = 300;
            label1.Height = 175;
            label1.Location = new System.Drawing.Point(15, 50 + 185 * Sayac++);
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(100)));
            label1.BackColor = Color.FromArgb(renk.Next(0, 255), renk.Next(0, 255), renk.Next(0, 255));
            pnlStories.Controls.Add(label1);
            txtStoryAdSoyad.Text = "";
            txtStory.Text = "";

            baglanti.Open();
            OleDbCommand cmd = new OleDbCommand("SELECT StoryYazanKisi,BaslamaTarihi,BitisTarihi,Story FROM KayıtEkle", baglanti);
            OleDbDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                label1.Text = dr["StoryYazanKisi"].ToString() + Environment.NewLine + dr["BaslamaTarihi"].ToString()+ Environment.NewLine + dr["BitisTarihi"].ToString() + Environment.NewLine + dr["Story"].ToString(); ;

            }
            baglanti.Close();

        }

        private void btnTaskEkle_Click(object sender, EventArgs e)
        {
            if (TxtTaskAdSoyad.Text != "" && txtTask.Text != "" || rdbtnNotStarted.Checked == false || rdbtnInProgress.Checked == false || rdbtnDone.Checked == false)
            {
                komut.Connection = baglanti;
                komut.CommandText = "INSERT INTO KayıtEkle(TaskSorumlusu,TaskBaslamaTarihi,TaskBitisTarihi,Task) VALUES('" + TxtTaskAdSoyad.Text + "','" + dT_BaslangicTarihi.Text + "','" + dT_BitisTarihi.Text + "','" + txtTask.Text + "')";
                baglanti.Open();
                komut.ExecuteNonQuery();
                komut.Dispose();
                baglanti.Close();
                MessageBox.Show("         Kayıt tamamlandı.            ");
            }
            else
            {
                MessageBox.Show("          Boş Alan Bırakmayınız.         ");
            }
            
            if (rdbtnNotStarted.Checked == true)
            {
                LabelOlustur();
                label1.Location = new System.Drawing.Point(30, 50 + 110 * Sayac1++);
                pnlNotStarted.Controls.Add(label1);
                Read();
               
            }

            else if (rdbtnInProgress.Checked == true)
             {
                 LabelOlustur();
                 label1.Location = new System.Drawing.Point(30, 50 + 110 * Sayac2++);
                 pnlInProgress.Controls.Add(label1);
                 Read();

             }
            else if (rdbtnDone.Checked == true)
            {
                LabelOlustur();
                label1.Location = new System.Drawing.Point(30, 50 + 110 * Sayac3++);
                pnlDone.Controls.Add(label1);
                Read();

            }




        }

    }
}
