using ICities;

namespace CitiesSkylinesDetour
{
    public class DebugLoadingExtension : LoadingExtensionBase
    {
        public override void OnLevelLoaded(LoadMode mode)
        {
            if (mode != LoadMode.NewGame && mode != LoadMode.LoadGame)
                return;
            DebugLog.Log("Debug information mode initialized");
        }

        public override void OnLevelUnloading()
        {
            DebugLog.Close();
        }
    }
}