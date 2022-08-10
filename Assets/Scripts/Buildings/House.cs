using UnityEngine;
using Valzuroid.SurvivalGame.Creatures.Workers;

namespace Valzuroid.SurvivalGame.Buildings
{
    public class House : MonoBehaviour
    {
        [SerializeField] int capacityIncrease;
        [SerializeField] Job buildingJobScript;
        bool isBuilt = false;
        void Start()
        {
            buildingJobScript.onJobFinished.AddListener(Effect);
        }

        protected virtual void Effect()
        {
            WorkersManager.Instance.IncreaseWorkersLimit(capacityIncrease);
        }

        void OnDestroy() 
        {
            if(isBuilt)
                WorkersManager.Instance.DecreaseWorkersLimit(capacityIncrease);
        }
    }
}