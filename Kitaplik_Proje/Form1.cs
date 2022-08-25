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

namespace Kitaplik_Proje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection oleDbConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\iremh\OneDrive\Masaüstü\Kitaplik.mdb");

        void listele()
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("select * from Kitaplar", oleDbConnection);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            listele();
        }
        string durum = "";
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            oleDbConnection.Open();
            OleDbCommand komut = new OleDbCommand("insert into kitaplar (ad, yazar,tur,sayfa,durum) values (@p1,@p2,@p3,@p4,@p5)",oleDbConnection);
            komut.Parameters.AddWithValue("@p1", txtKitapAd.Text);
            komut.Parameters.AddWithValue("@p2", txtYazar.Text);
            komut.Parameters.AddWithValue("@p3", cmbTur.Text);
            komut.Parameters.AddWithValue("@p4", txtSayfaSayısı.Text);
            komut.Parameters.AddWithValue("@p5", durum);
            komut.ExecuteNonQuery();
            oleDbConnection.Close();
            MessageBox.Show("Kitap kaydedildi.", "Biigi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();

        }

        private void rdbKullanilmis_CheckedChanged(object sender, EventArgs e)
        {
            durum = "0";
        }

        private void rdbSifir_CheckedChanged(object sender, EventArgs e)
        {
            durum = "1";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtKitapAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtYazar.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cmbTur.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtSayfaSayısı.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            if (dataGridView1.Rows[secilen].Cells[5].Value.ToString()=="True")
            {
                rdbKullanilmis.Checked = true;
            }
            else
            {
                rdbSifir.Checked = true;
            }



        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            oleDbConnection.Open();
            OleDbCommand komut = new OleDbCommand("delete from kitaplar where ıd=@p1", oleDbConnection);
            komut.Parameters.AddWithValue  ("@p1", txtId.Text);
            komut.ExecuteNonQuery();
            oleDbConnection.Close();
            MessageBox.Show("Kitap listeden silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            oleDbConnection.Open();
            OleDbCommand komut = new OleDbCommand("Update kitaplar set ad=@p1,yazar=@p2,tur=@3,sayfa=@p4,durum=@p5 where ıd=@p6", oleDbConnection);
            komut.Parameters.AddWithValue("@p1", txtKitapAd.Text);
            komut.Parameters.AddWithValue("@p2", txtYazar.Text);
            komut.Parameters.AddWithValue("@p3", cmbTur.Text);
            komut.Parameters.AddWithValue("@p4", txtSayfaSayısı.Text);
            if (rdbKullanilmis.Checked==true)
            {
                komut.Parameters.AddWithValue("@p5", durum);

            }

            if (rdbSifir.Checked==true)
            {
                komut.Parameters.AddWithValue("@p5", durum);
            }
            komut.Parameters.AddWithValue("@p6", txtId.Text);

            komut.ExecuteNonQuery();
            oleDbConnection.Close();
            MessageBox.Show("Kitap Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();


        }

        private void btnBul_Click(object sender, EventArgs e)
        {
            oleDbConnection.Open();
            OleDbCommand komut = new OleDbCommand("select * from kitaplar where ad=@p1", oleDbConnection);
            komut.Parameters.AddWithValue("@p1", txtAra.Text);
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            oleDbConnection.Close();
           

            

        }

        private void btnAra_Click_1(object sender, EventArgs e)
        {
            oleDbConnection.Open();
            OleDbCommand komut = new OleDbCommand("select * from kitaplar where ad like '%"+txtAra.Text+"%'", oleDbConnection);
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
                da.Fill(dt);
            dataGridView1.DataSource = dt;
            oleDbConnection.Close();
        }
    }
}
