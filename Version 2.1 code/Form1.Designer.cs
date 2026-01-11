namespace PriceCalculatorGUI
{
    partial class Form1
    {
            // Declare controls
        private System.Windows.Forms.TextBox txtTK;
        private System.Windows.Forms.TextBox txtKG;
        private System.Windows.Forms.TextBox txtProfit;
        private System.Windows.Forms.TextBox txtCustomRate;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.ComboBox cmbCurrency;
        private System.Windows.Forms.ComboBox cmbShippingRate;
        private System.Windows.Forms.CheckBox chkAdd400;
        private System.Windows.Forms.CheckBox chkAdd200;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnToggleCalc;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.Panel panelCalculator;
        private System.Windows.Forms.TextBox txtCalc;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuOptions;
        private System.Windows.Forms.ToolStripMenuItem itemDarkMode;
        private System.Windows.Forms.ToolStripMenuItem itemGitHub;
        private System.Windows.Forms.ToolStripMenuItem itemConverter;
        private System.ComponentModel.IContainer components = null;
    
        // Dispose method to clean up resources
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.txtTK = new System.Windows.Forms.TextBox();
            this.txtKG = new System.Windows.Forms.TextBox();
            this.txtProfit = new System.Windows.Forms.TextBox();
            this.txtCustomRate = new System.Windows.Forms.TextBox();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.cmbCurrency = new System.Windows.Forms.ComboBox();
            this.cmbShippingRate = new System.Windows.Forms.ComboBox();
            this.chkAdd400 = new System.Windows.Forms.CheckBox();
            this.chkAdd200 = new System.Windows.Forms.CheckBox();
            this.btnCalc = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnToggleCalc = new System.Windows.Forms.Button();
            this.lblOutput = new System.Windows.Forms.Label();
            this.panelCalculator = new System.Windows.Forms.Panel();
            this.txtCalc = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.itemConverter = new System.Windows.Forms.ToolStripMenuItem();
            this.itemDarkMode = new System.Windows.Forms.ToolStripMenuItem();
            this.itemGitHub = new System.Windows.Forms.ToolStripMenuItem();
            this.panelCalculator.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();

            // txtTK (Price Input)
            this.txtTK.Location = new System.Drawing.Point(20, 40);
            this.txtTK.Name = "txtTK";
            this.txtTK.Size = new System.Drawing.Size(120, 23);
            this.txtTK.PlaceholderText = "Amount";

            // cmbCurrency (BDT/Yuan)
            this.cmbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurrency.FormattingEnabled = true;
            this.cmbCurrency.Items.AddRange(new object[] { "BDT", "Yuan" });
            this.cmbCurrency.Location = new System.Drawing.Point(150, 40);
            this.cmbCurrency.Name = "cmbCurrency";
            this.cmbCurrency.Size = new System.Drawing.Size(60, 23);

            // txtKG
            this.txtKG.Location = new System.Drawing.Point(20, 80);
            this.txtKG.Name = "txtKG";
            this.txtKG.Size = new System.Drawing.Size(120, 23);
            this.txtKG.PlaceholderText = "Weight";

            // cmbUnit
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Items.AddRange(new object[] { "KG", "Gram" });
            this.cmbUnit.Location = new System.Drawing.Point(150, 80);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(60, 23);

            // txtProfit
            this.txtProfit.Location = new System.Drawing.Point(20, 120);
            this.txtProfit.Name = "txtProfit";
            this.txtProfit.Size = new System.Drawing.Size(190, 23);
            this.txtProfit.PlaceholderText = "Profit (%)";

            // cmbShippingRate
            this.cmbShippingRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbShippingRate.FormattingEnabled = true;
            this.cmbShippingRate.Location = new System.Drawing.Point(20, 160);
            this.cmbShippingRate.Name = "cmbShippingRate";
            this.cmbShippingRate.Size = new System.Drawing.Size(190, 23);
            this.cmbShippingRate.SelectedIndexChanged += new System.EventHandler(this.cmbShippingRate_SelectedIndexChanged);

            // txtCustomRate
            this.txtCustomRate.Location = new System.Drawing.Point(20, 190);
            this.txtCustomRate.Name = "txtCustomRate";
            this.txtCustomRate.Size = new System.Drawing.Size(190, 23);
            this.txtCustomRate.PlaceholderText = "Custom rate (BDT/kg)";

            // chkAdd400
            this.chkAdd400.AutoSize = true;
            this.chkAdd400.Location = new System.Drawing.Point(20, 225);
            this.chkAdd400.Name = "chkAdd400";
            this.chkAdd400.Size = new System.Drawing.Size(107, 19);
            this.chkAdd400.Text = "Add extra 400g";
            this.chkAdd400.UseVisualStyleBackColor = true;

            // chkAdd200
            this.chkAdd200.AutoSize = true;
            this.chkAdd200.Location = new System.Drawing.Point(20, 250);
            this.chkAdd200.Name = "chkAdd200";
            this.chkAdd200.Size = new System.Drawing.Size(104, 19);
            this.chkAdd200.Text = "Add +200 BDT";
            this.chkAdd200.UseVisualStyleBackColor = true;

            // btnCalc
            this.btnCalc.Location = new System.Drawing.Point(20, 280);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(190, 30);
            this.btnCalc.Text = "Calculate";
            this.btnCalc.UseVisualStyleBackColor = true;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);

            // btnClear
            this.btnClear.Location = new System.Drawing.Point(20, 315);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(190, 30);
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            // btnToggleCalc
            this.btnToggleCalc.Location = new System.Drawing.Point(20, 350);
            this.btnToggleCalc.Name = "btnToggleCalc";
            this.btnToggleCalc.Size = new System.Drawing.Size(190, 30);
            this.btnToggleCalc.Text = "Hide Calculator";
            this.btnToggleCalc.UseVisualStyleBackColor = true;
            this.btnToggleCalc.Click += new System.EventHandler(this.btnToggleCalc_Click);

            // lblOutput
            this.lblOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOutput.Location = new System.Drawing.Point(230, 30);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(330, 350);
            this.lblOutput.Text = "--- Developed by Fahim AF ---";

            // panelCalculator
            this.panelCalculator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCalculator.Controls.Add(this.txtCalc);
            this.panelCalculator.Location = new System.Drawing.Point(580, 30);
            this.panelCalculator.Name = "panelCalculator";
            this.panelCalculator.Size = new System.Drawing.Size(220, 350);

            // txtCalc
            this.txtCalc.Location = new System.Drawing.Point(10, 10);
            this.txtCalc.Name = "txtCalc";
            this.txtCalc.Size = new System.Drawing.Size(200, 23);
            this.txtCalc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtCalc_KeyDown);

            // menuStrip1
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.menuOptions });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(830, 24);
            this.menuStrip1.Text = "menuStrip1";

            // menuOptions
            this.menuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemConverter,
            this.itemDarkMode,
            this.itemGitHub});
            this.menuOptions.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.menuOptions.Name = "menuOptions";
            this.menuOptions.Text = "⋮";

            // itemConverter
            this.itemConverter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.itemConverter.Name = "itemConverter";
            this.itemConverter.Text = "Currency Converter";
            this.itemConverter.Click += new System.EventHandler(this.ItemConverter_Click);

            // itemDarkMode
            this.itemDarkMode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.itemDarkMode.Name = "itemDarkMode";
            this.itemDarkMode.Text = "Dark Mode";
            this.itemDarkMode.Click += new System.EventHandler(this.ItemDarkMode_Click);

            // itemGitHub
            this.itemGitHub.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.itemGitHub.Name = "itemGitHub";
            this.itemGitHub.Text = "Link";
            this.itemGitHub.Click += new System.EventHandler(this.ItemGitHub_Click);

            // main
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 400);
            this.Controls.Add(this.panelCalculator);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.btnToggleCalc);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnCalc);
            this.Controls.Add(this.chkAdd200);
            this.Controls.Add(this.chkAdd400);
            this.Controls.Add(this.txtCustomRate);
            this.Controls.Add(this.cmbShippingRate);
            this.Controls.Add(this.txtProfit);
            this.Controls.Add(this.cmbUnit);
            this.Controls.Add(this.txtKG);
            this.Controls.Add(this.cmbCurrency);
            this.Controls.Add(this.txtTK);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Price Calculator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelCalculator.ResumeLayout(false);
            this.panelCalculator.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

    }
}