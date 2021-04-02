using QuizApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizApplication.Controllers
{
    public class AdminController : Controller
    {
        db_QUIZEntities db = new db_QUIZEntities();

        //  For Login
        public ActionResult Login()
        {
            return View();
        }

        public JsonResult LoginWithAjax(string username, string password)
        {
            TBL_User user = db.TBL_User.Where(x => x.userLoginName.ToLower().Equals(username.ToLower()) && x.userPassword.Equals(password)).SingleOrDefault();

            if (user == null)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["usr"] = user;
                return Json("1", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Login");
        }

        public ActionResult Index()
        {
            if (Session["usr"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }



        //  For Exam
        public ActionResult Exam()
        {
            if (Session["usr"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public JsonResult GetExam()
        {
            db.Configuration.ProxyCreationEnabled = false;
            TBL_User user = (TBL_User)Session["usr"];
            if (user != null)
            {
                try
                {
                    List<TBL_Exam> exam = db.TBL_Exam.Where(x => x.examCreatedBy == user.userId).ToList();
                    return Json(exam, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }
            return Json("0", JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeStatus(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            try
            {
                TBL_Exam exam = db.TBL_Exam.Where(x => x.examId == id).SingleOrDefault();
                exam.examStatus = !exam.examStatus;
                db.SaveChanges();
                return Json("Status Updated", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

            }
            return Json("Status Failed to update", JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddEditExam(TBL_Exam data)
        {
            db.Configuration.ProxyCreationEnabled = false;
            try
            {
                TBL_Exam exam = null;
                if (data.examId == 0)
                {
                    TBL_User user = (TBL_User)Session["usr"];
                    exam = new TBL_Exam();
                    exam.examName = data.examName;
                    exam.examStatus = false;
                    exam.examCreatedBy = Convert.ToInt32(user.userId);
                    exam.examAppearCode = Utility.RandomString(8);   //    System.Guid.NewGuid().ToString();
                    db.TBL_Exam.Add(exam);
                    db.SaveChanges();
                    return Json("Exam Added Successfully", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    TBL_User user = (TBL_User)Session["usr"];
                    exam = db.TBL_Exam.Where(x => x.examId == data.examId).SingleOrDefault();
                    exam.examName = data.examName;
                    db.SaveChanges();
                    return Json("Exam Updated Successfully", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

            }
            return Json("Status Failed to Perform Operation", JsonRequestBehavior.AllowGet);
        }



        //  For Question
        public ActionResult Question()
        {
            if (Session["usr"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public JsonResult AddQuestions(QuestionViewModel que)
        {
            bool success = true;
            string msg = "";

            try
            {
                TBL_Question ques = new TBL_Question();
                ques.questionType = que.type;
                ques.questionText = que.text;
                ques.questionOption1 = que.option1;
                ques.questionOption2 = que.option2;
                ques.questionOption3 = que.option3;
                ques.questionOption4 = que.option4;
                ques.questionAnswer = que.answer;
                ques.questionExamId = que.examId;

                db.TBL_Question.Add(ques);
                db.SaveChanges();
                que.id = ques.questionId;

                msg = "Question is added successfully";
            }
            catch (Exception)
            {
                success = false;
                msg = "Failed to add question";
            }
            return Json(new { success = success, msg = msg, data = que }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetQuestions(int id)
        {
            bool success = true;
            string msg = "";
            List<QuestionViewModel> obj = new List<QuestionViewModel>();

            try
            {
                List<TBL_Question> li = db.TBL_Question.Where(x => x.questionExamId == id).ToList();
                foreach (var item in li)
                {
                    QuestionViewModel que = new QuestionViewModel();
                    que.id = item.questionId;
                    que.text = item.questionText;
                    que.option1 = item.questionOption1;
                    que.option2 = item.questionOption2;
                    que.option3 = item.questionOption3;
                    que.option4 = item.questionOption4;
                    que.answer = item.questionAnswer;
                    que.type = (short)item.questionType;
                    que.examId = (int)item.questionExamId;
                    obj.Add(que);
                }
                msg = "Question is got successfully";
            }
            catch (Exception)
            {
                success = false;
                msg = "Failed to get questions";
            }
            return Json(new { success = success, msg = msg, data = obj }, JsonRequestBehavior.AllowGet);
        }



        //  For User
        public ActionResult User()
        {
            if (Session["usr"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult Report()
        {
            if (Session["usr"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}
