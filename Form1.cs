using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    enum eOperation
    {
        Mul,
        Sub,
        Sum,
        Div
    }
    struct Status
    {
        public bool IsOperation;
        public char Operation;
        public eOperation opt;
        public bool IsResult;
        public bool IsDot;
    };
    
    public partial class Form1 : Form
    {
        double number1 = 0;
        double number2 = 0;
        string s = "";
        double result = 0;
        public Form1()
        {
            InitializeComponent();
        }
        Status status;
        [DllImport("user32.dll")]
        public static extern bool MessageBeep(uint uType);

        private void PlayClickSound()
        {
            MessageBeep(0); 
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            PlayClickSound();
            tBResult.Clear();
            result = 0;
            s = string.Empty;
            status.IsOperation = false;
            btnSum.Enabled = true;
            btnMultiply.Enabled = true;
            btnDivide.Enabled = true;
            btnSubtract.Enabled = true;
        }
        
        private void btnNumber_Click(object sender, EventArgs e)
        {
            PlayClickSound();
            Button btn = sender as Button;
            string digit = btn.Tag.ToString();
            s = tBResult.Text += digit;
            if (!status.IsOperation)
            {
                number1 = Convert.ToDouble(s);
            }
            else
            {
                number2 = Convert.ToDouble(s.Substring(s.IndexOf(status.Operation) + 1));
                btnSum.Enabled = false;
                btnMultiply.Enabled = false;
                btnDivide.Enabled = false;
                btnSubtract.Enabled = false;
            }
        }
        private void btnBC_Click(object sender, EventArgs e)
        {
            PlayClickSound();
            
            if (tBResult.Text.Length > 0)
            {
                if (tBResult.Text[tBResult.Text.Length - 1] == '+' ||
                tBResult.Text[tBResult.Text.Length - 1] == '-' ||
                tBResult.Text[tBResult.Text.Length - 1] == '*' ||
                tBResult.Text[tBResult.Text.Length - 1] == '/')
                {


                    tBResult.Text = tBResult.Text.Substring(0, tBResult.TextLength - 1);
                    status.IsOperation = false;
                    s = tBResult.Text;
                    btnSum.Enabled = true;
                    btnMultiply.Enabled = true;
                    btnDivide.Enabled = true;
                    btnSubtract.Enabled = true;
                    btnDot.Enabled = true;
                }
                else
                {
                    tBResult.Text = tBResult.Text.Substring(0, tBResult.TextLength - 1);
                    s = tBResult.Text;
                }
            }
        }

        private void btnSum_Click(object sender, EventArgs e)
        {
            PlayClickSound();
            tBResult.Text += "+";
            status.IsOperation = true;
            status.Operation = '+';
            status.opt = eOperation.Sum;
            btnSum.Enabled = false;
            btnMultiply.Enabled = false;
            btnDivide.Enabled = false;
            btnSubtract.Enabled = false;
            btnDot.Enabled = true;
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            PlayClickSound();
            tBResult.Text += "*";
            status.IsOperation = true;
            status.Operation = '*';
            status.opt = eOperation.Mul;
            btnSum.Enabled = false;
            btnMultiply.Enabled = false;
            btnDivide.Enabled = false;
            btnSubtract.Enabled = false;
            btnDot.Enabled = true;
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            PlayClickSound();
            tBResult.Text += "/";
            status.IsOperation = true;
            status.Operation = '/';
            status.opt = eOperation.Div;
            btnSum.Enabled = false;
            btnMultiply.Enabled = false;
            btnDivide.Enabled = false;
            btnSubtract.Enabled = false;
            btnDot.Enabled = true;
        }

        private void btnSubtract_Click(object sender, EventArgs e)
        {
            PlayClickSound();
            tBResult.Text += "-";
            status.IsOperation = true;
            status.Operation = '-';
            status.opt = eOperation.Sub;
            btnSum.Enabled = false;
            btnMultiply.Enabled = false;
            btnDivide.Enabled = false;
            btnSubtract.Enabled = false;

            btnDot.Enabled = true;
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            PlayClickSound();
            string currentNumber = status.IsOperation
        ? s.Substring(s.IndexOf(status.Operation) + 1)
        : s;

            if (!currentNumber.Contains("."))
                tBResult.Text += ".";
                btnDot.Enabled = false;
        }

        private double ChooseOperation()
        {
            switch (status.opt)
            {
                case eOperation.Sub:
                    if (!status.IsResult)
                    {
                        result = number1 - number2;
                        status.IsOperation = true;
                        number1=result;
                        btnSum.Enabled = true;
                        btnMultiply.Enabled = true;
                        btnDivide.Enabled = true;
                        btnSubtract.Enabled = true;
                    }
                    else
                    {
                        result -= number2;
                        status.IsOperation = true;
                        number1 = result;
                        btnSum.Enabled = true;
                        btnMultiply.Enabled = true;
                        btnDivide.Enabled = true;
                        btnSubtract.Enabled = true;

                    }
                    break;
                case eOperation.Sum:
                    if (!status.IsResult)
                    {
                        result = number1 + number2;
                        status.IsOperation = true;
                        number1 = result;
                        btnSum.Enabled = true;
                        btnMultiply.Enabled = true;
                        btnDivide.Enabled = true;
                        btnSubtract.Enabled = true;
                    }
                    else
                    {
                        result += number2;
                        status.IsOperation = true;
                        number1 = result;
                        btnSum.Enabled = true;
                        btnMultiply.Enabled = true;
                        btnDivide.Enabled = true;
                        btnSubtract.Enabled = true;
                    }
                    break;
                case eOperation.Mul:
                    if (!status.IsResult)
                    {
                        result = number1 * number2;
                        status.IsOperation = true;
                        number1 = result;
                        btnSum.Enabled = true;
                        btnMultiply.Enabled = true;
                        btnDivide.Enabled = true;
                        btnSubtract.Enabled = true;
                    }
                    else
                    {
                        result *= number2;
                        status.IsOperation = true;
                        number1 = result;
                        btnSum.Enabled = true;
                        btnMultiply.Enabled = true;
                        btnDivide.Enabled = true;
                        btnSubtract.Enabled = true;
                    }
                    break;
                case eOperation.Div:
                    if (number2 != 0)
                    {
                        if (!status.IsResult)
                        {
                            result = Convert.ToDouble(number1) / number2;
                            status.IsOperation = true;
                            number1 = result;
                            btnSum.Enabled = true;
                            btnMultiply.Enabled = true;
                            btnDivide.Enabled = true;
                            btnSubtract.Enabled = true;
                        }
                        else
                        {
                            result /= number2;
                            status.IsOperation = true;
                            number1 = result;
                            btnSum.Enabled = true;
                            btnMultiply.Enabled = true;
                            btnDivide.Enabled = true;
                            btnSubtract.Enabled = true;
                        }
                        break;
                    }
                    else
                    {
                        string temp = tBResult.Text;
                        tBResult.Text = "error";
                        tBResult.Text = s;
                    }
                        break;
            }
            
            return result;
        }
        private void btnEqual_Click(object sender, EventArgs e)
        {
            PlayClickSound();
            btnDot.Enabled = true;

            if (status.IsOperation && s.Contains(status.Operation))
            {
                double output = ChooseOperation();

                if (double.IsInfinity(output) || double.IsNaN(output))
                {
                    tBResult.Text = "Error";
                }
                else
                {
                    tBResult.Text = output.ToString();
                }

                status.IsResult = true;
                status.IsOperation = false;
                s = output.ToString(); 
                number2 = 0;
            }
            

        }
       
    }
}
