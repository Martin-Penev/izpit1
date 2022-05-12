using BusinessLayer;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace TestingLayer
{
    public class Tests
    {
        private HobbyDbContext _dbContext;
        private AreaContext _AreaContext;
        DbContextOptionsBuilder builder;

        [SetUp]
        public void Setup()
        {
            builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _dbContext = new HobbyDbContext(builder.Options);
            _AreaContext = new AreaContext(_dbContext);
        }

        [Test]
        public void CreateArea()
        {
            int areaBefore = _AreaContext.ReadAll().Count();
            _AreaContext.Create(new Area("Ronaldo"));
            int areaAfter = _AreaContext.ReadAll().Count();
            Assert.IsTrue(areaBefore != areaAfter);
        }
        [Test]
        public void ReadArea()
        {
            _AreaContext.Create(new Area("Ronaldo"));
            Area Area = _AreaContext.Read(1);
            Assert.That(Area != null, "There's no record with id 1");
        }
        [Test]
        public void UpdateArea()
        {
            _AreaContext.Create(new Area("Messi"));
            Area Area = _AreaContext.Read(1);
            Area.Name = "Ronaldo";
            _AreaContext.Update(Area);
            Area AreaAlt = _AreaContext.Read(1);
            Assert.IsTrue(AreaAlt.Name == "Ronaldo", "Update() doesn't change anything!");
        }
        [Test]
        public void DeleteArea()
        {
            _AreaContext.Create(new Area("Delete area"));
            int areaBeforeDel = _AreaContext.ReadAll().Count();
            _AreaContext.Delete(1);
            int areaAfterDel = _AreaContext.ReadAll().Count();
            Assert.AreNotEqual(areaBeforeDel, areaAfterDel);
        }
    }


}
