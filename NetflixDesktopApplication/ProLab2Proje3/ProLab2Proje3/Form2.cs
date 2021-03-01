using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Diagnostics;

namespace ProLab2Proje3
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=netflixveritabani.accdb");
        OleDbCommand komut = new OleDbCommand();
        OleDbDataReader dr;
        DataTable tablo = new DataTable();
        private void Form2_Load(object sender, EventArgs e)
        {
            string debugyol = Application.StartupPath;
            string tercih1, tercih2, tercih3;
            baglan.Open();
            komut.Connection = baglan;
            komut.CommandText = "SELECT * FROM kullanici WHERE kID=" + kullanicisinifi.KullaniciID + "";
            dr = komut.ExecuteReader();
            if (dr.Read())
            {
                tercih1 = dr["kTercih1"].ToString();
                tercih2 = dr["kTercih2"].ToString();
                tercih3 = dr["kTercih3"].ToString();
                dr.Close();
                komut.CommandText = "SELECT TOP 2 program.pID,pAd,pResim FROM (SELECT *  FROM (((program INNER JOIN programtur ON program.pID = programtur.pID)INNER JOIN kullaniciprogram ON program.pID = kullaniciprogram.pID)INNER JOIN tur ON programtur.tID = tur.tID)WHERE tur.tAd = '" + tercih1 + "'ORDER BY kullaniciprogram.kpPuan DESC)";
                dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    string film1 = debugyol + dr[2];
                    string film1ad = dr[1].ToString();
                    int film1id = Convert.ToInt32(dr[0]);
                    kullanicisinifi.film1 = film1;
                    kullanicisinifi.film1id = film1id;
                    kullanicisinifi.film1ad = film1ad;
                    pictureBox2.BackgroundImage = Image.FromFile(film1);
                    if (dr.Read())
                    {
                        string film2 = debugyol + dr[2];
                        string film2ad = dr[1].ToString();
                        int film2id = Convert.ToInt32(dr[0]);
                        kullanicisinifi.film2 = film2;
                        kullanicisinifi.film2id = film2id;
                        kullanicisinifi.film2ad = film2ad;
                        pictureBox3.BackgroundImage = Image.FromFile(film2);
                    }
                }
                dr.Close();
                komut.CommandText = "SELECT TOP 2 program.pID,pAd,pResim FROM (SELECT *  FROM (((program INNER JOIN programtur ON program.pID = programtur.pID)INNER JOIN kullaniciprogram ON program.pID = kullaniciprogram.pID)INNER JOIN tur ON programtur.tID = tur.tID)WHERE tur.tAd = '" + tercih2 + "'ORDER BY kullaniciprogram.kpPuan DESC)";
                dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    string film3 = debugyol + dr[2];
                    string film3ad = dr[1].ToString();
                    int film3id = Convert.ToInt32(dr[0]);
                    kullanicisinifi.film3 = film3;
                    kullanicisinifi.film3id = film3id;
                    kullanicisinifi.film3ad = film3ad;
                    pictureBox4.BackgroundImage = Image.FromFile(film3);
                    if (dr.Read())
                    {
                        string film4 = debugyol + dr[2];
                        string film4ad = dr[1].ToString();
                        int film4id = Convert.ToInt32(dr[0]);
                        kullanicisinifi.film4 = film4;
                        kullanicisinifi.film4id = film4id;
                        kullanicisinifi.film4ad = film4ad;
                        pictureBox5.BackgroundImage = Image.FromFile(film4);
                    }
                }
                dr.Close();
                komut.CommandText = "SELECT TOP 2 program.pID,pAd,pResim FROM (SELECT *  FROM (((program INNER JOIN programtur ON program.pID = programtur.pID)INNER JOIN kullaniciprogram ON program.pID = kullaniciprogram.pID)INNER JOIN tur ON programtur.tID = tur.tID)WHERE tur.tAd = '" + tercih3 + "'ORDER BY kullaniciprogram.kpPuan DESC)";
                dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    string film5 = debugyol + dr[2];
                    string film5ad = dr[1].ToString();
                    int film5id = Convert.ToInt32(dr[0]);
                    kullanicisinifi.film5 = film5;
                    kullanicisinifi.film5id = film5id;
                    kullanicisinifi.film5ad = film5ad;
                    pictureBox6.BackgroundImage = Image.FromFile(film5);
                    if (dr.Read())
                    {
                        string film6 = debugyol + dr[2];
                        string film6ad = dr[1].ToString();
                        int film6id = Convert.ToInt32(dr[0]);
                        kullanicisinifi.film6 = film6;
                        kullanicisinifi.film6id = film6id;
                        kullanicisinifi.film6ad = film6ad;
                        pictureBox7.BackgroundImage = Image.FromFile(film6);
                    }
                }
            }
            baglan.Close();
            label1.Text = "Hoşgeldin " + kullanicisinifi.KullaniciAd;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Refresh();
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form4 fr4 = new Form4();
            fr4.Show();
            this.Hide();
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Refresh();
        }
        /*      private void textBox1_TextChanged(object sender, EventArgs e)
              {
                  if (radioButton1.Checked)
                  {
                      if (textBox1.Text.Trim() == "")
                      {
                          tablo.Clear();
                          OleDbDataAdapter adp = new OleDbDataAdapter("SELECT * FROM program", baglan);
                          adp.Fill(tablo);
                          dataGridView1.DataSource = tablo;
                      }

                      else
                      {
                          tablo.Clear();
                          OleDbDataAdapter adp = new OleDbDataAdapter("SELECT * FROM program WHERE pAd LIKE '" + textBox1.Text + "%'", baglan);
                          adp.Fill(tablo);
                          dataGridView1.DataSource = tablo;
                      }
                  }

                  if (radioButton2.Checked)
                  {
                      if (textBox1.Text.Trim() == "")
                      {
                          tablo.Clear();
                          OleDbDataAdapter adp = new OleDbDataAdapter("SELECT * FROM program", baglan);
                          adp.Fill(tablo);
                          dataGridView1.DataSource = tablo;
                      }

                      else
                      {
                          tablo.Clear();
                          OleDbDataAdapter adp = new OleDbDataAdapter("SELECT * FROM program WHERE pTur LIKE '" + textBox1.Text + "%'", baglan);
                          adp.Fill(tablo);
                          dataGridView1.DataSource = tablo;
                      }
                  }
              }
              */
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form3 fr3 = new Form3();
            fr3.pictureBox2.BackgroundImage = Image.FromFile(kullanicisinifi.Film1);
            fr3.label1.Text = kullanicisinifi.Film1Ad;
            fr3.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 fr1 = new Form1();
            fr1.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form3 fr3 = new Form3();
            fr3.pictureBox2.BackgroundImage = Image.FromFile(kullanicisinifi.Film2);
            fr3.label1.Text = kullanicisinifi.Film2Ad;
            fr3.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Form3 fr3 = new Form3();
            fr3.pictureBox2.BackgroundImage = Image.FromFile(kullanicisinifi.Film3);
            fr3.label1.Text = kullanicisinifi.Film3Ad;
            fr3.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Form3 fr3 = new Form3();
            fr3.pictureBox2.BackgroundImage = Image.FromFile(kullanicisinifi.Film4);
            fr3.label1.Text = kullanicisinifi.Film4Ad;
            fr3.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Form3 fr3 = new Form3();
            fr3.pictureBox2.BackgroundImage = Image.FromFile(kullanicisinifi.Film5);
            fr3.label1.Text = kullanicisinifi.Film5Ad;
            fr3.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Form3 fr3 = new Form3();
            fr3.pictureBox2.BackgroundImage = Image.FromFile(kullanicisinifi.Film6);
            fr3.label1.Text = kullanicisinifi.Film6Ad;
            fr3.Show();
            this.Hide();
        }
    }
}
