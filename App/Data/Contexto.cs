using Microsoft.EntityFrameworkCore;
using App.Models;

namespace App.Data
{
    public class Contexto : DbContext
    {         
        public Contexto(DbContextOptions<Contexto> options): base(options)
        {
            
        }   
        public DbSet<Designacao> designacoes {get;set;}                 
        public DbSet<Publicador> publicadores { get; set; }
    
    }
}