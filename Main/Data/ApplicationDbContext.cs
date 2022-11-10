using Main.Models;
using Microsoft.EntityFrameworkCore;

namespace Main.Data
{
    public class ApplicationDbContext: DbContext
    {
        // 對應資料庫Table名稱
        public DbSet<Villa> Villas { get; set; }
    }
}
