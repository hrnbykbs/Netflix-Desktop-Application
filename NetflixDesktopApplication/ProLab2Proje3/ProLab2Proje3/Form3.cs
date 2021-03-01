using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Media;

namespace ProLab2Proje3
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=netflixveritabani.accdb");
        OleDbCommand komut = new OleDbCommand();
        OleDbCommand kontrolkomut = new OleDbCommand();
        OleDbDataReader dr;
        int tiklandimi = 0;
        private void Form3_Load(object sender, EventArgs e)
        {
            SoundPlayer ses = new SoundPlayer();
            string debugyol = Application.StartupPath.ToString();
            string sesyol = debugyol + "\\netflixsound.wav";
            ses.SoundLocation = sesyol;
            ses.Play();
            string gifyol = debugyol + "\\netflixintrogif1.gif";
            pictureBox1.Image = Image.FromFile(gifyol);
            label2.Visible = false;
            baglan.Open();
            komut.Connection = baglan;
            komut.CommandText = "SELECT * FROM program WHERE pAd='" + label1.Text + "'";
            dr = komut.ExecuteReader();
            if (dr.Read())
            {
                int bolumsayisi = Convert.ToInt16(dr["pBolumSayisi"]);
                kullanicisinifi.bolumsayisi = bolumsayisi;
                int uzunluk = Convert.ToInt32(dr["pUzunluk"]);
                int izlenenfilmid = Convert.ToInt32(dr["pID"]);
                kullanicisinifi.izlenenfilmid = izlenenfilmid;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = uzunluk;
                int saat, dakika, saniye;
                saat = uzunluk / 3600;
                uzunluk -= saat * 3600;
                dakika = uzunluk / 60;
                uzunluk -= dakika * 60;
                saniye = uzunluk;
                kullanicisinifi.saat = saat;
                kullanicisinifi.dakika = dakika;
                kullanicisinifi.saniye = saniye;
            }
            dr.Close();
            komut.Connection = baglan;
            komut.CommandText = "SELECT * FROM kullaniciprogram WHERE kID=" + kullanicisinifi.KullaniciID + " AND pID=" + kullanicisinifi.IzlenenFilmID + "";
            dr = komut.ExecuteReader();
            if (dr.Read())
            {
                int kalinansure = Convert.ToInt32(dr["kpIzlenmeSuresi"].ToString());
                int kalinanbolum = Convert.ToInt32(dr["kpKalinanBolum"]);
                kullanicisinifi.kalinanbolum = kalinanbolum;
                int saat, dakika, saniye;
                saat = kalinansure / 3600;
                kalinansure -= saat * 3600;
                dakika = kalinansure / 60;
                kalinansure -= dakika * 60;
                saniye = kalinansure;
                saatlabel.Text = saat.ToString();
                dakikalabel.Text = dakika.ToString();
                saniyelabel.Text = saniye.ToString();
                label7.Text = kullanicisinifi.KalinanBolum.ToString();
                int pbvalue = Convert.ToInt32(dr["kpIzlenmeSuresi"]);
                progressBar1.Value = pbvalue;
            }
            else
            {
                saatlabel.Text = "0";
                dakikalabel.Text = "0";
                saniyelabel.Text = "0";
                progressBar1.Value = 0;
            }
            baglan.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tiklandimi++;
            string debugyol = Application.StartupPath.ToString();
            string play = debugyol + "\\play.png";
            string pause = debugyol + "\\pause.png";

            if (tiklandimi % 2 == 0)
            {
                button1.BackgroundImage = Image.FromFile(play);
                timer1.Stop();
                label2.Visible = false;
            }

            if (tiklandimi % 2 == 1)
            {
                button1.BackgroundImage = Image.FromFile(pause);
                timer1.Start();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            string debugyol = Application.StartupPath.ToString();
            string play = debugyol + "\\play.png";
            if (saatlabel.Text == kullanicisinifi.Saat.ToString() && dakikalabel.Text == kullanicisinifi.Dakika.ToString() && saniyelabel.Text == kullanicisinifi.Saniye.ToString())
            {
                timer1.Stop();
                label2.Visible = true;
                label2.Text = label7.Text + ". Bölüm Tamamlandı!";
                kullanicisinifi.KalinanBolum++;
                if (kullanicisinifi.KalinanBolum == (kullanicisinifi.BolumSayisi + 1))
                {
                    timer1.Stop();
                    label2.Text = "İçerik tamamlandı!";
                    button1.Enabled = false;
                    label7.Text = (kullanicisinifi.KalinanBolum - 1).ToString();
                }
                else
                {
                    label7.Text = kullanicisinifi.KalinanBolum.ToString();
                }
                progressBar1.Value = 0;
                saatlabel.Text = "0";
                dakikalabel.Text = "0";
                saniyelabel.Text = "0";
            }

            else
            {
                progressBar1.Value += 1;
                saniyelabel.Text = (Convert.ToInt32(saniyelabel.Text) + 1).ToString();
                if (saniyelabel.Text == "59")
                {
                    if (dakikalabel.Text == "59")
                    {
                        saatlabel.Text = (Convert.ToInt32(saatlabel.Text) + 1).ToString();
                        dakikalabel.Text = "0";
                        saniyelabel.Text = "0";
                        progressBar1.Value += 1;
                    }
                    else
                    {
                        dakikalabel.Text = (Convert.ToInt32(dakikalabel.Text) + 1).ToString();
                        saniyelabel.Text = "0";
                        progressBar1.Value += 1;
                    }
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            DateTime anliktarih = DateTime.Now;
            var date = anliktarih.Date;
            int uzunluk, saat, dakika, saniye, puan;
            saat = Convert.ToInt32(saatlabel.Text);
            dakika = Convert.ToInt32(dakikalabel.Text);
            saniye = Convert.ToInt32(saniyelabel.Text);
            uzunluk = ((saat * 3600) + (dakika * 60) + (saniye));
            puan = Convert.ToInt32(comboBox1.SelectedItem);
            if (comboBox1.Text != "")
            {
                baglan.Open();
                komut.Connection = baglan;
                komut.CommandText = "SELECT pID,pTip FROM program WHERE pAd='" + label1.Text + "'";
                dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    int izlenenfilmid = Convert.ToInt32(dr["pID"]);
                    string izlenentur = dr["pTip"].ToString();
                    dr.Close();
                    if (izlenentur == "Film")
                    {
                        kontrolkomut.Connection = baglan;
                        kontrolkomut.CommandText = "SELECT COUNT(*) FROM kullaniciprogram WHERE kID=" + kullanicisinifi.KullaniciID + " AND pID=" + kullanicisinifi.IzlenenFilmID + "";
                        if (Convert.ToInt32(kontrolkomut.ExecuteScalar()) > 0)
                        {
                            komut.Connection = baglan;
                            komut.CommandText = "UPDATE kullaniciprogram SET kpIzlenmeTarihi='" + date.ToString() + "', kpIzlenmeSuresi=" + uzunluk + ", kpPuan=" + puan + " WHERE kID=" + kullanicisinifi.KullaniciID + " AND pID=" + izlenenfilmid + "";
                            komut.ExecuteNonQuery();
                            timer1.Stop();
                            MessageBox.Show("Veri tabanında güncellendi!", "Güncelleme", MessageBoxButtons.OK);
                        }
                        else
                        {
                            komut.CommandText = "INSERT INTO kullaniciprogram(kID, pID, kpIzlenmeTarihi, kpIzlenmeSuresi, kpKalinanBolum, kpPuan) VALUES(" + kullanicisinifi.KullaniciID + "," + izlenenfilmid + ",'" + date.ToString() + "'," + uzunluk + ",'" + 1 + "'," + puan + ")";
                            komut.ExecuteNonQuery();
                            timer1.Stop();
                            MessageBox.Show("Veri tabanına kayıt edildi!", "Kayıt", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        kontrolkomut.Connection = baglan;
                        kontrolkomut.CommandText = "SELECT COUNT(*) FROM kullaniciprogram WHERE kID=" + kullanicisinifi.KullaniciID + " AND pID=" + kullanicisinifi.IzlenenFilmID + "";
                        if (Convert.ToInt32(kontrolkomut.ExecuteScalar()) > 0)
                        {
                            komut.Connection = baglan;
                            komut.CommandText = "UPDATE kullaniciprogram SET kpIzlenmeTarihi='" + date.ToString() + "', kpIzlenmeSuresi=" + uzunluk + ",kpKalinanBolum=" + kullanicisinifi.KalinanBolum + ", kpPuan=" + puan + " WHERE kID=" + kullanicisinifi.KullaniciID + " AND pID=" + izlenenfilmid + "";
                            komut.ExecuteNonQuery();
                            timer1.Stop();
                            MessageBox.Show("Veri tabanında güncellendi!", "Güncelleme", MessageBoxButtons.OK);
                        }
                        else
                        {
                            komut.CommandText = "INSERT INTO kullaniciprogram(kID, pID, kpIzlenmeTarihi, kpIzlenmeSuresi, kpKalinanBolum, kpPuan) VALUES(" + kullanicisinifi.KullaniciID + "," + izlenenfilmid + ",'" + date.ToString() + "'," + uzunluk + ",'" + kullanicisinifi.KalinanBolum + "'," + puan + ")";
                            komut.ExecuteNonQuery();
                            timer1.Stop();
                            MessageBox.Show("Veri tabanına kayıt edildi!", "Kayıt", MessageBoxButtons.OK);
                        }
                    }
                }
                baglan.Close();

            }

            else
            {
                MessageBox.Show("Lütfen puanlamanızı yapınız!", "Hata", MessageBoxButtons.OK);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 fr2 = new Form2();
            fr2.Show();
            this.Hide();
        }
    }
}
