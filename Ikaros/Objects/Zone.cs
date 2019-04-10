using System;

namespace Ikaros.Objects
{
    public struct Section
    {
        public int id;
        public int nextSection;
        public TimeSpan inteval;
        public String name;
        public Step[] steps;
    }

    public struct Step
    {
        public int id;
        public int position;
        public String tips;
        public String description;
        public int map;
        public float x;
        public float y;
        public float z;
        public float exp;
        public int level;
        public String overlay;
    }

    public class Zone
    {
        public int id;
        public String name;
        public int nextZoneId;
        public Section[] sections;

        // section id is the REAL data ID
        public int sectionId = -1;
        // step id is the list array INDEX ... cuz we have a orderd list
        public int stepId = 0;

        public void SelectSectionWithRealSectionId(int realSectionId)
        {
            Section section = this.GetCurrentSection();
            if (section.id == realSectionId)
            {
                return;
            }
            if (sections.Length > 0)
            {
                foreach (Section s in sections)
                {
                    if (s.id == realSectionId)
                    {
                        sectionId = s.id;
                        // reset step to zero (new section selected)
                        stepId = 0;
                        return;
                    }
                }
            }
        }

        public void SelectStepWithRealStepId(int realStepId)
        {
            Section section = this.GetCurrentSection();
            if (section.steps.Length > 0)
            {
                int i = 0;
                foreach (Step s in section.steps)
                {
                    if (s.id == realStepId)
                    {
                        stepId = i;
                        return;
                    }
                    i++;
                }
            }

            // step ID not found in current Section ... expand search to all sections
            SelectStepWithRealStepIdInAllSections(realStepId);
        }

        public void SelectStepWithRealStepIdInAllSections(int realStepId)
        {
            if (sections.Length > 0)
            {
                foreach (Section s in sections)
                {
                    if (s.steps.Length > 0)
                    {
                        int i = 0;
                        foreach (Step st in s.steps)
                        {
                            if (st.id == realStepId)
                            {
                                stepId = i;
                                sectionId = s.id;
                                return;
                            }
                            i++;
                        }
                    }
                }
            }
        }

        public Step GetCurrentStep()
        {
            Section section = GetCurrentSection();
            if (section.id > 0)
            {
                if (section.steps.Length > stepId)
                {
                    return section.steps[stepId];
                }
            }

            // -1 means invalid
            return new Step() { id = -1 };
        }

        public Step GetNextStep()
        {
            Section section = GetCurrentSection();
            if (section.id > 0)
            {
                int newStepId = stepId + 1;
                if (section.steps.Length > newStepId)
                {
                    stepId = newStepId;
                    return section.steps[stepId];
                }
                else if (section.steps.Length > 0)
                {
                    section = GetNextSection();
                    if (section.id > 0)
                    {
                        if (section.steps.Length > 0)
                        {
                            stepId = 0;
                            return section.steps[stepId];
                        }
                    }
                    else if (section.id == -10)
                    {
                        // new zone is needed, reached END
                        return new Step() { id = -10 };
                    }
                }
            }

            // -1 means invalid
            return new Step() { id = -1 };
        }

        public Section GetCurrentSection()
        {
            if (sectionId == -1)
            {
                Section s = GetFirstSection();
                if (s.id > 0)
                {
                    sectionId = s.id;
                    return s;
                }
            }

            // id == -1 means invalid
            return GetSectionWithId(sectionId);
        }

        protected Section GetSectionWithId(int id)
        {
            foreach (Section s in sections)
            {
                if (s.id == id)
                {
                    sectionId = s.id;
                    return s;
                }
            }

            return new Section() { id = -1 };
        }

        protected Section GetFirstSection()
        {
            if (sections.Length > 0)
            {
                return sections[0];
            }

            return new Section() { id = -1 };
        }

        public Section GetNextSection()
        {
            Section curSection = GetCurrentSection();
            if (curSection.id > 0 && curSection.nextSection > 0)
            {
                foreach (Section s in sections)
                {
                    if (s.id == curSection.nextSection)
                    {
                        sectionId = s.id;
                        return s;
                    }
                }
            }
            else if (curSection.nextSection == 0)
            {
                // new zone is needed, reached END
                return new Section() { id = -10 };
            }

            // -1 means invalid
            return new Section() { id = -1 };
        }

        public Step GetPreviewsStep()
        {
            Section section = GetCurrentSection();
            if (section.id > 0 && section.steps.Length > 0)
            {
                if (stepId > 0) // current section is fine, where also not on first place in step chain
                {
                    stepId = stepId - 1;
                    return GetCurrentStep();
                }
                else // load prev section
                {
                    section = GetPreviewsSection();
                    if (section.id == -20)
                    {
                        return new Step() { id = -20 };
                    }

                    if (section.id > 0 && section.steps.Length > 0)
                    {
                        stepId = section.steps.Length - 1;
                        sectionId = section.id;
                        return GetCurrentStep();
                    }
                }
            }
            
            return new Step() { id = -1 };
        }

        public Section GetPreviewsSection()
        {
            Section section = GetCurrentSection();
            if (section.id > 0)
            {
                return FindPreviewSectionInChain(section);
            }

            return new Section() { id = -1 };
        }

        protected Section FindPreviewSectionInChain(Section section)
        {
            if (section.id > 0)
            {
                Section s = FindSectionWithNextId(section.id);
                if (s.id > 0)
                {
                    return s;
                }
            }
            // -20 need previews Zone
            return new Section() { id = -20 };
        }

        protected Section FindSectionWithNextId(int nextId)
        {
            foreach (Section s in sections)
            {
                if (s.steps.Length > 0 && s.nextSection == nextId)
                {
                    return s;
                }
            }

            return new Section() { id = -1 };
        }

        public void ResetZoneToLastSectionAndStep()
        {
            // reset section first | next is Zero means, its the last section in zone list
            Section s = FindSectionWithNextId(0);
            if (s.id > 0)
            {
                sectionId = s.id;
                stepId = s.steps.Length - 1;
            }
        }
    }
}
