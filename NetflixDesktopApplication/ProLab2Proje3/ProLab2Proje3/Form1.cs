using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Mail;
using System.Data.OleDb;
using System.Collections;

namespace ProLab2Proje3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=netflixveritabani.accdb");
        OleDbCommand komut = new OleDbCommand();
        OleDbCommand kontrolkomut = new OleDbCommand();
        OleDbDataReader dr;

        public static bool Email_Format_Kontrol(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string girismail = textBox1.Text;
            string girissifre = textBox2.Text;
            int kullaniciid;
            string kullaniciad;
            baglan.Open();
            komut.Connection = baglan;
            komut.CommandText = "SELECT * FROM kullanici WHERE kMail='" + girismail + "' AND kSifre='" + girissifre + "'";
            dr = komut.ExecuteReader();
            if (dr.Read())
            {
                kullaniciid = Convert.ToInt32(dr["kID"].ToString());
                kullaniciad = dr["kAd"].ToString();
                kullanicisinifi.kullaniciid = kullaniciid;
                kullanicisinifi.kullaniciad = kullaniciad;
                dr.Close();
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
            else
            {
                label9.Text = "Hata!";
                label9.ForeColor = Color.Red;
                textBox1.Text = "";
                textBox2.Text = "";
            }
            baglan.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string kayitad = textBox3.Text;
            string kayitmail = maskedTextBox1.Text;
            string kayitsifre = textBox4.Text;
            string kayittekrarsifre = textBox5.Text;
            string kayitdogum = maskedTextBox2.Text;
            ArrayList tercihdizi = new ArrayList();

            if (kayitad != "" && kayitmail != "" && kayitsifre != "" && kayittekrarsifre != "" && kayitdogum != "" && checkedListBox1.CheckedItems.Count == 3)
            {
                button2.Enabled = true;
                baglan.Open();
                kontrolkomut.Connection = baglan;
                kontrolkomut.CommandText = "SELECT COUNT(*) FROM kullanici WHERE kMail='" + kayitmail + "'";
                if (Convert.ToInt32(kontrolkomut.ExecuteScalar()) > 0)
                {
                    label12.Text = "Hesap zaten var!";
                    label12.ForeColor = Color.Red;
                }
                else
                {
                    foreach (string tercihler in checkedListBox1.CheckedItems)
                    {
                        tercihdizi.Add(tercihler);
                    }
                    komut.Connection = baglan;
                    komut.CommandText = "INSERT INTO kullanici(kAd, kMail, kSifre, kDogum, kTercih1,kTercih2,kTercih3) VALUES ('" + kayitad + "','" + kayitmail + "', '" + kayitsifre + "', '" + kayitdogum + "', '" + tercihdizi[0] + "','" + tercihdizi[1] + "','" + tercihdizi[2] + "')";
                    komut.ExecuteNonQuery();
                    label12.Text = "Kayıt tamamlandı!";
                    label12.ForeColor = Color.Green;
                    textBox3.Text = ""; maskedTextBox1.Text = ""; textBox4.Text = ""; textBox5.Text = ""; maskedTextBox2.Text = "";
                }
            }
            if (kayitad == "" || kayitmail == "" || kayitsifre == "" || kayittekrarsifre == "" || kayitdogum == "" || checkedListBox1.CheckedItems.Count < 3)
            {
                label12.Text = "Tüm alanları doldurun";
                label12.ForeColor = Color.Red;
            }

            baglan.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            label12.Text = "";
            groupBox1.Visible = false;
            groupBox2.Visible = false;

        }
        private void textBox4_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBox4.Text != "" && textBox5.Text != "")
            {
                if (textBox4.Text != textBox5.Text)
                {
                    label11.Text = "Parolalar eşleşmiyor";
                    label11.ForeColor = Color.Red;
                    button2.Enabled = false;
                }
                else
                {
                    label11.Text = "Parolalar eşleşti";
                    label11.ForeColor = Color.Green;
                    button2.Enabled = true;
                }
            }
            else if (textBox4.Text == "" && textBox5.Text == "")
            {
                label11.Text = "Parola yok";
                label11.ForeColor = Color.Red;
                button2.Enabled = false;
            }

            else if (textBox4.Text == "" || textBox5.Text == "")
            {
                label11.Text = "Parolalar eşleşmiyor";
                label11.ForeColor = Color.Red;
                button2.Enabled = false;
            }
        }
        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox4.Text != "" && textBox5.Text != "")
            {
                if (textBox4.Text != textBox5.Text)
                {
                    label11.Text = "Parolalar eşleşmiyor";
                    label11.ForeColor = Color.Red;
                    button2.Enabled = false;
                }
                else
                {
                    label11.Text = "Parolalar eşleşti";
                    label11.ForeColor = Color.Green;
                    button2.Enabled = true;
                }
            }
            else if (textBox4.Text == "" && textBox5.Text == "")
            {
                label11.Text = "Parola yok";
                label11.ForeColor = Color.Red;
                button2.Enabled = false;
            }

            else if (textBox4.Text == "" || textBox5.Text == "")
            {
                label11.Text = "Parolalar eşleşmiyor";
                label11.ForeColor = Color.Red;
                button2.Enabled = false;
            }
        }
        private void textBox5_KeyUp_2(object sender, KeyEventArgs e)
        {
            if (textBox4.Text != "" && textBox5.Text != "")
            {
                if (textBox4.Text != textBox5.Text)
                {
                    label11.Text = "Parolalar eşleşmiyor";
                    label11.ForeColor = Color.Red;
                    button2.Enabled = false;
                }
                else
                {
                    label11.Text = "Parolalar eşleşti";
                    label11.ForeColor = Color.Green;
                    button2.Enabled = true;
                }
            }
            else if (textBox4.Text == "" && textBox5.Text == "")
            {
                label11.Text = "Parola yok";
                label11.ForeColor = Color.Red;
                button2.Enabled = false;
            }

            else if (textBox4.Text == "" || textBox5.Text == "")
            {
                label11.Text = "Parolalar eşleşmiyor";
                label11.ForeColor = Color.Red;
                button2.Enabled = false;
            }
        }
        private void textBox5_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (textBox4.Text != "" && textBox5.Text != "")
            {
                if (textBox4.Text != textBox5.Text)
                {
                    label11.Text = "Parolalar eşleşmiyor";
                    label11.ForeColor = Color.Red;
                    button2.Enabled = false;
                }
                else
                {
                    label11.Text = "Parolalar eşleşti";
                    label11.ForeColor = Color.Green;
                    button2.Enabled = true;
                }
            }
            else if (textBox4.Text == "" && textBox5.Text == "")
            {
                label11.Text = "Parola yok";
                label11.ForeColor = Color.Red;
                button2.Enabled = false;
            }

            else if (textBox4.Text == "" || textBox5.Text == "")
            {
                label11.Text = "Parolalar eşleşmiyor";
                label11.ForeColor = Color.Red;
                button2.Enabled = false;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = false;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = true;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Refresh();
        }
    }
}