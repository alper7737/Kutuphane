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

namespace kütüphane
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("server=.;Initial Catalog=kutuphane;Integrated Security=true");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        void göster2()
        {
            string sorgu = "select * from tblyazar";
            da = new SqlDataAdapter(sorgu, con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "tblyazar");
            con.Close();
            dataGridView2.DataSource = ds.Tables["tblyazar"];
        }
        void göster()
        {
            string sorgu="select * from tblkitap";
            da=new SqlDataAdapter(sorgu,con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "tblkitap");
            con.Close();
            dataGridView1.DataSource = ds.Tables["tblkitap"];
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            pictureBox1.ImageLocation = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }
        

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox5.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            textBox6.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            textBox7.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            textBox8.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
            dateTimePicker1.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string datas = "select * from tbltur";
            cmd = new SqlCommand(datas, con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["kitap_tur"]);
            }
            con.Close();
            göster();
            göster2();
        }
       

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string datas = ("select kitap_tur from tbltur where kitap_tur=(select kitap_tur from tblkitap where kitap_tur=@tur)");
            cmd = new SqlCommand(datas, con);
            cmd.Parameters.AddWithValue("@tur", comboBox1.Text.ToString());
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            con.Close();
            göster();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string datas = "insert into tblkitap (kitap_id,kitap_ad,kitap_sayfa,kitap_resim,kitap_tur,yazar_id)values(@id,@ad,@sayfa,@resim,@tur,@yid)";
            cmd = new SqlCommand(datas, con);
            cmd.Parameters.AddWithValue("@id", (textBox1.Text));
            cmd.Parameters.AddWithValue("@ad", (textBox2.Text));
            cmd.Parameters.AddWithValue("@sayfa", (textBox3.Text));
            cmd.Parameters.AddWithValue("@resim", (pictureBox1.ImageLocation));
            cmd.Parameters.AddWithValue("@tur", (comboBox1.Text));
            cmd.Parameters.AddWithValue("@yid", (textBox4.Text));
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            göster();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.ShowDialog();
            string dosyaoku = dosya.FileName;
            pictureBox1.ImageLocation = dosyaoku;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string datas = "delete from tblkitap where kitap_id=@id";
            cmd = new SqlCommand(datas, con);
            cmd.Parameters.AddWithValue("@id", (textBox1.Text));            
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            göster();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string datas = "insert into tblyazar (yazar_id,yazar_ad,yazar_soyad,yazar_dog,yazar_dyer)values(@id,@ad,@soyad,@dog,@dyer)";
            cmd = new SqlCommand(datas, con);
            cmd.Parameters.AddWithValue("@id", (textBox5.Text));
            cmd.Parameters.AddWithValue("@ad", (textBox6.Text));
            cmd.Parameters.AddWithValue("@soyad", (textBox7.Text));
            cmd.Parameters.AddWithValue("@dog", (dateTimePicker1.Value));
            cmd.Parameters.AddWithValue("@dyer", (textBox8.Text));
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            göster2();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string datas = "delete from tblyazar where yazar_id=@id";
            cmd = new SqlCommand(datas, con);
            cmd.Parameters.AddWithValue("@id", (textBox5.Text));
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            göster2();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string datas = "update tblyazar set yazar_ad=@ad,yazar_soyad=@soyad,yazar_dyer=@dyer,yazar_dog=@dog where yazar_id=@id";
            cmd = new SqlCommand(datas, con);
            cmd.Parameters.AddWithValue("@id", (textBox5.Text));
            cmd.Parameters.AddWithValue("@ad", (textBox6.Text));
            cmd.Parameters.AddWithValue("@soyad", (textBox7.Text));
            cmd.Parameters.AddWithValue("@dog", (dateTimePicker1.Value));
            cmd.Parameters.AddWithValue("@dyer", (textBox8.Text));
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            göster2();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string datas = "update tblkitap set kitap_ad=@ad,kitap_sayfa=@sayfa,kitap_resim=@resim where kitap_id=@id";
            cmd = new SqlCommand(datas, con);
            cmd.Parameters.AddWithValue("@id", (textBox1.Text));
            cmd.Parameters.AddWithValue("@ad", (textBox2.Text));
            cmd.Parameters.AddWithValue("@sayfa", (textBox3.Text));
            cmd.Parameters.AddWithValue("@resim", (pictureBox1.ImageLocation));
            cmd.Parameters.AddWithValue("@yid", (textBox4.Text));
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            göster();
        }
    }
}