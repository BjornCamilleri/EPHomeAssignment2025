using DataAccess.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class PollController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreatePoll()
        {
            return View();
        }

        //Method Injection
        [HttpPost]
        public IActionResult CreatePoll(Poll poll, [FromServices] PollRepository pollRepository)
        {
            if (ModelState.IsValid)
            {
                poll.Option1VotesCount = 0;
                poll.Option2VotesCount = 0;
                poll.Option3VotesCount = 0;

                poll.DateCreated = DateTime.Now;

                pollRepository.CreatePoll(poll);
                return View();
            }

            return View(poll);
        }



    }
}
