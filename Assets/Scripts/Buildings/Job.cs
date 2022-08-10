using UnityEngine;
using UnityEngine.Events;
using Valzuroid.SurvivalGame.Creatures.Workers;

namespace Valzuroid.SurvivalGame.Buildings
{
    [System.Serializable]
    public class Job : MonoBehaviour
    {
        public UnityEvent onJobStarted;
        public UnityEvent onJobFinished;
        public UnityEvent onJobStopped;
        [Tooltip("Set to -1 if a passive job")]
        [SerializeField] float jobDuration;
        [SerializeField] int requiredWorkers;
        public float Progress{ get { return (jobDuration - timeLeftToFinish) / jobDuration; }}
        public int RequiredWorkers{ get{ return requiredWorkers; } }
        int currentWorkers;
        public int CurrentWorkers{ get{ return currentWorkers; } }
        bool isSeeking = false;
        public bool IsSeeking{ get{ return isSeeking; } }
        bool isWorking;
        public bool IsWorking{ get{ return isWorking; } }
        bool isDone;
        public bool IsDone{ get{ return isDone; } }
        float timeLeftToFinish = 0;

        public void SubscribeJob()
        {
            WorkersManager.Instance.AddJob(this);
            isSeeking = true;
        }

        void Update()
        {
            if(!isWorking) return;
            timeLeftToFinish -= Time.deltaTime;
            if(timeLeftToFinish <= 0 && jobDuration > 0)
            {
                onJobFinished?.Invoke();
                isWorking = false;
                isDone = true;
            }
        }

        public bool Work()
        {
            if(currentWorkers == requiredWorkers) return false;
            currentWorkers++;
            if(currentWorkers == requiredWorkers)
            {
                WorkersManager.Instance.RemoveJob(this);
                isSeeking = false;
                isWorking = true;
                timeLeftToFinish = jobDuration;
                onJobStarted?.Invoke();
            }
            return true;
        }

        public void LeaveWork()
        {
            currentWorkers--;
            WorkersManager.Instance.AddJob(this);
            isSeeking = true;
            isWorking = false;
            onJobStopped?.Invoke();
        }
    }
}