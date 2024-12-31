using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGameProject
{
    public class PlayerNames
    {
        public string Player1Name {  get; set; }
        public string Player2Name { get; set; }
        public string Player3Name { get; set; }
        public string Player4Name { get; set; }
        //Honestly dont know if after all the trouble I went through to get this to work if I even need these constructor anymore or any from any of the other ones
        public PlayerNames()
        {
            Player1Name = "Player1";
            Player2Name = "Player2";
            Player3Name = "Player3";
            Player4Name = "Player4";
        }

        public PlayerNames(string play1name)
        {
            Player1Name = play1name;
        }

        public PlayerNames(string play1name, string play2name)
        {
            Player1Name= play1name;
            Player2Name = play2name;
        }

        public PlayerNames(string play1name, string play2name, string play3name)
        {
            Player1Name = play1name;
            Player2Name = play2name;
            Player3Name = play3name;
        }

        public PlayerNames(string play1name, string play2name, string play3name, string play4name)
        {
            Player1Name = play1name;
            Player2Name = play2name;
            Player3Name = play3name;
            Player4Name = play4name;
        }
    }
}
