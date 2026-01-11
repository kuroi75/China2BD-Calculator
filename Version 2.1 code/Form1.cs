using System;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace PriceCalculatorGUI
{
    public partial class Form1 : Form
    {
        // === SETTINGS ===
        // Change this number to your current agent rate
        double exchangeRate = 19.0; 

        bool calculatorVisible = true;
        bool isDarkMode = false;
        
        // This button is created dynamically in code, so we declare it here to reference it later
        private Button btnEqual; 

        public Form1()
        {
            InitializeComponent();
            GenerateCalculatorButtons(); // Build the calculator keypad
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Default selections
            cmbUnit.SelectedIndex = 0; 
            cmbCurrency.SelectedIndex = 0; 

            cmbShippingRate.Items.Add("850 BDT/kg (Default)");
            cmbShippingRate.Items.Add("Custom");
            cmbShippingRate.SelectedIndex = 0;

            txtCustomRate.Enabled = false;
        }

        // --- DYNAMIC CALCULATOR UI GENERATION ---
        private void GenerateCalculatorButtons()
        {
            int x = 10, y = 50;
            string[] keys = { "7","8","9","+","4","5","6","-","1","2","3","*","0",".","c","/" };
            
            foreach (string key in keys)
            {
                Button b = new Button();
                b.Text = key;
                b.Width = 45; 
                b.Height = 35;
                b.Location = new Point(x, y);
                
                if (key.ToUpper() == "C") 
                {
                    b.Click += (s, e) => { txtCalc.Clear(); };
                    b.BackColor = Color.LightCoral;
                } 
                else 
                {
                    b.Click += (s, e) => { 
                        txtCalc.Text += key; 
                        txtCalc.Focus(); 
                        txtCalc.SelectionStart = txtCalc.Text.Length; 
                    };
                }
                panelCalculator.Controls.Add(b);

                x += 50;
                if (x > 160) { x = 10; y += 40; }
            }

            // Create Equal Button
            btnEqual = new Button();
            btnEqual.Text = "=";
            btnEqual.Width = 195; 
            btnEqual.Height = 35;
            btnEqual.Location = new Point(10, y + 5); 
            btnEqual.BackColor = Color.LightGreen; 
            btnEqual.Click += btnCalcEqual_Click;
            panelCalculator.Controls.Add(btnEqual);
        }

        // --- EVENT HANDLERS ---

        private void cmbShippingRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbShippingRate.SelectedItem.ToString() == "Custom") {
                txtCustomRate.Enabled = true;
            } else {
                txtCustomRate.Enabled = false;
                txtCustomRate.Text = "";
            }
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            lblOutput.Text = "";
            try
            {
                double inputAmount = double.Parse(txtTK.Text);
                
                // Logic: Convert Currency if Yuan is selected
                double tk = inputAmount;
                string currency = cmbCurrency.SelectedItem.ToString();
                
                if (currency == "Yuan")
                {
                    tk = inputAmount * exchangeRate;
                }

                double weightInput = double.Parse(txtKG.Text);
                
                double rawKg;
                if (cmbUnit.SelectedItem.ToString() == "Gram") rawKg = weightInput / 1000.0;
                else rawKg = weightInput;

                double shippingKg = rawKg;
                if (chkAdd400.Checked) shippingKg += 0.400; 

                // 1. Get Suggestion %
                double suggestedProfit = GetSuggestedProfit(rawKg, tk);

                double profitPercent = 0;
                if (txtProfit.Text == "") profitPercent = suggestedProfit;
                else double.TryParse(txtProfit.Text, out profitPercent);

                double shippingRate = 850;
                if (cmbShippingRate.SelectedItem.ToString() == "Custom")
                {
                    if (!double.TryParse(txtCustomRate.Text, out shippingRate)) {
                        MessageBox.Show("Please enter valid custom shipping rate!"); return;
                    }
                }

                // 2. Calculate Costs
                double totalCost = (shippingKg * shippingRate) + tk;
                if (chkAdd200.Checked) totalCost += 200;

                // 3. Apply Profit & Rounding
                double profit = (totalCost * profitPercent) / 100;
                double originalFinalPrice = totalCost + profit;

                int lastTwoDigits = ((int)originalFinalPrice) % 100;
                double addAmount;
                if (lastTwoDigits < 50) addAmount = 50 - lastTwoDigits;
                else addAmount = 99 - lastTwoDigits;

                double sellingPrice = originalFinalPrice + addAmount;
                double newProfit = sellingPrice - totalCost;
                double newProfitPercent = (newProfit / totalCost) * 100;

                string currencyLabel = currency == "Yuan" ? $" ({inputAmount} Yuan)" : "";
                
                lblOutput.Text =
                    $"Base Price: {tk:F0} BDT{currencyLabel}\n" +
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
                    $"Based on {rawKg}kg & {tk:F0} TK:\n" +
                    $"Suggested Profit: {suggestedProfit} %";
            }
            catch
            {
                MessageBox.Show("Please enter valid numbers!");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtTK.Text = ""; txtKG.Text = ""; txtProfit.Text = ""; txtCustomRate.Text = "";
            cmbUnit.SelectedIndex = 0; 
            cmbCurrency.SelectedIndex = 0; 
            cmbShippingRate.SelectedIndex = 0;
            chkAdd400.Checked = false; chkAdd200.Checked = false;
            lblOutput.Text = "--- Developed by Fahim AF ---";
        }

        private void btnToggleCalc_Click(object sender, EventArgs e)
        {
            calculatorVisible = !calculatorVisible;
            panelCalculator.Visible = calculatorVisible;
            btnToggleCalc.Text = calculatorVisible ? "Hide Calculator" : "Show Calculator";
        }

        // --- CALCULATOR LOGIC ---
        private void TxtCalc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnCalcEqual_Click(this, new EventArgs());
            }
        }

        private void btnCalcEqual_Click(object sender, EventArgs e)
        {
            try {
                var result = new DataTable().Compute(txtCalc.Text, null);
                txtCalc.Text = result.ToString();
                txtCalc.SelectionStart = txtCalc.Text.Length;
            } catch {
                txtCalc.Text = "Error";
            }
        }

        // --- MENU ACTIONS ---
        private void ItemConverter_Click(object sender, EventArgs e)
        {
            Form prompt = new Form() {
                Width = 300, Height = 180,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Yuan to BDT",
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false, MinimizeBox = false
            };
            Label textLabel = new Label() { Left = 20, Top = 20, Text = "Enter Yuan Amount:" };
            TextBox textBox = new TextBox() { Left = 20, Top = 50, Width = 240 };
            Button confirmation = new Button() { Text = "Convert", Left = 160, Width = 100, Top = 90, DialogResult = DialogResult.OK };
            Label resultLabel = new Label() { Left = 20, Top = 95, Text = $"Rate: {exchangeRate}", Width = 140, Font = new Font(this.Font, FontStyle.Bold) };
            
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(resultLabel);
            prompt.AcceptButton = confirmation;

            confirmation.Click += (s, ev) => {
                if(double.TryParse(textBox.Text, out double yuan)){
                    resultLabel.Text = $"= {yuan * exchangeRate:F0} BDT";
                    prompt.DialogResult = DialogResult.None; 
                } else {
                    resultLabel.Text = "Invalid Number";
                }
            };
            prompt.ShowDialog();
        }

        private void ItemGitHub_Click(object sender, EventArgs e)
        {
             try { Process.Start(new ProcessStartInfo("https://forms.gle/9i9uAepaQrGoKwjN8") { UseShellExecute = true }); } catch { }
        }

        private void ItemDarkMode_Click(object sender, EventArgs e)
        {
            isDarkMode = !isDarkMode;
            ToggleTheme(isDarkMode);
        }

        // --- THEME LOGIC/ Dark mode---
        private void ToggleTheme(bool dark)
        {
            Color backColor = dark ? Color.FromArgb(30,30,30) : SystemColors.Control;
            Color foreColor = dark ? Color.White : Color.Black;
            Color textBoxBack = dark ? Color.FromArgb(50,50,50) : Color.White;
            Color btnBack = dark ? Color.FromArgb(60,60,60) : SystemColors.ControlLight;

            this.BackColor = backColor; this.ForeColor = foreColor;

            Control[] inputs = { txtTK, txtKG, txtProfit, txtCustomRate, txtCalc, cmbUnit, cmbCurrency, cmbShippingRate };
            foreach(Control c in inputs) 
            { 
                c.BackColor = textBoxBack; c.ForeColor = foreColor; 
            }

            chkAdd400.ForeColor = foreColor; chkAdd200.ForeColor = foreColor;
            lblOutput.BackColor = backColor; lblOutput.ForeColor = foreColor;
            
            menuStrip1.BackColor = dark ? Color.Black : SystemColors.Control;
            menuStrip1.ForeColor = foreColor;
            menuOptions.ForeColor = foreColor;
            
            itemDarkMode.BackColor = backColor; itemDarkMode.ForeColor = foreColor;
            itemGitHub.BackColor = backColor; itemGitHub.ForeColor = foreColor;
            itemConverter.BackColor = backColor; itemConverter.ForeColor = foreColor;

            Button[] btns = { btnCalc, btnClear, btnToggleCalc };
            foreach(Button b in btns) 
            {
                b.BackColor = btnBack; b.ForeColor = foreColor; b.FlatStyle = FlatStyle.Flat;
            }

            // Dark Mode for Calculator Equal Button
            if (dark) btnEqual.BackColor = Color.Silver; 
            else btnEqual.BackColor = Color.LightGreen;
            btnEqual.ForeColor = Color.Black; 

            // while dark mode other text color to red
            if (dark)
            {
                lblOutput.ForeColor = Color.Red;
            }
        }

        // --- PROFIT MATRIX ---
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

    }
}