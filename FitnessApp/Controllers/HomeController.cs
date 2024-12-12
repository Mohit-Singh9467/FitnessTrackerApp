using FitnessApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FitnessApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private User user;
        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                user = _context.Users.Include(x => x.Goals).Include(x => x.Workout).FirstOrDefault(x => x.Name == (HttpContext.Session.GetString("Username"))); ;


                var goals = _context.Goals.ToList();
                return View(goals);
            }



            else
                return View("../Auth/Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult AddGoal()
        {

            return View();  // Pass userId to the view to associate with the goal

        }
       
        public ActionResult Details(int id)
        {
            var goal = _context.Goals.Include("Workouts.Workout").FirstOrDefault(g => g.GoalId == id);
            if (goal == null)
                return HttpNotFound();
            return View(goal);
        }

        // GET: Goals/Create
        public ActionResult Create()
        {
            ViewBag.Workouts = _context.Workouts.ToList();
            return View();
        }

        // POST: Goals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Goal goal, int[] workoutIds, int[] repetitions, int[] sets)
        {
            if (ModelState.IsValid)
            {
                if (workoutIds != null)
                {
                    for (int i = 0; i < workoutIds.Length; i++)
                    {
                        goal.Workouts.Add(new GoalWorkout
                        {
                            WorkoutId = workoutIds[i],
                            Repetitions = repetitions[i],
                            Sets = sets[i]
                        });
                    }
                }
                _context.Goals.Add(goal);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Workouts = _context.Workouts.ToList();
            return View(goal);
        }


        public ActionResult Edit(int id)
        {
            var goal = _context.Goals
                .Include("Workouts.Workout") // Include associated workouts
                .FirstOrDefault(g => g.GoalId == id);

            if (goal == null)
            {
                return HttpNotFound();
            }

            ViewBag.Workouts = _context.Workouts.ToList(); // Populate the available workouts
            return View(goal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Goal goal, int[] workoutIds, int[] repetitions, int[] sets)
        {
            if (ModelState.IsValid)
            {
                var existingGoal = _context.Goals
                    .Include(g => g.Workouts)
                    .FirstOrDefault(g => g.GoalId == goal.GoalId);

                if (existingGoal == null)
                {
                    return HttpNotFound();
                }

                // Update goal properties
                existingGoal.Name = goal.Name;

                // Clear existing workouts and re-add them
                existingGoal.Workouts.Clear();
                if (workoutIds != null)
                {
                    for (int i = 0; i < workoutIds.Length; i++)
                    {
                        existingGoal.Workouts.Add(new GoalWorkout
                        {
                            WorkoutId = workoutIds[i],
                            Repetitions = repetitions[i],
                            Sets = sets[i]
                        });
                    }
                }

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Workouts = _context.Workouts.ToList();
            return View(goal);
        }
        public ActionResult Delete(int id)
        {
            var goal = _context.Goals.Find(id);
            if (goal == null)
                return HttpNotFound();
            return View(goal);
        }

        private ActionResult HttpNotFound()
        {
            return null;
        }

        // POST: Goals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var goal = _context.Goals.Find(id);
            if (goal == null)
                return HttpNotFound();

            _context.Goals.Remove(goal);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}