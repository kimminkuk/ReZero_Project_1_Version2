using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReZero_Project_1
{
    class BP_Learn
    {
        public void BP_Start() 
        {
            //int 
            int Bias = 1;
            double L_N_G = 0.2;

            double[] Input      = new double[10];
            double[] Sigmoid    = new double[100];
            double[] Delta      = new double[100];
            double[] Sum        = new double[100];

            double[] Sum_Output     = new double[10];
            double[] Sigmoid_Output = new double[10];
            double[] Delta_Output   = new double[10];
            double[] Error          = new double[10];
            double[] Error_Result   = new double[10];
            double[] Error_add      = new double[10];


            double[] Bias_Weight   = new double[10];

            double[] Weight_Input_Layer  = new double[10];
            double[] Weight_Output_Layer = new double[10];
            double[,] Weight_Layer = new double[10,10];

        }

    }
}
