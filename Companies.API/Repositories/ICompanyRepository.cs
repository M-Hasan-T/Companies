using Companies.API.Entities;

namespace Companies.API.Repositories
{
    public interface ICompanyRepository
    {
        Task<List<Company>> GetAsync(bool includeEmployees = false);
        Task<Company?> GetAsync(Guid id);
    }
}