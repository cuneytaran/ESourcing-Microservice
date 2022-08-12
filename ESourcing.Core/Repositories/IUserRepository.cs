using ESourcing.Core.Entities;
using ESourcing.Core.Repositories.Base;

namespace ESourcing.Core.Repositories
{
    public interface IUserRepository : IRepository<AppUser>//IRepository den türe ve AppUser üzerinden türe
    {
    }
}
