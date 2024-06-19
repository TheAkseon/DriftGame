
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

        public int Coins;
        public int RecordDriftScore;
        public bool muteMusic;
        public int AppliedCarIndex;
        public List<bool> IsBuyShop;

        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            Coins = 0;
            RecordDriftScore = 0;
            muteMusic = false;
            AppliedCarIndex = 0;
            IsBuyShop = new List<bool>() {true, false, false, false, false};
    }
    }
}
