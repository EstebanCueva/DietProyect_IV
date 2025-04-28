using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DietProyect_IV.Models;

namespace DietProyect_IV.Data
{
    public class DietProyect_IVContext : DbContext
    {
        public DietProyect_IVContext(DbContextOptions<DietProyect_IVContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; } = default!;
        public DbSet<NivelActividad> NivelActividades { get; set; } = default!;
        public DbSet<CalculoCalorias> CalculoCalorias { get; set; } = default!;
        public DbSet<ObjetivoCalorico> ObjetivosCaloricos { get; set; } = default!;
    }
}
