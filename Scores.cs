using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGameProject
{
    public class Scores
    {
        public int HighScore { get; set; }
        public int SecondScore { get; set; }
        public int ThirdScore {  get; set; }
        public int FourthScore {  get; set; }
        public int FifthScore {  get; set; }

        public string HighScoreName { get; set; }

        public string SecondScoreName { get; set; }

        public string ThirdScoreName { get; set; }

        public string FourthScoreName { get; set; }

        public string FifthScoreName { get; set; }

        public Scores() 
        {
            HighScore = 30;
            SecondScore = 25;
            ThirdScore = 20;
            FourthScore = 15;
            FifthScore = 10;
        }
    }
}
