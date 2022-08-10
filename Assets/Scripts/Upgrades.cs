using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Valzuroid.SurvivalGame
{
    public enum Category{Tower, Player, Enemy}
    public class Upgrades : MonoBehaviour
    {
        public UnityEvent onUpgradesDirty;


        #region Serialized References
        [SerializeField] List<Buff> buffs = new List<Buff>();
        //Tower stuff
        [SerializeField] float towerRangeMultiplier;
        [SerializeField] float towerRangeFlat;
        [SerializeField] float towerDamageMultiplier;
        [SerializeField] float towerDamageFlat;
        [SerializeField] float towerHealthMultiplier;
        [SerializeField] float towerHealthFlat;

        //Player
        [SerializeField] float playerRangeMultiplier;
        [SerializeField] float playerRangeFlat;
        [SerializeField] float playerDamageMultiplier;
        [SerializeField] float playerDamageFlat;
        [SerializeField] float playerHealthMultiplier;
        [SerializeField] float playerHealthFlat;
        [SerializeField] float playerSpeedMultiplier;
        [SerializeField] float playerSpeedFlat;
        [SerializeField] float playerXPMultiplier;

        //Enemy
        [SerializeField] float enemyRangeMultiplier;
        [SerializeField] float enemyRangeFlat;
        [SerializeField] float enemyDamageMultiplier;
        [SerializeField] float enemyDamageFlat;
        [SerializeField] float enemyHealthMultiplier;
        [SerializeField] float enemyHealthFlat;
        [SerializeField] float enemySpeedMultiplier;
        [SerializeField] float enemySpeedFlat;
        #endregion
        
        #region Getters
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
        
        #region Functions
        public void AddBuff(Buff buff)
        {
            if(!buffs.Contains(buff)) 
            {
                buffs.Add(buff);
                RecalculateStats();
            }
        }

        public void RemoveBuff(Buff buff)
        {
            if(buffs.Remove(buff)) RecalculateStats();
        }

        void RecalculateStats()
        {
            foreach (var buff in buffs)
            {
                towerRangeMultiplier += buff.TowerRangeMultiplier;
                towerRangeFlat += buff.TowerRangeFlat;
                towerDamageMultiplier += buff.TowerDamageMultiplier;
                towerDamageFlat += buff.TowerDamageFlat;
                towerHealthMultiplier += buff.TowerHealthMultiplier;
                towerHealthFlat += buff.TowerHealthFlat;
                playerRangeMultiplier += buff.PlayerRangeMultiplier;
                playerRangeFlat += buff.PlayerRangeFlat;
                playerDamageMultiplier += buff.PlayerDamageMultiplier;
                playerDamageFlat += buff.PlayerDamageFlat;
                playerHealthMultiplier += buff.PlayerHealthMultiplier;
                playerHealthFlat += buff.PlayerHealthFlat;
                playerSpeedMultiplier += buff.PlayerSpeedMultiplier;
                playerSpeedFlat += buff.PlayerSpeedFlat;
                playerXPMultiplier += buff.PlayerXPMultiplier;
                enemyRangeMultiplier += buff.EnemyRangeMultiplier;
                enemyRangeFlat += buff.EnemyRangeFlat;
                enemyDamageMultiplier += buff.EnemyDamageMultiplier;
                enemyDamageFlat += buff.EnemyDamageFlat;
                enemyHealthMultiplier += buff.EnemyHealthMultiplier;
                enemyHealthFlat += buff.EnemyHealthFlat;
                enemySpeedMultiplier += buff.EnemySpeedMultiplier;
                enemySpeedFlat += buff.EnemySpeedFlat;
            }

            onUpgradesDirty?.Invoke();
        }
        #endregion
    }
}