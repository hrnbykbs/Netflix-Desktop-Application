using System;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Drawing;
using System.Diagnostics;

namespace ProLab2Proje3
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=netflixveritabani.accdb");
        OleDbCommand komut = new OleDbCommand();
        OleDbCommand kontrolkomut = new OleDbCommand();
        OleDbDataReader dr;
        DataTable tablo = new DataTable();
        private void Form4_Load(object sender, EventArgs e)
        {
            string debugyol = Application.StartupPath.ToString();
            string gifyol = debugyol + "\\netflixgif.gif";
            pictureBox1.Image = Image.FromFile(gifyol);
            pictureBox4.Visible = false;
            button1.Visible = false;
        }
        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            string debugyol = Application.StartupPath.ToString();

            if (radioButton1.Checked)
            {
                if (textBox1.Text.Trim() == "")
                {
                    //pictureBox4.BackgroundImage = null;
                    pictureBox4.Visible = false;
                    button1.Visible = false;
                    label1.Text = "";
                }

                else
                {
                    baglan.Open();
                    komut.Connection = baglan;
                    komut.CommandText = "SELECT * FROM program WHERE pAd LIKE '" + textBox1.Text + "%'";
                    dr = komut.ExecuteReader();
                    if (dr.Read())
                    {
                        pictureBox4.Visible = true;
                        button1.Visible = true;
                        string arananfilmad = dr["pAd"].ToString();
                        string arananfilmresim = debugyol + dr["pResim"];
                        int arananfilmid = Convert.ToInt32(dr["pID"]);
                        kullanicisinifi.arananfilmresim = arananfilmresim;
                        kullanicisinifi.arananfilmid = arananfilmid;
                        kullanicisinifi.arananfilmad = arananfilmad;
                        pictureBox4.BackgroundImage = Image.FromFile(arananfilmresim);
                        label1.Text = arananfilmad;
                    }
                    baglan.Close();
                }
            }

            if (radioButton2.Checked)
            {
                if (textBox1.Text.Trim() == "")
                {
                    //pictureBox4.BackgroundImage = null;
                    pictureBox4.Visible = false;
                    button1.Visible = false;
                    label1.Text = "";
                }
                else
                {
                    baglan.Open();
                    komut.Connection = baglan;
                    komut.CommandText = "SELECT program.pID,pAd,pResim FROM ((program INNER JOIN programtur ON program.pID = programtur.pID)INNER JOIN tur ON programtur.tID = tur.tID)WHERE tur.tAd LIKE '" + textBox1.Text + "%'";
                    dr = komut.ExecuteReader();
                    if (dr.Read())
                    {
                        pictureBox4.Visible = true;
                        button1.Visible = true;
                        string arananfilmad = dr[1].ToString();
                        string arananfilmresim = debugyol + dr[2].ToString();
                        int arananfilmid = Convert.ToInt32(dr[0]);
                        kullanicisinifi.arananfilmresim = arananfilmresim;
                        kullanicisinifi.arananfilmid = arananfilmid;
                        kullanicisinifi.arananfilmad = arananfilmad;
                        pictureBox4.BackgroundImage = Image.FromFile(arananfilmresim);
                        label1.Text = arananfilmad;
                    }
                    baglan.Close();
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form3 fr3 = new Form3();
            fr3.pictureBox2.BackgroundImage = Image.FromFile(kullanicisinifi.ArananFilmResim);
            fr3.label1.Text = kullanicisinifi.ArananFilmAd;
            fr3.Show();
            this.Hide();
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            baglan.Open();
            kontrolkomut.Connection = baglan;
            kontrolkomut.CommandText = "SELECT TOP 1 kID FROM kullanici ORDER BY kID DESC";
            dr = kontrolkomut.ExecuteReader();
            if (dr.Read())
            {
                int kontrolid = Convert.ToInt32(dr["kID"]);
                if (kullanicisinifi.KullaniciID == kontrolid)
                {
                    Form2 frm2 = new Form2();
                    frm2.Show();
                    this.Hide();
                }
                else
                {
                    Form2 frm2 = new Form2();
                    frm2.groupBox1.Visible = false;
                    frm2.Show();
                    this.Hide();
                }
            }
        }
    }
}