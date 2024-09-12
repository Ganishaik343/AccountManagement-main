using AccountManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace AccountManagement.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly IConfiguration _configuration;

        public FeedbackController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("FeedbackSuccess");
        }

        public IActionResult RedirectAction()
        {
            return RedirectToAction("Feedback", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitFeedback(FeedbackViewModel feedback)
        {
            if (ModelState.IsValid)
            {
                var sanitizer = new Ganss.Xss.HtmlSanitizer();
                feedback.CustomerName = sanitizer.Sanitize(feedback.CustomerName);
                feedback.EmailAddress = sanitizer.Sanitize(feedback.EmailAddress);
                feedback.FeedbackType = sanitizer.Sanitize(feedback.FeedbackType);
                feedback.FeedbackMessage = sanitizer.Sanitize(feedback.FeedbackMessage);
                feedback.AppVersion = sanitizer.Sanitize(feedback.AppVersion);

                string connectionString = _configuration.GetConnectionString("dbConn");
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Feedback (CustomerName, EmailAddress, FeedbackType, FeedbackMessage, AppVersion) VALUES (@CustomerName, @EmailAddress, @FeedbackType, @FeedbackMessage, @AppVersion)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@CustomerName", feedback.CustomerName);
                    cmd.Parameters.AddWithValue("@EmailAddress", feedback.EmailAddress);
                    cmd.Parameters.AddWithValue("@FeedbackType", feedback.FeedbackType);
                    cmd.Parameters.AddWithValue("@FeedbackMessage", feedback.FeedbackMessage);
                    cmd.Parameters.AddWithValue("@AppVersion", feedback.AppVersion);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                return RedirectToAction("Index", "Feedback");
            }
            return View("Index", feedback);
        }
    }
}
