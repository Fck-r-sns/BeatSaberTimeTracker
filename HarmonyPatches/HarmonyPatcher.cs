using System;
using System.Reflection;
using HarmonyLib;

namespace BeatSaberTimeTracker.HarmonyPatches
{
    public class HarmonyPatcher
    {
        private static bool _patched;

        public static void ApplyPatches()
        {
            if (_patched)
            {
                return;
            }

            try
            {
                Harmony harmony = new Harmony("com.fck_r_sns.BeatSaberTimeTracker");
                harmony.PatchAll(Assembly.GetExecutingAssembly());
                _patched = true;
                Plugin.logger.Debug("HarmonyPatcher: Applied");
            }
            catch (Exception e)
            {
                Plugin.logger.Error("HarmonyPatcher: Exception: " + e.Message);
            }
        }
    }
}