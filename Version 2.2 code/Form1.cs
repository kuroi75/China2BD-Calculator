using System;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace PriceCalculatorGUI
{
    public partial class Form1 : Form
    {
        // === SETTINGS ===
        double exchangeRate = 19.0; 

        bool calculatorVisible = true;
        bool isDarkMode = false;
        
        private Button btnEqual; 

        public Form1()
        {
            InitializeComponent();
            GenerateCalculatorButtons(); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbUnit.SelectedIndex = 0; 
            cmbCurrency.SelectedIndex = 0; 

            cmbShippingRate.Items.Add("Default (850 BDT)");
            cmbShippingRate.Items.Add("Custom");
            cmbShippingRate.SelectedIndex = 0;

            txtCustomRate.Enabled = false;

            //  BIGGER DISPLAY FOR CALCULATOR 
            txtCalc.Font = new Font("Segoe UI", 14F, FontStyle.Regular); 
            txtCalc.Height = 35; 
        }

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
                //  Get Quantity
                double quantity = 1;
                if (!string.IsNullOrEmpty(txtQuantity.Text))
                {
                    double.TryParse(txtQuantity.Text, out quantity);
                    if (quantity < 1) quantity = 1;
                }

                // Get Input Price
                double inputAmount = double.Parse(txtTK.Text);
                double tk = inputAmount;
                string currency = cmbCurrency.SelectedItem.ToString();
                
                // Convert to BDT if needed
                if (currency == "Yuan")
                {
                    tk = inputAmount * exchangeRate;
                }

                // 3. Get Input Weight
                double weightInput = double.Parse(txtKG.Text);
                double rawKg;
                if (cmbUnit.SelectedItem.ToString() == "Gram") rawKg = weightInput / 1000.0;
                else rawKg = weightInput;

                // APPLY QUANTITY
                double totalBasePrice = tk * quantity;
                double totalWeight = rawKg * quantity;

                // Calculate Shipping Weight
                double shippingKg = totalWeight;
                if (chkAdd400.Checked) shippingKg += 0.400; // Add 400g to TOTAL package

                // Calculate Profit %
                double suggestedProfit = GetSuggestedProfit(rawKg, tk); // Based on SINGLE unit stats
                double profitPercent = 0;
                if (txtProfit.Text == "") profitPercent = suggestedProfit;
                else double.TryParse(txtProfit.Text, out profitPercent);

                // Get Shipping Rate
                double shippingRate = 850;
                if (cmbShippingRate.SelectedItem.ToString() == "Custom")
                {
                    if (!double.TryParse(txtCustomRate.Text, out shippingRate)) {
                        MessageBox.Show("Please enter valid custom shipping rate!"); return;
                    }
                }

                //. Final Calculations
                double totalCost = (shippingKg * shippingRate) + totalBasePrice;
                if (chkAdd200.Checked) totalCost += 200; // Add 200tk handling to TOTAL package

                double profit = (totalCost * profitPercent) / 100;
                double originalFinalPrice = totalCost + profit;

                // Rounding Logic
                int lastTwoDigits = ((int)originalFinalPrice) % 100;
                double addAmount;
                if (lastTwoDigits < 50) addAmount = 49 - lastTwoDigits;
                else addAmount = 99 - lastTwoDigits;

                double sellingPrice = originalFinalPrice + addAmount;
                double newProfit = sellingPrice - totalCost;
                double newProfitPercent = (newProfit / totalCost) * 100;
                
                // Per Unit Calculation for display
                double pricePerUnit = sellingPrice / quantity;

                string currencyLabel = currency == "Yuan" ? $" ({inputAmount} Yuan)" : "";
                
                // Display Output
                lblOutput.Text =
                    $"Quantity: {quantity:F0} pcs\n" +
                    $"Unit Base: {tk:F0} BDT | Unit Weight: {rawKg} kg\n" +
                    $"------------------------------------------------\n" +
                    $"Total Weight (with shipping): {shippingKg:F3} kg\n" +
                    $"Total Cost (Product + Shipping): {totalCost:F0} TK\n" +
                    $"Total Price (w/ Profit): {originalFinalPrice:F0} TK\n" +
                    $"------------------------------------------------\n" +
                    $"Round-up Amount: {addAmount:F0} TK\n" +
                    $"FINAL TOTAL SELLING PRICE: {sellingPrice:F0} TK\n\n" +
                    $">>> PRICE PER UNIT: {pricePerUnit:F0} TK <<<\n\n" + 
                    $"Total Profit: {newProfit:F0} TK\n" +
                    $"Profit Margin: {newProfitPercent:F2} %\n" +
                    $"------------------------------------------------\n" +
                    $"[SUGGESTION]\n" +
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
            txtQuantity.Text = "";
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

        /// Currency Converter Logic
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

        // GitHub Link Logic
        private void ItemGitHub_Click(object sender, EventArgs e)
        {
             try { Process.Start(new ProcessStartInfo("https://github.com/kuroi75/China2BD-Calculator/releases") { UseShellExecute = true }); } catch { }
        }

        // Dark Mode Toggle Logic
        private void ItemDarkMode_Click(object sender, EventArgs e)
        {
            isDarkMode = !isDarkMode;
            ToggleTheme(isDarkMode);
        }

        private void ToggleTheme(bool dark)
        {
            Color backColor = dark ? Color.FromArgb(30,30,30) : SystemColors.Control;
            Color foreColor = dark ? Color.White : Color.Black;
            Color textBoxBack = dark ? Color.FromArgb(50,50,50) : Color.White;
            Color btnBack = dark ? Color.FromArgb(60,60,60) : SystemColors.ControlLight;

            this.BackColor = backColor; this.ForeColor = foreColor;

            Control[] inputs = { txtTK, txtKG, txtProfit, txtCustomRate, txtCalc, cmbUnit, cmbCurrency, cmbShippingRate, txtQuantity };
            foreach(Control c in inputs) { c.BackColor = textBoxBack; c.ForeColor = foreColor; }

            chkAdd400.ForeColor = foreColor; chkAdd200.ForeColor = foreColor;
            lblOutput.BackColor = backColor; lblOutput.ForeColor = foreColor;
            
            menuStrip1.BackColor = dark ? Color.Black : SystemColors.Control;
            menuStrip1.ForeColor = foreColor;
            menuOptions.ForeColor = foreColor;
            
            itemDarkMode.BackColor = backColor; itemDarkMode.ForeColor = foreColor;
            itemGitHub.BackColor = backColor; itemGitHub.ForeColor = foreColor;
            itemConverter.BackColor = backColor; itemConverter.ForeColor = foreColor;

            Button[] btns = { btnCalc, btnClear, btnToggleCalc };
            foreach(Button b in btns) {
                b.BackColor = btnBack; b.ForeColor = foreColor; b.FlatStyle = FlatStyle.Flat;
            }

            if (dark) btnEqual.BackColor = Color.Silver; 
            else btnEqual.BackColor = Color.LightGreen;
            btnEqual.ForeColor = Color.Black; 

            if (dark) lblOutput.ForeColor = Color.Red;
            else lblOutput.ForeColor = Color.Black;
        }

        // PRODUCT image GENERATOR LOGIC 

        private void ItemProductAd_Click(object sender, EventArgs e)
        {
            // Create a dynamic popup form for inputs
            Form inputForm = new Form();
            inputForm.Size = new Size(400, 480);
            inputForm.Text = "Create Product image";
            inputForm.StartPosition = FormStartPosition.CenterParent;
            inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputForm.MaximizeBox = false;

            // UI Controls
            Label lblImage = new Label 
            { 
                Text = "1. Select Product Image:", 
                Left = 20, Top = 20, 
                Width = 340, 
                Font = new Font("Segoe UI", 9, 
                FontStyle.Bold) 
            };

            Button btnSelectImg = new Button 
            { 
                Text = "Browse Image...", 
                Left = 20, 
                Top = 45, 
                Width = 120, 
                Height = 30 
            };
            PictureBox previewBox = new PictureBox 
            { 
                Left = 150, 
                Top = 45, 
                Width = 100, 
                Height = 100, 
                SizeMode = PictureBoxSizeMode.Zoom, 
                BorderStyle = BorderStyle.FixedSingle 
            };
            
            Label lblName = new Label 
            { 
                Text = "2. Product Name (Optional):", 
                Left = 20, 
                Top = 160, 
                Width = 340, 
                Font = new Font("Segoe UI", 9, FontStyle.Bold) 
            };

            TextBox txtName = new TextBox { Left = 20, Top = 185, Width = 340 };

            Label lblSellPrice = new Label 
            { 
                Text = "3. Sell Price (BDT):", 
                Left = 20, Top = 220, Width = 340, 
                Font = new Font("Segoe UI", 9, FontStyle.Bold) 
            };
            TextBox txtSellPrice = new TextBox { Left = 20, Top = 245, Width = 340 };

            CheckBox chkAddBuyPrice = new CheckBox 
            { 
                Text = "Show Original/Buy Price?", 
                Left = 20, 
                Top = 280, 
                Width = 340 
            };

            TextBox txtBuyPrice = new TextBox 
            { 
                Left = 20, 
                Top = 305, 
                Width = 340, 
                Enabled = false, 
                PlaceholderText = "Original Price" 
            };

            Button btnGenerate = new Button 
            { 
                Text = "Save Image", 
                Left = 20,
                Top = 360,
                Width = 340,
                Height = 45,
                BackColor = Color.LightGreen, 
                Font = new Font("Segoe UI", 10, FontStyle.Bold) 
            };

            // Logic Variables
            string selectedImagePath = "";

            btnSelectImg.Click += (s, args) => {
                using (OpenFileDialog ofd = new OpenFileDialog()) {
                    ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                    if (ofd.ShowDialog() == DialogResult.OK) {
                        selectedImagePath = ofd.FileName;
                        previewBox.Image = Image.FromFile(ofd.FileName);
                    }
                }
            };

            chkAddBuyPrice.CheckedChanged += (s, args) => {
                txtBuyPrice.Enabled = chkAddBuyPrice.Checked;
            };

            btnGenerate.Click += (s, args) => {
                if (string.IsNullOrEmpty(selectedImagePath)) {
                    MessageBox.Show("Please select an image first!");
                    return;
                }
                SaveProductImageOverlay(selectedImagePath, txtName.Text, txtSellPrice.Text, chkAddBuyPrice.Checked ? txtBuyPrice.Text : null);
                inputForm.Close();
            };

            // Add controls
            inputForm.Controls.AddRange(new Control[] { lblImage, btnSelectImg, previewBox, lblName, txtName, lblSellPrice, txtSellPrice, chkAddBuyPrice, txtBuyPrice, btnGenerate });
            inputForm.ShowDialog();
        }

        private void SaveProductImageOverlay(string imagePath, string name, string sellPrice, string originalPrice)
        {
            try
            {
                using (Bitmap original = new Bitmap(imagePath))
                using (Bitmap result = new Bitmap(original.Width, original.Height)) 
                using (Graphics g = Graphics.FromImage(result))
                {
                    // High Quality Settings
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                    // Draw Original Image
                    g.DrawImage(original, 0, 0, original.Width, original.Height);

                    // Setup Fonts (Scale based on image width)
                    float scale = original.Width / 800f; 
                    if (scale < 1) scale = 1; 
                    
                    Font nameFont = new Font("Segoe UI", 22 * scale, FontStyle.Bold);
                    Font priceFont = new Font("Segoe UI", 32 * scale, FontStyle.Bold);
                    Font oldPriceFont = new Font("Segoe UI", 16 * scale, FontStyle.Strikeout);

                    // Prepare Text
                    string priceText = $"{sellPrice} TK";
                    string oldPriceText = string.IsNullOrEmpty(originalPrice) ? "" : $"{originalPrice} TK";
                    
                    // Measure Text Sizes
                    SizeF nameSize = string.IsNullOrEmpty(name) ? SizeF.Empty : g.MeasureString(name, nameFont);
                    SizeF priceSize = g.MeasureString(priceText, priceFont);
                    SizeF oldPriceSize = string.IsNullOrEmpty(originalPrice) ? SizeF.Empty : g.MeasureString(oldPriceText, oldPriceFont);

                    // Calculate Box Dimensions
                    float maxWidth = Math.Max(nameSize.Width, Math.Max(priceSize.Width, oldPriceSize.Width));
                    float boxWidth = maxWidth + (80 * scale); 
                    
                    float boxHeight = (30 * scale); // Initial padding
                    if (!string.IsNullOrEmpty(name)) boxHeight += nameSize.Height;
                    boxHeight += priceSize.Height;
                    if (!string.IsNullOrEmpty(originalPrice)) boxHeight += oldPriceSize.Height;
                    boxHeight += (20 * scale); // Bottom padding

                    // Calculate Position (Bottom Center)
                    float boxX = original.Width - boxWidth - (40 * scale);
                    float boxY = original.Height - boxHeight - (50 * scale); 

                    // Draw Semi-Transparent Background
                
                    using (SolidBrush darkBrush = new SolidBrush(Color.FromArgb(180, 0, 0, 0)))
                    {
                        g.FillRectangle(darkBrush, boxX, boxY, boxWidth, boxHeight);
                    }
                    
                    
                    // Draw Text (Centered inside the box)
                    StringFormat centerFormat = new StringFormat { Alignment = StringAlignment.Center };
                    float centerX = boxX + (boxWidth / 2);
                    float currentY = boxY + (15 * scale);

                    // Draw Name (White)
                    if (!string.IsNullOrEmpty(name))
                    {
                        g.DrawString(name, nameFont, Brushes.White, centerX, currentY, centerFormat);
                        currentY += nameSize.Height;
                    }

                    // Draw Sell Price (Gold)
                    g.DrawString(priceText, priceFont, Brushes.Gold, centerX, currentY, centerFormat);
                    currentY += priceSize.Height;

                    // Draw Original Price (Gray)
                    if (!string.IsNullOrEmpty(originalPrice))
                    {
                        g.DrawString(oldPriceText, oldPriceFont, Brushes.LightGray, centerX, currentY, centerFormat);
                    }

                    // Save Logic
                    using (SaveFileDialog sfd = new SaveFileDialog())
                    {
                        sfd.Filter = "JPG Image|*.jpg|PNG Image|*.png";
                        sfd.FileName = "Product_Ad_" + DateTime.Now.Ticks;
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            result.Save(sfd.FileName);
                            MessageBox.Show("Image Saved Successfully!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

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