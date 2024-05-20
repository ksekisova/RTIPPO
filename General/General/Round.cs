using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General
{
    public class Round
    {
        public int number;
        public List<Throw> throwList = new List<Throw>();
        public void CreateThrow(Player player)
        {
            Throw th = new Throw();
            th.player = player;
            AddThrow(th);
        }
        public void AddThrow(Throw th)
        {
            this.throwList.Add(th);
        }
    }
}
