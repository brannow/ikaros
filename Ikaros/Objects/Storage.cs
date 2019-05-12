using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Ikaros.Objects
{
    public struct StorageData
    {
        public Rectangle mapRect;
        public Rectangle descriptionRect;
        public Rectangle settingsRect;

        public int mapOpacity;
        public int descriptionOpacity;

        public bool mapShown;
        public bool descriptionShown;

        public int zoneId;
        public int sectionId;
        public int stepId;

        public HotkeyStruct nextHotkey;
        public HotkeyStruct prevHotkey;
        public HotkeyStruct showHotkey;
        public HotkeyStruct lockHotkey;
    }

    public class Storage
    {
        public Rectangle mapRect;
        public Rectangle descriptionRect;
        public Rectangle settingsRect;

        public int mapOpacity = 100;
        public int descriptionOpacity = 100;

        public bool mapShown;
        public bool descriptionShown;

        public int zoneId = -1;
        public int sectionId = -1;
        public int stepId = -1;

        public HotkeyStruct nextHotkey;
        public HotkeyStruct prevHotkey;
        public HotkeyStruct showHotkey;
        public HotkeyStruct lockHotkey;

        public StorageData ToStruct()
        {
            StorageData sd = new StorageData();
            sd.mapRect = mapRect;
            sd.descriptionRect = descriptionRect;
            sd.settingsRect = settingsRect;
            sd.mapOpacity = mapOpacity;
            sd.descriptionOpacity = descriptionOpacity;
            sd.mapShown = mapShown;
            sd.descriptionShown = descriptionShown;
            sd.zoneId = zoneId;
            sd.sectionId = sectionId;
            sd.stepId = stepId;
            sd.nextHotkey = nextHotkey;
            sd.prevHotkey = prevHotkey;
            sd.showHotkey = showHotkey;
            sd.lockHotkey = lockHotkey;
            return sd;
        }

        public void FromStruct(StorageData sd)
        {
            mapRect = sd.mapRect;
            descriptionRect = sd.descriptionRect;
            settingsRect = sd.settingsRect;
            mapOpacity = sd.mapOpacity;
            descriptionOpacity = sd.descriptionOpacity;
            mapShown = sd.mapShown;
            descriptionShown = sd.descriptionShown;
            zoneId = sd.zoneId;
            sectionId = sd.sectionId;
            stepId = sd.stepId;
            nextHotkey = sd.nextHotkey;
            prevHotkey = sd.prevHotkey;
            showHotkey = sd.showHotkey;
            lockHotkey = sd.lockHotkey;
        }
    }
}
