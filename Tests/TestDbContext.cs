using System.Data.Entity;

using EntityRepository.Models;

namespace EntityRepository.Tests
{
    public class Item : Entity
    {
        public string Name { get; set; }
    }

    public class TrackedItem : TrackedEntity
    {
        public string Name { get; set; }
    }

    public class TestDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }

        public DbSet<TrackedItem> TrackedItems { get; set; }

        static TestDbContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TestDbContext>());
        }
    }
}