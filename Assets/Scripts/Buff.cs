using UnityEngine;

namespace Valzuroid.SurvivalGame
{
    [System.Serializable]
    public class Buff: MonoBehaviour
    {
        #region Serialized References
        [SerializeField] float baseEffectRange;
        [SerializeField] float towerRangeMultiplier;
        [SerializeField] float towerRangeFlat = 0;
        [SerializeField] float towerDamageMultiplier;
        [SerializeField] float towerDamageFlat = 0;
        [SerializeField] float towerHealthMultiplier;
        [SerializeField] float towerHealthFlat = 0;
        [SerializeField] float playerRangeMultiplier;
        [SerializeField] float playerRangeFlat = 0;
        [SerializeField] float playerDamageMultiplier;
        [SerializeField] float playerDamageFlat = 0;
        [SerializeField] float playerHealthMultiplier;
        [SerializeField] float playerHealthFlat = 0;
        [SerializeField] float playerSpeedMultiplier;
        [SerializeField] float playerSpeedFlat = 0;
        [SerializeField] float playerXPMultiplier;
        [SerializeField] float enemyRangeMultiplier;
        [SerializeField] float enemyRangeFlat = 0;
        [SerializeField] float enemyDamageMultiplier;
        [SerializeField] float enemyDamageFlat = 0;
        [SerializeField] float enemyHealthMultiplier;
        [SerializeField] float enemyHealthFlat = 0;
        [SerializeField] float enemySpeedMultiplier;
        [SerializeField] float enemySpeedFlat = 0;
        #endregion
        Upgrades upgrades;
        #region Getters
        public float EffectRange{ get{ return baseEffectRange * (1 + upgrades.TowerRangeMultiplier) + upgrades.TowerRangeFlat; } }
        public float TowerRangeMultiplier{ get{ return towerRangeMultiplier; } }
        public float TowerRangeFlat{ get{ return towerRangeFlat; } }
        public float TowerDamageMultiplier{ get{ return towerDamageMultiplier; } }
        public float TowerDamageFlat{ get{ return towerDamageFlat; } }
        public float TowerHealthMultiplier{ get{ return towerHealthMultiplier; } }
        public float TowerHealthFlat{ get{ return towerHealthFlat; } }
        public float PlayerRangeMultiplier{ get{ return playerRangeMultiplier; } }
        public float PlayerRangeFlat{ get{ return playerRangeFlat; } }
        public float PlayerDamageMultiplier{ get{ return playerDamageMultiplier; } }
        public float PlayerDamageFlat{ get{ return playerDamageFlat; } }
        public float PlayerHealthMultiplier{ get{ return playerHealthMultiplier; } }
        public float PlayerHealthFlat{ get{ return playerHealthFlat; } }
        public float PlayerSpeedMultiplier{ get{ return playerSpeedMultiplier; } }
        public float PlayerSpeedFlat{ get{ return playerSpeedFlat; } }
        public float PlayerXPMultiplier{ get{ return playerXPMultiplier; } }
        public float EnemyRangeMultiplier{ get{ return enemyRangeMultiplier; } }
        public float EnemyRangeFlat{ get{ return enemyRangeFlat; } }
        public float EnemyDamageMultiplier{ get{ return enemyDamageMultiplier; } }
        public float EnemyDamageFlat{ get{ return enemyDamageFlat; } }
        public float EnemyHealthMultiplier{ get{ return enemyHealthMultiplier; } }
        public float EnemyHealthFlat{ get{ return enemyHealthFlat; } }
        public float EnemySpeedMultiplier{ get{ return enemySpeedMultiplier; } }
        public float EnemySpeedFlat{ get{ return enemySpeedFlat; } }
        #endregion
        #region UnityFunctions

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, EffectRange);
        }
#endif
        #endregion
        #region Functions

        void Awake()
        {
            upgrades = GetComponent<Upgrades>();
            if(!upgrades) upgrades = gameObject.AddComponent<Upgrades>();
        }
        void OnEnable()
        {
            OnBuildingsChanged();
            Buildings.Building.onBuildingsChanged.AddListener(OnBuildingsChanged);
        }

        void OnDisable()
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, EffectRange);
            foreach (var col in colliders)
            {
                var upgrades = col.GetComponent<Upgrades>();
                if(upgrades) upgrades.RemoveBuff(this);
            }
        }

        void OnBuildingsChanged()
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, EffectRange);
            foreach (var col in colliders)
            {
                if(col.transform == transform) continue;
                var upgrades = col.GetComponent<Upgrades>();
                if(upgrades) upgrades.AddBuff(this);
            }
        }
        #endregion
    }
}