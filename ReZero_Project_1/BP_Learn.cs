﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReZero_Project_1
{
    class BP_Learn
    {
        //const
        const int Input_Neuron = 5;
        const int Number_Layer = 5;
        const int Hd_L_Number = 5;
        //int 
        int Bias = 1;
        int bnc = 0;
        int inc = 0;
        int k = 0;
        int jump = 0;
        int New_Lable = Hd_L_Number * Number_Layer; //TEMP

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
        
        double[] Weight_Input_Layer  = new double[100]; //Input_Neuron * Hidden_Layer 1layer
        double[] Weight_Output_Layer = new double[10];
        double[,] Weight_Layer = new double[10,10];
        
        int[] Hidden_Layer = new int[10];

        /*BPA Learn*/
         public void BP_START()
        {
            /*Input - Hidden Layer[0] 사이 Sum,Sigmoid,Delta */
            for (int i = 0; i < Input_Neuron; i++) 
            {
                for (int j = 0; j < Input_Neuron; j++)
                {
                    Sum[i] += Input[j+bnc*Input_Neuron] * Weight_Input_Layer[inc];
                    ++inc;
                }
                Sum[i] += (Bias * Bias_Weight[i]);
                Sigmoid[i] = (1.0 / (1.0 + Math.Exp(-Sum[i])));
            }
            inc = 0;

            /*Hidden Layer 사이의 Sum, Sigmoid*/
            for ( int i = Number_Layer-1; i > 0; i-- )
            {
                k += Hd_L_Number;
                //ex) 20,21,22,23,24 / 15,16,17,18,19 / ...
                for (int j = New_Lable - (Hd_L_Number + jump); j < New_Lable - jump; j++ )
                {
                    Sum[j] = 0;
                }
            }
        }

    }
}
