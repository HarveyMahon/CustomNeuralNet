using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neural_Network_Test
{
    class NeuralNetwork
    {
        #region Private Members

        private Neuron[][] neurons;
        //needs to be open here otherwise all neurons start with same weights
        private Random rng = new Random();

        #endregion

        #region Constructors

        public NeuralNetwork(int[] layers)
        {
            neurons = CreateNeurons(layers);
        }

        #endregion

        #region Private Methods

        private Neuron[][] CreateNeurons(int[] layers)
        {
            Neuron[][] neuronArray = new Neuron[layers.Length][];

            for (int i = 0; i < layers.Length; i++)
            {
                neuronArray[i] = new Neuron[layers[i]];
            }

            for (int i = neuronArray.Length - 1; i >= 0; i--)
            {
                for (int j = 0; j < neuronArray[i].Length; j++)
                {
                    if (i < neuronArray.Length - 1)
                    {
                        neuronArray[i][j] = new Neuron();
                        neuronArray[i][j].Connect(neuronArray[i + 1], rng);
                    }
                    else
                    {
                        neuronArray[i][j] = new Neuron();
                    }
                }
            }

            return neuronArray;
        }

        private float[] calcNextLayer(float[] inputs, int currentLayer)
        {
            //storing weights in matrix
            Matrix Weights = new Matrix(neurons[currentLayer][0].Weights.Length, neurons[currentLayer].Length);
            for (int i = 0; i < Weights.Rows; i++)
            {
                for (int j = 0; j < Weights.Columns; j++)
                {
                    Weights.matrix[i, j] = neurons[currentLayer][j].Weights[i];
                }
            }
            //storing inputs in matrix
            Matrix Inputs = new Matrix(inputs.Length, 1);
            for (int i = 0; i < inputs.Length; i++)
            {
                Inputs.matrix[i, 0] = inputs[i];
            }
            //storing biases in matrix
            Matrix Biases = new Matrix(neurons[currentLayer+1].Length, 1);
            for (int i = 0; i < neurons[currentLayer + 1].Length; i++)
            {
                Biases.matrix[i, 0] = neurons[currentLayer+1][i].Bias;
            }

            Matrix Outputs = (Weights * Inputs) + Biases;
            Outputs = Sigmoid(Outputs);
            float[] outArr = new float[Outputs.Rows];
            for (int i = 0; i < Outputs.Rows; i++)
            {
                outArr[i] = Outputs.matrix[i,0];
                if (float.IsNaN(outArr[i]))
                    continue;
            }
            return outArr;
        }

        //Sum of Squared Error loss function
        private float Cost(float[] output, float[] answer)
        {
            float score = 0;
            //if (float.IsNaN(answer[0]) || float.IsNaN(output[0]))
            //    Console.WriteLine();
            for (int i = 0; i < answer.Length; i++)
            {
                score += (answer[i] - output[i]) * (answer[i] - output[i]);
            }
            //if (float.IsNaN(score))
            //    Console.WriteLine();
            score /= answer.Length;
            return -score;
        }

        private void AdjustWeights(float lr, float grad)
        {
            for (int i = 0; i < neurons.Length-1; i++)
            {
                for (int j = 0; j < neurons[i].Length; j++)
                {
                    for (int k = 0; k < neurons[i][j].Weights.Length; k++)
                    {
                        neurons[i][j].Weights[k] += lr * grad;
                    }
                }
            }
            return;
        }

        private Matrix Sigmoid(Matrix m)
        {
            Matrix outputMatrix = new Matrix(m.Rows, m.Columns);
            for (int i = 0; i < m.Rows; i++)
            {
                for (int j = 0; j < m.Columns; j++)
                {
                    outputMatrix.matrix[i, j] = (float)(Math.Exp(m.matrix[i, j]) / (Math.Exp(m.matrix[i, j]) + 1));              
                }
            }
            return outputMatrix;
        }

        private float LogIt(float f)
        {
            return (float)Math.Log(f / (f - 1));
        }

        #endregion

        #region Public Methods

        public float[] RunGeneration(float[] inputs)
        {
            for (int i = 0; i < neurons.Length-1; i++)
            {
                inputs = calcNextLayer(inputs, i);
            }

            return inputs;
        }

        public void Train(float[][] data, float[][] answers, int generations, float lr)
        {
            float delta = (float)Math.Pow(10, -25);
            float[] prevGuess;
            for (int i = 0; i < generations; i++)
            {
                float[] answer = answers[i % data.Length];
                if (answer[0] == 0)
                {
                    //Console.WriteLine();
                }
                float[] guess = RunGeneration(data[i % data.Length]);

                float[] guessDelta = new float[guess.Length];
                for (int j = 0; j < guess.Length; j++)
                {
                    guessDelta[j] = guess[j] + delta;
                }
                float cost = Cost(answer, guess);
                float grad = cost * guess[0];
                grad *= 2;
                prevGuess = guess;
            }
            return;
        }
        
        public float[][] Test(float[][] data)
        {
            float[][]guesses = new float[data.Length][];
            for (int i = 0; i < data.Length; i++)
            {
                float[] guess = RunGeneration(data[i % data.Length]);
                guesses[i] = guess;
            }
            return guesses;
        }

        #endregion

    }
}
