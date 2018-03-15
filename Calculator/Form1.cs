using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        bool isNewEntry = false, isInfinityExecption = false, isRepeatLastOperation = false;
        double dblResult = 0, dblOperand = 0;
        char chPreviousOperator = new char();

        public Form1()
        {
            InitializeComponent();//Initialisation de tout les objet dans la forme
        }

        private void button1_Click(object sender, EventArgs e)//updateOperand
        {
            if (!isInfinityExecption)
            {
                if(isNewEntry)
                {
                    /*Quand on a une nouvelle entrer on remet 0 
                     *dans la text area et on remets 
                     *la veleur a false 
                     *ce qui veut dire qu'on n'a pas de nouvelle 
                     *entre*/
                    txtResult.Text = "0";
                    isNewEntry = false;
                }
                if(isRepeatLastOperation)
                {
                    chPreviousOperator = '\0';//NULL
                    dblResult = 0;
                }
                /*
                 * 1. si on a déja 0 et on clique sur zero on ne fait rien
                 * 2. si on a deja 5.4 et on appuie encore sur . on ne fait rien
                 
                 */
                if(!(txtResult.Text == "0" && (Button)sender == btn0) && !(((Button)sender) == btnDecimalPoint && txtResult.Text.Contains(".")))
                    txtResult.Text = (txtResult.Text == "0" && ((Button)sender) ==  btnDecimalPoint) ? "0.": ((txtResult.Text == "0") ? ((Button)sender).Text : txtResult.Text+ ((Button)sender).Text);
            }
        }

        private void CleanOperator(object sender, EventArgs e)
        {
            isInfinityExecption = false;
            txtResult.Text = "0";
        }

        private void button2_Click(object sender, EventArgs e)//ChangeSign
        {
            if (isInfinityExecption)
                txtResult.Text = (double.Parse(txtResult.Text) * -1).ToString();
            //on mutiplie par -1 LUL et on le mets en string
        }

        private void button5_Click(object sender, EventArgs e)//operatorFound
        {
            if (!isInfinityExecption)
            {
                if (chPreviousOperator == '\0')
                {
                    chPreviousOperator = ((Button)sender).Text[0];
                    dblResult = double.Parse(txtResult.Text);
                } else if (isNewEntry)
                    chPreviousOperator = ((Button)sender).Text[0];
                else
                {
                    Operate(dblResult, chPreviousOperator, double.Parse(txtResult.Text));
                    chPreviousOperator = ((Button)sender).Text[0];
                }
                isNewEntry = true;
                isRepeatLastOperation = false;
            }

        }
        
        void Operate(double dblPreviousResult, char chPreviousOperator, double dblOperand)
        {
            switch (chPreviousOperator)
            {
                case '+':
                    txtResult.Text = (dblResult = (dblPreviousResult + dblOperand)).ToString();
                    break;
                case '-':
                    txtResult.Text = (dblResult = (dblPreviousResult - dblOperand)).ToString();
                    break;
                case '*':
                    txtResult.Text = (dblResult = (dblPreviousResult * dblOperand)).ToString();
                    break;
                case '/':
                    if (dblOperand == 0)
                    {
                        txtResult.Text = "NOPE /0 is not possible";
                        isInfinityExecption = true;
                    }
                    else
                        txtResult.Text = (dblResult = (dblPreviousResult / dblOperand)).ToString();
                        break;
            }
        }

        private void button11_Click(object sender, EventArgs e)//Equals
        {
            if (!isInfinityExecption)
            {
                if (!isRepeatLastOperation)
                {
                    dblOperand = double.Parse(txtResult.Text);
                    isRepeatLastOperation = true;
                }
                Operate(dblResult, chPreviousOperator, dblOperand);
                isNewEntry = true;
            }

        }

        private void button18_Click(object sender, EventArgs e)//ClearALL
        {
            isInfinityExecption = isRepeatLastOperation = false;
            dblOperand = dblResult = 0;
            txtResult.Text = "0";
            isNewEntry = true;
            chPreviousOperator = '\0';
        }
    }
}
