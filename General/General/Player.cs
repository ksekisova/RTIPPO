using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General
{
    public class Player
    {
        public string name;
        public int firstDice;
        public int queueNumber;
        public ResultTable resultTable = new ResultTable();
        public void ThrowFirstDice()
        {
            Random rand = new Random();
            this.SetDice(rand.Next(1, 7));
        }
        public void SetDice(int dice)
        {
            this.firstDice = dice;
        }
        public void SetQueueNum(int num)
        {
            this.queueNumber = num;
        }
    }
}
