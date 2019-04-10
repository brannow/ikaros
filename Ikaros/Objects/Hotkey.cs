using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikaros.Objects
{
    public struct HotkeyStruct
    {
        public Hotkey.Type id;
        public int keyCode;
        public int modifier;
    }

    /**
         * ä --> Oem7
         * ö --> oemtilde (das kommt mir bisschen komisch vor)
         * ü --> Oem1
         * + --> OemPlus
         * # --> OemQuestion (das auch)
         * 
         **/
    // fsModifier Alt = 1, Ctrl = 2, Shift = 4, Win = 8
    public static class Hotkey
    {
        public enum Type
        {
            None = 0,
            Next = 1,
            Prev = 2,
            show = 3,
            Lock = 4
        }

        public static Dictionary<Type, HotkeyStruct> hotkeys = null;

        public static void Setup(Storage storage)
        {
            // TEST!
            SetHotkey(storage.nextHotkey);
            SetHotkey(storage.prevHotkey);
            SetHotkey(storage.showHotkey);
            SetHotkey(storage.lockHotkey);
        }

        public static HotkeyStruct GetHotkey(Type hotkeyId)
        {
            if (hotkeys != null && hotkeys.ContainsKey(hotkeyId))
            {
                if (hotkeys.TryGetValue(hotkeyId, out HotkeyStruct value))
                {
                    return value;
                }
            }

            return new HotkeyStruct()
            {
                id = Type.None,
                keyCode = 0,
                modifier = 0
            };
        }

        public static bool HasHotkeys()
        {
            if (hotkeys != null && hotkeys.Count > 0)
            {
                foreach (KeyValuePair<Type, HotkeyStruct> entry in hotkeys)
                {
                    if (entry.Value.id != Type.None)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static HotkeyStruct GetHotkeyWithKey(int key, int modifier)
        {
            if (hotkeys != null)
            {
                foreach (KeyValuePair<Type, HotkeyStruct> entry in hotkeys)
                {
                    if (entry.Value.keyCode == key && entry.Value.modifier == modifier)
                    {
                        return entry.Value;
                    }
                }
            }

            return new HotkeyStruct()
            {
                id = Type.None
            };
        }

        public static void SetHotkey(Type hotkeyId, int key)
        {
            SetHotkey(hotkeyId, key, 0);
        }

        public static void SetHotkey(HotkeyStruct hkStruct)
        {
            SetHotkey(hkStruct.id, hkStruct.keyCode, hkStruct.modifier);
        }

        public static HotkeyStruct SetHotkey(Type hotkeyId, int key, int modifier)
        {
            if (hotkeys == null)
            {
                hotkeys = new Dictionary<Type, HotkeyStruct>(Enum.GetValues(typeof(Type)).Length);
            }

            if (key == 0)
            {
                hotkeys.Remove(hotkeyId);
                return new HotkeyStruct()
                {
                    id = Type.None
                };
            }

            RemoveDuplicateHotkeys(hotkeyId, key, modifier);
            if (hotkeys.ContainsKey(hotkeyId))
            {
                if (hotkeys.TryGetValue(hotkeyId, out HotkeyStruct value))
                {
                    hotkeys.Remove(hotkeyId);
                    value.id = hotkeyId;
                    value.keyCode = key;
                    value.modifier = modifier;
                    hotkeys.Add(hotkeyId ,value);
                    return value;
                }
            }
            else
            {
                HotkeyStruct value = new HotkeyStruct
                {
                    id = hotkeyId,
                    keyCode = key,
                    modifier = modifier
                };
                hotkeys.Add(hotkeyId, value);
                return value;
            }

            return new HotkeyStruct()
            {
                id = Type.None
            };
        }

        private static void RemoveDuplicateHotkeys(Type hotkeyType, int key, int modifier)
        {
            List<Type> typeList = new List<Type>();
            foreach (KeyValuePair<Type, HotkeyStruct> entry in hotkeys)
            {
                if (entry.Key == hotkeyType)
                {
                    continue;
                }

                if (entry.Value.keyCode == key && entry.Value.modifier == modifier)
                {
                    typeList.Add(entry.Key);
                }
            }

            foreach (Type typeToRemove in typeList)
            {
                hotkeys.Remove(typeToRemove);
            }
        }
    }
}
