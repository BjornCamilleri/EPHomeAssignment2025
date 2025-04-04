﻿using DataAccess.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class PollController : Controller
    {

        [HttpGet]
        public IActionResult Index([FromServices] PollRepository pollRepository)
        {
            var polls = pollRepository.GetPolls().OrderByDescending(p => p.DateCreated);
            return View(polls);
        }

        [HttpGet]
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
                return RedirectToAction("Index");
            }

            return View(poll);
        }



    }
}
