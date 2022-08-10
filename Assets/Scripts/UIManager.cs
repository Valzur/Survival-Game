using UnityEngine;
using UnityEngine.UI;
using Valzuroid.SurvivalGame.Buildings;

namespace Valzuroid.SurvivalGame
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
        [SerializeField] Image healthBar;
        [SerializeField] Image xpBar;
        [SerializeField] TMPro.TMP_Text levelText;
        [SerializeField] Transform buildingsRoot;
        [SerializeField] Transform buildingsParent;
        [SerializeField] TMPro.TMP_Text coinsText;
        [SerializeField] BuildingPanelItemController buildingPanelItemPrefab;
        [SerializeField] TMPro.TMP_Text cycleText;
        [SerializeField] TMPro.TMP_Text timeText;
        [SerializeField] TMPro.TMP_Text workersText;

        void Awake() => Instance = this;

        public void UpdateHealth(float ratio)
        {
            healthBar.fillAmount = ratio;
        }

        public void UpdateXP(float ratio)
        {
            xpBar.fillAmount = ratio;
        }

        public void UpdateLevel(int level)
        {
            levelText.text = level.ToString();
        }

        public void LoadBuildings(Building[] buildings)
        {
            foreach (var building in buildings)
            {
                Instantiate(buildingPanelItemPrefab, buildingsParent).Setup(building);
            }
        }
        
        public void UpdateCoins(int amt)
        {
            coinsText.text = amt.ToString() + " $";
        }

        public void ToggleBuildingsPanel() => buildingsRoot.gameObject.SetActive(!buildingsRoot.gameObject.activeSelf);

        public void UpdateCycle(int timeLeft, bool isNight)
        {
            cycleText.text = isNight? "Night" : "Day";
            timeText.text = (timeLeft / 60).ToString() + " : " + (timeLeft % 60).ToString();
        }

        public void UpdateCurrentWorkers(int currentWorkers, int totalWorkers)
        {
            workersText.text = "Workers: " + currentWorkers + "/" + totalWorkers;
        }
    }
}