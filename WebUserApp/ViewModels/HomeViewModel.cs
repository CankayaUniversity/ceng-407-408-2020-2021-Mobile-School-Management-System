using MobilOkulProc.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilOkulProc.WebUserApp.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public string ClassName { get; set; }
        public string StudentsTeacherName { get; set; }
        public string TotalAbsence { get; set; }
        public string WeeklyLoad { get; set; }
        public string MostRecentExam { get; set; }
        public string ExamsLeft { get; set; }
        public string LatestGrade { get; set; }
        public string TotalMessages { get; set; }
        public string SchoolLocation { get; set; }
        public List<SYLLABUS> SyllabusList { get; set; }
        public List<EXAM> ExamList { get; set; }
        public List<TEACHER> TeacherList { get; set; }
        public List<NEWS> NewsList { get; set; }


    }
}
