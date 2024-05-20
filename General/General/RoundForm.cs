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
    public partial class RoundForm : Form
    {
        Game game;
        int playerNum = 0; 
        public RoundForm(Game game)
        {
            InitializeComponent();
            this.game = game;
            for(int i = 0; i < game.playerList.Count(); i++ )
            {
                if(game.playerList[i].queueNumber == 1)
                {
                    playerNum = i;
                    break;
                }
            }
            game.roundList.Last().CreateThrow(game.playerList[playerNum]);
            table.Columns.Add("Комбинация", -2, HorizontalAlignment.Left);
            table.Columns.Add("Очки", -2, HorizontalAlignment.Left);
            foreach (var pair in game.playerList[playerNum].resultTable.combScoreDict)
            {
                table.Items.Add(new ListViewItem(new string[] { pair.Key, pair.Value.ToString() }));
            }
            table.Items.Add(new ListViewItem(new string[] { }));
            table.Items.Add(new ListViewItem(new string[] { "Всего", game.playerList[playerNum].resultTable.totalScore.ToString() }));
            label3.Text = game.roundList.Count().ToString();
            label4.Text = game.playerList[playerNum].name;
            checkBox1.Visible = false;
            checkBox2.Visible = false;
            checkBox3.Visible = false;
            checkBox4.Visible = false;
            checkBox5.Visible = false;
            button2.Enabled = false;
            button3.Visible = false;
            comboBox1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)//бросок
        {
            game.roundList.Last().throwList.Last().ThrowDice();
            game.roundList.Last().throwList.Last().FindComb();
            var  th= game.roundList.Last().throwList.Last();            
            var checkBox = new CheckBox[] { checkBox1, checkBox2, checkBox3, checkBox4, checkBox5 };
            var pictureBox = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5 };
            for(int i = 0; i < 5; i++)
            {
                checkBox[i].Visible = true;
                checkBox[i].Text = th.diceList[i].ToString();
                pictureBox[i].Visible = true;
                pictureBox[i].Image = Image.FromFile("image\\" + th.diceList[i].ToString() + ".png");
            }
            if (th.combination == null)
            {
                comboBox1.Items.Add("Нет комбинации");                
                MakeNumChekBox(th);                
            }
            else comboBox1.Items.Add(th.combination.name);
            comboBox1.SelectedIndex = 0;
            button1.Visible = false;
            button3.Visible = true;
            button2.Enabled = true;
            comboBox1.Visible = true;
            if (th.combination != null && th.combination.isAutoWin)
            {
                MessageBox.Show("Вы выбили Большого Генерала!", "Победа");
                game.ChooseWinner(th.player);
                EndGameForm form4 = new EndGameForm(game);
                Hide();
                form4.ShowDialog();
                Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)//завершить
        {
            game.playerList[playerNum].resultTable.WriteResult(game.roundList.Last().throwList.Last());
            playerNum = (playerNum + 1) % game.playerList.Count();
            if(game.roundList.Last().throwList.Count == game.playerList.Count())
            {
                if(game.roundList.Count() == 10)
                {
                    game.ChooseWinner();
                    EndGameForm form4 = new EndGameForm(game);
                    Hide();
                    form4.ShowDialog();
                    Close();
                }
                game.CreateRound(game.roundList.Count() + 1);
            }
            game.roundList.Last().CreateThrow(game.playerList[playerNum]);
            table.Items.Clear();
            foreach (var pair in game.playerList[playerNum].resultTable.combScoreDict)
            {
                table.Items.Add(new ListViewItem(new string[] { pair.Key, pair.Value.ToString() }));
            }
            table.Items.Add(new ListViewItem(new string[] { }));
            table.Items.Add(new ListViewItem(new string[] { "Всего", game.playerList[playerNum].resultTable.totalScore.ToString() }));
            label3.Text = game.roundList.Count().ToString();
            label4.Text = game.playerList[playerNum].name;
            var checkBox = new CheckBox[] { checkBox1, checkBox2, checkBox3, checkBox4, checkBox5 };
            var pictureBox = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5 };
            for (int i = 0; i < 5; i++)
            {
                checkBox[i].Visible = false;
                checkBox[i].Checked = false;
                pictureBox[i].Visible = false;
            }            
            button2.Enabled = false;
            button3.Visible = false;
            button1.Visible = true;
            comboBox1.Items.Clear();          
            comboBox1.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)//перебросить
        {
            var reList = new List<int>();
            var checkBox = new CheckBox[] { checkBox1, checkBox2, checkBox3, checkBox4, checkBox5 };
            var pictureBox = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5 };
            for (int i = 0; i < 5; i++)
            {
                if (checkBox[i].Checked) reList.Add(i);
            }
            if (reList.Count() == 0)
            {
                MessageBox.Show("Выберите кости для переброски", "Ошибка");
                return;
            }
            game.roundList.Last().throwList.Last().ThrowDiceAgain(reList);
            game.roundList.Last().throwList.Last().throwNumber += 1;
            game.roundList.Last().throwList.Last().FindComb();            
            var th = game.roundList.Last().throwList.Last();
            for (int i = 0; i < 5; i++)
            {
                checkBox[i].Visible = true;
                checkBox[i].Text = th.diceList[i].ToString();
                checkBox[i].Checked = false;
                pictureBox[i].Visible = true;
                pictureBox[i].Image = Image.FromFile("image\\" + th.diceList[i].ToString() + ".png");
            }
            comboBox1.Items.Clear();
            if (th.combination == null)
            {
                comboBox1.Items.Add("Нет комбинации");
                MakeNumChekBox(th);
            }
            else
            {
                comboBox1.Items.Add(th.combination.name);
            }
            comboBox1.SelectedIndex = 0;
            if (th.throwNumber == 3)
            {
                button3.Visible = false;              
            }
        }
        private void MakeNumChekBox(Throw th)
        {
            if (th.diceList.Contains(1) && th.player.resultTable.combScoreDict["Единицы"] == 0) comboBox1.Items.Add("Единицы");
            if (th.diceList.Contains(2) && th.player.resultTable.combScoreDict["Двойки"] == 0) comboBox1.Items.Add("Двойки");
            if (th.diceList.Contains(3) && th.player.resultTable.combScoreDict["Тройки"] == 0) comboBox1.Items.Add("Тройки");
            if (th.diceList.Contains(4) && th.player.resultTable.combScoreDict["Четверки"] == 0) comboBox1.Items.Add("Четверки");
            if (th.diceList.Contains(5) && th.player.resultTable.combScoreDict["Пятерки"] == 0) comboBox1.Items.Add("Пятерки");
            if (th.diceList.Contains(6) && th.player.resultTable.combScoreDict["Шестерки"] == 0) comboBox1.Items.Add("Шестерки");
        }

            private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dictionary<string, int> nums = new Dictionary<string, int> { { "Единицы", 1 }, { "Двойки", 2 }, { "Тройки", 3 }, { "Четверки", 4 }, { "Пятерки", 5 }, { "Шестерки", 6 } };
            if (comboBox1.SelectedIndex != 0)
            {
                game.roundList.Last().throwList.Last().ChooseNumber(nums[comboBox1.Text]);
            }
        }
    }
}
