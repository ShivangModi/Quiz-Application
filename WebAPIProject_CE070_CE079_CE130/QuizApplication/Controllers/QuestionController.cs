using QuizApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QuizApplication.Controllers
{
    public enum QuestionType
    {
        MultipleChoice,     //  0
        SingleChoice         //  1
    }

    public class QuestionController : ApiController
    {
        List<QuestionViewModel> li = new List<QuestionViewModel>();
        List<TBL_Question> questions = null;
        db_QUIZEntities db = new db_QUIZEntities();

        public QuestionController()
        {
            
        }

        [HttpGet]
        public HttpResponseMessage Get(long id)
        {
            questions = db.TBL_Question.Where(x => x.questionExamId == id).ToList();
            foreach (var item in questions)
            {
                QuestionViewModel que = new QuestionViewModel();
                que.id = item.questionId;
                que.text = item.questionText;
                que.option1 = item.questionOption1;
                que.option2 = item.questionOption2;
                que.option3 = item.questionOption3;
                que.option4 = item.questionOption4;
                li.Add(que);
            }
            return Request.CreateResponse(HttpStatusCode.OK, li);
        }

        [HttpPost]
        public HttpResponseMessage Post(List<Answers> answers)
        {
            int score = 0;
            foreach (var item in answers)
            {
                string ans = db.TBL_Question.Where(x => x.questionId == item.id).SingleOrDefault().questionAnswer;
                if (ans.ToLower().Equals(item.answer.ToLower()))
                {
                    score++;
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, score);
        }
    }
}
