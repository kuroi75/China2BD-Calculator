using System;
using System.Data;
using System.Windows.Forms;

namespace PriceCalculatorGUI
{
    public partial class Form1 : Form
    {
        bool calculatorVisible = true;
        public Form1()
        {
            InitializeComponent();
            cmbUnit.SelectedIndex = 0; 
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            lblOutput.Text = "";
            try
            {
                double tk = double.Parse(txtTK.Text);
                double weightInput = double.Parse(txtKG.Text);
                double profitPercent = double.Parse(txtProfit.Text);

                // Unit conversion
                double kg = cmbUnit.SelectedItem.ToString() == "Gram"
                    ? weightInput / 1000.0
                    : weightInput;

                // Add 400g
                if (chkAdd400.Checked)
                    kg += 0.400;

                // Base calculation
                double totalCost = (kg * 850) + tk;
                // Add +200 BDT
                if (chkAdd200.Checked)  
                    totalCost += 200;

                double profit = (totalCost * profitPercent) / 100;
                double originalFinalPrice = totalCost + profit;

                // ===== ROUNDING LOGIC =====
                int lastTwoDigits = ((int)originalFinalPrice) % 100;
                double addAmount;

                if (lastTwoDigits < 50)
                    addAmount = 50 - lastTwoDigits;
                else
                    addAmount = 99 - lastTwoDigits;

                double sellingPrice = originalFinalPrice + addAmount;

                double newProfit = sellingPrice - totalCost;
                double newProfitPercent = (newProfit / totalCost) * 100;

                // ===== OUTPUT =====
                lblOutput.Text =
                    $"Product price (after shipping): {totalCost:F0} TK\n" +
                    $"Original final price: {originalFinalPrice:F0} TK\n" +
                    $"------------------------------------------------\n" +
                    $"For rounding product price (last 2 digits): {lastTwoDigits}\n" +
                    $"Add amount (round-up): {addAmount:F0} TK\n\n" +
                    $"Selling price / Consumer price: {sellingPrice:F0} TK\n\n" +
                    $"New Profit: {newProfit:F0} TK\n" +
                    $"New Profit %: {newProfitPercent:F2} %";
            }
            catch
            {
                MessageBox.Show(
                    "Please enter valid numbers!",
                    "Input Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // Show / Hide calculator
        private void btnToggleCalc_Click(object sender, EventArgs e)
        {
            calculatorVisible = !calculatorVisible;
            panelCalculator.Visible = calculatorVisible;
            btnToggleCalc.Text = calculatorVisible ? "Hide Calculator" : "Show Calculator";
        }

        // Normal calculator buttons
        private void CalcButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            txtCalc.Text += btn.Text;
        }

        private void btnCalcEqual_Click(object sender, EventArgs e)
        {
            try
            {
                var result = new DataTable().Compute(txtCalc.Text, null);
                txtCalc.Text = result.ToString();
            }
            catch
            {
                txtCalc.Text = "Error";
            }
        }

        private void btnCalcClear_Click(object sender, EventArgs e)
        {
            txtCalc.Clear();
        }
    }
}
