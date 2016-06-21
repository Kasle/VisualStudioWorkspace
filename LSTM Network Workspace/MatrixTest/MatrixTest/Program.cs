using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace LSTMNetworkTest
{
    class Program
    {
        public static double[] MatrixMultiply1D(double[] value1, double[] value2)
        {
            double[] temp = value1.Select((x, index) => x * value2[index]).ToArray();
            return temp;
        }

        public static double[] MatirxAdd1D(double[] value1, double[] value2)
        {
            double[] temp = value1.Select((x, index) => x + value2[index]).ToArray();
            return temp;
        }

        static void Main(string[] args)
        { 
            int[] networkSize = { 1, 2, 2, 1 }; //Input, Hidden Size, Hidden Amount, Output

            double[] a = new double[] { 1, 1 };
            double[] b = a;
            a[0] = 0;
            Console.WriteLine(a[0]);

            int outputOffset = 0; //An offset for more processing layers

            double[] inputArray = new double[networkSize[0]]; //Input values
            double[] outputArray = new double[networkSize[3]]; //Final output values

            double[,,] nodeArray = new double[networkSize[2], networkSize[1],2]; //Array to store sum and activation

            double[,,] inputWeightArray = new double[networkSize[0], networkSize[1], networkSize[1]]; //Weight array for abnormal input
            double[,,] outputWeightArray = new double[networkSize[1], networkSize[4], networkSize[1]]; //Weight array for abnormal output

            double[,,,] weightArray = new double[networkSize[2] - 1, networkSize[1], networkSize[1], networkSize[1]]; //Regular Weight Arrays

            Random r = new Random();
            for (int layer = 0; layer < networkSize[2]; layer++)
            {
                Console.Write(layer.ToString());
                for (int node = 0; node < networkSize[1]; node++)
                {
                    Console.Write(" " + nodeArray[layer, node, 0].ToString());
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
