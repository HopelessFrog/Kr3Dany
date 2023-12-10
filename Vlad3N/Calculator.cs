using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kr3G
{
    public interface ICalculator
    {
        GraphData GetGraphData(DataHolder<double> dataHolder);

    }

    public class Calculator : ICalculator
    {
        public GraphData GetGraphData(DataHolder<double> dataHolder)
        {
            List<double> xs = new List<double>();
            List<double> ys = new List<double>();

  

            for (double x = dataHolder.LeftBorder; x <= dataHolder.RightBorder; x+=dataHolder.Step)
            {
                xs.Add(x);

                ys.Add(Math.Sqrt(Math.Sqrt(Math.Pow(dataHolder.A,4) +
                                           4 * Math.Pow(dataHolder.C,2) * 
                                           Math.Pow(x, 2)) - Math.Pow(x, 2) - Math.Pow(dataHolder.C, 2)));
            }
            for (double x = dataHolder.RightBorder; x >= dataHolder.LeftBorder; x -= dataHolder.Step)
            {
                xs.Add(x);

                ys.Add(-1* Math.Sqrt(Math.Sqrt(Math.Pow(dataHolder.A, 4) +
                                           4 * Math.Pow(dataHolder.C, 2) *
                                           Math.Pow(x, 2)) - Math.Pow(x, 2) - Math.Pow(dataHolder.C, 2)));
            }
            return new GraphData(xs.ToArray(), ys.ToArray());
        }

       
    }
}
