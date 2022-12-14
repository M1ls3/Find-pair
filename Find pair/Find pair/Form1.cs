using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace Find_pair
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        List<string> cells = new List<string>()
        {
        "!", "!", "N", "N", ",", ",", "с", "с",
        "b", "b", "v", "v", "ї", "ї", "ф", "ф"
        };

        Label[] clickedCells = new Label[2];
        int countClick = 0;    
        int timeLeft = 0;

        //TableLayoutPanel cell = new TableLayoutPanel();
        //int countCells = 4;

        public Form1()
        {
            Menu();
            InitializeComponent();
            CellsFiller();
            //Init();
        }

        private void Menu()
        {
            MainMenu mainMenu = new MainMenu();
            MenuItem menuItem = new MenuItem();
            menuItem.Text = "Difficulty";
            mainMenu.MenuItems.Add(menuItem);
        }

        //void Init () 
        //{
        //    Text = "Find pair";
        //    Width = 600;
        //    Height = 600;
        //    cell.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        //    cell.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        //    cell.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        //    cell.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

        //    cell.Dock= DockStyle.Fill;
        //    cell.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
        //    Controls.Add(cell);
        //    cell.Controls.Add(new Label
        //    {
        //        Text = "A",
        //        Dock = DockStyle.Fill,
        //        BackColor = Color.Wheat,
        //    },0,0);
        //    cell.Controls.Add(new Label
        //    {
        //        Text = "A",
        //        Dock = DockStyle.Fill,
        //        BackColor = Color.Wheat,
        //    },0,1);
        //    cell.Controls.Add(new Label
        //    {
        //        Text = "A",
        //        Dock = DockStyle.Fill,
        //        BackColor = Color.Wheat,
        //    },1,0);
        //    cell.Controls.Add(new Label
        //    {
        //        Text = "A",
        //        Dock = DockStyle.Fill,
        //        BackColor = Color.Wheat,
        //    },1,1);

        //}

        private void CellsFiller()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label cellLabel = control as Label;
                if (cellLabel != null)
                {
                    var rnd = random.Next(cells.Count);
                    cellLabel.Text = cells[rnd];
                    cellLabel.ForeColor = cellLabel.BackColor;
                    cells.RemoveAt(rnd);
                }
            }
        }

        private void CellClick(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;
            countClick++;

            if (clickedLabel != null)
            {
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                if (clickedCells[0] == null)
                {
                    clickedCells[0] = clickedLabel;
                    clickedLabel.ForeColor = Color.Black;
                    return;
                }
                clickedCells[1] = clickedLabel;
                clickedCells[1].ForeColor = Color.Black;

                CheckForWinner();

                if (clickedCells[0].Text == clickedCells[1].Text)
                {
                    clickedCells[0] = null;
                    clickedCells[1] = null;
                    return;
                }

                timer1.Start();
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CheckForWinner()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label cellLabel = control as Label;

                if (cellLabel != null)
                {
                    if (cellLabel.ForeColor == cellLabel.BackColor)
                        return;
                }
            }
            timer2.Stop();
            MessageBox.Show($"You matched all the cells!\nSсore: {countClick}", "Congratulations");
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            clickedCells[0].ForeColor = clickedCells[0].BackColor;
            clickedCells[1].ForeColor = clickedCells[1].BackColor;

            clickedCells[0] = null;
            clickedCells[1] = null;
        }

        private void difficultyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void easyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timeLeft = 40;
            timer2.Start();
        }

        private void midleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timeLeft = 30;
            timer2.Start();
        }

        private void hardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timeLeft = 20;
            timer2.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft = timeLeft - 1;
                if (timeLeft > 9)
                    label18.Text = $"00:{timeLeft}"; 
                else
                    label18.Text = $"00:0{timeLeft}";
            }
            else
            {
                timer2.Stop();
                MessageBox.Show("You didn't finish in time.", "Defeat!");
                Close();
            }
        } 
    }
}
