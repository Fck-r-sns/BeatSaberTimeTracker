using System;

namespace BeatSaberTimeTracker
{
    public static class EventsHelper
    {
        public static event Action onGamePaused;
        public static event Action onGameResumed;

        public static void FireOnGamePausedEvent()
        {
            onGamePaused?.Invoke();
        }

        public static void FireOnGameResumeEvent()
        {
            onGameResumed?.Invoke();
        }
    }
}