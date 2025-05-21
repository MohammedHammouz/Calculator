using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace calc {
	enum eOperation {
		Mul,
		Sub,
		Sum,
		Div
	}
	struct Status {
		public bool IsOperation;
		public char Operation;
		public eOperation opt;
		public bool IsResult;
		public bool IsDot;
	};

	public partial class Form1 : Form {
		private double number1 = 0;
		private double number2 = 0;
		private string s = "";
		private double result = 0;

		public Form1() {
			InitializeComponent();
		}

		Status status;
		[DllImport("user32.dll")]
		public static extern bool MessageBeep(uint uType);
		private void PlayClickSound() {
			MessageBeep(0);
		}

		private void changeButtonsStatus(bool state) {
			btnSum.Enabled = state;
			btnMultiply.Enabled = state;
			btnDivide.Enabled = state;
			btnSubtract.Enabled = state;
		}
		private void btnClear_Click(object sender, EventArgs e) {
			PlayClickSound();
			tBResult.Clear();
			result = 0;
			s = string.Empty;
			status.IsOperation = false;
			changeButtonsStatus(true);
		}

		private void btnNumber_Click(object sender, EventArgs e) {
			PlayClickSound();
			Button btn = sender as Button;
			string digit = btn.Tag.ToString();
			s = tBResult.Text += digit;
			if (!status.IsOperation) {
				number1 = Convert.ToDouble(s);
			} else {
				number2 = Convert.ToDouble(s.Substring(s.IndexOf(status.Operation) + 1));
				changeButtonsStatus(false);
			}
		}
		private void btnBC_Click(object sender, EventArgs e) {
			PlayClickSound();

			if (tBResult.Text.Length > 0) {
				if (tBResult.Text[tBResult.Text.Length - 1] == '+' ||
				tBResult.Text[tBResult.Text.Length - 1] == '-' ||
				tBResult.Text[tBResult.Text.Length - 1] == '*' ||
				tBResult.Text[tBResult.Text.Length - 1] == '/') {
					tBResult.Text = tBResult.Text.Substring(0, tBResult.TextLength - 1);
					status.IsOperation = false;
					s = tBResult.Text;
					changeButtonsStatus(true);
				} else {
					tBResult.Text = tBResult.Text.Substring(0, tBResult.TextLength - 1);
					s = tBResult.Text;
				}
			}
		}

		private eOperation getOperation(char c) {
			switch (c) {
				case '+': return eOperation.Sum;
				case '-': return eOperation.Sub;
				case '*': return eOperation.Mul;
				default: return eOperation.Div;
			}
		}

		private void handleOperationClick(char operation) {
			PlayClickSound();
			tBResult.Text += operation;
			status.IsOperation = true;
			status.Operation = operation;
			status.opt = getOperation(operation);
			changeButtonsStatus(false);
			btnDot.Enabled = true;
		}

		private void btnSum_Click(object sender, EventArgs e) {
			handleOperationClick('+');
		}

		private void btnMultiply_Click(object sender, EventArgs e) {
			handleOperationClick('*');
		}

		private void btnDivide_Click(object sender, EventArgs e) {
			handleOperationClick('/');
		}

		private void btnSubtract_Click(object sender, EventArgs e) {
			handleOperationClick('-');
		}

		private void btnDot_Click(object sender, EventArgs e) {
			PlayClickSound();
			string currentNumber = status.IsOperation
	? s.Substring(s.IndexOf(status.Operation) + 1)
	: s;

			if (!currentNumber.Contains("."))
				tBResult.Text += ".";
			btnDot.Enabled = false;
		}

		private double ChooseOperation() {
			switch (status.opt) {
				case eOperation.Sub:
					if (!status.IsResult) {
						result = number1 - number2;
						status.IsOperation = true;
						number1 = result;
						changeButtonsStatus(true);
					} else {
						result -= number2;
						status.IsOperation = true;
						number1 = result;
						changeButtonsStatus(true);
					}
					break;
				case eOperation.Sum:
					if (!status.IsResult) {
						result = number1 + number2;
						status.IsOperation = true;
						number1 = result;
						changeButtonsStatus(true);
					} else {
						result += number2;
						status.IsOperation = true;
						number1 = result;
						changeButtonsStatus(true);
					}
					break;
				case eOperation.Mul:
					if (!status.IsResult) {
						result = number1 * number2;
						status.IsOperation = true;
						number1 = result;
						changeButtonsStatus(true);
					} else {
						result *= number2;
						status.IsOperation = true;
						number1 = result;
						changeButtonsStatus(true);
					}
					break;
				case eOperation.Div:
					if (number2 != 0) {
						if (!status.IsResult) {
							result = Convert.ToDouble(number1) / number2;
							status.IsOperation = true;
							number1 = result;
							changeButtonsStatus(true);
						} else {
							result /= number2;
							status.IsOperation = true;
							number1 = result;
							changeButtonsStatus(true);
						}
						break;
					} else {
						string temp = tBResult.Text;
						tBResult.Text = "error";
						tBResult.Text = s;
					}
					break;
			}

			return result;
		}

		private void btnEqual_Click(object sender, EventArgs e) {
			PlayClickSound();
			btnDot.Enabled = true;

			if (status.IsOperation && s.Contains(status.Operation)) {
				double output = ChooseOperation();

				if (double.IsInfinity(output) || double.IsNaN(output)) {
					tBResult.Text = "Error";
				} else {
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
