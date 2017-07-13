using System;
using System.ComponentModel.DataAnnotations;

namespace Quizmaker_Report_Suite.Models
{
    public class QuizResults
    {
        public int ID { get; set; }

        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Display(Name = "Quiz Title")]
        public string QuizTitle { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "User Email")]
        public string UserEmail { get; set; }
        
        [DataType(DataType.DateTime), Display(Name = "Quiz Date"), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public System.DateTime QuizDate { get; set; }

        [Display(Name = "Detailed Results")]
        public string DetailedResults { get; set; }

        [Display(Name = "Earned Points")]
        public int EarnedPoints { get; set; }
        
        [Display(Name = "Time Limit")]
        public string TimeLimit { get; set; }

        [Display(Name = "Used Time")]
        public int UsedTime { get; set; }
    }
}