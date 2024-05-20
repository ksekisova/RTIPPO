using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace General
{
    public partial class QueueForm : Form
    {
        private Game game;
        private int playerNum = 0;
        public QueueForm(Game game)
        {
            InitializeComponent();
            this.game = game;
            table.Columns.Add("№", -2, HorizontalAlignment.Left);
            table.Columns.Add("Имя", -2, HorizontalAlignment.Left);
            table.Columns.Add("Жребий", -2, HorizontalAlignment.Left);
            for (int i = 1; i <= game.playerList.Count; i++)
            {
                table.Items.Add(new ListViewItem(new string[] { i.ToString(), game.playerList[i - 1].name, "" }));
            }
            button2.Enabled = false;
            button3.Visible = false;
            label2.Text = game.playerList[playerNum].name;
            label4.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            table.Items[playerNum] = new ListViewItem(new string[] { (playerNum+1).ToString(), game.playerList[playerNum].name, game.playerList[playerNum].firstDice.ToString() });
            if(game.playerList[playerNum].firstDice == 1 || playerNum == (game.playerList.Count - 1))
            {
                button3.Enabled = false;
                game.MakeQueue();
                foreach (Player p in game.playerList)
                {
                    if (p.queueNumber == 1)
                    {
                        label4.Text = "Первым ходит: " + p.name;
                        button2.Enabled = true;
                    }
                }
            }
            else
            {
                pictureBox1.Visible = false;
                button3.Visible = false;
                button1.Visible = true;
                playerNum += 1;
                label2.Text = game.playerList[playerNum].name;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            game.playerList[playerNum].ThrowFirstDice();
            pictureBox1.Image = Image.FromFile("image\\"+ game.playerList[playerNum].firstDice.ToString()+".png");
            pictureBox1.Visible = true;
            button1.Visible = false;
            button3.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            game.StartGame();
            RoundForm form4 = new RoundForm(game);
            Hide();
            form4.ShowDialog();
            Close();
        }
    }
}
