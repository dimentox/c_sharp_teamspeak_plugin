using RGiesecke.DllExport;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace TS
{

    public class Exports
    {
        

        static Boolean Is64Bit()
        {

            return Marshal.SizeOf(typeof(IntPtr)) == 8;

        }
        [DllExport]
        public static String ts3plugin_name()
        {
            return TSPlugin.Instance.PluginName;
        }
        [DllExport]
        public static String ts3plugin_version()
        {
            return TSPlugin.Instance.PluginVersion;
        }
        [DllExport]
        public static int ts3plugin_apiVersion()
        {
            return TSPlugin.Instance.ApiVersion;
        }
        [DllExport]
        public static String ts3plugin_author()
        {
            return TSPlugin.Instance.Author;
        }
        [DllExport]
        public static String ts3plugin_description()
        {
            return TSPlugin.Instance.Description;
        }
        [DllExport]
        public static void ts3plugin_setFunctionPointers(TS3Functions funcs)
        {
            TSPlugin.Instance.Functions = funcs;
            funcs.printMessageToCurrentTab("TEST");
           // funcs("TEST");
        }
        [DllExport]
        public static int ts3plugin_init()
        {
            return TSPlugin.Instance.Init();
        
        }
        [DllExport]
        public static void ts3plugin_shutdown()
        {

        }
        [DllExport]
        public static void ts3plugin_registerPluginID(String id)
        {
            var functs = (TS3Functions)TSPlugin.Instance.Functions;
            functs.printMessageToCurrentTab(id);
            TSPlugin.Instance.PluginID = id;
        }
        [DllExport]
        public static void ts3plugin_freeMemory(System.IntPtr data)
        {
            Marshal.FreeHGlobal(data);
        }
        [DllExport]
        public static void ts3plugin_currentServerConnectionChanged(ulong serverConnectionHandlerID)
        {
            var functs = (TS3Functions)TSPlugin.Instance.Functions;
            functs.printMessageToCurrentTab(serverConnectionHandlerID.ToString());
        }
        public unsafe static char* my_strcpy(char* destination, int buffer,char* source)
        {
            char* p = destination;
            int x = 0;
            while (*source != '\0' && x < buffer)
            {
                *p++ = *source++;
                x++;
            }
            *p = '\0';
            return destination;
        }
        private static byte[] convertLPSTR(string _string)
        {
            List<byte> lpstr = new List<byte>();
            foreach (char c in _string.ToCharArray())
            {
                lpstr.Add(Convert.ToByte(c));
            }
            lpstr.Add(Convert.ToByte('\0'));

            return lpstr.ToArray();
        }
        public static unsafe PluginMenuItem* createMenuItem(PluginMenuType type, int id, String text, String icon)
        {

            PluginMenuItem* menuItem = (PluginMenuItem*)Marshal.AllocHGlobal(sizeof(PluginMenuItem));
            menuItem->type = type;
            menuItem->id = id;

           
            IntPtr i_ptr = System.Runtime.InteropServices.Marshal.StringToHGlobalAnsi(icon);
            void* i_strPtr = i_ptr.ToPointer();
            char* i_cptr = (char*)i_strPtr;
            *menuItem->icon = *my_strcpy(menuItem->icon, 128, i_cptr);

            IntPtr t_ptr = System.Runtime.InteropServices.Marshal.StringToHGlobalAnsi(text);
            void* t_strPtr = t_ptr.ToPointer();
            char* t_cptr = (char*)t_strPtr;
            my_strcpy(menuItem->text, 128, t_cptr);

            


            return menuItem;


        }

       

        [DllExport]
        public unsafe static void ts3plugin_initMenus(PluginMenuItem*** menuItems, char** menuIcon)
        {
            
            int x = 1;
            int sz = x + 1; 
            int n = 0; 

            *menuItems = (PluginMenuItem**)Marshal.AllocHGlobal((sizeof(PluginMenuItem*) * sz));
            string name = "Test";
            string icon = "2.png";
          
            (*menuItems)[n++] = createMenuItem(PluginMenuType.PLUGIN_MENU_TYPE_GLOBAL, 1, name, icon);
        
            (*menuItems)[n++] = null; 
            //Debug.Assert(n == sz);

            *menuIcon = (char*)Marshal.AllocHGlobal(128 * sizeof(char));

            IntPtr ptr = System.Runtime.InteropServices.Marshal.StringToHGlobalAnsi("t.png");
            void* strPtr = ptr.ToPointer();
            char* cptr = (char*)strPtr;
            my_strcpy(*menuIcon, 128, cptr);

          
            
        }
    
        [DllExport]
        public static void ts3plugin_onMenuItemEvent(ulong serverConnectionHandlerID, PluginMenuType type, int menuItemID, ulong selectedItemID)
        {

        }

 

    }
}
