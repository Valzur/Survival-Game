using UnityEngine;

namespace Valzuroid.SurvivalGame.Creatures.Enemies
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        void Start() => EnemySpawner.Instance.AddSpawnPoint(this);
        void OnDisable() => EnemySpawner.Instance.RemoveSpawnPoint(this);
    }
}