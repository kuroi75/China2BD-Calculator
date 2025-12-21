using System;
using System.Windows.Forms;

namespace PriceCalculatorApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void calculateButton_Click(object sender, EventArgs e)
        {
            try
            {
                double tk = double.Parse(txtTK.Text);
                double weightInput = double.Parse(txtKG.Text);
                double profitPercent = double.Parse(txtProfit.Text);

                // KG or Gram Handling
                double kg;

                if (cmbUnit.SelectedItem.ToString() == "Gram")
                    kg = weightInput / 1000.0;
                else
                    kg = weightInput;

                // Extra 400g checkbox
                if (chkAdd400.Checked)
                {
                    kg += 0.400;
                }

                double newTk = tk + 200;
                double totalCost = (kg * 850) + newTk;

                double profit = (totalCost * profitPercent) / 100;
                double finalPrice = totalCost + profit;

                // Rounding Logic
                int lastTwo = ((int)finalPrice) % 100;
                double addAmount = 0;
                double roundedPrice = finalPrice;

                if (lastTwo < 50)
                {
                    addAmount = 50 - lastTwo;
                    roundedPrice = finalPrice + addAmount;
                }
                else
                {
                    addAmount = 99 - lastTwo;
                    roundedPrice = finalPrice + addAmount;
                }

                double newProfit = roundedPrice - totalCost;
                double newProfitPercent = (newProfit / totalCost) * 100;

                // Output
                outputBox.Clear();
                outputBox.AppendText("Product price (after shipping): " + totalCost + " TK\n");
                outputBox.AppendText("Original final price: " + finalPrice + " TK\n");
                outputBox.AppendText("------------------------------------------------\n");
                outputBox.AppendText("For rounding product price (last 2 digits): " + lastTwo + "\n");
                outputBox.AppendText("Add amount (round-up): " + addAmount + " TK\n\n");
                outputBox.AppendText("Selling price / Consumer price: " + roundedPrice + " TK\n\n");
                outputBox.AppendText("New Profit: " + newProfit + " TK\n");
                outputBox.AppendText("New Profit %: " + newProfitPercent.ToString("0.00") + " %\n");
            }
            catch
            {
                MessageBox.Show("Please enter valid numeric input.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // at the end type a cadit message
            //outputBox.AppendText("\n--- Developed by Fahim AF ---");
        }
    }
}
