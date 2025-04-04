using Domain.Interfaces;
using Domain.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace DataAccess.Repositories
{
    public class PollFileRepository : IPollRepository
    {
        private readonly string filePath = "PollDetailsJSONFile.json";

        public void CreatePoll(Poll poll)
        {
            var polls = GetPolls().ToList();
            poll.Id = polls.Any() ? polls.Max(p => p.Id) + 1 : 1;
            polls.Add(poll);
            SavePollsToFile(polls);
        }

        public IQueryable<Poll> GetPolls()
        {
            if (!File.Exists(filePath))
                return new List<Poll>().AsQueryable();

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Poll>>(json)?.AsQueryable() ?? new List<Poll>().AsQueryable();
        }

        public Poll GetPollById(int id)
        {
            return GetPolls().FirstOrDefault(p => p.Id == id);
        }

        public void Vote(int pollId, int option)
        {
            var polls = GetPolls().ToList();
            var poll = polls.FirstOrDefault(p => p.Id == pollId);

            if (poll == null) return;

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

            SavePollsToFile(polls);
        }

        private void SavePollsToFile(IEnumerable<Poll> polls)
        {
            var json = JsonSerializer.Serialize(polls, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}
