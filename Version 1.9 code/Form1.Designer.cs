using System;
using System.Drawing;
using System.Windows.Forms;

namespace PriceCalculatorGUI
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private TextBox txtTK;
        private TextBox txtKG;
        private TextBox txtProfit;
        private TextBox txtCustomRate;
        private ComboBox cmbUnit;
        private ComboBox cmbShippingRate;
        private CheckBox chkAdd400;
        private CheckBox chkAdd200;
        private Button btnCalc;
        private Button btnClear;
        private Button btnToggleCalc;
        private Label lblOutput;
        private Panel panelCalculator;
        private TextBox txtCalc;

        private void InitializeComponent()
        {
            this.txtTK = new TextBox();
            this.txtKG = new TextBox();
            this.txtProfit = new TextBox();
            this.txtCustomRate = new TextBox();
            this.cmbUnit = new ComboBox();
            this.cmbShippingRate = new ComboBox();
            this.chkAdd400 = new CheckBox();
            this.chkAdd200 = new CheckBox();
            this.btnCalc = new Button();
            this.btnClear = new Button();
            this.btnToggleCalc = new Button();
            this.lblOutput = new Label();
            this.panelCalculator = new Panel();
            this.txtCalc = new TextBox();

            // txtTK
            txtTK.Location = new Point(20, 30);
            txtTK.Width = 180;
            txtTK.PlaceholderText = "BDT Amount";

            // txtKG
            txtKG.Location = new Point(20, 70);
            txtKG.Width = 120;
            txtKG.PlaceholderText = "Weight";

            // cmbUnit
            cmbUnit.Location = new Point(150, 70);
            cmbUnit.Width = 50;
            cmbUnit.Items.AddRange(new object[] { "KG", "Gram" });
            cmbUnit.SelectedIndex = 0;

            // txtProfit
            txtProfit.Location = new Point(20, 110);
            txtProfit.Width = 180;
            txtProfit.PlaceholderText = "Profit (%)";

            // cmbShippingRate
            cmbShippingRate.Location = new Point(20, 150);
            cmbShippingRate.Width = 180;
            cmbShippingRate.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbShippingRate.SelectedIndexChanged += cmbShippingRate_SelectedIndexChanged;

            // txtCustomRate
            txtCustomRate.Location = new Point(20, 180);
            txtCustomRate.Width = 180;
            txtCustomRate.PlaceholderText = "Custom rate (BDT/kg)";

            // chkAdd400
            chkAdd400.Text = "Add extra 400g";
            chkAdd400.Location = new Point(20, 215);

            // chkAdd200
            chkAdd200.Text = "Add +200 BDT";
            chkAdd200.Location = new Point(20, 240);

            // btnCalc
            btnCalc.Text = "Calculate";
            btnCalc.Location = new Point(20, 270);
            btnCalc.Width = 180;
            btnCalc.Click += btnCalc_Click;

            // btnClear
            btnClear.Text = "Clear";
            btnClear.Location = new Point(20, 305);
            btnClear.Width = 180;
            btnClear.Click += btnClear_Click;

            // btnToggleCalc
            btnToggleCalc.Text = "Hide Calculator";
            btnToggleCalc.Location = new Point(20, 340);
            btnToggleCalc.Width = 180;
            btnToggleCalc.Click += btnToggleCalc_Click;

            // lblOutput
            lblOutput.Location = new Point(220, 20);
            lblOutput.Size = new Size(330, 350);
            lblOutput.BorderStyle = BorderStyle.FixedSingle;
            lblOutput.Text = "--- Developed by Fahim AF ---";

            // panelCalculator
            panelCalculator.Location = new Point(570, 20);
            panelCalculator.Size = new Size(220, 350);
            panelCalculator.BorderStyle = BorderStyle.FixedSingle;

            txtCalc.Location = new Point(10, 10);
            txtCalc.Width = 200;
            panelCalculator.Controls.Add(txtCalc);

            // form
            this.Controls.Add(txtTK);
            this.Controls.Add(txtKG);
            this.Controls.Add(cmbUnit);
            this.Controls.Add(txtProfit);
            this.Controls.Add(cmbShippingRate);
            this.Controls.Add(txtCustomRate);
            this.Controls.Add(chkAdd400);
            this.Controls.Add(chkAdd200);
            this.Controls.Add(btnCalc);
            this.Controls.Add(btnClear);
            this.Controls.Add(btnToggleCalc);
            this.Controls.Add(lblOutput);
            this.Controls.Add(panelCalculator);

            this.Text = "Price Calculator";
            this.ClientSize = new Size(820, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Load += Form1_Load;


            // === RIGHT CALCULATOR PANEL ===
            panelCalculator.Location = new System.Drawing.Point(570, 20);
            panelCalculator.Size = new System.Drawing.Size(220, 350);
            panelCalculator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            //display textbox
            txtCalc.Location = new System.Drawing.Point(10, 10);
            txtCalc.Width = 400;
            panelCalculator.Controls.Add(txtCalc);

            // Buttons 0–9 + operators
            int x = 10, y = 50;
            string[] keys = { "7","8","9","+","4","5","6","-","1","2","3","*","0",".","c","/" };
            foreach (string key in keys)
            {
                System.Windows.Forms.Button b = new System.Windows.Forms.Button();
                b.Text = key;
                b.Width = 45;
                b.Height = 35;
                b.Location = new System.Drawing.Point(x, y);
               
                if (key.ToUpper() == "C")
                {
                    b.Click += (s, e) => { txtCalc.Clear(); };
                    b.BackColor = System.Drawing.Color.LightCoral;
                    panelCalculator.Controls.Add(b);
                }
                else
                {
                     b.Click += (s, e) => { txtCalc.Text += key; };
                    panelCalculator.Controls.Add(b);
                }
                panelCalculator.Controls.Add(b);

                x += 50;
                if (x > 160)
                {
                    x = 10;
                    y += 40;
                }
            }
            // Equal button
            System.Windows.Forms.Button btnEqual = new System.Windows.Forms.Button();
            btnEqual.Text = "=";
            btnEqual.Width = 195; 
            btnEqual.Height = 35;
            btnEqual.Location = new System.Drawing.Point(10, y + 5); 
            btnEqual.BackColor = System.Drawing.Color.LightGreen;
            btnEqual.Click += btnCalcEqual_Click;
            panelCalculator.Controls.Add(btnEqual);
        }
    }
}
