using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGameProject
{
    public class Game
    {
        public int PlayerNum { get; set; }
        public int RoundNum { get; set; }
        public int QuestionNum { get; set; }
        public int Player1Score { get; set; }
        public int Player2Score { get; set; }
        public int Player3Score { get; set; }
        public int Player4Score { get; set; }
        public int Player1Rounds {  get; set; }
        public int Player2Rounds { get; set; }
        public int Player3Rounds { get; set; }
        public int Player4Rounds { get; set; }
        public int TurnNum { get; set; }



        public Game(int playnum)
        {
            PlayerNum = playnum;
            RoundNum = 1;
            QuestionNum = 1;
            Player1Rounds = 0;
            Player1Score = 0;
            Player2Rounds = 0;
            Player2Score = 0;
            Player3Rounds = 0;
            Player3Score = 0;
            Player4Rounds = 0;
            Player4Score = 0;
            TurnNum = 1;
        }

        public Game()
        {
            PlayerNum = 1;
            RoundNum = 1;
            QuestionNum = 1;
            Player1Rounds = 0;
            Player1Score = 0;
            Player2Rounds = 0;
            Player2Score = 0;
            Player3Rounds = 0;
            Player3Score = 0;
            Player4Rounds = 0;
            Player4Score = 0;
            TurnNum = 1;
        }
        public Game(int playnum, int roundnum, int questionnum, int play1score, int play1rounds) 
        { 
            PlayerNum = playnum;
            RoundNum = roundnum;
            QuestionNum = questionnum;
            Player1Score = play1score;
            Player1Rounds = play1rounds;
        }

        public Game(int playnum, int roundnum, int questionnum, int play1score, int play2score, int play1rounds, int play2rounds, int turnNum)
        {
            PlayerNum = playnum;
            RoundNum = roundnum;
            QuestionNum = questionnum;
            Player1Score = play1score;
            Player1Rounds = play1rounds;
            Player2Score = play2score;
            Player2Rounds = play2rounds;
            TurnNum = turnNum;
        }

        public Game(int playnum, int roundnum, int questionnum, int play1score, int play2score, int play3score, int play1rounds, int play2rounds, int play3rounds, int turnNum)
        {
            PlayerNum = playnum;
            RoundNum = roundnum;
            QuestionNum = questionnum;
            Player1Score = play1score;
            Player1Rounds = play1rounds;
            Player2Score = play2score;
            Player2Rounds = play2rounds;
            Player3Score = play3score;
            Player3Rounds = play3rounds;
            TurnNum = turnNum;
        }

        public Game(int playnum, int roundnum, int questionnum, int play1score, int play2score, int play3score, int play4score, int play1rounds, int play2rounds, int play3rounds, int play4rounds,int turnNum)
        {
            PlayerNum = playnum;
            RoundNum = roundnum;
            QuestionNum = questionnum;
            Player1Score = play1score;
            Player1Rounds = play1rounds;
            Player2Score = play2score;
            Player2Rounds = play2rounds;
            Player3Score = play3score;
            Player3Rounds = play3rounds;
            Player4Score = play4score;
            Player4Rounds = play4rounds;
            TurnNum = turnNum;
        }
    }
}
