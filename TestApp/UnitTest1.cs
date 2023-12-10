using Kr3G;

namespace TestApp
{
    [TestClass]
    public class UnitTest1
    {
        Calculator calculator = new Calculator();

        DataHolder<double> test1Data = new DataHolder<double>(1, 5, 1, 1,10);

        double[] yAnswer1 = { 9.9005049366383275, 9.7508957550357618, 9.4963065604973789,9.1279510095023859,8.6312661997651823,
            -8.6312661997651823,-9.1279510095023859,-9.4963065604973789,-9.7508957550357618,-9.9005049366383275 };
        double[] xAnswer1 = { 1, 2, 3, 4, 5,5,4,3,2,1 };



        DataHolder<double> test2Data = new DataHolder<double>(1, 3, 1, 1,3);

        double[] yAnswer2 = { 2.6869209994513956, 2.2020122165410672, 0.9036890097771294, -0.9036890097771294,-2.2020122165410672,-2.6869209994513956 };
        double[] xAnswer2 = { 1, 2, 3,3,2,1 };

        DataHolder<double> test3Data = new DataHolder<double>(1, 2, 1, 1,4);

        double[] yAnswer3 = { 3.7582596366665646, 3.3900475664023717, -3.3900475664023717,-3.7582596366665646 };
        double[] xAnswer3 = { 1, 2, 2,1 };
        [TestMethod]
        public void TestMethod1()
        {
            GraphData graphData =  calculator.GetGraphData(test1Data);
          
           
            Assert.IsTrue(Enumerable.SequenceEqual(graphData.xs,xAnswer1) && Enumerable.SequenceEqual(graphData.ys, yAnswer1));
        }

        [TestMethod]
        public void TestMethod2()
        {
            GraphData graphData = calculator.GetGraphData(test2Data);

            Assert.IsTrue(Enumerable.SequenceEqual(graphData.xs, xAnswer2) && Enumerable.SequenceEqual(graphData.ys, yAnswer2));
        }

        [TestMethod]
        public void TestMethod3()
        {
            GraphData graphData = calculator.GetGraphData(test3Data);

            Assert.IsTrue(Enumerable.SequenceEqual(graphData.xs, xAnswer3) && Enumerable.SequenceEqual(graphData.ys, yAnswer3));
        }
    }
}