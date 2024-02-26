using Core.DataAccess.EntityFramework;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntityFramework.Contexts;
using Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes.Repositories
{
    public class BlacklistRepository : EfRepositoryBase<Blacklist, int, BaseDbContext>, IBlacklistRepository
    {
        public BlacklistRepository(BaseDbContext context) : base(context)
        {
        }
        public async Task<bool> IsApplicantBlacklistedAsync(int applicantId)
        {
            return await Context.Set<Blacklist>().AnyAsync(b => b.ApplicantId == applicantId);
        }
    }
}
