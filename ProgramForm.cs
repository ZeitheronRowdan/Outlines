using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sem3
{
    public partial class ProgramForm : Form
    {
        public ProgramForm()
        {
            InitializeComponent();
            this.MouseWheel += panel1_MouseWheel;
        }

        float pictureScale = 1F;

        private void panel1_MouseWheel(object sender, MouseEventArgs e)
        {
        }

        private void ImageForm_Load(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button2.Enabled = false;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void ImageForm_SizeChanged(object sender, EventArgs e)
        {
            UpdatePB();
            Update();
        }

        private void UpdatePB()
        {
            pictureBox1.Width = Width - 40;
            pictureBox1.Height = Height - 81;
            pictureBox1.Scale(new SizeF(pictureScale, pictureScale));
            pictureBox1.Location = new Point(12, 30);
        }


        private bool processed = false;
        private Bitmap unproc, proc;

        // Open file
        private void button1_Click(object sender, EventArgs e)
        {
            string file = FileDialogs.OpenFile();
            if (file != null)
            {
                if (File.Exists(file))
                {
                    try
                    {
                        pictureBox1.Image = unproc = proc = new Bitmap(file);
                        processed = false;
                        button3.Enabled = true;
                        button2.Enabled = false;
                    }catch(Exception err)
                    {
                        MessageBox.Show("Не вдалось прочитати файл як зображення!");
                    }
                }
                else
                    MessageBox.Show("Файл не знайдено!");
            }
        }

        // Save
        private void button2_Click(object sender, EventArgs e)
        {
            string file = FileDialogs.SaveFile();
            if (file != null)
            {
                if(proc != null)
                {
                    try
                    {
                        proc.Save(file);
                    } catch(Exception err)
                    {
                        MessageBox.Show("Не вдалось зберегти оброблене зображення у файл!");
                    }
                }
            }
        }

        // Process
        private void button3_Click(object sender, EventArgs e)
        {
            if (!processed)
            {
                pictureBox1.Image = proc = Outlines.CreateOutlines(unproc);
                processed = true;
                button3.Enabled = false;
                button2.Enabled = true;
                Update();
            }
        }
    }
}
