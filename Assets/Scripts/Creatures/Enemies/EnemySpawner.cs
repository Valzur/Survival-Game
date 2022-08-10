using System.Collections.Generic;
using UnityEngine;

namespace Valzuroid.SurvivalGame.Creatures.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        public static EnemySpawner Instance;
        [SerializeField] float spawnRate = 0.5f;
        EnemyController[] enemies;
        List<EnemySpawnPoint> enemySpawnPoints = new List<EnemySpawnPoint>();
        float timeLeftToSpawn = 0;
        bool isSimulating = true;

        void Awake() => Instance = this;
        void Start()
        {
            enemies = Resources.LoadAll<EnemyController>("Enemies");
        }

        void Update()
        {
            timeLeftToSpawn -= Time.deltaTime;

            if(!isSimulating) return;
            if(GameManager.Instance.IsNight) return;
            
            if(timeLeftToSpawn <= 0)
            {
                timeLeftToSpawn = 1/spawnRate;
                int enemyChoice = Random.Range(0, enemies.Length);
                int spawnPointChoise = Random.Range(0, enemySpawnPoints.Count);
                Instantiate(enemies[enemyChoice], enemySpawnPoints[spawnPointChoise].transform.position, Quaternion.identity);
            }
        }

        public void AddSpawnPoint(EnemySpawnPoint enemySpawnPoint) => enemySpawnPoints.Add(enemySpawnPoint);
        public void RemoveSpawnPoint(EnemySpawnPoint enemySpawnPoint) => enemySpawnPoints.Remove(enemySpawnPoint);
    }
}