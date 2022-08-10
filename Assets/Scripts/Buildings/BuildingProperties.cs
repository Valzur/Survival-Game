using UnityEngine;

namespace Valzuroid.SurvivalGame.Buildings
{
    public class BuildingProperties : MonoBehaviour
    {
        [SerializeField] BuildConstraints buildConstraints;
            [SerializeField] int baseBuildingMaxHealth = 2000;
            [SerializeField] Sprite inProgressSprite;
            [SerializeField] Sprite finishedSprite;
            [SerializeField] int price;
            [SerializeField][Multiline] string description;
            public int buildingHealth{ get{ return (int)(baseBuildingMaxHealth * (1 + upgrades.TowerHealthMultiplier) + upgrades.TowerHealthFlat); } }
            int _currentHealth;
            bool isColliding = false;
            bool onFloor = false;
            Upgrades upgrades;
    }
}