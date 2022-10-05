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

namespace Rehber
{
    public partial class frmRehber : Form
    {
        public frmRehber()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-SBTQ48V\SQLEXPRESS;Initial Catalog=Rehber;Integrated Security=True");

        void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from kisiler", baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void Temizle()
        {
            txtId.Text = "";
            txtAd.Text = "";
            txtMail.Text = "";
            txtSoyad.Text = "";
            mskTxtTel.Text = "";
            txtAd.Focus();
        }
        private void frmRehber_Load(object sender, EventArgs e)
        {
            Listele();

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into kisiler (ad,soyad,telefon,mail) values (@p1,@p2,@p3,@p4)",baglanti);
            komut.Parameters.AddWithValue("@p1",txtAd.Text);
            komut.Parameters.AddWithValue("@p2",txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3",mskTxtTel.Text);
            komut.Parameters.AddWithValue("@p4",txtMail.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kişi sisteme eklendi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            Listele();
            Temizle();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            txtId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            mskTxtTel.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtMail.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Delete from kisiler where ıd="+txtId.Text,baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kişi Rehberden silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            Listele();
            Temizle();
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update kisiler set ad=@p1,soyad=@p2,telefon=@p3,mail=@p4 where ıd=@p5",baglanti);
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", mskTxtTel.Text);
            komut.Parameters.AddWithValue("@p4", txtMail.Text);
            komut.Parameters.AddWithValue("@p5", txtId.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kişi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Listele();
            Temizle();
        }
    }
}
