using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General
{
    public class Throw
    {
        public Player player;
        public int throwNumber = 1;
        public List<int> diceList = new List<int>();
        public DiceCombination combination = null;
        public int score = 0;
        public void ThrowDice()
        {
            var rand = new Random();
            for(int i = 0; i < 5; i++)
            {
                this.diceList.Add(rand.Next(1, 7));
            }
        }
        public void FindComb()
        {
            this.combination = null;
            this.score = 0;
            var resTable = this.player.resultTable.combScoreDict;
            Dictionary<int, int> dice = new Dictionary<int, int>() { { 1, 0}, { 2, 0}, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 } };
            foreach(int d in this.diceList)
            {
                dice[d] += 1;
            }
            if(dice.ContainsValue(5) && (resTable["Генерал"] == 0 || this.throwNumber == 1))
            {
                this.combination = new DiceCombination("Генерал", 0, 60, true);
            }
            else if (dice.ContainsValue(4) && resTable["Каре"] == 0)
            {
                this.combination = new DiceCombination("Каре", 45, 40, false);
            }
            else if ((dice.ContainsValue(3) && dice.ContainsValue(2)) && resTable["Фулл Хаус"] == 0)
            {
                this.combination = new DiceCombination("Фулл Хаус", 35, 30, false);
            }
            else if ((dice[3] == 1 && dice[4] == 1 && dice[5] == 1 && (dice[1] == 2 || (dice[1] == 1 && (dice[2]+dice[6]) == 1) || (dice[2] == 1 && dice[6] == 1))) && resTable["Стрит"] == 0)
            {
                this.combination = new DiceCombination("Стрит", 25, 20, false);
            }
            if(this.combination != null)
            {
                if (this.throwNumber == 1) this.score = this.combination.firstTryScore;
                else this.score = this.combination.secondTryScore;
            }
        }
        public void ThrowDiceAgain(List<int> dice)
        {
            var rand = new Random();
            foreach (int d in dice)
            {
                this.diceList[d] = rand.Next(1, 7);
            }
        }
        public void ChooseNumber(int num)
        {
            int count = this.diceList.Count(num.Equals);
            Dictionary<int, string> dice = new Dictionary<int, string>() { { 1, "Единицы" }, { 2, "Двойки" }, { 3, "Тройки" }, { 4, "Четверки" }, { 5, "Пятерки" }, { 6, "Шестерки" } };
            this.combination = new DiceCombination(dice[num], 0, num * count, false);
            this.score = this.combination.secondTryScore;
        }
    }
}
