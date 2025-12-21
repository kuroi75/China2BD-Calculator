namespace PriceCalculatorGUI
{
        partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)

        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }
        private void InitializeComponent()

        {
            this.txtTK = new System.Windows.Forms.TextBox();
            this.txtTK.Name = "BDT";
            this.txtTK.Width = 180;
            this.txtTK.Location = new System.Drawing.Point(20, 30);
            this.txtTK.PlaceholderText = "BDT Amount";

            this.txtKG = new System.Windows.Forms.TextBox();
            this.txtProfit = new System.Windows.Forms.TextBox();

            this.txtProfit.Name = "txtProfit";
            this.txtProfit.Width = 180;
            this.txtProfit.Location = new System.Drawing.Point(20, 110);
            this.txtProfit.PlaceholderText = "Desired Profit (%)";

            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.chkAdd400 = new System.Windows.Forms.CheckBox();
            this.chkAdd200 = new System.Windows.Forms.CheckBox();
            this.btnCalc = new System.Windows.Forms.Button();
            this.lblOutput = new System.Windows.Forms.Label();
            this.btnToggleCalc = new System.Windows.Forms.Button();
            this.panelCalculator = new System.Windows.Forms.Panel();
            this.txtCalc = new System.Windows.Forms.TextBox();
            this.SuspendLayout();

            // === LEFT SIDE ===
            txtTK.Location = new System.Drawing.Point(20, 30);
            txtTK.Width = 180;
            txtKG.Location = new System.Drawing.Point(20, 70);
            txtKG.Width = 120;
            cmbUnit.Location = new System.Drawing.Point(150, 70);
            cmbUnit.Width = 50;
            cmbUnit.Items.AddRange(new object[] { "KG", "Gram" });
            cmbUnit.SelectedIndex = 0;
            txtProfit.Location = new System.Drawing.Point(20, 110);
            txtProfit.Width = 180;
            chkAdd400.Text = "Add extra 400g";
            chkAdd400.Location = new System.Drawing.Point(20, 150);

            chkAdd200.Text = "Add +200 BDT";
            chkAdd200.Location = new System.Drawing.Point(20, 175);
            chkAdd200.AutoSize = true;
            this.Controls.Add(chkAdd200); 

            btnCalc.Text = "Calculate";
            btnCalc.Location = new System.Drawing.Point(20, 210);
            btnCalc.Width = 180;
            btnCalc.Click += btnCalc_Click;
            
           
            // Clear button
            Button btnClear = new Button();
            btnClear.Text = "Clear Output";
            btnClear.Location = new System.Drawing.Point(45, 300);
            btnClear.Width = 120;
            btnClear.Height = btnCalc.Height;
            btnClear.Click += (s, e) => { lblOutput.Text = "--- Developed by Fahim AF ---"; };
            this.Controls.Add(btnClear);

            // hyperlink

            Button btnLink = new Button();
            btnLink.Text = "link";
            btnLink.Location = new System.Drawing.Point(300, 330);
            btnLink.Width = 120;
            btnLink.Height = btnCalc.Height;
            btnLink.Click += (s, e) => { System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://github.com/kuroi75/China2BD-Calculator"
            , UseShellExecute = true
            }); };
            this.Controls.Add(btnLink);

            lblOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            lblOutput.Location = new System.Drawing.Point(220, 20);
            lblOutput.Size = new System.Drawing.Size(330, 300);
            lblOutput.Text = "--- Developed by Fahim AF ---";
            btnToggleCalc.Text = "Hide Calculator";
            btnToggleCalc.Location = new System.Drawing.Point(20, 255);
            btnToggleCalc.Width = 180;
            btnToggleCalc.Click += btnToggleCalc_Click;

            // === RIGHT CALCULATOR PANEL ===
            panelCalculator.Location = new System.Drawing.Point(570, 20);
            panelCalculator.Size = new System.Drawing.Size(220, 300);
            panelCalculator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtCalc.Location = new System.Drawing.Point(10, 10);
            txtCalc.Width = 200;
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
            btnEqual.Width = 195; // Span the width
            btnEqual.Height = 35;
            btnEqual.Location = new System.Drawing.Point(10, y + 5); // Place just below the last row
            btnEqual.BackColor = System.Drawing.Color.LightGreen;
            btnEqual.Click += btnCalcEqual_Click;
            panelCalculator.Controls.Add(btnEqual);

            // === FORM CONTROLS ===
            this.Controls.Add(txtTK);
            this.Controls.Add(txtKG);
            this.Controls.Add(cmbUnit);
            this.Controls.Add(txtProfit);
            this.Controls.Add(chkAdd400);
            this.Controls.Add(chkAdd200);
            this.Controls.Add(btnCalc);
            this.Controls.Add(lblOutput);
            this.Controls.Add(btnToggleCalc);
            this.Controls.Add(panelCalculator);
            this.Text = "Price Calculator";
            this.ClientSize = new System.Drawing.Size(820, 360);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }



        private void btnCalcKey_Click(object sender, EventArgs e)

        {

            throw new NotImplementedException();

        }



        private System.Windows.Forms.TextBox txtTK;

        private System.Windows.Forms.TextBox txtKG;

        private System.Windows.Forms.TextBox txtProfit;

        private System.Windows.Forms.ComboBox cmbUnit;

        private System.Windows.Forms.CheckBox chkAdd400;

        private System.Windows.Forms.CheckBox chkAdd200; // ✅ DECLARED

        private System.Windows.Forms.Button btnCalc;

        private System.Windows.Forms.Label lblOutput;

        private System.Windows.Forms.Button btnToggleCalc;



        private System.Windows.Forms.Panel panelCalculator;
        private System.Windows.Forms.TextBox txtCalc;

    }

}