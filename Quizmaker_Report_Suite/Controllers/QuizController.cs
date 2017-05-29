using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Quizmaker_Report_Suite.DAL;
using Quizmaker_Report_Suite.Models;

namespace Quizmaker_Report_Suite.Controllers
{
    public class QuizController : Controller
    {
        private DALContext db = new DALContext();

        // GET: Quiz
        public ActionResult Index(string sortOrder, string searchString, string projectName)
        {
            var projectNames = db.Quizs.Select(p => new { p.ProjectName }).ToDictionary(p => p.ProjectName, p => p.ProjectName);

            ViewBag.ProjectNames = projectNames;

            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParam = sortOrder == "Date" ? "Date" : "date_desc";
            ViewBag.UsedTimeSortParam = sortOrder == "UsedTime" ? "UsedTime" : "used_time_desc";

            var quizes = from q in db.Quizs
                         select q;

            if (!String.IsNullOrEmpty(searchString))
                quizes = quizes.Where(q => q.UserName.Contains(searchString) || q.UserEmail.Contains(searchString));

            if (!String.IsNullOrEmpty(projectName))
                quizes = quizes.Where(q => q.ProjectName.Contains(projectName));

            switch (sortOrder)
            {
                case "name_desc":
                    quizes = quizes.OrderByDescending(q => q.UserName);
                    break;
                case "Date":
                    quizes = quizes.OrderBy(q => q.QuizDate);
                    break;
                case "date_desc:":
                    quizes = quizes.OrderByDescending(q => q.QuizDate);
                    break;
                case "UsedTime":
                    quizes = quizes.OrderBy(q => q.UsedTime);
                    break;
                case "used_time_desc":
                    quizes = quizes.OrderByDescending(q => q.UsedTime);
                    break;
                default:
                    quizes = quizes.OrderBy(q => q.QuizDate);
                    break;
            }

            return View(quizes.ToList());
        }

        public HttpStatusCodeResult SaveResult()
        {

            if (ModelState.IsValid)
            {

                db.Quizs.Add(GetRequestParameters(Request));
                db.SaveChanges();

                Console.Write("OK");
                Response.Write("OK");

                return new HttpStatusCodeResult(HttpStatusCode.OK);

            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);

        }

        public ActionResult ByDate()
        {
            IQueryable<QuizDateReportGroup> data = from quiz in db.Quizs
                                             group quiz by new { quiz.QuizDate, quiz.ProjectName } into dateGroup
                                             select new QuizDateReportGroup()
                                             {
                                                 QuizDate = dateGroup.Key.QuizDate,
                                                 ProjectName = dateGroup.Key.ProjectName,
                                                 UserCount = dateGroup.Count()
                                             };
            return View(data.ToList());
        }

        private Quiz GetRequestParameters(HttpRequestBase request)
        {
            Quiz qResults = new Quiz();

            if (request.Form.Count > 0)
            {
                qResults.ProjectName = (request.Form["PROJECT_NAME"] == null) ? "" : request.Form["PROJECT_NAME"];
                qResults.UserName = (request.Form["USER_NAME"] == null) ? "" : request.Form["USER_NAME"];
                qResults.UserEmail = (request.Form["USER_EMAIL"] == null) ? "" : request.Form["USER_EMAIL"];
                qResults.DetailedResults = (request.Unvalidated.Form["dr"] == null) ? "" : request.Unvalidated.Form["dr"];
                qResults.EarnedPoints = (request.Form["sp"] == null) ? "" : request.Form["sp"];
                qResults.PassingScore = (request.Form["ps"] == null) ? "" : request.Form["ps"];
                qResults.PassingScorePercentage = (request.Form["psp"] == null) ? "" : request.Form["psp"];
                qResults.GainedScore = (request.Form["tp"] == null) ? "" : request.Form["tp"];

                qResults.QuizTitle = (request.Form["qt"] == null) ? "" : request.Form["qt"];
                qResults.QuizType = (request.Form["t"] == null) ? "" : request.Form["t"];
                //qResults.QuizReport = (request.Unvalidated.Form["rt"] == null) ? "" : request.Unvalidated.Form["rt"];

                qResults.QuizID = (request.Form["sid"] == null) ? "" : request.Form["sid"];
                qResults.TimeLimit = (request.Form["tl"] == null) ? "" : request.Form["tl"];
                qResults.UsedTime = (request.Form["ut"] == null) ? "" : request.Form["ut"];
                qResults.QuizDate = System.DateTime.Now;
            }

            return qResults;
        }

        // GET: Quiz/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quiz quiz = db.Quizs.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            return View(quiz);
        }

        [NonAction]
        // GET: Quiz/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: Quiz/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [NonAction]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProjectName,QuizID,QuizType,QuizTitle,PassingScore,PassingScorePercentage,UserName,UserEmail,UserScore,QuizDate,DetailedResults,EarnedPoints,GainedScore,QuizReport,TimeLimit,UsedTime")] Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                db.Quizs.Add(quiz);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quiz);
        }

        // GET: Quiz/Edit/5
        [NonAction]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quiz quiz = db.Quizs.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            return View(quiz);
        }

        // POST: Quiz/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [NonAction]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProjectName,QuizID,QuizType,QuizTitle,PassingScore,PassingScorePercentage,UserName,UserEmail,UserScore,QuizDate,DetailedResults,EarnedPoints,GainedScore,QuizReport,TimeLimit,UsedTime")] Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quiz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quiz);
        }

        // GET: Quiz/Delete/5
        [NonAction]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quiz quiz = db.Quizs.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            return View(quiz);
        }

        // POST: Quiz/Delete/5
        [NonAction]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quiz quiz = db.Quizs.Find(id);
            db.Quizs.Remove(quiz);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
