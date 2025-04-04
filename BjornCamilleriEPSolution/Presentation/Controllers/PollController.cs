using DataAccess.Repositories;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class PollController : Controller
    {
        [HttpGet]
        public IActionResult Index([FromServices] IPollRepository pollRepository)
        {
            var polls = pollRepository.GetPolls().OrderByDescending(p => p.DateCreated);
            return View(polls);
        }

        [HttpGet]
        public IActionResult CreatePoll()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePoll(Poll poll, [FromServices] IPollRepository pollRepository)
        {
            if (ModelState.IsValid)
            {
                poll.Option1VotesCount = 0;
                poll.Option2VotesCount = 0;
                poll.Option3VotesCount = 0;

                poll.DateCreated = DateTime.Now;

                pollRepository.CreatePoll(poll);
                return RedirectToAction("Index");
            }
            return View(poll);
        }

        [HttpGet]
        public IActionResult Details(int id, [FromServices] IPollRepository pollRepository)
        {
            var poll = pollRepository.GetPollById(id);
            if (poll == null)
                return NotFound();
            return View(poll);
        }

        [HttpPost]
        public IActionResult Vote(int id, int option, [FromServices] IPollRepository pollRepository)
        {
            pollRepository.Vote(id, option);
            return RedirectToAction("Details", new { id });
        }






    }
}
