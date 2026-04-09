namespace 坡度基滤波
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            bl = new Button();
            bf = new Button();
            ib = new Label();
            ddd = new Button();
            SuspendLayout();
            // 
            // bl
            // 
            bl.Location = new Point(12, 110);
            bl.Name = "bl";
            bl.Size = new Size(379, 328);
            bl.TabIndex = 0;
            bl.Text = "输入点云";
            bl.UseVisualStyleBackColor = true;
            bl.Click += bl_Click;
            // 
            // bf
            // 
            bf.Location = new Point(397, 176);
            bf.Name = "bf";
            bf.Size = new Size(225, 262);
            bf.TabIndex = 1;
            bf.Text = "进行滤波";
            bf.UseVisualStyleBackColor = true;
            bf.Click += bf_Click;
            // 
            // ib
            // 
            ib.AutoSize = true;
            ib.Location = new Point(262, 52);
            ib.Name = "ib";
            ib.Size = new Size(297, 28);
            ib.TabIndex = 2;
            ib.Text = "只能输入txt文件，导出也是txt";
            // 
            // ddd
            // 
            ddd.Location = new Point(628, 266);
            ddd.Name = "ddd";
            ddd.Size = new Size(160, 172);
            ddd.TabIndex = 3;
            ddd.Text = "导出地面点";
            ddd.UseVisualStyleBackColor = true;
            ddd.Click += ddd_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(13F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ddd);
            Controls.Add(ib);
            Controls.Add(bf);
            Controls.Add(bl);
            Name = "MainForm";
            Text = "MainForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button bl;
        private Button bf;
        private Label ib;
        private Button ddd;
    }
}