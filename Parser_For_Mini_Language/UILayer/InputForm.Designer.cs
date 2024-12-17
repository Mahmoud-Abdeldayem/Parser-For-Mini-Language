namespace UILayer
{
    partial class InputForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox1 = new TextBox();
            ExecuteBtn = new Button();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 2);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(1094, 590);
            textBox1.TabIndex = 0;
            // 
            // ExecuteBtn
            // 
            ExecuteBtn.Location = new Point(1142, 23);
            ExecuteBtn.Name = "ExecuteBtn";
            ExecuteBtn.Size = new Size(207, 52);
            ExecuteBtn.TabIndex = 1;
            ExecuteBtn.TabStop = false;
            ExecuteBtn.Text = "<< Execute >>";
            ExecuteBtn.UseVisualStyleBackColor = true;
            ExecuteBtn.Click += button1_Click;
            // 
            // InputForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1430, 604);
            Controls.Add(ExecuteBtn);
            Controls.Add(textBox1);
            Name = "InputForm";
            Text = "Input";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Button ExecuteBtn;
    }
}
