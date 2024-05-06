namespace Zombieshooter
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            picShotgun = new PictureBox();
            picRevolver = new PictureBox();
            pictureBox4 = new PictureBox();
            timerMove = new System.Windows.Forms.Timer(components);
            label1 = new Label();
            timerSpawn = new System.Windows.Forms.Timer(components);
            buttonStart = new Button();
            labelScore = new Label();
            ((System.ComponentModel.ISupportInitialize)picShotgun).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picRevolver).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            SuspendLayout();
            // 
            // picShotgun
            // 
            picShotgun.BackColor = Color.Transparent;
            picShotgun.Image = Properties.Resources.sawedoff;
            picShotgun.Location = new Point(130, 144);
            picShotgun.Name = "picShotgun";
            picShotgun.Size = new Size(439, 110);
            picShotgun.SizeMode = PictureBoxSizeMode.Zoom;
            picShotgun.TabIndex = 1;
            picShotgun.TabStop = false;
            picShotgun.Click += picShotgun_Click;
            // 
            // picRevolver
            // 
            picRevolver.BackColor = Color.Transparent;
            picRevolver.Image = Properties.Resources.revolver;
            picRevolver.Location = new Point(190, -3);
            picRevolver.Name = "picRevolver";
            picRevolver.Size = new Size(242, 117);
            picRevolver.SizeMode = PictureBoxSizeMode.Zoom;
            picRevolver.TabIndex = 2;
            picRevolver.TabStop = false;
            picRevolver.Click += picRevolver_Click;
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.Transparent;
            pictureBox4.Image = Properties.Resources.zombie;
            pictureBox4.Location = new Point(1163, 12);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(299, 381);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 3;
            pictureBox4.TabStop = false;
            pictureBox4.Visible = false;
            // 
            // timerMove
            // 
            timerMove.Tick += timerMove_Tick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Stencil", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.White;
            label1.Location = new Point(1163, 12);
            label1.Name = "label1";
            label1.Size = new Size(102, 29);
            label1.TabIndex = 4;
            label1.Text = "HP: 328";
            label1.Visible = false;
            // 
            // timerSpawn
            // 
            timerSpawn.Interval = 5000;
            timerSpawn.Tick += timerSpawn_Tick;
            // 
            // buttonStart
            // 
            buttonStart.BackColor = Color.Black;
            buttonStart.Font = new Font("Stencil", 14F, FontStyle.Regular, GraphicsUnit.Point);
            buttonStart.ForeColor = Color.White;
            buttonStart.Location = new Point(3, 292);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(219, 36);
            buttonStart.TabIndex = 5;
            buttonStart.Text = "Start shootin'";
            buttonStart.UseVisualStyleBackColor = false;
            buttonStart.Click += buttonStart_Click;
            // 
            // labelScore
            // 
            labelScore.AutoSize = true;
            labelScore.BackColor = Color.Transparent;
            labelScore.Font = new Font("Stencil", 14F, FontStyle.Regular, GraphicsUnit.Point);
            labelScore.ForeColor = Color.White;
            labelScore.Location = new Point(255, 297);
            labelScore.Name = "labelScore";
            labelScore.Size = new Size(114, 29);
            labelScore.TabIndex = 6;
            labelScore.Text = "SCORE: 0";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            BackgroundImage = Properties.Resources.dualwield3;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(1454, 340);
            Controls.Add(labelScore);
            Controls.Add(buttonStart);
            Controls.Add(label1);
            Controls.Add(pictureBox4);
            Controls.Add(picRevolver);
            Controls.Add(picShotgun);
            ForeColor = SystemColors.ControlText;
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)picShotgun).EndInit();
            ((System.ComponentModel.ISupportInitialize)picRevolver).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox picShotgun;
        private PictureBox picRevolver;
        private PictureBox pictureBox4;
        private System.Windows.Forms.Timer timerMove;
        private Label label1;
        private System.Windows.Forms.Timer timerSpawn;
        private Button buttonStart;
        private Label labelScore;
    }
}