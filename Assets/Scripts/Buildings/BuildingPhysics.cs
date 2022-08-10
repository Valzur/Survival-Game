using UnityEngine;

namespace Valzuroid.SurvivalGame.Buildings
{
    public enum Direction{ Left, Right, Top, Bottom }

    [RequireComponent(typeof(Building))]
    public class BuildingPhysics : MonoBehaviour
    {
        #region Initializations;
        [SerializeField] int baseWeight;
        [SerializeField] int baseMaxWeight;
        Building building;
        float currentWeight = 0;
        
        public int BaseWeight{ get{ return baseWeight; } }
        public int BaseMaxWeight{ get{ return baseMaxWeight; } }
        #endregion

        void Start()
        {
            building = GetComponent<Building>();
            return;
            //ReCalculateOwnWeight();
        }
        
        void Update()
        {

        }

        public float ReCalculateDirectionWeight(Direction source)
        {
            float weight = BaseWeight;
            

            return weight;
        }
        /*
        void ReCalculateOwnWeight()
        {
            currentWeight = BaseWeight;
            foreach (var item in building.Connections.BottomConnections)
            {
                currentWeight += item.ReCalculateDirectionWeight(Direction.Top);
            }

            foreach (var item in topConnections)
            {
                currentWeight += item.ReCalculateDirectionWeight(Direction.Bottom);
            }

            foreach (var item in rightConnections)
            {
                currentWeight += item.ReCalculateDirectionWeight(Direction.Left);
            }

            foreach (var item in leftConnections)
            {
                currentWeight += item.ReCalculateDirectionWeight(Direction.Right);
            }

        }
        */

    }
}