using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal2.Data;
using StudentPortal2.Models.Entities;

namespace StudentPortal2.Controllers
{
    public class StudentsController : Controller
    {
        
        private readonly ApplicationDbContext dbContext;

        public StudentsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Student viewModel)
        {
            

            var student = new Student
            {
                
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Subscribed = viewModel.Subscribed,

            };

           await dbContext.Students.AddAsync(student);
           await dbContext.SaveChangesAsync();

         return RedirectToAction("List", "Students");

        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var student = await dbContext.Students.ToListAsync();
            return View(student);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid Id)
        {
            var student = await dbContext.Students.FindAsync(Id);
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            var student = await dbContext.Students.FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            if (student is not null)
            {
                student.Name = viewModel.Name;
                student.Email = viewModel.Email;
                student.Subscribed = viewModel.Subscribed;
                student.Phone = viewModel.Phone;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Students");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var student = await dbContext.Students.FirstOrDefaultAsync(x => x.Id == Id );

            if (student is not null)
            {
                 dbContext.Students.Remove(student);
                await dbContext.SaveChangesAsync();
              
            }
            return RedirectToAction("List", "Students");
        }

    }
}
