using UnityEngine;
using UnityEngine.UI;

namespace Clicker
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private Text clicksText;
        [Space]
        [SerializeField] private int clickWeight = 1;
        private int savedClicks;

        private void Start()
        {
            savedClicks = PlayerPrefs.GetInt("Clicks");
            if (savedClicks <= 0)
                savedClicks = 0;

            GameSceneManger.Instance.onSceneLoaded += Save;

            UpdateText();
        }

        private void OnApplicationQuit()
        {
            Save();
        }

        private void UpdateText()
        {
            if (savedClicks > 99999)
                clicksText.text = (savedClicks / 1000).ToString() + "k";
            else
                clicksText.text = savedClicks.ToString();
        }

        public void AddClick()
        {
            savedClicks += clickWeight;
            UpdateText();
        }

        public void Save()
        {
            PlayerPrefs.SetInt("Clicks", savedClicks);
        }
    }
}
