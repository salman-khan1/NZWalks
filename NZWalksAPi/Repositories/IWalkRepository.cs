using NZWalksAPi.Controllers;
using NZWalksAPi.Models.Domain;

namespace NZWalksAPi.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
    }
}
