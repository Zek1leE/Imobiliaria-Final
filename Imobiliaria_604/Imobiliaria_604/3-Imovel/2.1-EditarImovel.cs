using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Imobiliaria_604
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        bool elevador = false;
        bool estacionamento = false;
        bool nvimg=false;
        byte[] foto;
        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream mStream = new MemoryStream(byteArrayIn))

            {
                return Image.FromStream(mStream);
            }

        }

        

        private void Form4_Load(object sender, EventArgs e)
        {
            textBox15.Text = "True";

            //Distrito
            //DataTable dis = BLL.Distrito.Distritos();
            //comboBox4.DataSource = dis;
            //comboBox4.DisplayMember = "NomeDistrito";

            textBox1.Text= "" + Global.row.Cells[0].Value;
            textBox2.Text = "" + Global.row.Cells[1].Value;
            textBox3.Text = "" + Global.row.Cells[2].Value;
            textBox4.Text = "" + Global.row.Cells[3].Value;
            textBox5.Text = "" + Global.row.Cells[4].Value;
            textBox6.Text = "" + Global.row.Cells[5].Value;
            textBox7.Text = "" + Global.row.Cells[6].Value;
            textBox8.Text = "" + Global.row.Cells[7].Value;
            textBox9.Text = "" + Global.row.Cells[8].Value;
            comboBox1.Text= "" + Global.row.Cells[9].Value;
            if (Convert.ToBoolean(Global.row.Cells[9].Value) == false)
            {
                comboBox1.Text = "Não";
            }
            else
            {
                comboBox1.Text = "Sim";
            }
            comboBox2.Text = "" + Global.row.Cells[10].Value;
            if (Convert.ToBoolean(Global.row.Cells[10].Value) == false)
            {
                comboBox2.Text = "Não";
            }
            else
            {
                comboBox2.Text = "Sim";
            }
            comboBox3.Text= "" + Global.row.Cells[11].Value;
            textBox10.Text = "" + Global.row.Cells[12].Value;
            comboBox4.Text = "" + Global.row.Cells[13].Value;
            textBox14.Text = "" + Global.row.Cells[14].Value;
            checkedListBox1.Text= "" + Global.row.Cells[15].Value;
            checkedListBox2.Text= "" + Global.row.Cells[16].Value;
            textBox11.Text = "" + Global.row.Cells[17].Value;
            textBox13.Text = "" + Global.row.Cells[18].Value;
            pictureBox2.Image = byteArrayToImage( (byte[])Global.row.Cells[19].Value) ;

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
          //  byte[] foto = ImageToByteArray(pictureBox2.Image);
            if (nvimg)
            {
                BLL.Imovel.updateNvImovel(Convert.ToInt32(textBox1.Text), textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text, textBox8.Text, textBox9.Text, elevador, estacionamento, comboBox3.SelectedIndex.ToString(), textBox10.Text, comboBox4.Text, textBox14.Text, checkedListBox1.Text, checkedListBox2.Text, textBox11.Text, textBox13.Text, foto, Convert.ToBoolean(textBox15.Text));
            }
            else
            {

          
            BLL.Imovel.updateNvImovel2(Convert.ToInt32(textBox1.Text), textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text, textBox8.Text, textBox9.Text, elevador, estacionamento, comboBox3.SelectedIndex.ToString(), textBox10.Text, comboBox4.Text, textBox14.Text, checkedListBox1.Text, checkedListBox2.Text, textBox11.Text, textBox13.Text, Convert.ToBoolean(textBox15.Text));
             }
            textBox2.Clear();
            textBox1.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            textBox10.Clear();
            comboBox3.Items.Clear();
            textBox14.Clear();
            textBox11.Clear();  
           
            textBox13.Clear();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Sim")
            {
                elevador = true;

            }
            else { elevador = false; }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem.ToString() == "Sim")
            {
                estacionamento = true;
            }
            else { estacionamento = false; }
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string img = openFileDialog1.FileName;
            Image image = Image.FromFile(img);
            foto = ImageToByteArray(image);
            pictureBox2.Image = image;
            nvimg = true;
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

            if (char.IsDigit(e.KeyChar) || e.KeyChar.Equals((char)Keys.Back))
            {
                TextBox t = (TextBox)sender;
                string w = Regex.Replace(t.Text, "[^0-9]", string.Empty);
                if (w == string.Empty) w = "00";

                if (e.KeyChar.Equals((char)Keys.Back))
                    w = w.Substring(0, w.Length - 1);
                else
                    w += e.KeyChar;
                t.Text = string.Format("{0:#,##0.00}", Double.Parse(w) / 100);
                t.Select(t.Text.Length, 0);
            }
            e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
