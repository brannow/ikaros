using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Text;
using System.Collections.Generic;
using Ikaros.Objects;

namespace Ikaros.Forms
{
    public partial class DescriptionView : Form
    {
        //double so division keeps decimal points

        private int stepId = 0;
        private int level = 0;
        private float exp = 0.0f;
        private List<String> descriptionLines;
        private List<String> tipLines;
        private Size textAreaMaxLineSize;
        private Font stepFont;
        protected Storage storage;

        private int lineSpacing = 5;
        private int textOffset = 30;
        private const int WM_SIZING = 0x214;
        private const int WMSZ_LEFT = 1;
        private const int WMSZ_RIGHT = 2;
        private const int WMSZ_TOP = 3;
        private const int WMSZ_BOTTOM = 6;
        private const int WM_NCHITTEST = 0x84;
        private const int WS_EX_TOOLWINDOW = 0x00000080;

        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        private const int cGrip = 16;      // Grip size
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_NCHITTEST)
            {  // Trap WM_NCHITTEST
                base.WndProc(ref m);
                if ((int)m.Result == 0x1)
                    m.Result = (IntPtr)0x2;
                return;
            }

            base.WndProc(ref m);
        }

        public DescriptionView(Storage storage)
        {
            this.storage = storage;
            InitializeComponent();
            this.stepFont = new Font(this.Font.FontFamily, 10f, FontStyle.Bold);

            int style = GetWindowLong(this.Handle, GWL_EXSTYLE);
            style |= WS_EX_TOOLWINDOW;
            SetWindowLong(this.Handle, GWL_EXSTYLE, style);
        }

        private void DescriptionView_Load(object sender, EventArgs e)
        {
            if (storage.descriptionRect.Width != 0 && storage.descriptionRect.Height != 0)
            {
                this.Bounds = storage.descriptionRect;
            }

            this.Opacity = (double)storage.descriptionOpacity / 100;
        }

        public void UpdateStepData(Step step)
        {
            if (step.id > 0)
            {
                this.SetString(step.description, step.tips);
                this.SetLevel(step.level);
                this.SetExp(step.exp);
                this.SetStepId(step.id);
            }
            else
            {
                this.SetString("", "");
                this.SetLevel(0);
                this.SetExp(0F);
                this.SetStepId(0);
            }
            this.Invalidate();
        }

        public void SetLevel(int lvl)
        {
            this.level = lvl;
        }

        public void SetExp(float exp)
        {
            this.exp = exp;
        }

        public void SetStepId(int stepId)
        {
            this.stepId = stepId;
        }

        public void SetString(String text, String tip)
        {
            if (tip != "")
            {
                String[] tips = tip.Split(
                    new[] { "\r\n", "\r", "\n" },
                    StringSplitOptions.RemoveEmptyEntries
                );
                this.tipLines = new List<string>(tips);
            }
            else
            {
                this.tipLines = null;
            }


            String[] lines = text.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.RemoveEmptyEntries
            );
            this.descriptionLines = new List<string>(lines);

            Graphics gc = this.CreateGraphics();

            int maxW = 0, maxH = 0;
            int DescOffsetX = (int)Math.Ceiling((float)textOffset * 3.5F);
            foreach (String element in this.descriptionLines)
            {
                SizeF stringSize = gc.MeasureString(element.Trim(), this.stepFont);
                int tw = (int)Math.Ceiling(stringSize.Width) + DescOffsetX;
                int th = (int)Math.Ceiling(stringSize.Height);
                if (maxW < tw)
                {
                    maxW = tw;
                }
                if (maxH < th)
                {
                    maxH = th;
                }
            }

            if (this.tipLines != null && this.tipLines.Count > 0)
            {
                foreach (String element in this.tipLines)
                {
                    SizeF stringSize = gc.MeasureString(element.Trim(), this.stepFont);
                    int tw = (int)Math.Ceiling(stringSize.Width) + DescOffsetX;
                    int th = (int)Math.Ceiling(stringSize.Height);
                    if (maxW < tw)
                    {
                        maxW = tw;
                    }
                    if (maxH < th)
                    {
                        maxH = th;
                    }
                }
            }

            gc.Dispose();
            this.textAreaMaxLineSize = new Size(maxW, maxH);

            int c = this.descriptionLines.Count;

            if (this.tipLines != null && this.tipLines.Count > 0)
            {
                c += this.tipLines.Count + 1;
            }

            int maxClinetH = (lineSpacing * (c + 1)) + (maxH * c) + (lineSpacing * 2);
            if (maxClinetH < 80)
            {
                maxClinetH = 80;
            }

            if (maxW < 80)
            {
                maxW = 80;
            }

            if (maxW > 500)
            {
                maxW += (int)Math.Ceiling((float)maxW * 0.05f);
            }

            this.ClientSize = new Size(maxW, maxClinetH);
        }

        protected void DrawExpArea(PaintEventArgs e, int size)
        {
            int lvlHeight = (int)Math.Ceiling((double)size * 1.2);
            // activate AA
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            
            // exp Bar
            int expHeight = this.Size.Height - lvlHeight;
            double expProgressSize = ((double)expHeight) * (this.exp / 100);
            int calculatedExpHeight = (int)Math.Floor(expProgressSize);
            Rectangle expBarRect = new Rectangle(0, expHeight - calculatedExpHeight, size, calculatedExpHeight);
            Brush expBrush = new SolidBrush(Color.FromArgb(255, 121, 0, 114));
            e.Graphics.FillRectangle(expBrush, expBarRect);
            expBrush.Dispose();

            // Exp BORDER
            Pen boderPen = new Pen(Color.FromArgb(255, 50, 50, 50), 1.0F);
            Rectangle expBorderRect = expBarRect;
            expBorderRect.Y = 0;
            expBorderRect.Height = expHeight;
            e.Graphics.DrawRectangle(boderPen, expBorderRect);
            boderPen.Dispose();


            int textOffsetX = 0;
            if (this.exp < 10F)
            {
                textOffsetX = 3;
            }
            // EXP percent
            TextRenderer.DrawText(e.Graphics, ((int)this.exp).ToString(), new Font(this.Font.FontFamily, 10f), new Point(textOffsetX, expHeight / 2), Color.White);
            // Render Level 
            textOffsetX = 0;
            if (this.level < 10)
            {
                textOffsetX = 3;
            }
            TextRenderer.DrawText(e.Graphics, this.level.ToString(), new Font(this.Font.FontFamily, 10f), new Point(textOffsetX, expHeight + (int)((double)lvlHeight * 0.1)), Color.White);
        }

        protected void DrawDescription(PaintEventArgs e, int offset)
        {
            if (this.descriptionLines == null || this.descriptionLines.Count == 0) {
                return;
            }

            int YPos = lineSpacing;
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            Font idFont = new Font(this.Font.FontFamily, 6f);
            SizeF stringSize = e.Graphics.MeasureString(this.stepId.ToString(), idFont);
            Point idPoint = new Point(this.ClientSize.Width - (int)(int)Math.Floor(stringSize.Width) - 10, this.ClientSize.Height - (int)(int)Math.Floor(stringSize.Height));
            TextRenderer.DrawText(e.Graphics, this.stepId.ToString(), idFont, idPoint, Color.Gray);
            YPos += lineSpacing;

            foreach (String element in this.descriptionLines)
            {
                TextRenderer.DrawText(e.Graphics, element.Trim(), this.stepFont, new Point(offset, YPos), Color.White);
                YPos += lineSpacing;
                YPos += this.textAreaMaxLineSize.Height;
            }
            // bottom offset
            

            if (this.tipLines != null  && this.tipLines.Count > 0)
            {
                Pen p = new Pen(Color.FromArgb(40, 40, 40), 2);
                int lineHeightHalf = this.textAreaMaxLineSize.Height / 2;
                e.Graphics.DrawLine(p, new Point(offset, YPos + lineHeightHalf), new Point(this.ClientSize.Width - lineSpacing, YPos + lineHeightHalf));
                YPos += this.textAreaMaxLineSize.Height;
                foreach (String element in this.tipLines)
                {
                    TextRenderer.DrawText(e.Graphics, element.Trim(), this.stepFont, new Point(offset, YPos), Color.White);
                    YPos += lineSpacing;
                    YPos += this.textAreaMaxLineSize.Height;
                }
            }
            YPos += this.textAreaMaxLineSize.Height;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawDescription(e, textOffset);
            DrawExpArea(e, 20);

            Pen p = new Pen(Color.FromArgb(40, 40, 40), 4);
            e.Graphics.DrawRectangle(p, new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height));
            p.Dispose();

            e.Graphics.Dispose();
        }

        // SHARE
        [DllImport("user32.dll", SetLastError = true)]
        extern static int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        extern static int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_LAYERED = 0x80000;
        private const int WS_EX_TRANSPARENT = 0x20;

        public bool IsFormLocked()
        {
            int style = GetWindowLong(this.Handle, GWL_EXSTYLE);
            return !((style & WS_EX_TRANSPARENT) == 0);
        }

        public void ToggleLockFrom()
        {
            int style = GetWindowLong(this.Handle, GWL_EXSTYLE);
            if ((style & WS_EX_TRANSPARENT) == 0)
            {
                style |= WS_EX_TRANSPARENT;
            }
            else
            {
                style ^= WS_EX_TRANSPARENT;
            }
            SetWindowLong(this.Handle, GWL_EXSTYLE, style);
        }

        private void DescriptionView_FormClosing(object sender, FormClosingEventArgs e)
        {
            storage.descriptionRect = this.Bounds;
        }
    }
}
