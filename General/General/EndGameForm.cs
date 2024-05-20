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
    public partial class EndGameForm : Form
    {
        Game game;
        public EndGameForm(Game game)
        {
            InitializeComponent();
            this.game = game;
            table.Columns.Add("Игрок", -2, HorizontalAlignment.Left);
            foreach(string s in game.playerList[0].resultTable.combScoreDict.Keys) table.Columns.Add(s, -2, HorizontalAlignment.Left);
            table.Columns.Add("Всего", -2, HorizontalAlignment.Left);
            foreach(Player p in game.playerList)
            {
                string[] s = new string[12];
                s[0] = p.name;
                int i = 1;
                foreach(int score in p.resultTable.combScoreDict.Values)
                {
                    s[i] = score.ToString();
                    i += 1;
                }
                s[i] = p.resultTable.totalScore.ToString();
                table.Items.Add(new ListViewItem(s));
            }
            label1.Text = "Выиграл " + game.winner.name;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
