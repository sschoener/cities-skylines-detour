using System;
using System.Reflection;
using ColossalFramework;

namespace CitiesSkylinesDetour
{
    public static class FakeTransferManager
    {
        public static void Init()
        {
            DebugLog.Log("Init fake transfer manager");
            try
            {
                var inst = Singleton<TransferManager>.instance;
                var incomingCount = typeof(TransferManager).GetField("m_incomingCount", BindingFlags.NonPublic | BindingFlags.Instance);
                var incomingOffers = typeof (TransferManager).GetField("m_incomingOffers", BindingFlags.NonPublic | BindingFlags.Instance);
                var incomingAmount = typeof (TransferManager).GetField("m_incomingAmount", BindingFlags.NonPublic | BindingFlags.Instance);
                if (inst == null)
                {
                    DebugLog.LogError("No instance of TransferManager found!");
                    return;
                }
                _incomingCount = incomingCount.GetValue(inst) as ushort[];
                _incomingOffers = incomingOffers.GetValue(inst) as TransferManager.TransferOffer[];
                _incomingAmount = incomingAmount.GetValue(inst) as int[];
                if (_incomingCount == null || _incomingOffers == null || _incomingAmount == null)
                {
                    DebugLog.LogError("Arrays are null");
                }
            }
            catch (Exception ex)
            {
                DebugLog.LogError("Exception: " + ex.Message);
            }
        }

        private static TransferManager.TransferOffer[] _incomingOffers;
        private static ushort[] _incomingCount;
        private static int[] _incomingAmount;
        private static bool _init;

        /// <summary>
        /// Point of note: This is a static function whereas the original function uses __thiscall.
        /// On x64 machines only __fastcall is left, which means that the first parameter lives in
        /// RCX - which conveniently conincides with the register usually used for the this-ptr 
        /// (at least on Windows).
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="material"></param>
        /// <param name="offer"></param>
        public static void AddIncomingOffer(TransferManager manager, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            // note: do NOT just use 
            //   DebugOutputPanel.AddMessage
            // here. This method is called so frequently that it will actually crash the game.
            if (!_init)
            {
                _init = true;
                Init();
            }
            DebugLog.LogToFileOnly("AddIncomingOffer for " + material + " from " + Environment.StackTrace);

            for (int priority = offer.Priority; priority >= 0; --priority)
            {
                int index = (int)material * 8 + priority;
                int count = _incomingCount[index];
                if (count < 256)
                {
                    _incomingOffers[index * 256 + count] = offer;
                    _incomingCount[index] = (ushort)(count + 1);
                    _incomingAmount[(int)material] += offer.Amount;
                    break;
                }
            }
        }
    }
}