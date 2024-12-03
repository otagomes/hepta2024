using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Painel.Models;

namespace Painel.Data
{
    public class ctxContext : DbContext
    {
        public ctxContext (DbContextOptions<ctxContext> options)
            : base(options)
        {
        }
    }
}
