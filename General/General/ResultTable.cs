using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General
{
    public class ResultTable
    {
        public Dictionary<string, int> combScoreDict = new Dictionary<string, int> { { "Единицы", 0 }, { "Двойки", 0 }, { "Тройки", 0 }, { "Четверки", 0 }, { "Пятерки", 0 }, { "Шестерки", 0 }, { "Стрит", 0 }, { "Фулл Хаус", 0 }, { "Каре", 0 }, { "Генерал", 0 } };
        public int totalScore = 0;
        public void WriteResult(Throw th)
        {
            if(th.combination != null)
            {
                this.combScoreDict[th.combination.name] = th.score;
                this.totalScore += th.score;
            }
        }
    }
}
