using CinemaNS.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaNS.Abstract
{
    public abstract class DefaultEntityManager
    {
        public DefaultEntityManager(ApplicationDbContext context)
        {
            Context = context;
        }

        protected ApplicationDbContext Context { get; }
    }
}
