using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS
{
    public class TSPlugin
    {
        #region singleton
        private readonly static Lazy<TSPlugin> _instance = new Lazy<TSPlugin>(() => new TSPlugin());

        private TSPlugin()
        {

        }

        public static TSPlugin Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        #endregion
        public TS3Functions Functions { get; set; }


        public String PluginName = "Plugin name";
        public String PluginVersion = "0.1";
        public int ApiVersion = 19;
        public String Author = "Dimentox";
        public String Description = "A plugin for some ts server";
        public String PluginID { get; set; }
        public int Init()
        {
         
            return 0;
        }
        public void Shutdown()
        {

        }
       

    }
}
