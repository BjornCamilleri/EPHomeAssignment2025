using Domain.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace DataAccess.Repositories
{
    public class PollFileRepository
    {
        private readonly string filePath = "PollDetailsJSONFile.json";

        public void CreatePoll(Poll poll)
        {
            var polls = GetPolls().ToList();
            poll.Id = polls.Any() ? polls.Max(p => p.Id) + 1 : 1;
            polls.Add(poll);
            SavePollsToFile(polls);
        }

        public IEnumerable<Poll> GetPolls()
        {
            if (!File.Exists(filePath))
                return new List<Poll>();

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Poll>>(json) ?? new List<Poll>();
        }

        private void SavePollsToFile(IEnumerable<Poll> polls)
        {
            var json = JsonSerializer.Serialize(polls, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}
