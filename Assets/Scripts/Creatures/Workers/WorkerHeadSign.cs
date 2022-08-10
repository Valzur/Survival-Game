using UnityEngine;

namespace Valzuroid.SurvivalGame.Creatures.Workers
{
    [RequireComponent(typeof(Worker))]
    public class WorkerHeadSign : MonoBehaviour
    {
        Worker worker;
        [SerializeField] TMPro.TMP_Text statsText;

        void Start() 
        {
            worker = GetComponent<Worker>();
            worker.onWorkerJobStatusChange += WorkerStatus;
        }

        void OnDisable() 
        {
            worker.onWorkerJobStatusChange -= WorkerStatus;
        }

        void WorkerStatus(bool isWorking)
        {
            if(isWorking)
            {
                statsText.enabled = true;
                statsText.text = "Working...";
            }
            else statsText.enabled = false;
        }
    }
}