using System;
using System.Windows.Forms;

using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using Ikaros.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections.Generic;

namespace Ikaros.Services
{
    public class JsonLoader
    {
        protected const String FILE_EXT = "json";
        protected const String ZONE_LOCATION = "Resources\\export";

        public static ZoneFileName[] ZoneFileNameList()
        {
            ZoneFileName[] list = new ZoneFileName[0];
            String fullPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), ZONE_LOCATION);
            string[] files = Directory.GetFiles(fullPath, "*." + FILE_EXT);
            if (files.Length > 0)
            {
                list = new ZoneFileName[files.Length];
                int c = 0;
                foreach (string file in files)
                {
                    String fileName = Path.GetFileNameWithoutExtension(file);
                    string[] split = fileName.Split(new[] { '_' }, 2);
                    if (split.Length == 2 && Int32.TryParse(split[0], out int id))
                    {
                        list[c++] = new ZoneFileName()
                        {
                            id = id,
                            name = fileName,
                            displayName = split[1]
                        };
                    }
                }
            }

            return list;
        }

        public static Zone LoadZone(String zoneFileName)
        {
            Zone zone = null;
            String fullPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), ZONE_LOCATION, zoneFileName);
            fullPath = Path.ChangeExtension(fullPath, FILE_EXT);

            try
            {
                using (StreamReader r = new StreamReader(fullPath))
                {
                    String json = r.ReadToEnd();
                    JObject data = (JObject)JsonConvert.DeserializeObject(json);

                    // clear memory
                    json = "";

                    zone = new Zone
                    {
                        id = (int)data.GetValue("id"),
                        name = (String)data.GetValue("name"),
                        nextZoneId = (int)data.GetValue("nextZone")
                    };
                    JArray sectionsData = (JArray)data.GetValue("sections");
                    Section[] sections = new Section[sectionsData.Count];
                    int c = 0;
                    foreach (JObject sectionData in sectionsData)
                    {
                        Section s = new Section
                        {
                            id = (int)sectionData.GetValue("id"),
                            nextSection = (int)sectionData.GetValue("nextSection"),
                            name = (String)sectionData.GetValue("name")
                        };

                        JArray stepsData = (JArray)sectionData.GetValue("steps");
                        Step[] steps = new Step[stepsData.Count];
                        int a = 0;
                        foreach (JObject stepData in stepsData)
                        {
                            Step st = new Step
                            {
                                id = (int)stepData.GetValue("id"),
                                position = (int)stepData.GetValue("position"),
                                map = (int)stepData.GetValue("map"),
                                level = (int)stepData.GetValue("level"),
                                description = (String)stepData.GetValue("description"),
                                tips = (String)stepData.GetValue("tips"),
                                overlay = (String)stepData.GetValue("overlay"),
                                exp = (float)stepData.GetValue("exp"),
                                x = (float)stepData.GetValue("x"),
                                y = (float)stepData.GetValue("y"),
                                z = (float)stepData.GetValue("z"),
                            };

                            steps[a++] = st;
                        }

                        steps = steps.OrderBy(step => step.position).ThenBy(step => step.id).ToList().ToArray();
                        s.steps = steps;
                        sections[c++] = s;
                    }

                    zone.sections = sections;
                }
            }
            catch
            {
                return null;
            }

            return zone;
        }
    }
}
