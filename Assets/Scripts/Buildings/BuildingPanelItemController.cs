using UnityEngine;

namespace Valzuroid.SurvivalGame.Buildings
{
    public class BuildingPanelItemController : MonoBehaviour
    {
        [SerializeField] TMPro.TMP_Text priceText;
        [SerializeField] UnityEngine.UI.Image image;
        [SerializeField] Building building;

        public void Setup(Building building)
        {
            this.building = building;
            image.sprite = building.BuildingSprite;
            priceText.text = building.Price.ToString() + " $";
        }

        public void SelectBuilding()
        {
            if(!Creatures.Player.PlayerProperties.Instance.Buy(building.Price)) return;
            BuildingManager.Instance.SelectBuilding(building);
        }
        
    }
}