using System.ComponentModel.DataAnnotations;

namespace Quizmaker_Report_Suite.Models
{
    public class Quiz
    {
        public int ID { get; set; }
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        [Display(Name = "Quiz ID")]
        public string QuizID { get; set; }
        [Display(Name = "Quiz Type")]
        public string QuizType { get; set; }
        [Display(Name = "Quiz Title")]
        public string QuizTitle { get; set; }
        [Display(Name = "Passing Score")]
        public string PassingScore { get; set; }
        [Display(Name = "Passing Score Percentage")]
        public string PassingScorePercentage { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Display(Name = "User Email")]
        public string UserEmail { get; set; }
        [Display(Name = "User Score")]
        public string UserScore { get; set; }


        [DataType(DataType.DateTime), Display(Name = "Quiz Date"), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime QuizDate { get; set; }
        [Display(Name = "Detailed Results")]
        public string DetailedResults { get; set; }
        [Display(Name = "Earned Points")]
        public string EarnedPoints { get; set; }
        [Display(Name = "Gained Score")]
        public string GainedScore { get; set; }
        [Display(Name = "Quiz Report")]
        public string QuizReport { get; set; }

        [Display(Name = "Time Limit")]
        public string TimeLimit { get; set; }
        [Display(Name = "Used Time")]
        public string UsedTime { get; set; }
    }
}