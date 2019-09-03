using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandomRename
{
    public partial class Form1 : Form
    {
        string chars = "abcdefghijklmnopqrstuvwxyz";
        string numbers = "01234546789";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                this.listBox1.Items.Clear();
                FolderBrowserDialog ofd = new FolderBrowserDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (this.toolStripComboBox1.Text == "Recursive")
                    {
                        foreach (String s in Directory.GetFiles(ofd.SelectedPath.ToString(), this.toolStripTextBox1.Text,
                            SearchOption.AllDirectories))
                        {
                            this.listBox1.Items.Add(s);
                        }
                    }
                    else
                    {
                        foreach (String s in Directory.GetFiles(ofd.SelectedPath.ToString(), this.toolStripTextBox1.Text,
                               SearchOption.TopDirectoryOnly))
                        {
                            this.listBox1.Items.Add(s);
                        }

                    }
                    this.toolStripStatusLabel1.Text = "Files: " + this.listBox1.Items.Count.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (!this.checkBox1.Checked && !this.checkBox2.Checked)
            {
                MessageBox.Show("You need to check at least one of the random name options: \"numbers\" or \"letters\"");
                return;
            }

            if (this.listBox1.Items.Count < 1)
            {
                MessageBox.Show("Please insert some files to rename in the Search List first");
                return;
            }

            foreach (var i in this.listBox1.Items)
            {
                FileInfo fi = new FileInfo(i.ToString());

                if (fi.Exists)
                {
                    NewNameForFile(fi);
                    
                }
            }

            MessageBox.Show("Finished.");
            this.listBox1.Items.Clear();
        }

        private void NewNameForFile(FileInfo fi)
        {
            try
            {
                String extension = fi.Extension;

                string charBox = "";
                if (this.checkBox1.Checked) { charBox += numbers; }
                if (this.checkBox2.Checked) { charBox += chars; }


                Random r = new Random();
                int length = r.Next((int)this.numericUpDown1.Value, (int)this.numericUpDown2.Value);

                string newName = "";
                for (int pos = 0; pos < length; pos++)
                {
                    int cpos = r.Next(charBox.Length - 1);
                    newName += charBox[cpos].ToString();
                }

                if (File.Exists(fi.DirectoryName + "\\" + newName + extension))
                {
                    NewNameForFile(fi);
                }
                else
                {
                    fi.CopyTo(fi.DirectoryName + "\\" + newName + extension);

                    if (this.checkBox3.Checked)
                    {
                        File.Delete(fi.FullName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = "https://www.marcogriep.de";
            p.Start();
        }
    }
}
