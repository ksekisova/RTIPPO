using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General
{
    public class DiceCombination
    {
        public string name;
        public int firstTryScore = 0;
        public int secondTryScore = 0;
        public bool isAutoWin;       
        public DiceCombination(string name, int fScore, int sScore, bool isAutoWin)
        {
            this.name = name;
            this.firstTryScore = fScore;
            this.secondTryScore = sScore;
            this.isAutoWin = isAutoWin;
        }
    }
}
