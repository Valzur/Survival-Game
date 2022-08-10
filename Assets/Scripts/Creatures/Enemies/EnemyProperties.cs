using UnityEngine;
using Valzuroid.SurvivalGame.Utility;

namespace Valzuroid.SurvivalGame.Creatures.Enemies
{
    public class EnemyProperties : MonoBehaviour
    {
        public event VoidEvent onDeath;
        public event VoidEvent onHealthChanged;
        [SerializeField] Attack[] enemyAttacks;
        [SerializeField] float speed;
        [SerializeField] float attackDamage;
        [SerializeField] float baseMaxHealth;
        [SerializeField] float attackSpeed;
        [SerializeField] float xpMultiplier;
        [SerializeField] int coinsWorth = 100;
        Upgrades upgrades;
        int level = 1;
        float _currentHealth;
        public float currentHealth
        {
            get{ return _currentHealth; }
            private set
            {
                _currentHealth = value;
                onHealthChanged?.Invoke();
            }
        }
        public float MaxHealth
        { 
            get
            {
                int health = (int)(baseMaxHealth / (1 + upgrades.EnemyHealthMultiplier) - upgrades.EnemyHealthFlat);
                return Mathf.Max(health, 1);
            } 
        }
        public Attack[] EnemyAttacks{ get{ return enemyAttacks; } }
        public float Speed
        { 
            get
            { 
                float spd = speed / (1 + upgrades.EnemySpeedMultiplier) - upgrades.EnemySpeedFlat;
                return Mathf.Max(0.5f, speed); 
            } 
        }
        public float AttackSpeed{ get{ return attackSpeed; } }

        void Awake()
        {
            upgrades = GetComponent<Upgrades>();
            if(upgrades)
                upgrades.onUpgradesDirty.AddListener(OnUpgradesDirty);
            else  
                upgrades = gameObject.AddComponent<Upgrades>();
            currentHealth = MaxHealth;
        }

        void OnValidate() 
        {
            foreach (var attack in enemyAttacks)
            {
                if(attack.collidersRoot) attack.collidersRoot.damage = attack.damage;
            }
        }
        
        void OnDestroy() => Player.PlayerProperties.Instance.EarnCoins(coinsWorth);

        void OnUpgradesDirty()
        {
            //Health
            currentHealth = Mathf.Min(currentHealth, MaxHealth);

            //Attacks
            foreach (var attack in enemyAttacks)
            {
                int damage = (int)(attack.damage / (1 - upgrades.EnemyDamageMultiplier) - upgrades.EnemyDamageFlat);
                if(attack.collidersRoot) attack.collidersRoot.damage = Mathf.Max(damage, 1);
            }
        }

        public void Damage(int amt)
        {
            currentHealth -= amt;
            if(currentHealth <= 0)
            {
                Player.PlayerProperties.Instance.AddXP((int)(xpMultiplier * level));
                currentHealth = 0;
                onDeath?.Invoke();
                Destroy(this.gameObject);
            }
        }
    }
}