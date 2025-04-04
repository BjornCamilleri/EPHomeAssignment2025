﻿using DataAccess.DataContext;
using Domain.Interfaces;
using Domain.Models;
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

        public void Vote(int pollId, int option)
        {
            var poll = GetPollById(pollId);

            if (poll == null)
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

            context.SaveChanges();
        }





    }
}
