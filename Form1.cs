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

namespace GasesteBriosica
{
    public partial class Form1 : Form
    {
        List<Imagine> imagini = new List<Imagine>();
        string[] files;
        int ind;
        int lung, lat;
        int x;
        int raspuns; 

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lung = panelButtons.Width / 3 - 10;
            lat = panelButtons.Height;

            string workingdirectory = Environment.CurrentDirectory;
            string projectdirectory = Directory.GetParent(workingdirectory).Parent.FullName;
            string path = Path.Combine(projectdirectory, "Resurse");
            files = Directory.GetFiles(path);


            using (StreamReader reader = new StreamReader(Path.Combine(path,"informatii.txt")))
            {
                while (reader.Peek() > 0)
                {
                    var line = reader.ReadLine().Split(';');
                    string file = files.Where(r => r.Contains(line[0])).First();
                    Imagine imagine = new Imagine() {
                        image = Image.FromFile(file),
                        raspunsuri = new string[3] { line[1], line[2], line[3] },
                        raspunscorect = Convert.ToInt32(line[4])
                    };
                    imagini.Add(imagine);
                }

            }
            NewImage();
        }

        private void NewImage()
        {
            if (ind < imagini.Count)
            {
                panelButtons.Controls.Clear();
                Imagine img = imagini[ind];
                pictureBox.Image = img.image;
                x = 0;
                for (int i = 0; i < img.raspunsuri.Length; i++)
                {
                    Button button = new Button()
                    {
                        BackColor = Color.Ivory,
                        Font = new System.Drawing.Font("Minecraft", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                        Width = lung,
                        Height = lat,
                        Location = new Point(x, 0),
                        Text = img.raspunsuri[i],
                        Tag = i

                    };
                    button.Click += Button_Click;
                    panelButtons.Controls.Add(button);
                    x += lung + 10;
                }
                raspuns = img.raspunscorect;
            }
            else { MessageBox.Show("Felicitari!", "Ai terminat!");
                ind = 0;
                NewImage();
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button= sender as Button;
            if (Convert.ToInt32(button.Tag) ==raspuns-1) 
            {
                ind++;
                NewImage();
            }
        }
    }
}
