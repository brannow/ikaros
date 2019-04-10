using System;
using System.Windows.Forms;

using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

using Ikaros.Objects;

namespace Ikaros.Services
{
    public class StorageHandler
    {
        public static bool WriteStorageToFile(Storage storage, String filename, String extension)
        {
            byte[] data = ConvertObjectToBytes(storage);
            if (data != null)
            {
                return SaveFile(filename, extension, data);
            }

            return false;
        }

        public static Storage ReadStorageToFile(String filename, String extension)
        {
            byte[] data = LoadFile(filename, extension);
            if (data != null)
            {
                return ConvertBytesToObject(data);
            }

            return new Storage();
        }

        protected static byte[] ConvertObjectToBytes(Storage storage)
        {
            StorageData data = storage.ToStruct();

            int size = Marshal.SizeOf(data);
            byte[] arr = new byte[size];
            IntPtr ptr = Marshal.AllocHGlobal(size);

            Marshal.StructureToPtr(data, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);

            return arr;
        }

        protected static Storage ConvertBytesToObject(byte[] arr)
        {
            Storage storage = new Storage();
            StorageData data = new StorageData();
            int size = Marshal.SizeOf(data);
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.Copy(arr, 0, ptr, size);
            data = (StorageData)Marshal.PtrToStructure(ptr, data.GetType());
            Marshal.FreeHGlobal(ptr);

            storage.FromStruct(data);

            return storage;
        }

        protected static byte[] LoadFile(String fileName, String extension)
        {
            bool successSave = false;
            bool invalidData = false;
            byte[] bytes = null;

            if (fileName != null && fileName.Length > 0)
            {
                try
                {
                    string regexSearch = new string(Path.GetInvalidPathChars());
                    Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
                    String validFileName = r.Replace(fileName, "");

                    String fullPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), validFileName);
                    fullPath = Path.ChangeExtension(fullPath, extension);

                    if (File.Exists(fullPath))
                    {
                        using (FileStream fs = new FileStream(fullPath, FileMode.Open))
                        {
                            using (BinaryReader binReader = new BinaryReader(fs))
                            {
                                bytes = binReader.ReadBytes((int)binReader.BaseStream.Length);
                                binReader.Close();
                                successSave = true;
                            }
                            fs.Close();
                        }
                    }
                    else
                    {
                        successSave = false;
                    }
                }
                catch
                {
                    successSave = false;
                }
            }

            if (successSave && !invalidData)
            {
                return bytes;
            }
            else
            {
                return null;
            }
        }

        protected static bool SaveFile(String fileName, String extension, byte[] byteData)
        {
            bool successSave = false;
            if (fileName != null && fileName.Length > 0)
            {
                try
                {
                    string regexSearch = new string(Path.GetInvalidPathChars());
                    Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
                    fileName = r.Replace(fileName, "");

                    String fullPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), fileName);
                    fullPath = Path.ChangeExtension(fullPath, extension);

                    using (FileStream fs = new FileStream(fullPath, FileMode.Create))
                    {
                        using (BinaryWriter binWriter = new BinaryWriter(fs))
                        {
                            binWriter.Write(byteData);
                            successSave = true;
                            binWriter.Close();
                        }
                        fs.Close();
                    }
                }
                catch
                {
                    successSave = false;
                }
            }

            return successSave;
        }
    }
}
