using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using Ikaros.Forms;
using Ikaros.Objects;

namespace Ikaros
{
    public partial class TrayMenu : ApplicationContext
    {
        private IKeyboardMouseEvents m_GlobalHook;

        private NotifyIcon trayIcon;
        private ContextMenuStrip systemMenu;

        private DescriptionView description;
        private MapView map;
        private SettingsView settings;
        private bool lockStatus;

        private Storage storage;

        private ToolStripMenuItem mapStrip;
        private ToolStripMenuItem descStrip;
        private ToolStripMenuItem lockStrip;

        private ZoneList zones;

        public TrayMenu()
        {
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.storage = Services.StorageHandler.ReadStorageToFile("storage", "dat");
            this.zones = new ZoneList(this.storage);
            Hotkey.Setup(this.storage);


            systemMenu = new ContextMenuStrip();
            mapStrip = new ToolStripMenuItem("Map", null, new EventHandler(OnMapToggle));
            systemMenu.Items.Add(mapStrip);
            descStrip = new ToolStripMenuItem("Description", null, new EventHandler(OnDescriptionToggle));
            systemMenu.Items.Add(descStrip);
            systemMenu.Items.Add(new ToolStripSeparator());
            lockStrip = new ToolStripMenuItem("Lock", null, new EventHandler(OnToggleLockForms));
            systemMenu.Items.Add(lockStrip);
            systemMenu.Items.Add(new ToolStripSeparator());
            systemMenu.Items.Add(new ToolStripMenuItem("Settings", null, new EventHandler(OnSettings)));
            systemMenu.Items.Add(new ToolStripMenuItem("Exit", null, new EventHandler(OnExit)));
            
            Icon i = Icon.FromHandle(Properties.Resources.icon.GetHicon());

            trayIcon = new NotifyIcon
            {
                BalloonTipTitle = "Ikaros",
                Icon = i,
                ContextMenuStrip = systemMenu,
                Visible = true
            };

            // Forms
            map = new MapView(this.storage);
            description = new DescriptionView(this.storage);
            settings = new SettingsView(this.storage, this.zones, this);

            if (storage.mapShown)
            {
                OnMapToggle(mapStrip, null);
            }
            if (storage.descriptionShown)
            {
                OnDescriptionToggle(descStrip, null);
            }


            m_GlobalHook = Hook.GlobalEvents();
            m_GlobalHook.KeyDown += GlobalHookKeyPress;
        }

        private void TrayMenu_Load(object sender, EventArgs e)
        {
            // DO init Stuff HERE like pre loading
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OnSettings(object sender, EventArgs e)
        {
            ToggleForm(settings);
        }

        private void OnMapToggle(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ((ToolStripMenuItem)sender).Checked = !((ToolStripMenuItem)sender).Checked;
                storage.mapShown = ((ToolStripMenuItem)sender).Checked;
            }
            ToggleForm(map);
            if (map.Visible)
            {
                Zone z = zones.GetCurrentZone();
                if (z != null)
                {
                    map.UpdateStepData(z.GetCurrentStep());
                }
            }
        }

        private void OnDescriptionToggle(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ((ToolStripMenuItem)sender).Checked = !((ToolStripMenuItem)sender).Checked;
                storage.descriptionShown = ((ToolStripMenuItem)sender).Checked;
            }
            ToggleForm(description);
            if (description.Visible)
            {
                Zone z = zones.GetCurrentZone();
                if (z != null)
                {
                    description.UpdateStepData(z.GetCurrentStep());
                }
            }
        }

        public void ToggleForm(Form f)
        {
            if (f.Visible == false)
            {
                f.Visible = true;
                f.Show();
            }
            else
            {
                f.Hide();
                f.Visible = false;
            }
        }

        private void OnToggleLockForms(object sender, EventArgs e)
        {
            lockStatus = !lockStatus;
            ((ToolStripMenuItem)sender).Checked = lockStatus;
            if (lockStatus)
            {
                ((ToolStripMenuItem)sender).Text = "Locked";
            } else
            {
                ((ToolStripMenuItem)sender).Text = "Lock";
            }
            ChangeLockState();
        }

        private void ChangeLockState()
        {
            if (lockStatus)
            {
                if (!map.IsFormLocked())
                {
                    map.ToggleLockFrom();
                }
                if (!description.IsFormLocked())
                {
                    description.ToggleLockFrom();
                }
            }
            else
            {
                if (map.IsFormLocked())
                {
                    map.ToggleLockFrom();
                }
                if (description.IsFormLocked())
                {
                    description.ToggleLockFrom();
                }
            }
        }

        protected override void OnMainFormClosed(object sender, EventArgs e)
        {
            if (Application.OpenForms.Count > 0)
            {
                MainForm = Application.OpenForms[0];
            }
            else
            {
                base.OnMainFormClosed(sender, e);
            }
        }

        public void UpdateMapOpacity(int percent)
        {
            this.map.Opacity = (double)percent / 100;
            this.storage.mapOpacity = percent;
        }

        public void UpdateDescriptionOpacity(int percent)
        {
            this.description.Opacity = (double)percent / 100;
            this.storage.descriptionOpacity = percent;
        }

        public void UpdateZoneSectionStepSetup(Zone zone)
        {
            Step step = zone.GetCurrentStep();
            if (map.Visible)
            {
                map.UpdateStepData(step);
            }

            if (description.Visible)
            {
                description.UpdateStepData(step);
            }
        }

        public void CaptureNextHotkey()
        {
            Step step = zones.GetNextStep();
            if (map.Visible)
            {
                map.UpdateStepData(step);
            }

            if (description.Visible)
            {
                description.UpdateStepData(step);
            }
        }

        public void CapturePrevHotkey()
        {
            Step step = zones.GetPreviewsStep();
            if (map.Visible)
            {
                map.UpdateStepData(step);
            }

            if (description.Visible)
            {
                description.UpdateStepData(step);
            }
        }

        public void CaptureShowHotkey()
        {
            if (map.Visible || description.Visible)
            {
                if (map.Visible)
                {
                    OnMapToggle(mapStrip, null);
                }
                if (description.Visible)
                {
                    OnDescriptionToggle(descStrip, null);
                }
            }
            else
            {
                OnMapToggle(mapStrip, null);
                OnDescriptionToggle(descStrip, null);
            }
        }

        public void CaptureLockHotkey()
        {
            OnToggleLockForms(lockStrip, null);
        }

        public void GlobalHookKeyPress(object sender, KeyEventArgs e)
        {
            if (settings.Visible || !Hotkey.HasHotkeys())
            {
                return;
            }

            HotkeyStruct hks = Hotkey.GetHotkeyWithKey((int)e.KeyCode, (int)e.Modifiers);

            if (hks.id != Hotkey.Type.None)
            {
                if (hks.id == Hotkey.Type.Next)
                {
                    CaptureNextHotkey();
                    return;
                }

                if (hks.id == Hotkey.Type.Prev)
                {
                    CapturePrevHotkey();
                    return;
                }

                if (hks.id == Hotkey.Type.show)
                {
                    CaptureShowHotkey();
                    return;
                }

                if (hks.id == Hotkey.Type.Lock)
                {
                    CaptureLockHotkey();
                    return;
                }
            }
        }

        private void SaveHotkeysIntoStorage()
        {
            storage.nextHotkey = Hotkey.GetHotkey(Hotkey.Type.Next);
            storage.prevHotkey = Hotkey.GetHotkey(Hotkey.Type.Prev);
            storage.showHotkey = Hotkey.GetHotkey(Hotkey.Type.show);
            storage.lockHotkey = Hotkey.GetHotkey(Hotkey.Type.Lock);
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            SaveHotkeysIntoStorage();
            Services.StorageHandler.WriteStorageToFile(storage, "storage", "dat");
            m_GlobalHook.KeyDown -= GlobalHookKeyPress;
            //It is recommened to dispose it
            m_GlobalHook.Dispose();
        }
    }
}
