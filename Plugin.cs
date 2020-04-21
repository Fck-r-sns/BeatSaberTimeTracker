using IPA;
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
            logger.Debug("Init");
        }

        [OnStart]
        public void OnStart()
        {
            logger.Debug("OnStart");
        }

        [OnExit]
        public void OnExit()
        {
            logger.Debug("OnExit");
        }
    }
}