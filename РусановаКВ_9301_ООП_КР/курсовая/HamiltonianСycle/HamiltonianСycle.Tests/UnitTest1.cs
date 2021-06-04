using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HamiltonianСycle.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Graph_nullMatrix()
        {
            try
            {
                int[,] matrix = null;
                Graph g = new Graph(matrix);
            }
            catch (System.Exception e)
            {
                Assert.IsTrue(e is System.ArgumentNullException);
            }
        }
        [TestMethod]
        public void Graph_bigMatrix()
        {
            try
            {
                int[,] matrix = new int[20,20];
                for (int i = 0; i < 20; i++)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        matrix[i, j] = i * j;
                    }
                }
                Graph g = new Graph(matrix);
            }
            catch (System.Exception e)
            {
                Assert.IsTrue(e is System.ArgumentOutOfRangeException);
            }
        }
        [TestMethod]
        public void Graph_correctTest_1()
        {
            try
            {
                int[,] matrix = new int[5, 5] 
                { { 0, 0, 1, 0, 1 },
                { 1, 0, 1, 1, 1 },
                { 1, 1 ,0 ,1, 0},
                { 0, 1, 1, 0, 1},
                { 1, 0, 0 ,0, 0} };
                Graph g = new Graph(matrix);
                Assert.AreEqual(g.GamAlg(),"13402");
            }
            catch (System.Exception e)
            {
                throw;
            }
        }
        [TestMethod]
        public void Graph_correctTest_2()
        {
            try
            {
                int[,] matrix = new int[5, 5]
                { { 0, 1, 1, 1, 1 },
                { 0, 0, 1, 0, 1 },
                { 1, 1, 0, 1, 0},
                {1, 0, 1, 0, 1},
                { 0, 1, 0, 1, 0} };
                Graph g = new Graph(matrix);
                Assert.AreEqual(g.GamAlg(), "21430");
            }
            catch (System.Exception e)
            {
                throw;
            }
        }
    }
}
