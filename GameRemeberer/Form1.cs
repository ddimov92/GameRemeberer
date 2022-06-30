using CsvHelper.Configuration.Attributes;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace GameRemeberer
{
    public partial class Form1 : Form
    {
        public List<Game> games = new List<Game>();
        public int beginning = 0;
        
        public Form1()
        {
            InitializeComponent();
            DatabaseHandler.ReadGames(games);
            Add50Games(beginning, beginning + 50);
        }
        public void Add50Games(int beginning, int end)
        {
            for (int i = beginning; i < end; i++)
            {
                var game = games[i];
                FlowLayoutPanel flowPanel = new FlowLayoutPanel();
                flowPanel.AutoSize = true;
                flowPanel.FlowDirection = FlowDirection.LeftToRight;
                flowPanel.Dock = DockStyle.Fill;
                flowPanel.BorderStyle = BorderStyle.FixedSingle;
                flowPanel.Click += flowPanel_Click;
                if (game.Owned.Contains("False"))
                {
                    flowPanel.BackColor = Color.Red;
                }
                else
                {
                    flowPanel.BackColor = Color.Green;
                }
                    
                //Image
                PictureBox image = new PictureBox();
                try
                {
                    image.Image = Image.FromFile($@"{Environment.CurrentDirectory}\Images\{i + 1}.jpg");
                }
                catch (Exception)
                {
                    MessageBox.Show("Images folder not found.");
                }
                image.SizeMode = PictureBoxSizeMode.AutoSize;
                image.Anchor = AnchorStyles.None;

                //Information
                Label nameLabel = new Label();
                nameLabel.AutoSize = true;
                nameLabel.Text = $"{game.Name}\n{game.Platform}\nRating: {game.Rating}";

                //ID
                Label idLabel = new Label();
                idLabel.Visible = false;
                idLabel.Text = game.ID.ToString();

                flowPanel.Controls.Add(image);
                flowPanel.Controls.Add(nameLabel);
                flowPanel.Controls.Add(idLabel);
                tableLayoutPanel1.Controls.Add(flowPanel, -1, -1);
            }
        }
        public void flowPanel_Click(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            int ID = int.Parse(control.Controls[2].Text);
            var game = games.Find(x => x.ID == ID);
            if (game.Owned.Contains("False"))
            {
                game.Owned = "True";
                DatabaseHandler.UpdateSingleRow(ID, "True");
            }
            else
            {
                game.Owned = "False";
                DatabaseHandler.UpdateSingleRow(ID, "False");
            }

            if (control.BackColor != Color.Green)
                control.BackColor = Color.Green;
            else
                control.BackColor = Color.Red;
        }

        private void tableLayoutPanel1_Scroll_1(object sender, ScrollEventArgs e)
        {
            VScrollProperties vs = tableLayoutPanel1.VerticalScroll;
            
            if (vs.Value == vs.Maximum - vs.LargeChange + 1)
            {
                beginning += 50;
                Add50Games(beginning, beginning + 50);
            }
        }
    }
}
