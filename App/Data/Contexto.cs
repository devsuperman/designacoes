using Microsoft.EntityFrameworkCore;

namespace App.Data
{
    public class Contexto : DbContext
    {         
        public Contexto(DbContextOptions<Contexto> options): base(options)
        {
            
        }             
    
    }
}