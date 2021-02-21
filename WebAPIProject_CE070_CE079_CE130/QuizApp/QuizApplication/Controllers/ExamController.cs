using QuizApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QuizApplication.Controllers
{
    public class ExamController : ApiController
    {
        db_QUIZEntities db = new db_QUIZEntities();
        List<TBL_Exam> li = null;

        [HttpGet]
        public HttpResponseMessage GetAllExams(long id)
        {
            li = db.TBL_Exam.Where(x => x.examCreatedBy == id).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, li);
        }
    }
}