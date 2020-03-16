using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReZero_Project_1
{
    class BP_Learn
    {
        //const
        const int Input_Neuron  = 5;
        const int Output_Neuron = 5;
        const int Number_Layer  = 5;
        const int Hd_L_Number   = 5;//hidden layer of neuron number
        const int Number_Neuron = Hd_L_Number * Number_Layer; //TEMP
        //int 
        int Bias = 1;
        int bnc = 0;
        int inc = 0;
        int k = 0;
        int jump = 0;
        int carry = 0;
        int small_jump = 0;

        int New_Lable = Number_Neuron - Output_Neuron;
        int Lable = Number_Neuron - Output_Neuron - Hd_L_Number;

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
        double[,] target_t = new double[10, 10]; // ?
        
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
                    //ex) 25-(5+5*k) -> n=20-5k; n < 25-5k; n++ -> 20,21,22,23,24 / 15,16,17,18,19 / ....
                    for(int n = New_Lable - (Hd_L_Number + k); n < New_Lable - k; n++)
                    {
                        Sum[j] += (Sigmoid[n] * Weight_Layer[5,inc]);
                    }
                    Sum[j] += (Bias * Bias_Weight[j]);
                    Sigmoid[j] = (1.0 / (1.0 + Math.Exp(-Sum[j]))); 
                }
                inc = 0;
                jump += Hd_L_Number;
            }
            jump = 0;
            k = 0;

            /*	Output Layer와 연결된 Hidden Layer이용하여 Output Sum,Sigmoid	*/
            for (int i = 0; i < Output_Neuron; ++i)
            {
                for (int j = Lable; j < New_Lable; j++)
                {
                    Sum_Output[i] += (Sigmoid[j] * Weight_Output_Layer[inc]);
                    inc++;
                }
                Sum_Output[i] += (Bias * Bias_Weight[New_Lable + i]);
                Sigmoid_Output[i] = (1.0 / (1.0 + Math.Exp(-Sum_Output[i])));
                Delta_Output[i] = (Sigmoid_Output[i] * (1 - Sigmoid_Output[i])) * (target_t[bnc,i] - Sigmoid_Output[i]);

                /*Target 값 설정 주의*/
                for (int j = Lable; j < New_Lable; ++j)
                {
                    Delta[j] += (Sigmoid[j] * (1 - Sigmoid[j]) * Weight_Output_Layer[carry] * Delta_Output[i]);
                    ++carry;
                }
            }
            inc = 0;
            carry = 0;

            /*Hidden Layer들 사이의 Delta*/
            for (int i = Number_Layer - 1; i > 0; --i)
            {
                carry += Hd_L_Number;
                //ex) 30 - (10+jump)  < 25 - jump -> 1. 20 < 25 2. 15 < 20 3.10 < 15
                for(int z = New_Lable - (2 * Hd_L_Number + jump); z < New_Lable - Hd_L_Number-jump; z++)
                {
                    //ex) 30 - carry < 30 - jump  1. 25 < 30 2. 20 < 25 ...
                    for (int j = (New_Lable - carry); j < New_Lable - jump; j++)
                    {
                        Delta[z] += (Sigmoid[z] * (1 - Sigmoid[z])) * Delta[j] * Weight_Layer[i - 1,inc + small_jump];
                        small_jump += Hd_L_Number;
                    }
                    small_jump = 0;
                    jump += Hd_L_Number;
                    inc++;
                }
            }
            carry = 0;
            inc = 0;
            jump = 0;
        }

    }
}
