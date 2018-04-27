using LZ.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace LZ.Data.OracleEF
{
    public class DbContextFactory
    {
        //单例DbContext
        public static DbContext GetCurrentDbContext()
        {
            Entities dbContext = CallContext.GetData("Entities") as Entities;
            if (dbContext == null)
            {
                dbContext = new Entities();
                CallContext.SetData("Entities", dbContext);
            }
            return dbContext;
        }
    }
}
