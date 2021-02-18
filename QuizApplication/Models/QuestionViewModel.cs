using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizApplication.Models
{
    public class Answers
    {
        public long id { get; set; }
        public string answer { get; set; }
    }

    public class QuestionViewModel : Answers
    {
        public string text { get; set; }

        public string option1 { get; set; }

        public string option2 { get; set; }

        public string option3 { get; set; }

        public string option4 { get; set; }

        public short type { get; set; }

        public int examId { get; set; }
    }
}