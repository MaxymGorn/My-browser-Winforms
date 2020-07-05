using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp10
{
    [Serializable]
    public class ListCategory
    {
        public ListCategory()
        {

        }
        public List<SiteAndCategory> SiteAndCategories { get; set; } = new List<SiteAndCategory>();
    }
    [Serializable]
    public class SiteAndCategory
    {
        public string Name_Category { get; set; }
        public List<Site> Sites { get; set; } = new List<Site>();
        public SiteAndCategory()
        {
            Name_Category = "Dafault Name";

        }
    }
    [Serializable]
    public class Site
    {
        public Site()
        {

        }
        public bool Notifications { get; set; } = false;
        public string Name_Site { get; set; }
    }
}
