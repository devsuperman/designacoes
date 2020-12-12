using Microsoft.EntityFrameworkCore;
using App.Models;

namespace App.Data
{
    public class Contexto : DbContext
    {         
        public Contexto(DbContextOptions<Contexto> options): base(options)
        {
            
        }   
        public DbSet<Designacao> Designacoes {get;set;}                 
    
    }
}