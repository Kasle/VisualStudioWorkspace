﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace LSTM_Network_Workspace
{
    class Program
    {
        static void Main(string[] args) // Main ----------------------------------------------------------------------------------------------------
        {
            List<Cage> cageList = new List<Cage>();
            for (int i = 1; i < 500; i++)
            {
                cageList.Add(new Cage(i));
            }
            
            while (true) //Runtime
            {
                Console.WriteLine("Data:\n");
                for (int i = 0;i < cageList.Count; i++)
                {
                    Console.WriteLine(cageList[i].data);//Update graph
                }
                //5second delay
            }
            /*int netSize = 1000;
            Network net = new Network(new int[] { netSize, 1, 5 }); //Word Count, Hidden Size (Given Value + 2), Depth
            Console.WriteLine("Start.");
            double averageMSec = 0;
            net.Forward(new double[netSize]);
            double weightAmount = net.weightArray.Length + net.nodeArray.Length;
            for (int test = 0; test < 10000; test++) {
                double[] input = new double[netSize];
                Random nR = new Random();
                for (int eachInput = 0; eachInput < netSize; eachInput++)
                {
                    input[eachInput] = nR.Next(-100, 100) / 100.0;
                }
                Stopwatch timer = new Stopwatch();
                timer.Start();
                net.Forward(input);
                averageMSec = ((averageMSec * test) + timer.Elapsed.TotalMilliseconds) / (test + 1.0);
                double nps = weightAmount / averageMSec;
                Console.WriteLine("{0}, {1}, {2}", nps, ((averageMSec * 1000) / 1000) / 60, (averageMSec / weightAmount) * 1000);
            }
            /*double[] netOut = net.Forward(input);
            foreach (double output in netOut)
            {
                Console.WriteLine(Math.Round(output));
            }
            net.Print();*/
            Console.WriteLine("End.");
            Console.ReadLine();
        }
    }

    public class Network
    {
        private int[] networkSize;
        private int randomPrecision;
        public double bias;
        public double learningConstant;
        public double[,] nodeArray;
        public double[,,] weightArray;
        private double[,,] deltaArray;

        public Network(int[] setSize, double setBias = 1.0, double setLearningConstant = 0.01, int setRandomPrecision = 1000)
        {
            networkSize = setSize; //Word Count, Hidden Size

            bias = setBias;
            learningConstant = setLearningConstant;
            randomPrecision = setRandomPrecision;

            nodeArray = new double[networkSize[1] + 2, networkSize[0] + 1];

            weightArray = new double[networkSize[1] + 1, networkSize[0] + 1, networkSize[0]];
            deltaArray = new double[networkSize[1] + 1, networkSize[0] + 1, networkSize[0]];

            Setup();
        }

        public void SetLearningConstant(double setLearningConstant)
        {
            learningConstant = setLearningConstant;
        }

        private void Setup()
        {
            Random r = new Random();

            for (int i = 0; i < weightArray.GetLength(0); i++)
            {
                for (int j = 0; j < weightArray.GetLength(1); j++)
                {
                    for (int k = 0; k < weightArray.GetLength(2); k++)
                    {
                        weightArray[i, j, k] = r.Next(-randomPrecision, randomPrecision) / (1.0 * randomPrecision);
                    }
                }
            }

            for (int i = 0; i < nodeArray.GetLength(0) - 1; i++)
            {
                nodeArray[i, nodeArray.GetLength(1) - 1] = bias;
            }
        }

        public void Print()
        {
            for (int layer = 0; layer < nodeArray.GetLength(0); layer++)
            {
                Console.WriteLine("\n# {0} #########################################################################################\n", layer);
                for (int node = 0; node < nodeArray.GetLength(1); node++)
                {

                    Console.WriteLine("- Node: {0} ---------- Sum: {1}, Activated: {2} -----\n",node ,nodeArray[layer, node], Math.Tanh(nodeArray[layer, node]));
                    if (layer != nodeArray.GetLength(0)-1)
                    {
                        for (int weight = 0; weight < weightArray.GetLength(2); weight++)
                        {
                            Console.WriteLine(weightArray[layer, node, weight]);
                        }
                    }
                    Console.WriteLine();
                }
            }
        }

        public double[] Forward(double[] inputArray)
        {
            Clear();

            if (inputArray.Length != nodeArray.GetLength(1)-1)
            {
                Console.WriteLine("Invalid input given. \nLength given was \"{0}\", but it must be \"{1}\".", inputArray.Length, nodeArray.GetLength(1)-1);
                return new double[0];
            }
            for (int input = 0; input < inputArray.Length; input++)
            {
                nodeArray[0, input] = inputArray[input];
            }

            for (int layer = 0; layer < nodeArray.GetLength(0) - 1; layer++)
            {
                for (int node = 0; node < nodeArray.GetLength(1); node++)
                {
                    for (int weight = 0; weight < weightArray.GetLength(2); weight++)
                    {
                        nodeArray[layer + 1, weight] += weightArray[layer, node, weight] * Math.Tanh(nodeArray[layer, node]);
                    }
                }
            }

            double[] outputArray = new double[nodeArray.GetLength(1)-1];

            for (int endNodes = 0; endNodes < nodeArray.GetLength(1) - 1; endNodes++)
            {
                outputArray[endNodes] = Math.Tanh(nodeArray[nodeArray.GetLength(0) - 1, endNodes]);
            }

            return outputArray;
        }

        public void Backprop(double[] inputArray, double[] expectedOutput)
        {
            Forward(inputArray);
            /*for (int endNodes = 0; endNodes < nodeArray.GetLength(1); endNodes++)
            {
                for (int endWeights = 0; endN)
            }*/
        }

        private void Clear()
        {
            nodeArray = new double[nodeArray.GetLength(0), nodeArray.GetLength(1)];
            for (int layer = 0; layer < nodeArray.GetLength(0); layer++)
            {
                nodeArray[layer,nodeArray.GetLength(1)-1] = bias;
            }
        }
    }
}
