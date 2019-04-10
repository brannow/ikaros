using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikaros.Objects
{
    public struct ZoneFileName
    {
        public int id;
        public String name;
        public String displayName;
    }

    public class ZoneList
    {
        public ZoneFileName[] list;
        public Zone zone = null;

        protected Storage storage;

        public ZoneList(Storage storage)
        {
            this.storage = storage;
            LoadZoneList();

            if (this.storage.zoneId >= 0)
            {
                LoadZoneWithId(this.storage.zoneId);
                if (zone != null && this.storage.stepId > -1 && this.storage.sectionId > -1)
                {
                    zone.sectionId = this.storage.sectionId;
                    zone.stepId = this.storage.stepId;
                }
            }
            else
            {
                LoadFirstZone();
            }
        }

        public void LoadZoneList()
        {
            list = Services.JsonLoader.ZoneFileNameList().OrderBy(zoneFile => zoneFile.id).ToList().ToArray();
        }

        public Step GetCurrentStep()
        {
            if (zone != null)
            {
                Step step = zone.GetCurrentStep();
                return step;
            }

            return new Step() { id = -1 };
        }

        public Step GetNextStep()
        {
            if (zone != null)
            {
                Step step = zone.GetNextStep();
                if (step.id == -10)
                {
                    LoadNextZone();
                    return GetCurrentStep();
                }
                return step;
            }

            return new Step() { id = -1 };
        }

        public Step GetPreviewsStep()
        {
            if (zone != null)
            {
                Step step = zone.GetPreviewsStep();
                // step bottom reached, need preview ZONE
                if (step.id == -20)
                {
                    LoadPreviewsZone();
                    if (zone != null)
                    {
                        return GetCurrentStep();
                    }
                }
                else if (step.id > 0)
                {
                    return step;
                }
            }

            return new Step() { id = -1 };
        }

        public Zone GetCurrentZone()
        {
            if (zone == null)
            {
                LoadFirstZone();
            }

            return zone;
        }

        public Zone LoadNextZone()
        {
            if (zone == null || zone.nextZoneId <= 0)
            {
                zone = null;
            }

            if (zone.nextZoneId > 0)
            {
                zone = LoadZoneWithId(zone.nextZoneId);
            }

            return zone;
        }

        public Zone LoadPreviewsZone()
        {
            if (zone == null)
            {
                return LoadFirstZone();
            }

            if (zone != null)
            {
                // dirty but works...
                int prevZoneId = zone.id - 1;
                if (prevZoneId > 0)
                {
                    zone = LoadZoneWithId(prevZoneId);
                    zone.ResetZoneToLastSectionAndStep();
                    return zone;
                }
            }

            return zone;
        }

        public Zone LoadZoneWithId(int id)
        {
            if (zone == null || (zone != null && zone.id != id))
            {
                ZoneFileName zfm = GetZoneFileNameWithId(id);
                if (zfm.id > 0)
                {
                    return LoadZoneWithZoneFileName(zfm);
                }
            }

            return zone;
        }

        public Zone LoadZoneWithZoneFileName(ZoneFileName zfn)
        {
            if (zfn.id > 0)
            {
                zone = Services.JsonLoader.LoadZone(zfn.name);
            }

            return zone;
        }

        public ZoneFileName GetZoneFileNameWithId(int id)
        {
            foreach (ZoneFileName zfm in list)
            {
                if (zfm.id == id)
                {
                    return zfm;
                }
            }
            return new ZoneFileName() { id = -1 };
        }

        public Zone LoadFirstZone()
        {
            if (list.Length > 0)
            {
                ZoneFileName zfn = list[0];
                LoadZoneWithZoneFileName(zfn);
            }

            return null;
        }
    }
}
