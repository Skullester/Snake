using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;
        public bool IsNew;
        public bool[] IsThemeBought = new bool[30];
        public bool IsOn = true;
        public bool IsReward;
        public bool IsRewardGiven;
        public bool IsSunOn = true;
        public int CounterCameras;
        public int CountOfCollectedItems;
        public int Record;
        public int indexOfQuality = 2;
    }
}
