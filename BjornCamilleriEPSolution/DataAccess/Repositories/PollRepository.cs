using DataAccess.DataContext;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;

namespace DataAccess.Repositories
{
    public class PollRepository : IPollRepository
    {
        private readonly PollDbContext context;

        //Constructor Injection
        public PollRepository(PollDbContext _context)
        {
            context = _context;
        }

        public void CreatePoll(Poll poll)
        {
            context.Polls.Add(poll);
            context.SaveChanges();
        }

        public IQueryable<Poll> GetPolls()
        {
            return context.Polls;
        }

        public Poll GetPollById(int id)
        {
            return context.Polls.FirstOrDefault(p => p.Id == id);
        }

        public void Vote(int pollId, int option, string userId)
        {
            var poll = GetPollById(pollId);

            if (poll == null)
                return;

            if (poll.UserIds.Contains(userId))
                return;

            switch (option)
            {
                case 1:
                    poll.Option1VotesCount++;
                    break;
                case 2:
                    poll.Option2VotesCount++;
                    break;
                case 3:
                    poll.Option3VotesCount++;
                    break;
            }

            poll.UserIds.Add(userId);
            context.SaveChanges();
        }


        public bool CheckUserVote(int pollId, string userId)
        {

            var poll = context.Polls.FirstOrDefault(p => p.Id == pollId);
            return poll != null && poll.UserIds.Contains(userId);

        }





    }
}
