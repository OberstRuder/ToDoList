using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ToDo.Models;

namespace ToDo.Controllers
{
    public class HomeController : Controller
    {
        private ToDoDbContext context;

        public HomeController(ToDoDbContext dbContext)
        {
            context = dbContext;
        }
        public IActionResult Index(string id)
        {
            var filters = new Filters(id);
            ViewBag.Filters = filters;
            ViewBag.Categories = context.Categories.ToList();
            ViewBag.Statuses = context.Statuses.ToList();
            ViewBag.Users = context.Users.ToList();
            ViewBag.DueFilters = Filters.DueFilterValues;

            IQueryable<Models.Todo> query = context.ToDos
                .Include(t => t.Category)
                .Include(t => t.Status)
                .Include(t => t.User);

            if (filters.HasCategory) 
            { 
                query = query.Where(t => t.CategoryId == filters.CategoryId);
            }

            if (filters.HasStatus)
            {
                query = query.Where(t => t.StatusId == filters.StatusId);
            }

            if (filters.HasDue)
            {
                var today = DateTime.Today;
                if (filters.IsPast)
                {
                    query = query.Where(t => t.DueDate < today);
                }
                else if (filters.IsFuture)
                {
                    query = query.Where(t => t.DueDate > today);
                }
                else if (filters.IsToday)
                {
                    query = query.Where(t => t.DueDate == today);
                }
            }

            if (filters.HasUser)
            {
                query = query.Where(t => t.UserId == filters.UserId);
            }

            var tasks = query.OrderBy(t => t.DueDate).ToList();

            return View(tasks);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Categories = context.Categories.ToList();
            ViewBag.Statuses = context.Statuses.ToList();
            ViewBag.Users = context.Users.ToList();
            var task = new Todo { StatusId = "open" };
            return View(task);
        }

        [HttpPost]
        public IActionResult Add(Todo task)
        {
            if (ModelState.IsValid)
            {
                context.ToDos.Add(task);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Categories = context.Categories.ToList();
                ViewBag.Statuses = context.Statuses.ToList();
                ViewBag.Users = context.Users.ToList();
                return View(task);
            }
        }

        [HttpPost]
        public IActionResult Filter(string[] filter)
        {
            return RedirectToAction("Index", new { id = string.Join('-', filter) });
        }

        [HttpPost]
        public IActionResult MarkComplete([FromRoute] string id, Todo selected)
        {
            selected = context.ToDos.Find(selected.Id)!;

            if (selected != null)
            {
                selected.StatusId = "closed";
                context.SaveChanges();
            }
            return RedirectToAction("Index", new { ID = id });
        }

        [HttpPost]
        public IActionResult DeleteComplete(string id)
        {
            var toDelete = context.ToDos.Where(t => t.StatusId == "closed").ToList();
            foreach (var todo in toDelete)
            {
                context.ToDos.Remove(todo);
            }
            context.SaveChanges();

            return RedirectToAction("Index", new { ID = id });
        }
    }
}