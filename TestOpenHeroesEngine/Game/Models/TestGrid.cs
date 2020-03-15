using NUnit.Framework;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.Game.Models;

namespace TestOpenHeroesEngine.Game.Models
{
    public class TestGrid
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestSimpleGridPropertiesAndIndexMath()
        {
            var grid = new Grid(4, 4);
            Assert.AreEqual(16, grid.Size);
            Assert.AreEqual(4, grid.Width);
            Assert.AreEqual(4, grid.Height);
            Assert.IsFalse(grid.InverseIndexDimension);
            
            Assert.AreEqual(0, grid.GetNodeIndex(0, 0));
            Assert.AreEqual(1, grid.GetNodeIndex(0, 1));
            Assert.AreEqual(2, grid.GetNodeIndex(0, 2));
            Assert.AreEqual(3, grid.GetNodeIndex(0, 3));
            Assert.AreEqual(4, grid.GetNodeIndex(1, 0));
            Assert.AreEqual(5, grid.GetNodeIndex(1, 1));
            Assert.AreEqual(6, grid.GetNodeIndex(1, 2));
            Assert.AreEqual(7, grid.GetNodeIndex(1, 3));
            Assert.AreEqual(8, grid.GetNodeIndex(2, 0));
            Assert.AreEqual(9, grid.GetNodeIndex(2, 1));
            Assert.AreEqual(10, grid.GetNodeIndex(2, 2));
            Assert.AreEqual(11, grid.GetNodeIndex(2, 3));
            Assert.AreEqual(12, grid.GetNodeIndex(3, 0));
            Assert.AreEqual(13, grid.GetNodeIndex(3, 1));
            Assert.AreEqual(14, grid.GetNodeIndex(3, 2));
            Assert.AreEqual(15, grid.GetNodeIndex(3, 3));

            Assert.AreEqual(new Point(0, 0), grid.GetPositionIndex(0));
            Assert.AreEqual(new Point(0, 2), grid.GetPositionIndex(2));
            Assert.AreEqual(new Point(1, 3), grid.GetPositionIndex(7));
            Assert.AreEqual(new Point(3, 1), grid.GetPositionIndex(13));
            Assert.AreEqual(new Point(3, 3), grid.GetPositionIndex(15));
        }
        
        [Test]
        public void TestSimpleInverseGridPropertiesAndIndexMath()
        {
            var grid = new Grid(2, 4);
            Assert.AreEqual(8, grid.Size);
            Assert.AreEqual(2, grid.Width);
            Assert.AreEqual(4, grid.Height);
            Assert.IsTrue(grid.InverseIndexDimension);
            
            Assert.AreEqual(0, grid.GetNodeIndex(0, 0));
            Assert.AreEqual(4, grid.GetNodeIndex(0, 1));
            Assert.AreEqual(8, grid.GetNodeIndex(0, 2));
            Assert.AreEqual(12, grid.GetNodeIndex(0, 3));
            Assert.AreEqual(1, grid.GetNodeIndex(1, 0));
            Assert.AreEqual(5, grid.GetNodeIndex(1, 1));
            Assert.AreEqual(9, grid.GetNodeIndex(1, 2));
            Assert.AreEqual(13, grid.GetNodeIndex(1, 3));
            
            Assert.AreEqual(new Point(0, 0), grid.GetPositionIndex(0));
            Assert.AreEqual(new Point(0, 2), grid.GetPositionIndex(8));
            Assert.AreEqual(new Point(1, 3), grid.GetPositionIndex(13));
            Assert.AreEqual(new Point(1, 1), grid.GetPositionIndex(5));
            Assert.AreEqual(new Point(1, 3), grid.GetPositionIndex(13));
        }
    }
}