using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Antivirus
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private int viruses = 0;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            label2.Text = "Carpeta seleccionada : " + folderBrowserDialog1.SelectedPath;
            viruses = 0;
            progressBar1.Value = 0;
            listBox1.Items.Clear();
            button2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            viruses = 0;
            string[] search = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.*");
            progressBar1.Maximum = search.Length;
            foreach (string item in search)
            {
                try
                {
                    StreamReader stream = new StreamReader(item);
                    string read = stream.ReadToEnd();
                    string[] virus = new string[] { "VIRUSES", "INFECTED", "HACKED" };
                    foreach (string st in virus)
                    {
                        if (Regex.IsMatch(read, st))
                        {
                            viruses += 1;
                            listBox1.Items.Add(item);
                            button3.Show();
                            button4.Show();
                            label3.Text = "Detectados : " + viruses.ToString();
                        }
                        progressBar1.Increment(1);
                    }
                }
                catch
                {
                    string read = item;
                    string[] virus = new string[] { "VIRUSES", "INFECTED", "HACKED" };
                    foreach (string st in virus)
                    {
                        if (Regex.IsMatch(read, st))
                        {
                            viruses += 1;
                            listBox1.Items.Add(item);
                            button3.Show();
                            button4.Show();
                            label3.Text = "Dectados : " + viruses.ToString();

                        }
                        progressBar1.Increment(1);

                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Hide();
            button3.Hide();
            button4.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string removex = listBox1.Text;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (this.listBox1.Text != ""){
                File.Delete(this.listBox1.Text);
                MessageBox.Show("El archivo seleccionado fue removido");
                listBox1.Items.Clear();
                button2.PerformClick();
            }
            else
            {
                MessageBox.Show("Selecciona un archivo");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string removex = listBox1.Text;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (this.listBox1.Text != "")
            {
                string text = File.ReadAllText(this.listBox1.Text);
                text = text.Replace("INFECTED", "LIMPIO1");
                text = text.Replace("VIRUSES", "LIMPIO2");
                text = text.Replace("HACKED", "LIMPIO3");
                File.WriteAllText(this.listBox1.Text, text);
                MessageBox.Show("El archivo seleccionado fue reparado");
                listBox1.Items.Clear();
                button2.PerformClick();
            }
            else
            {
                MessageBox.Show("Selecciona un archivo");
            }
        }
    }
}
