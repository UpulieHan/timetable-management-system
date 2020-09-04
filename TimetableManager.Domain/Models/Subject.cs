using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableManager.Domain.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public string SubjectCode { get; set; }
        public int LectureHours { get; set; }
        public int TutorialHours { get; set; }
        public int LabHours { get; set; }
        public int EvaluationHours { get; set; }
        // OfferedYearSemester can be changed to a Year_Semester type instead of string
        public string OfferedYearSemester { get; set; }
    }
}
