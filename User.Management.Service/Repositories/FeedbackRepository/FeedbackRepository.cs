using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashFood.Data;
using Microsoft.EntityFrameworkCore;
using User.Management.Data.Data;

namespace User.Management.Service.Repositories.FeedbackRepository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly ApplicationDbContext _context;

        public FeedbackRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid feedbackId)
        {
            var feedback = await _context.Feedbacks.FindAsync(feedbackId);
            if (feedback != null)
            {
                _context.Feedbacks.Remove(feedback);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Feedback>> GetAllFeedbacksByUserAsync(string userName)
        {
            return await _context.Feedbacks
                                 .Where(f => f.UserName == userName)
                                 .ToListAsync();
        }
    }
}
