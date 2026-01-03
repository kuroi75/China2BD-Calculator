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

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbShippingRate.Items.Add("850 BDT/kg (Default)");
            cmbShippingRate.Items.Add("Custom");
            cmbShippingRate.SelectedIndex = 0;

            txtCustomRate.Enabled = false;
        }

        private void cmbShippingRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbShippingRate.SelectedItem.ToString() == "Custom")
            {
                txtCustomRate.Enabled = true;
            }
            else
            {
                txtCustomRate.Enabled = false;
                txtCustomRate.Text = "";
            }
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
                double kg;
                if (cmbUnit.SelectedItem.ToString() == "Gram")
                    kg = weightInput / 1000.0;
                else
                    kg = weightInput;

                // Add 400g
                if (chkAdd400.Checked)
                    kg += 0.400;

                // Shipping rate
                double shippingRate = 850;

                if (cmbShippingRate.SelectedItem.ToString() == "Custom")
                {
                    if (!double.TryParse(txtCustomRate.Text, out shippingRate))
                    {
                        MessageBox.Show("Please enter valid custom shipping rate!");
                        return;
                    }
                }

                // Base calculation
                double totalCost = (kg * shippingRate) + tk;

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

                // ===== OUTPUT (UNCHANGED) =====
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
                MessageBox.Show("Please enter valid numbers!");
            }
        }

        // Clear button
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtTK.Text = "";
            txtKG.Text = "";
            txtProfit.Text = "";
            txtCustomRate.Text = "";

            cmbUnit.SelectedIndex = 0;
            cmbShippingRate.SelectedIndex = 0;

            chkAdd400.Checked = false;
            chkAdd200.Checked = false;

            lblOutput.Text = "--- Developed by Fahim AF ---";
        }

        // Show / Hide calculator
        private void btnToggleCalc_Click(object sender, EventArgs e)
        {
            calculatorVisible = !calculatorVisible;
            panelCalculator.Visible = calculatorVisible;
            btnToggleCalc.Text = calculatorVisible ? "Hide Calculator" : "Show Calculator";
        }

        // Calculator equal
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
    }
}
