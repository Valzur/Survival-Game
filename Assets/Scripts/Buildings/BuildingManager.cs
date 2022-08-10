using UnityEngine;
using Valzuroid.SurvivalGame.Utility;

namespace Valzuroid.SurvivalGame.Buildings
{
    public class BuildingManager : MonoBehaviour
    {
        public static BuildingManager Instance;
        public VoidEventBool onBuildingModeEntered;
        Building selectedBuilding;
        Building buildPrefab;
        [SerializeField] KeyCode buildingModeKey = KeyCode.B;
        [SerializeField] KeyCode unselectBuildingKey = KeyCode.Mouse2;
        bool isInBuildMode = false;
        Valzuroid.SurvivalGame.Buildings.Building[] buildings;

        void Awake() => Instance = this;

        void Start()
        {
            buildings = Resources.LoadAll<Building>("Buildings");
            UIManager.Instance.LoadBuildings(buildings);
        }

        void Update()
        {
            SwitchBuildMode();
            HandleBuildingSelection();
        }
        
        void SwitchBuildMode()
        {
            if(!Input.GetKeyDown(buildingModeKey)) return;

            isInBuildMode = !isInBuildMode;
            if(!isInBuildMode) UnSelectBuilding();
            onBuildingModeEntered?.Invoke(isInBuildMode);
            UIManager.Instance.ToggleBuildingsPanel();
        }

        void HandleBuildingSelection()
        {
            if(!selectedBuilding) return;
            ViewSelectedBuilding();

            if(Input.GetKeyDown(KeyCode.Mouse0)) Build();
            if(Input.GetKeyDown(unselectBuildingKey)) UnSelectBuilding();
        }

        void ViewSelectedBuilding()
        {
            if(!selectedBuilding) return;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.x = Mathf.Round(mousePos.x);
            mousePos.y = Mathf.Round(mousePos.y);
            mousePos.z = 0;
            buildPrefab.transform.position = mousePos;
        }

        void UnSelectBuilding()
        {
            if(!selectedBuilding) return;
            selectedBuilding = null;
            Destroy(buildPrefab.gameObject);
            buildPrefab = null;
        }

        void Build()
        {
            if(!buildPrefab.CanBuild) return;
            buildPrefab.ConfirmBuilding();
            selectedBuilding = null;
            buildPrefab = null;
        }

        public void SelectBuilding(Building building)
        {
            UnSelectBuilding();
            selectedBuilding = building;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            buildPrefab = Instantiate(building, mousePos, Quaternion.identity);
        }
        
    }
}