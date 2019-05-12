using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing.Text;
using Ikaros.Objects;
using System.Collections.Generic;

namespace Ikaros.Forms
{
    public struct Vector3f
    {
        public float x;
        public float y;
        public float z;

        public Vector3f(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    };

    public partial class MapView : Form
    {
        //double so division keeps decimal points
        private const double widthRatio = 1;
        private const double heightRatio = 1;

        private int currentMapId = 0;
        private Bitmap map;
        private Bitmap overlay;
        private Vector3f mapVector;
        protected Storage storage;
        private const int defaultHeight = 500;
        private const int WM_SIZING = 0x214;
        private const int WMSZ_LEFT = 1;
        private const int WMSZ_RIGHT = 2;
        private const int WMSZ_TOP = 3;
        private const int WMSZ_BOTTOM = 6;
        private const int WM_NCHITTEST = 0x84;
        private const int WS_EX_TOOLWINDOW = 0x00000080;

        protected const String overlayHeader = "data:image/png;base64,";

        protected const String ZONE_LOCATION = "Resources\\map";
        protected const String FILE_EXT = "jpg";

        Dictionary<int, String> mapList = null;

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
                Point pos = new Point(m.LParam.ToInt32());
                pos = this.PointToClient(pos);
                if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)17; // HTBOTTOMRIGHT
                    return;
                }

                base.WndProc(ref m);
                if ((int)m.Result == 0x1)
                    m.Result = (IntPtr)0x2;
                return;
            }

            if (m.Msg == WM_SIZING)
            {
                RECT rc = (RECT)Marshal.PtrToStructure(m.LParam, typeof(RECT));

                int res = m.WParam.ToInt32();
                if (res == WMSZ_LEFT || res == WMSZ_RIGHT)
                {
                    //Left or right resize -> adjust height (bottom)
                    rc.Bottom = rc.Top + (int)(heightRatio * this.Width / widthRatio);
                }
                else if (res == WMSZ_TOP || res == WMSZ_BOTTOM)
                {
                    //Up or down resize -> adjust width (right)
                    rc.Right = rc.Left + (int)(widthRatio * this.Height / heightRatio);
                }
                else if (res == WMSZ_RIGHT + WMSZ_BOTTOM)
                {
                    //Lower-right corner resize -> adjust height (could have been width)
                    rc.Bottom = rc.Top + (int)(heightRatio * this.Width / widthRatio);
                }
                else if (res == WMSZ_LEFT + WMSZ_TOP)
                {
                    //Upper-left corner -> adjust width (could have been height)
                    rc.Left = rc.Right - (int)(widthRatio * this.Height / heightRatio);
                }
                Marshal.StructureToPtr(rc, m.LParam, true);
                if (this.MaximumSize.Height > this.Height && this.MinimumSize.Height < this.Height)
                {
                    this.Invalidate();
                }
                
            }

            base.WndProc(ref m);
        }

        public MapView(Storage storage)
        {
            this.storage = storage;
            InitializeComponent();
            map = null;
            this.Height = (int)(heightRatio * this.Width / widthRatio);

            int style = GetWindowLong(this.Handle, GWL_EXSTYLE);
            style |= WS_EX_TOOLWINDOW;
            SetWindowLong(this.Handle, GWL_EXSTYLE, style);
        }

        private void LoadMapLocations()
        {
            String fullPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), ZONE_LOCATION);
            string[] files = Directory.GetFiles(fullPath, "*." + FILE_EXT);

            if (files.Length > 0)
            {
                mapList = new Dictionary<int, String>(files.Length);
                foreach (string file in files)
                {
                    String fileName = Path.GetFileNameWithoutExtension(file);
                    if (Int32.TryParse(fileName, out int id))
                    {
                        mapList.Add(id, Path.Combine(fullPath,file));
                    }
                }
            }
        }

        private void MapView_Load(object sender, EventArgs e)
        {
            if (storage.mapRect.Width != 0 && storage.mapRect.Height != 0)
            {
                this.Bounds = storage.mapRect;
            }

            this.Opacity = (double)storage.mapOpacity / 100;
            LoadMapLocations();
        }

        public void UpdateStepData(Step step)
        {
            if (step.id > 0)
            {
                SetMapCoords(step.x, step.y, step.z);
                SetOverlay(step.overlay);
                SetMap(step.map);
            }
            else
            {
                SetMapCoords(0.0F, 0.0F, 0.0F);
                SetOverlay("");
                SetMap(0);
            }
            this.Invalidate();
        }

        public void SetMap(int mapId)
        {
            if (currentMapId > 0 && currentMapId == mapId)
            {
                return;
            }
            if (map != null)
            {
                map.Dispose();
            }

            map = null;
            if (mapId > 0)
            {
                if (mapList != null && mapList.ContainsKey(mapId))
                {
                    String path = mapList[mapId];
                    try
                    {
                        map = new Bitmap(path);
                        currentMapId = mapId;
                    }
                    catch
                    {
                        map = null;
                        currentMapId = 0;
                    }
                }
            }
        }

        public void SetMapCoords(float x, float y, float z)
        {
            if (z == 0)
            {
                z = 1;
            }
            this.mapVector = new Vector3f(x, y, z);
        }

        public void SetOverlay(String overlayBaseData)
        {
            if (this.overlay != null)
            {
                this.overlay.Dispose();
            }

            // overlayHeader
            if (overlayBaseData.Length < 30)
            {
                this.overlay = null;
                return;
            }
            if (overlayBaseData.StartsWith(overlayHeader, StringComparison.InvariantCultureIgnoreCase))
            {
                overlayBaseData = overlayBaseData.Substring(overlayHeader.Length);
            }
            overlayBaseData = overlayBaseData.Replace(' ', '+');
            byte[] byteBuffer = Convert.FromBase64String(overlayBaseData);
            MemoryStream memoryStream = new MemoryStream(byteBuffer)
            {
                Position = 0
            };

            this.overlay = (Bitmap)Bitmap.FromStream(memoryStream);

            memoryStream.Close();
            memoryStream = null;
            byteBuffer = null;
        }

        protected void DrawMap(PaintEventArgs e)
        {
            if (map != null)
            {
                float wo = (float)this.ClientSize.Width / (float)defaultHeight;

                float x = this.mapVector.x * wo;
                float y = this.mapVector.y * wo;
                float w = ((float)map.Size.Width * this.mapVector.z) * wo;
                float h = ((float)map.Size.Height * this.mapVector.z) * wo;

                RectangleF imgRect = new RectangleF(x, y, w, h);
                e.Graphics.DrawImage(map, imgRect);
            }
            else
            {
                e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                Font font = new Font(this.Font.FontFamily, 12f);
                TextRenderer.DrawText(e.Graphics, "Map Area", font, new Point((int)(this.ClientSize.Width / 2) - 35, (int)(this.ClientSize.Height / 2) - 5), Color.FromArgb(60,60,60));
                font.Dispose();
            }
        }

        protected void DrawOverlay(PaintEventArgs e)
        {
            if (overlay != null)
            {
                float wo = (float)this.ClientSize.Width / (float)defaultHeight;

                float w = (float)overlay.Size.Width * wo;
                float h = (float)overlay.Size.Height * wo;

                RectangleF rect = new RectangleF(0, 0, w, h);
                e.Graphics.DrawImage(overlay, rect);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawMap(e);
            DrawOverlay(e);

            // border
            // optional ?
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

        private void MapView_FormClosing(object sender, FormClosingEventArgs e)
        {
            storage.mapRect = this.Bounds;
        }
    }
}
