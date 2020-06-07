using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neural_Network_Test
{
    class Neuron
    {
        #region Private Members

        private float value;

        private Neuron[] connections;

        private float[] weights;

        private float bias;

        #endregion

        #region Public Accessors

        public float Value { get => value; set => this.value = value; }
        public float[] Weights { get => weights; set => weights = value; }
        public float Bias { get => bias; set => bias = value; }

        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Neuron()
        {

        }
        /// <summary>
        /// Create a Neuron with a set value
        /// </summary>
        /// <param name="value">value to be stored in neuron</param>
        public Neuron(float value)
        {
            Value = value;
            Bias = 0f;//(float)(rng.NextDouble() - 0.5d);
        }

        #endregion

        #region Public Methods

        public bool Connect(Neuron[] nextLayer, Random rng)
        {
            try
            {
                connections = nextLayer;
                Weights = new float[nextLayer.Length];
                for (int i = 0; i < Weights.Length; i++)
                {
                    Weights[i] = (float)(rng.NextDouble());
                }
                return true;
            }
            catch (Exception)
            {
                return false;                
            }
        }

        #endregion

        #region Overides

        public override string ToString()
        {
            string outstring = "";
            foreach (float w in weights)
            {
                outstring += w + " ";
            }
            return outstring;
        }

        #endregion

    }
}
