using ICities;

namespace CitiesSkylinesDetour
{
    public class DetourMod : IUserMod
    {
        public string Name
        {
            get
            {
                // point of note: we are already patching in the main menu. We don't
                // want to miss anything.
                Deploy();
                return "Cope's Detour Proof of Concept";
            }
        }

        public string Description { get { return ""; } }

        private void Deploy()
        {
            DebugLog.Init();
            var srcMethod = typeof(TransferManager).GetMethod("AddIncomingOffer");
            var destMethod = typeof(FakeTransferManager).GetMethod("AddIncomingOffer");
            RedirectionHelper.RedirectCalls(srcMethod, destMethod);
        }
    }
}
