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
        public int Player1TotalScore { get; set; }
        public int Player2TotalScore { get; set; }
        public int Player3TotalScore { get; set; }
        public int Player4TotalScore { get; set; }

        public bool ContinueGame { get; set; }



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
        //This is the troublesome constructor and my code breaks totally without it so I guess its gonna stay put here
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
            Player1TotalScore = 0;
            Player2TotalScore = 0;
            Player3TotalScore = 0;
            Player4TotalScore = 0;
            ContinueGame = false;
        }
        //Honestly dont know if after all the trouble I went through to get this to work if I even need these constructor anymore or any from any of the other ones
        public Game(int playnum, int roundnum, int questionnum, int play1score, int play1rounds, bool cont) 
        { 
            PlayerNum = playnum;
            RoundNum = roundnum;
            QuestionNum = questionnum;
            Player1Score = play1score;
            Player1Rounds = play1rounds;
        }

        public Game(int playnum, int roundnum, int questionnum, int play1score, int play2score, int play1rounds, int play2rounds, int turnNum, int play1totscore, int play2totscore)
        {
            PlayerNum = playnum;
            RoundNum = roundnum;
            QuestionNum = questionnum;
            Player1Score = play1score;
            Player1Rounds = play1rounds;
            Player2Score = play2score;
            Player2Rounds = play2rounds;
            TurnNum = turnNum;
            Player1TotalScore = play1totscore;
            Player2TotalScore = play2totscore;
            ContinueGame = false;
        }

        public Game(int playnum, int roundnum, int questionnum, int play1score, int play2score, int play3score, int play1rounds, int play2rounds, int play3rounds, int turnNum, int play1totscore, int play2totscore, int play3totscore)
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
            Player1TotalScore = play1totscore;
            Player2TotalScore = play2totscore;
            Player3TotalScore = play3totscore;
        }

        public Game(int playnum, int roundnum, int questionnum, int play1score, int play2score, int play3score, int play4score, int play1rounds, int play2rounds, int play3rounds, int play4rounds,int turnNum, int play1totscore, int play2totscore, int play3totscore, int play4totscore)
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
            Player1TotalScore = play1totscore;
            Player2TotalScore = play2totscore;
            Player3TotalScore = play3totscore;
            Player4TotalScore = play4totscore;
            TurnNum = turnNum;
        }
    }
}
