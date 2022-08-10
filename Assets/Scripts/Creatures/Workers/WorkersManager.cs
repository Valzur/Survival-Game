using System.Collections.Generic;
using UnityEngine;
using Valzuroid.SurvivalGame.Buildings;

namespace Valzuroid.SurvivalGame.Creatures.Workers
{
    public class WorkersManager : MonoBehaviour
    {
        public static WorkersManager Instance;
        [SerializeField] int baseWorkersLimit;
        [SerializeField] Worker workerPrefab;
        [SerializeField] Transform workerSpawnPoint;
        List<Job> availableJobs = new List<Job>();
        int _workersLimit;
        int _currentWorkers = 0;
        Worker[] workersPrefabs;
        int workersLimit
        { 
            get{ return _workersLimit; }
            set
            {
                _workersLimit = value;
                UIManager.Instance.UpdateCurrentWorkers(currentWorkers, value);
            }
        }

        int currentWorkers
        { 
            get{ return _currentWorkers; }
            set
            { 
                _currentWorkers = value;
                UIManager.Instance.UpdateCurrentWorkers(value, workersLimit);
            } 
        }

        void Awake() => Instance = this;

        void Start() 
        {
            workersLimit = baseWorkersLimit;
            workersPrefabs = Resources.LoadAll<Worker>("Workers");
            GameManager.Instance.onDayStateChange += OnDayStateChange;
        }
        
        void OnDayStateChange(bool isNight)
        {
            if(isNight) return;
            int maxAmt = Mathf.Min(workersLimit - currentWorkers, 5);
            int choice = Random.Range(0, maxAmt);
            for (int i = 0; i < choice; i++)
            {
                SpawnNewWorker();
            }
        }

        void SpawnNewWorker()
        {
            Instantiate(workerPrefab, workerSpawnPoint.position, Quaternion.identity);
        }

        public void AddJob(Job job) => availableJobs.Add(job);
        public void RemoveJob(Job job) => availableJobs.Remove(job);
        public void AddWorker() => currentWorkers++;
        public void RemoveWorker() => currentWorkers--; 
        public Job JobSearch()
        {
            if(availableJobs.Count == 0) return null;
            return availableJobs[0];
        }
        public void IncreaseWorkersLimit(int amt) => workersLimit += amt;
        public void DecreaseWorkersLimit(int amt) => workersLimit -= amt;
    }
}