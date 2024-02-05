using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Management.Data.Data;

namespace User.Management.Service.Repositories.FeedbackRepository
{
    public interface IFeedbackRepository
    {
        Task AddAsync(Feedback feedback);
        Task DeleteAsync(Guid feedbackId);
        Task<IEnumerable<Feedback>> GetAllFeedbacksByUserAsync(string userName);
    }
}
