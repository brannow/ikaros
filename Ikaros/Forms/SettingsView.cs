using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Ikaros.FormElements;
using Ikaros.Objects;

namespace Ikaros.Forms
{
    public partial class SettingsView : Form
    {
        protected TrayMenu del;
        protected Storage storage;
        protected ZoneList zoneList;

        // drag Form with mouse
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }

            base.WndProc(ref m);
        }

        public SettingsView(Storage storage, ZoneList zoneList, TrayMenu del)
        {
            this.storage = storage;
            this.del = del;
            this.zoneList = zoneList;
            InitializeComponent();
        }

        private void SetHotkeyInTextBoxes()
        {
            HotkeyStruct hks = Hotkey.GetHotkey(Hotkey.Type.Next);
            if (hks.id == Hotkey.Type.Next)
            {
                UpdateTextBoxWithHotkey(hks, nextTextBox, Hotkey.Type.None);
            }

            hks = Hotkey.GetHotkey(Hotkey.Type.Prev);
            if (hks.id == Hotkey.Type.Prev)
            {
                UpdateTextBoxWithHotkey(hks, prevTextBox, Hotkey.Type.None);
            }

            hks = Hotkey.GetHotkey(Hotkey.Type.Lock);
            if (hks.id == Hotkey.Type.Lock)
            {
                UpdateTextBoxWithHotkey(hks, lockTextBox, Hotkey.Type.None);
            }

            hks = Hotkey.GetHotkey(Hotkey.Type.show);
            if (hks.id == Hotkey.Type.show)
            {
                UpdateTextBoxWithHotkey(hks, ShowTextBox, Hotkey.Type.None);
            }
        }

        private void SettingsView_Load(object sender, EventArgs e)
        {
            if (storage.settingsRect.Width != 0 && storage.settingsRect.Height != 0)
            {
                storage.settingsRect.Size = this.Bounds.Size;
                this.Bounds = storage.settingsRect;
            }

            SetHotkeyInTextBoxes();
        }

        private void RefreshZoneComboBox(ZoneList zones)
        {
            ZoneComboBox.Items.Clear();
            if (zones.list.Length > 0)
            {
                Zone currentZone = zones.GetCurrentZone();
                ZoneComboBox.Enabled = true;
                int index = 0, selectedIndex = 0;
                foreach (ZoneFileName zfn in zones.list)
                {
                    ZoneComboBox.Items.Add(new ComboBoxItem()
                    {
                        Text = zfn.displayName,
                        Value = zfn.id
                    });
                    if (currentZone != null && zfn.id == currentZone.id)
                    {
                        selectedIndex = index;
                    }
                    index++;
                }

                ZoneComboBox.SelectedIndex = selectedIndex;
            }
            else
            {
                ZoneComboBox.Enabled = false;
            }
        }

        private void RefreshSectionComboBox(Zone zone)
        {
            SectionComboBox.Items.Clear();
            if (zone.sections.Length > 0)
            {
                SectionComboBox.Enabled = true;
                Section currentSection = zone.GetCurrentSection();
                int index = 0, selectedIndex = 0;
                foreach (Section section in zone.sections)
                {
                    if (section.id > 0)
                    {
                        SectionComboBox.Items.Add(new ComboBoxItem()
                        {
                            Text = section.name,
                            Value = section.id
                        });
                        if (section.id == currentSection.id)
                        {
                            selectedIndex = index;
                        }
                        index++;
                    }
                }

                SectionComboBox.SelectedIndex = selectedIndex;
            }
            else
            {
                SectionComboBox.Enabled = false;
                RefreshStepComboBox(zone);
            }
        }

        private void RefreshStepComboBox(Zone zone)
        {
            StepComboBox.Items.Clear();
            Section section = zone.GetCurrentSection();
            if (section.id > 0 && section.steps.Length > 0)
            {
                StepComboBox.Enabled = true;
                Step currentStep = zone.GetCurrentStep();
                int index = 0, selectedIndex = 0;
                foreach (Step step in section.steps)
                {
                    StepComboBox.Items.Add(new ComboBoxItem()
                    {
                        Text = "[" + step.id.ToString() + "]  lvl " + step.level.ToString() + " -  section:  " + section.name,
                        Value = step.id
                    });
                    if (currentStep.id > 0 && currentStep.id == step.id)
                    {
                        selectedIndex = index;
                    }
                    index++;
                }

                StepComboBox.SelectedIndex = selectedIndex;
            }
            else
            {
                StepComboBox.Enabled = false;
            }
        }

        public void SetMapOpacity(int op)
        {
            if (op <= this.trackBar1.Maximum && op >= this.trackBar1.Minimum)
            {
                this.labelMapOpValue.Text = op.ToString() + "%";
                this.del.UpdateMapOpacity(op);
                if (this.trackBar1.Value != op)
                {
                    this.trackBar1.Value = op;
                }
            }
        }

        public void SetDescriptionOpacity(int op)
        {
            if (op <= this.trackBar2.Maximum && op >= this.trackBar2.Minimum)
            {
                this.labelDescOpValue.Text = op.ToString() + "%";
                this.del.UpdateDescriptionOpacity(op);
                if (this.trackBar2.Value != op)
                {
                    this.trackBar2.Value = op;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // border
            // optional ?
            Pen p = new Pen(Color.FromArgb(40, 40, 40), 4);
            e.Graphics.DrawRectangle(p, new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height));
            p.Dispose();

            base.OnPaint(e);
        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            int v = ((TrackBar)sender).Value;
            SetMapOpacity(v);
        }

        private void TrackBar2_Scroll(object sender, EventArgs e)
        {
            int v = ((TrackBar)sender).Value;
            SetDescriptionOpacity(v);
        }

        private void OnCloseFrom(object sender, EventArgs e)
        {
            del.ToggleForm(this);
        }

        private void SettingsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            storage.settingsRect = Bounds;
        }

        private void SettingsView_VisibleChanged(object sender, EventArgs e)
        {
            if (((SettingsView)sender).Visible == true)
            {
                SetMapOpacity(storage.mapOpacity);
                SetDescriptionOpacity(storage.descriptionOpacity);
                RefreshZoneComboBox(zoneList);
            }
        }

        private void ZoneComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb.Items.Count > 0)
            {
                ComboBoxItem item = (ComboBoxItem)cb.SelectedItem;
                Zone z = zoneList.LoadZoneWithId(item.Value);
                if (z != null)
                {
                    storage.zoneId = z.id;
                }
                RefreshSectionComboBox(z);
            }
        }

        private void SectionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb.Items.Count > 0)
            {
                Zone z = zoneList.GetCurrentZone();
                if (z != null)
                {
                    ComboBoxItem item = (ComboBoxItem)cb.SelectedItem;
                    z.SelectSectionWithRealSectionId(item.Value);
                    storage.sectionId = z.sectionId;
                    RefreshStepComboBox(zoneList.GetCurrentZone());
                }
            }
        }

        private void StepComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb.Items.Count > 0)
            {
                Zone z = zoneList.GetCurrentZone();
                if (z != null)
                {
                    ComboBoxItem item = (ComboBoxItem)cb.SelectedItem;
                    z.SelectStepWithRealStepId(item.Value);
                    storage.stepId = z.stepId;
                    del.UpdateZoneSectionStepSetup(z);
                }
            }
        }

        private void Capture_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode <= 6)
            {
                e.SuppressKeyPress = true;
                return;
            }

            TextBox tb = (TextBox)sender;
            tb.Text = "";
            Hotkey.Type id = (Hotkey.Type)Convert.ToInt32(tb.Tag);

            if (e.KeyCode == Keys.Escape)
            {
                Hotkey.SetHotkey(id, 0, 0);
                //this.del.UpdateHotkeySetup();
                e.SuppressKeyPress = true;
                return;
            }

            int modifier = 0;
            if (e.Alt)
            {
                modifier = (int)Keys.Alt;
            }
            else if (e.Control)
            {
                modifier = (int)Keys.Control;
            }
            else if (e.Shift)
            {
                modifier = (int)Keys.Shift;
            }

            HotkeyStruct ohk = Hotkey.GetHotkeyWithKey((int)e.KeyCode, modifier);
            HotkeyStruct nhk = Hotkey.SetHotkey(id, (int)e.KeyCode, modifier);
            UpdateTextBoxWithHotkey(nhk, tb, ohk.id);
            e.SuppressKeyPress = true;
        }

        protected void UpdateTextBoxWithHotkey(HotkeyStruct hks, TextBox tb, Hotkey.Type oldType)
        {
            if (hks.id == Hotkey.Type.None)
            {
                return;
            }

            if ((int)Keys.Alt == hks.modifier)
            {
                tb.Text = "Alt + ";
            }
            else if ((int)Keys.Control == hks.modifier)
            {
                tb.Text = "Ctrl + ";
            }
            else if ((int)Keys.Shift == hks.modifier)
            {
                tb.Text = "Shift + ";
            }

            if (hks.keyCode != (int)Keys.ControlKey && hks.keyCode != (int)Keys.ShiftKey && hks.keyCode != (int)Keys.Menu && hks.keyCode != (int)Keys.Alt)
            {
                string keyChar = KeyCodeToUnicode((Keys)hks.keyCode);
                if (keyChar == "None")
                {

                }
                if (keyChar == "")
                {
                    tb.Text += (Keys)hks.keyCode;
                }
                else
                {
                    tb.Text += keyChar;
                }
            }

            Hotkey.Type id = (Hotkey.Type)Convert.ToInt32(tb.Tag);
            if (oldType != hks.id && oldType != Hotkey.Type.None)
            {
                // clear other textBox
                foreach (Control x in this.Controls)
                {
                    if (x is TextBox)
                    {
                        Hotkey.Type subId = (Hotkey.Type)Convert.ToInt32(x.Tag);
                        if (oldType == subId)
                        {
                            ((TextBox)x).Text = String.Empty;
                            break;
                        }
                    }
                }
            }
        }

        public string KeyCodeToUnicode(Keys key)
        {
            byte[] keyboardState = new byte[255];
            bool keyboardStateStatus = GetKeyboardState(keyboardState);

            if (!keyboardStateStatus)
            {
                return "";
            }

            uint virtualKeyCode = (uint)key;
            uint scanCode = MapVirtualKey(virtualKeyCode, 0);
            IntPtr inputLocaleIdentifier = GetKeyboardLayout(0);

            StringBuilder result = new StringBuilder();
            ToUnicodeEx(virtualKeyCode, scanCode, keyboardState, result, (int)5, (uint)0, inputLocaleIdentifier);

            return result.ToString();
        }

        [DllImport("user32.dll")]
        static extern bool GetKeyboardState(byte[] lpKeyState);

        [DllImport("user32.dll")]
        static extern uint MapVirtualKey(uint uCode, uint uMapType);

        [DllImport("user32.dll")]
        static extern IntPtr GetKeyboardLayout(uint idThread);

        [DllImport("user32.dll")]
        static extern int ToUnicodeEx(uint wVirtKey, uint wScanCode, byte[] lpKeyState, [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszBuff, int cchBuff, uint wFlags, IntPtr dwhkl);
    }
}
