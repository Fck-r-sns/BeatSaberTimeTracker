using HarmonyLib;

namespace BeatSaberTimeTracker.HarmonyPatches
{
    struct PauseState
    {
        public bool wasPaused;
    }

    [HarmonyPatch(typeof(GamePause), nameof(GamePause.Pause), MethodType.Normal)]
    class GamePausePausePatch
    {
        [HarmonyPrefix]
        static void CheckIfAlreadyPaused(out PauseState __state, bool ____pause)
        {
            __state = new PauseState { wasPaused = ____pause };
        }

        [HarmonyPostfix]
        static void FireOnGamePausedEvent(PauseState __state, bool ____pause)
        {
            if (!__state.wasPaused && ____pause)
            {
                EventsHelper.FireOnGamePausedEvent();
            }
        }
    }

    [HarmonyPatch(typeof(GamePause), nameof(GamePause.Resume), MethodType.Normal)]
    class GamePauseResumePatch
    {
        [HarmonyPrefix]
        static void CheckIfAlreadyPaused(out PauseState __state, bool ____pause)
        {
            __state = new PauseState { wasPaused = ____pause };
        }

        [HarmonyPostfix]
        static void FireOnGameResumedEvent(PauseState __state, bool ____pause)
        {
            if (__state.wasPaused && !____pause)
            {
                EventsHelper.FireOnGameResumeEvent();
            }
        }
    }
}