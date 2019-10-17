using MindfireSolutions.DataModel;
using MindfireSolutions.Models;
using System.Collections.Generic;

namespace MindfireSolutions.ViewModel
{
    public class VMIndex
    {
        public List<Blog> LatestTopic { get; set; }
        public List<Blog> TrendingTopic { get; set; }
        public Blog HotTopic { get; set; }
    }
}