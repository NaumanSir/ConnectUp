using System.Collections.Generic;
using System.Linq;
using firstbelt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace firstbelt.Controllers
{
    public class UserController : Controller
    {
        private readonly MyContext _context;
        public UserController(MyContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                user.Password = Hasher.HashPassword(user, user.Password);
                _context.Users.Add(user);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("UserId", user.UserId);
                return RedirectToAction("Home");
            }
            else
            {
                return View("Index");
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(string Email, string Password)
        {
            var user = _context.Users.SingleOrDefault(p => p.Email == Email);
            if (user != null && Password != null)
            {
                var Hasher = new PasswordHasher<User>();
                var result = Hasher.VerifyHashedPassword(user, user.Password, Password);
                if (result != 0)
                {
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    HttpContext.Session.SetString("CurrUser", user.FirstName);
                    return RedirectToAction("Home");
                }
            }
            return View("Index");
        }

        [HttpGet]
        [Route("Home")]
        public IActionResult Home()
        {
            ViewBag.SessionId = HttpContext.Session.GetInt32("UserId");
            List<Activity> AllActivities = _context.Activities.Include(p => p.User).Include(s => s.Participants).ToList();
            ViewBag.AllActivities = AllActivities;
            ViewBag.CurrUser = HttpContext.Session.GetString("CurrUser");
            // ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            return View();
        }

        [HttpGet]
        [Route("new")]
        public IActionResult New()
        {
            if(HttpContext.Session.GetInt32("UserId") == null){
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        [Route("new")]
        public IActionResult Add(Activity newActivity)
        {
            if(HttpContext.Session.GetInt32("UserId") == null){
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                Activity NewActivity = new Activity {
                Title = newActivity.Title,
                Time = newActivity.Time,
                Date = newActivity.Date,
                Duration = newActivity.Duration,
                DurType = newActivity.DurType,
                Desc = newActivity.Desc,
                User = _context.Users.SingleOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"))
                };
                _context.Activities.Add(NewActivity);
                _context.SaveChanges();
                return RedirectToAction("Home");
            }
            else
            {
                return View("New");
            }
        }

        [HttpGet]
        [Route("activity/{id}")]
        public IActionResult Display(int id)
        {
            Activity ThisActivity = _context.Activities.Include(z => z.Participants).ThenInclude(p => p.User).SingleOrDefault(p => p.ActivityId == id);
            // ViewBag.ThisActivity = ThisActivity;
            ViewBag.CurrUser = HttpContext.Session.GetString("CurrUser");
            return View();
        }

        [HttpGet]
        [Route("join/{id}")]
        public IActionResult Join(int id)
        {
            User ThisUser = _context.Users.SingleOrDefault(p => p.UserId == HttpContext.Session.GetInt32("UserId"));
            Activity OneActivity = _context.Activities.SingleOrDefault(p => p.ActivityId == id);
            Participant join = new Participant {
                UserId = ThisUser.UserId,
                User = ThisUser,
                ActivityId = id,
                Activity = OneActivity,
            };
            _context.Participants.Add(join);
            _context.SaveChanges();
            return RedirectToAction("Home");
        }

        [HttpPost]
        [Route("leave/{id}")]
        public IActionResult Leave(int id)
        {
            Participant CurrUserParticipant = _context.Participants.SingleOrDefault(s => s.ParticipantId == id);
            _context.Participants.Remove(CurrUserParticipant);
            _context.SaveChanges();
            return RedirectToAction("Home");
        }

        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            Activity SingleActivity = _context.Activities.SingleOrDefault(s => s.ActivityId == id);
            _context.Activities.Remove(SingleActivity);
            _context.SaveChanges();
            return RedirectToAction("Home");
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}