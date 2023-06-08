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

namespace AkaryakitStokTakipOtomasyonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //  Data Source = DESKTOP - PBFD0LU; Initial Catalog = petrolDB; Integrated Security = True
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-PBFD0LU;Initial Catalog=petrolDB;Integrated Security=True");

        void listele()
        {
            //Kurşunsuz95
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from tblbenzin where petroltur='Kurşunsuz95'", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblkursz95.Text = dr[3].ToString();//dr[3] sutun sırası
                progressBar1.Value = int.Parse(dr[4].ToString());
                lblkrsz95litre.Text = dr[4].ToString();

            }
            baglanti.Close();

            //  Kurşunsuz97
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select * from tblbenzin where petroltur='Kurşunsuz97'", baglanti);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                lblkrsz97.Text = dr2[3].ToString();//dr[3] sutun sırası
                progressBar2.Value = int.Parse(dr2[4].ToString());
                lblkrsz97litre.Text = dr2[4].ToString();
            }
            baglanti.Close();

            //  Euro Dizel 10
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("select * from tblbenzin where petroltur='EuroDizel10'", baglanti);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                lbldzl10.Text = dr3[3].ToString();//dr[3] sutun sırası
                progressBar3.Value = int.Parse(dr3[4].ToString());
                lbleurodizellitre.Text = dr3[4].ToString();
            }
            baglanti.Close();

            //  Yeni pro Dizel
            baglanti.Open();
            SqlCommand komut4 = new SqlCommand("select * from tblbenzin where petroltur='YeniProDizel'", baglanti);
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                lblprodizel.Text = dr4[3].ToString();//dr[3] sutun sırası
                progressBar4.Value = int.Parse(dr4[4].ToString());
                lblyeniprodizellitre.Text = dr4[4].ToString();
            }
            baglanti.Close();

            //  Gaz
            baglanti.Open();
            SqlCommand komut5 = new SqlCommand("select * from tblbenzin where petroltur='Gaz'", baglanti);
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                lblgaz.Text = dr5[3].ToString();//dr[3] sutun sırası
                progressBar5.Value = int.Parse(dr5[4].ToString());
                lbllgazlitre.Text = dr5[4].ToString();
            }
            baglanti.Close();

            baglanti.Open();
            SqlCommand komut6 = new SqlCommand("select * from tblkasa", baglanti);
            SqlDataReader dr6 = komut6.ExecuteReader();
            while (dr6.Read())
            {
                lblkasa.Text = dr6[0].ToString();

            }
            baglanti.Close();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            double kursunsuz95, litre, tutar;
            kursunsuz95 = Convert.ToDouble(lblkursz95.Text);
            litre = Convert.ToDouble(numericUpDown1.Value);
            tutar = kursunsuz95 * litre;
            txtkrsz95fiyat.Text = tutar.ToString();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            double kursunsuz97, litre, tutar;
            kursunsuz97 = Convert.ToDouble(lblkrsz97.Text);
            litre = Convert.ToDouble(numericUpDown2.Value);
            tutar = kursunsuz97 * litre;
            txtkrsz97fiyat.Text = tutar.ToString();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            double eurodizel, litre, tutar;
            eurodizel = Convert.ToDouble(lbldzl10.Text);
            litre = Convert.ToDouble(numericUpDown3.Value);
            tutar = eurodizel * litre;
            txteurodizelfiyat.Text = tutar.ToString();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            double gaz, litre, tutar;
            gaz = Convert.ToDouble(lblgaz.Text);
            litre = Convert.ToDouble(numericUpDown4.Value);
            tutar = gaz * litre;
            txtgazfiyat.Text = tutar.ToString();
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            double yeniprodizel, litre, tutar;
            yeniprodizel = Convert.ToDouble(lblprodizel.Text);
            litre = Convert.ToDouble(numericUpDown5.Value);
            tutar = yeniprodizel * litre;
            txtyeniprofiyat.Text = tutar.ToString();
        }

        private void btndepodoldur_Click(object sender, EventArgs e)
        {
            if(numericUpDown1.Value != 0)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into tblhareket (plaka,benzinturu,litre,fiyat) values (@p1,@p2,@p3,@p4)", baglanti);
                komut.Parameters.AddWithValue("@p1", txtplaka.Text);
                komut.Parameters.AddWithValue("@p2", "Kurşunsuz95");
                komut.Parameters.AddWithValue("@p3", numericUpDown1.Value);
                komut.Parameters.AddWithValue("@p4", decimal.Parse(txtkrsz95fiyat.Text));
                komut.ExecuteNonQuery();
                baglanti.Close();

                //kasa güncellenmesi
                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("update tblkasa set miktar=miktar+@p1", baglanti);
                komut2.Parameters.AddWithValue("@p1", decimal.Parse(txtkrsz95fiyat.Text));
                komut2.ExecuteNonQuery();
                baglanti.Close();

                //prograsbarın güncellenmesi

                baglanti.Open();
                SqlCommand komut3 = new SqlCommand("update tblbenzin set stok=stok-@p1 where petroltur='kurşunsuz95'",baglanti);
                komut3.Parameters.AddWithValue("@p1", numericUpDown1.Value);
                komut3.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Satış Yapıldı");
                listele();

            }
            if (numericUpDown2.Value != 0)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into tblhareket (plaka,benzinturu,litre,fiyat) values (@p1,@p2,@p3,@p4)", baglanti);
                komut.Parameters.AddWithValue("@p1", txtplaka.Text);
                komut.Parameters.AddWithValue("@p2", "Kurşunsuz97");
                komut.Parameters.AddWithValue("@p3", numericUpDown2.Value);
                komut.Parameters.AddWithValue("@p4", decimal.Parse(txtkrsz97fiyat.Text));
                komut.ExecuteNonQuery();
                baglanti.Close();

                //kasa güncellenmesi
                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("update tblkasa set miktar=miktar+@p1", baglanti);
                komut2.Parameters.AddWithValue("@p1", decimal.Parse(txtkrsz97fiyat.Text));
                komut2.ExecuteNonQuery();
                baglanti.Close();

                //prograsbarın güncellenmesi

                baglanti.Open();
                SqlCommand komut3 = new SqlCommand("update tblbenzin set stok=stok-@p1 where petroltur='Kurşunsuz97'", baglanti);
                komut3.Parameters.AddWithValue("@p1", numericUpDown2.Value);
                komut3.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Satış Yapıldı");
                listele();

            }
            if (numericUpDown3.Value != 0)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into tblhareket (plaka,benzinturu,litre,fiyat) values (@p1,@p2,@p3,@p4)", baglanti);
                komut.Parameters.AddWithValue("@p1", txtplaka.Text);
                komut.Parameters.AddWithValue("@p2", "EuroDizel10");
                komut.Parameters.AddWithValue("@p3", numericUpDown3.Value);
                komut.Parameters.AddWithValue("@p4", decimal.Parse(txteurodizelfiyat.Text));
                komut.ExecuteNonQuery();
                baglanti.Close();

                //kasa güncellenmesi
                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("update tblkasa set miktar=miktar+@p1", baglanti);
                komut2.Parameters.AddWithValue("@p1", decimal.Parse(txteurodizelfiyat.Text));
                komut2.ExecuteNonQuery();
                baglanti.Close();

                //prograsbarın güncellenmesi

                baglanti.Open();
                SqlCommand komut3 = new SqlCommand("update tblbenzin set stok=stok-@p1 where petroltur='EuroDizel10'", baglanti);
                komut3.Parameters.AddWithValue("@p1", numericUpDown3.Value);
                komut3.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Satış Yapıldı");
                listele();

            }
            if (numericUpDown4.Value != 0)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into tblhareket (plaka,benzinturu,litre,fiyat) values (@p1,@p2,@p3,@p4)", baglanti);
                komut.Parameters.AddWithValue("@p1", txtplaka.Text);
                komut.Parameters.AddWithValue("@p2", "Gaz");
                komut.Parameters.AddWithValue("@p3", numericUpDown4.Value);
                komut.Parameters.AddWithValue("@p4", decimal.Parse(txtgazfiyat.Text));
                komut.ExecuteNonQuery();
                baglanti.Close();

                //kasa güncellenmesi
                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("update tblkasa set miktar=miktar+@p1", baglanti);
                komut2.Parameters.AddWithValue("@p1", decimal.Parse(txtgazfiyat.Text));
                komut2.ExecuteNonQuery();
                baglanti.Close();

                //prograsbarın güncellenmesi

                baglanti.Open();
                SqlCommand komut3 = new SqlCommand("update tblbenzin set stok=stok-@p1 where petroltur='Gaz'", baglanti);
                komut3.Parameters.AddWithValue("@p1", numericUpDown4.Value);
                komut3.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Satış Yapıldı");
                listele();

            }
            if (numericUpDown5.Value != 0)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into tblhareket (plaka,benzinturu,litre,fiyat) values (@p1,@p2,@p3,@p4)", baglanti);
                komut.Parameters.AddWithValue("@p1", txtplaka.Text);
                komut.Parameters.AddWithValue("@p2", "YeniProDizel");
                komut.Parameters.AddWithValue("@p3", numericUpDown5.Value);
                komut.Parameters.AddWithValue("@p4", decimal.Parse(txtgazfiyat.Text));
                komut.ExecuteNonQuery();
                baglanti.Close();

                //kasa güncellenmesi
                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("update tblkasa set miktar=miktar+@p1", baglanti);
                komut2.Parameters.AddWithValue("@p1", decimal.Parse(txtgazfiyat.Text));
                komut2.ExecuteNonQuery();
                baglanti.Close();

                //prograsbarın güncellenmesi

                baglanti.Open();
                SqlCommand komut3 = new SqlCommand("update tblbenzin set stok=stok-@p1 where petroltur='YeniProDizel'", baglanti);
                komut3.Parameters.AddWithValue("@p1", numericUpDown5.Value);
                komut3.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Satış Yapıldı");
                listele();

            }
        }

      
    }
}
