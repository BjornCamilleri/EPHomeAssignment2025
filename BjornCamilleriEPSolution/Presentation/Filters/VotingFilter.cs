using DataAccess.Repositories;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

namespace Presentation.Filters
{
    public class VotingFilter : IActionFilter
    {
        private readonly IPollRepository _pollRepository;

        // Constructor Injection
        public VotingFilter(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue("id", out var pollIdObj) &&
                context.HttpContext.User.Identity?.IsAuthenticated == true)
            {
                int pollId = (int)pollIdObj;
                string userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (_pollRepository.CheckUserVote(pollId, userId))
                {
                    var controller = (Controller)context.Controller;
                    controller.TempData["AlreadyVoted"] = "You have already voted in this poll.";
                    context.Result = new RedirectToActionResult("Details", "Poll", new { id = pollId });
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
