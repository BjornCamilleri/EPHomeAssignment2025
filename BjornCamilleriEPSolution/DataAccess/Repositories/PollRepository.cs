using DataAccess.DataContext;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class PollRepository
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

    }
}
