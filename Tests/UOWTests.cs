using Microsoft.VisualStudio.TestTools.UnitTesting;

using EntityRepository.Components;

namespace EntityRepository.Tests
{
    [TestClass]
    public class UowTests
    {
        [TestMethod]
        public void UOW_AddItem()
        {
            // arrange
            var item = new Item { Name = "item 1" };
            var uow = new UnitOfWork<TestDbContext>();
            var items = uow.GetRepository<Item>();

            // act
            items.Add(item);
            var count = uow.Save();

            // assert
            Assert.AreEqual(1, count);
        }

        [TestMethod]
        public void UOW_AddTrackedItem()
        {
            // arrange
            var item = new TrackedItem { Name = "track 1" };
            var uow = new UnitOfWork<TestDbContext>();
            var items = uow.GetRepository<TrackedItem>();

            // act
            items.Add(item);
            var count = uow.Save();

            // assert
            Assert.AreEqual(1, count);
            Assert.AreEqual(UserContext.Current.UserName, item.CreatedBy);
        }

        [TestMethod]
        public void UOW_UpdateItem()
        {
            // arrange
            var item = new Item { Name = "item 1" };

            using (var uow = new UnitOfWork<TestDbContext>())
            {
                uow.GetRepository<Item>().Add(item);
                uow.Save();
            }

            // act
            using (var uow = new UnitOfWork<TestDbContext>())
            {
                item.Name = "item 2";
                uow.GetRepository<Item>().Update(item);
                uow.Save();
            }

            // assert
            using (var uow = new UnitOfWork<TestDbContext>())
            {
                var x = uow.GetRepository<Item>().Get(item.Id);
                Assert.AreEqual("item 2", x.Name);
            }
        }

        [TestMethod]
        public void UOW_UpdateTrackedItem()
        {
            // arrange
            var item = new TrackedItem { Name = "track 1" };

            using (var uow = new UnitOfWork<TestDbContext>())
            {
                uow.GetRepository<TrackedItem>().Add(item);
                uow.Save();
            }

            // act
            using (var uow = new UnitOfWork<TestDbContext>())
            {
                item.Name = "track 2";
                uow.GetRepository<TrackedItem>().Update(item);
                uow.Save();
            }

            // assert
            using (var uow = new UnitOfWork<TestDbContext>())
            {
                var x = uow.GetRepository<TrackedItem>().Get(item.Id);
                Assert.AreEqual("track 2", x.Name);
                Assert.AreEqual(UserContext.Current.UserName, x.ModifiedBy);
            }
        }

        [TestMethod]
        public void UOW_DeleteItem()
        {
            // arrange
            var item = new TrackedItem { Name = "item 3" };

            using (var uow = new UnitOfWork<TestDbContext>())
            {
                uow.GetRepository<TrackedItem>().Add(item);
                uow.Save();
            }

            // act
            using (var uow = new UnitOfWork<TestDbContext>())
            {
                uow.GetRepository<TrackedItem>().Delete(item);
                uow.Save();
            }

            // assert
            using (var uow = new UnitOfWork<TestDbContext>())
            {
                var x = uow.GetRepository<Item>().Get(item.Id);
                Assert.IsNull(x);
            }
        }

        [TestMethod]
        public void UOW_DeleteTrackedItem()
        {
            // arrange
            var item = new TrackedItem { Name = "track 3" };

            using (var uow = new UnitOfWork<TestDbContext>())
            {
                uow.GetRepository<TrackedItem>().Add(item);
                uow.Save();
            }

            // act
            using (var uow = new UnitOfWork<TestDbContext>())
            {
                uow.GetRepository<TrackedItem>().Delete(item);
                uow.Save();
            }

            // assert
            using (var uow = new UnitOfWork<TestDbContext>())
            {
                var x = uow.GetRepository<TrackedItem>().Get(item.Id);
                Assert.IsTrue(x.IsDeleted);
                Assert.AreEqual(UserContext.Current.UserName, x.ModifiedBy);
            }
        }
    }
}