using System;
using System.Data;
using System.Drawing;
using System.Diagnostics; // Needed for Process.Start
using System.Windows.Forms;

namespace PriceCalculatorGUI
{
    public partial class Form1 : Form
    {
        bool calculatorVisible = true;
        bool isDarkMode = false; 

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

        // GitHub Link Click
        private void ItemGitHub_Click(object sender, EventArgs e)
        {
            try
            {
                var ps = new ProcessStartInfo("https://github.com/kuroi75/China2BD-Calculator")
                {
                    UseShellExecute = true,
                    Verb = "open"
                };
                Process.Start(ps);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not open link: " + ex.Message);
            }
        }

        // DARK MODE LOGIC
        private void ItemDarkMode_Click(object sender, EventArgs e)
        {
            isDarkMode = !isDarkMode;
            ToggleTheme(isDarkMode);
        }

        private void ToggleTheme(bool dark)
        {
            // Colors
            Color backColor = dark ? Color.Black : SystemColors.Control;
            Color foreColor = dark ? Color.White : Color.Black;
            Color textBoxBack = dark ? Color.Black : Color.White;
            Color btnBack = dark ? Color.FromArgb(20, 20, 20) : SystemColors.ControlLight;

            // Main Form
            this.BackColor = backColor;
            this.ForeColor = foreColor;

            // Inputs
            txtTK.BackColor = textBoxBack; txtTK.ForeColor = foreColor;
            txtKG.BackColor = textBoxBack; txtKG.ForeColor = foreColor;
            txtProfit.BackColor = textBoxBack; txtProfit.ForeColor = foreColor;
            txtCustomRate.BackColor = textBoxBack; txtCustomRate.ForeColor = foreColor;
            txtCalc.BackColor = textBoxBack; txtCalc.ForeColor = foreColor;

            // Dropdowns
            cmbUnit.BackColor = textBoxBack; cmbUnit.ForeColor = foreColor;
            cmbShippingRate.BackColor = textBoxBack; cmbShippingRate.ForeColor = foreColor;

            // Checkboxes
            chkAdd400.ForeColor = foreColor;
            chkAdd200.ForeColor = foreColor;

            // Output Label
            lblOutput.BackColor = backColor;
            lblOutput.ForeColor = foreColor;
            lblOutput.BorderStyle = BorderStyle.FixedSingle; 

            // Menu Bar & Dropdown Items
            menuStrip1.BackColor = dark ? Color.Black : SystemColors.Control;
            menuStrip1.ForeColor = foreColor;
            menuOptions.ForeColor = foreColor;
            
            // Dropdown items bg/fg
            itemDarkMode.BackColor = backColor;
            itemDarkMode.ForeColor = foreColor;
            
            itemGitHub.BackColor = backColor;  // Color new item
            itemGitHub.ForeColor = foreColor;

            // Buttons
            Button[] btns = { btnCalc, btnClear, btnToggleCalc };
            foreach(Button b in btns)
            {
                b.BackColor = btnBack;
                b.ForeColor = foreColor;
                b.FlatStyle = FlatStyle.Flat;
                b.FlatAppearance.BorderColor = dark ? Color.White : Color.Gray;
            }
        }

        //suggested profit
        private double GetSuggestedProfit(double kg, double productPrice)
        {
            int row = 0, col = 0;

            if (productPrice <= 399) row = 0;
            else if (productPrice <= 599) row = 1;
            else if (productPrice <= 799) row = 2;
            else if (productPrice <= 999) row = 3;
            else if (productPrice <= 1200) row = 4;
            else row = 5;

            if (kg <= 0.3) col = 0;
            else if (kg <= 0.5) col = 1;
            else if (kg <= 0.8) col = 2;
            else if (kg <= 1.0) col = 3;
            else col = 4;

            double[,] profitTable = {
                {30, 30, 28, 28, 30},
                {30, 30, 30, 30, 28},
                {30, 30, 28, 27, 26},
                {25, 25, 25, 25, 20},
                {30, 30, 22, 22, 22},
                {20, 20, 20, 20, 20}
            };
            return profitTable[row, col];
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            lblOutput.Text = "";

            try
            {
                double tk = double.Parse(txtTK.Text);
                double weightInput = double.Parse(txtKG.Text);
                
                double rawKg;
                if (cmbUnit.SelectedItem.ToString() == "Gram") rawKg = weightInput / 1000.0;
                else rawKg = weightInput;

                double shippingKg = rawKg;
                if (chkAdd400.Checked) shippingKg += 0.400; 

                double suggestedProfit = GetSuggestedProfit(rawKg, tk);

                double profitPercent = 0;
                if (txtProfit.Text == "") profitPercent = suggestedProfit;
                else double.TryParse(txtProfit.Text, out profitPercent);

                double shippingRate = 850;
                if (cmbShippingRate.SelectedItem.ToString() == "Custom")
                {
                    if (!double.TryParse(txtCustomRate.Text, out shippingRate))
                    {
                        MessageBox.Show("Please enter valid custom shipping rate!");
                        return;
                    }
                }

                double totalCost = (shippingKg * shippingRate) + tk;
                if (chkAdd200.Checked) totalCost += 200;

                double profit = (totalCost * profitPercent) / 100;
                double originalFinalPrice = totalCost + profit;

                int lastTwoDigits = ((int)originalFinalPrice) % 100;
                double addAmount;
                if (lastTwoDigits < 50) addAmount = 50 - lastTwoDigits;
                else addAmount = 99 - lastTwoDigits;

                double sellingPrice = originalFinalPrice + addAmount;
                double newProfit = sellingPrice - totalCost;
                double newProfitPercent = (newProfit / totalCost) * 100;

                lblOutput.Text =
                    $"Product price (after shipping): {totalCost:F0} TK\n" +
                    $"Original final price: {originalFinalPrice:F0} TK\n" +
                    $"------------------------------------------------\n" +
                    $"For rounding product price (last 2 digits): {lastTwoDigits}\n" +
                    $"Add amount (round-up): {addAmount:F0} TK\n\n" +
                    $"Selling price / Consumer price: {sellingPrice:F0} TK\n\n" +
                    $"New Profit: {newProfit:F0} TK\n" +
                    $"New Profit %: {newProfitPercent:F2} %\n" +
                    $"------------------------------------------------\n" +
                    $"[SUGGESTION]\n" +
                    $"Based on {rawKg}kg & {tk} TK:\n" +
                    $"Suggested Profit: {suggestedProfit} %";
                    
            }
            catch
            {
                MessageBox.Show("Please enter valid numbers!");
            }
        }

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

        private void btnToggleCalc_Click(object sender, EventArgs e)
        {
            calculatorVisible = !calculatorVisible;
            panelCalculator.Visible = calculatorVisible;
            btnToggleCalc.Text = calculatorVisible ? "Hide Calculator" : "Show Calculator";
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
    }
}