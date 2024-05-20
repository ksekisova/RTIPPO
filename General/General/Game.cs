using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General
{
    public class Game
    {
        public List<Player> playerList = new List<Player>();
        public List<Round> roundList = new List<Round>();
        public Player winner;
        public void StartGame()
        {
            this.CreateRound(1);
        }
        public void MakeQueue()
        {
            int min = 6;
            int count = this.playerList.Count();
            int num = count;
            int minNum = num;
            foreach (Player p in this.playerList)
            {
                if(p.firstDice == 1)
                {
                    minNum = num;
                    break;
                }
                else
                {
                    if(min > p.firstDice)
                    {
                        min = p.firstDice;
                        minNum = num;
                    }
                    num = num - 1;
                }
            }
            foreach (Player q in this.playerList)
            {
                q.SetQueueNum(minNum % count + 1);
                minNum = minNum + 1;
            }
            return;
        }
        public void CreatePlayer(string name)
        {
            Player player = new Player();
            player.name = name;
            AddPlayer(player);
        }
        public void AddPlayer(Player player)
        {
            this.playerList.Add(player);
        }
        public void CreateRound(int number)
        {
            Round round = new Round();
            round.number = number;
            AddRound(round);
        }
        public void AddRound(Round round)
        {
            this.roundList.Add(round);
        }
        public void ChooseWinner()
        {
            int max = 0;
            Player winner = this.playerList[0];
            foreach (Player p in this.playerList)
            {
                if (p.resultTable.totalScore > max)
                {
                    max = p.resultTable.totalScore;
                    winner = p;
                }                    
            }
            this.winner = winner;
        }
        public void ChooseWinner(Player player)
        {
            this.winner = player;
        }
    }
}
