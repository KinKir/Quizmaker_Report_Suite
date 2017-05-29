using System;
using System.ComponentModel.DataAnnotations;

namespace Quizmaker_Report_Suite.Models
{
    public class QuizDateReportGroup
    {
        [DataType(DataType.Date)]
        [Display(Name = "Quiz Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? QuizDate { get; set; }
        public string ProjectName { get; set; }
        [Display(Name = "User Count")]
        public int UserCount { get; set; }
    }
}