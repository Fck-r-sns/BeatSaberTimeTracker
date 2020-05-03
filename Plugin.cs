using BeatSaberTimeTracker.HarmonyPatches;
using IPA;
using UnityEngine;
using Logger = IPA.Logging.Logger;

namespace BeatSaberTimeTracker
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    internal class Plugin
    {
        public static Logger logger { get; private set; }

        [Init]
        public Plugin(Logger logger)
        {
            Plugin.logger = logger;
            logger.Debug("Plugin.Init");
        }

        [OnStart]
        public void OnStart()
        {
            logger.Debug("Plugin.OnStart");

            HarmonyPatcher.ApplyPatches();

            GameObject timeTrackerGo = new GameObject("TimeTracker");
            timeTrackerGo.AddComponent<TimeTracker>();
            Object.DontDestroyOnLoad(timeTrackerGo);
        }

        [OnExit]
        public void OnExit()
        {
            logger.Debug("Plugin.OnExit");
        }
    }
}