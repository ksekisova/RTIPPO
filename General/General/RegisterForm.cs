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
    public partial class RegisterForm : Form
    {
        private Game game;
        public RegisterForm(Game game)
        {
            InitializeComponent();
            this.game = game;
            table.Columns.Add("№", -2, HorizontalAlignment.Left);
            table.Columns.Add("Имя",-2, HorizontalAlignment.Left);
            button2.Enabled = false;
            textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();
            if (name.Length == 0)
                MessageBox.Show("Введите имя игрока", "Ошибка");
            else
            {
                bool f = false;
                foreach (Player p in game.playerList)
                {
                    if (p.name == name)
                    {
                        MessageBox.Show("Данное имя уже занято, попробуте ввести другое", "Ошибка");
                        textBox1.Clear();
                        f = true;
                        break;
                    }
                }
                if (!f)
                {
                    game.CreatePlayer(name);
                    table.Items.Add(new ListViewItem(new string[] { game.playerList.Count.ToString(), name }));
                    textBox1.Clear();
                    if (game.playerList.Count > 0) button2.Enabled = true;
                }
            }
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            QueueForm form4 = new QueueForm(game);
            Hide();
            form4.ShowDialog();
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void table_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
