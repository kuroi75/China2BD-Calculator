using System.Runtime.InteropServices;

namespace PriceCalculatorApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTK = new System.Windows.Forms.Label();
            this.lblKG = new System.Windows.Forms.Label();
            this.lblProfit = new System.Windows.Forms.Label();
            this.txtTK = new System.Windows.Forms.TextBox();
            this.txtKG = new System.Windows.Forms.TextBox();
            this.txtProfit = new System.Windows.Forms.TextBox();
            this.chkAdd400 = new System.Windows.Forms.CheckBox();
            this.calculateButton = new System.Windows.Forms.Button();
            this.outputBox = new System.Windows.Forms.RichTextBox();
            this.cmbUnit = new System.Windows.Forms.ComboBox();

            this.SuspendLayout();

            // 
            // lblTK
            // 
            this.lblTK.AutoSize = true;
            this.lblTK.Location = new System.Drawing.Point(25, 25);
            this.lblTK.Name = "lblTK";
            this.lblTK.Size = new System.Drawing.Size(36, 16);
            this.lblTK.Text = "BDT:";

            // 
            // txtTK
            // 
            this.txtTK.Location = new System.Drawing.Point(120, 22);
            this.txtTK.Name = "txtTK";
            this.txtTK.Size = new System.Drawing.Size(150, 22);

            // 
            // lblKG
            // 
            this.lblKG.AutoSize = true;
            this.lblKG.Location = new System.Drawing.Point(25, 65);
            this.lblKG.Name = "lblKG";
            this.lblKG.Size = new System.Drawing.Size(63, 16);
            this.lblKG.Text = "KG / G:";

            // 
            // txtKG
            // 
            this.txtKG.Location = new System.Drawing.Point(120, 62);
            this.txtKG.Name = "txtKG";
            this.txtKG.Size = new System.Drawing.Size(150, 22);

            // 
            // cmbUnit
            // 
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.Items.AddRange(new object[] { "KG", "Gram" });
            this.cmbUnit.Location = new System.Drawing.Point(120, 90);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(150, 22);
            this.cmbUnit.SelectedIndex = 0;

            // 
            // lblProfit
            // 
            this.lblProfit.AutoSize = true;
            this.lblProfit.Location = new System.Drawing.Point(25, 130);
            this.lblProfit.Name = "lblProfit";
            this.lblProfit.Size = new System.Drawing.Size(62, 16);
            this.lblProfit.Text = "Profit %:";

            // 
            // txtProfit
            // 
            this.txtProfit.Location = new System.Drawing.Point(120, 127);
            this.txtProfit.Name = "txtProfit";
            this.txtProfit.Size = new System.Drawing.Size(150, 22);

            // 
            // chkAdd400
            // 
            this.chkAdd400.AutoSize = true;
            this.chkAdd400.Location = new System.Drawing.Point(28, 165);
            this.chkAdd400.Name = "chkAdd400";
            this.chkAdd400.Size = new System.Drawing.Size(185, 20);
            this.chkAdd400.Text = "Add extra 400g automatically";

            // 
            // calculateButton
            // 
            this.calculateButton.Location = new System.Drawing.Point(28, 200);
            this.calculateButton.Name = "calculateButton";
            this.calculateButton.Size = new System.Drawing.Size(242, 35);
            this.calculateButton.Text = "Calculate";
            this.calculateButton.UseVisualStyleBackColor = true;
            this.calculateButton.Click += new System.EventHandler(this.calculateButton_Click);

            // at the end type a cadit message
            outputBox.AppendText("--- Developed by Fahim AF ---");

            

            // 
            // outputBox
            // 
            this.outputBox.Location = new System.Drawing.Point(300, 20);
            this.outputBox.Name = "outputBox";
            this.outputBox.Size = new System.Drawing.Size(450, 350);
            this.outputBox.ReadOnly = true;
            this.outputBox.Anchor = ((System.Windows.Forms.AnchorStyles)
            ((((System.Windows.Forms.AnchorStyles.Top
            | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));

            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(780, 400);
            this.Controls.Add(this.lblTK);
            this.Controls.Add(this.txtTK);
            this.Controls.Add(this.lblKG);
            this.Controls.Add(this.txtKG);
            this.Controls.Add(this.cmbUnit);
            this.Controls.Add(this.lblProfit);
            this.Controls.Add(this.txtProfit);
            this.Controls.Add(this.chkAdd400);
            this.Controls.Add(this.calculateButton);
            this.Controls.Add(this.outputBox);
            this.Name = "Form1";
            this.Text = "Price Calculator";

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTK;
        private System.Windows.Forms.Label lblKG;
        private System.Windows.Forms.Label lblProfit;
        private System.Windows.Forms.TextBox txtTK;
        private System.Windows.Forms.TextBox txtKG;
        private System.Windows.Forms.TextBox txtProfit;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.CheckBox chkAdd400;
        private System.Windows.Forms.Button calculateButton;
        private System.Windows.Forms.RichTextBox outputBox;
        
    }
}
