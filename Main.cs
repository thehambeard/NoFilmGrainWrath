using Kingmaker.Blueprints;
using ModMaker;
using ModMaker.Utility;
using System;
using System.Reflection;
using UnityModManagerNet;
using UnityEngine;


namespace NoFilmGrainWrath
{
#if (DEBUG)
    [EnableReloading]
#endif
    static class Main
    {
        public static LocalizationManager<DefaultLanguage> Local;
        public static ModManager<Core, Settings> Mod;
        public static MenuManager Menu;

        static bool Load(UnityModManager.ModEntry modEntry)
        {
            Local = new LocalizationManager<DefaultLanguage>();
            Mod = new ModManager<Core, Settings>();
            Menu = new MenuManager();
            modEntry.OnToggle = OnToggle;

#if (DEBUG)
            modEntry.OnGUI = OnGUI;
            modEntry.OnUnload = Unload;
            return true;
        }

        static void OnGUI(UnityModManager.ModEntry modEntry)
        {
            if(GUILayout.Button("toggle", GUILayout.ExpandWidth(false)))
            {
                Settings.disableFilmGrain = !Settings.disableFilmGrain;
            }
        }

        static bool Unload(UnityModManager.ModEntry modEntry)
        {
            Mod.Disable(modEntry, true);
            Menu = null;
            Mod = null;
            Local = null;
            return true;
        }
#else
            return true;
        }
#endif
        static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            if (value)
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                Local.Enable(modEntry);
                Mod.Enable(modEntry, assembly);
                Menu.Enable(modEntry, assembly);
            }
            else
            {
                Menu.Disable(modEntry);
                Mod.Disable(modEntry, false);
                Local.Disable(modEntry);
                ReflectionCache.Clear();
            }
            Settings.disableFilmGrain = value;
            return true;
        }

        internal static Exception Error(String message)
        {
            Mod.Error(message);
            return new InvalidOperationException(message);
        }
    }
}
