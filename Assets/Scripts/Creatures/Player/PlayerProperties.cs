using UnityEngine;
using Valzuroid.SurvivalGame.Utility;

namespace Valzuroid.SurvivalGame.Creatures.Player
{
    [RequireComponent(typeof(Upgrades))]
    public class PlayerProperties : MonoBehaviour
    {
        public static PlayerProperties Instance;
        public event VoidEvent onDeath;
        [SerializeField] Attack[] playerAttacks;
        [Header("Base Stats")]
        [SerializeField] int baseMaxHealth;
        [SerializeField] float baseJumpHeight;
        [SerializeField] int baseNumberOfJumps;
        [SerializeField] float baseWalkSpeed;
        [SerializeField] float baseRunSpeed;
        [SerializeField] float healingRate = 2;
        Upgrades upgrades;
        float damageMultiplier = 1.0f;
        public Attack[] PlayerAttacks{ get{ return playerAttacks; } }

        float _currentXP;
        float currentXP{ 
            set
            {
                _currentXP = value; 
                UIManager.Instance.UpdateXP( value / MaxXP);
            }
            get{ return _currentXP; }
        }
        float _currentHealth;
        float currentHealth{ 
            set
            {
                _currentHealth = value; 
                UIManager.Instance.UpdateHealth( value / MaxHealth);
            }
            get{ return _currentHealth; }
        }
        int _currentLevel = 1;
        int currentLevel{ 
            set
            {
                _currentLevel = value; 
                UIManager.Instance.UpdateLevel(value);
            }
            get{ return _currentLevel; }
        }
        int _coins;
        public int Coins
        { 
            private set
            { 
                _coins = value; 
                UIManager.Instance.UpdateCoins(value);
            }
            get{ return _coins; } 
        }
        public int MaxHealth{ get{ return (int)(baseMaxHealth * (1 + upgrades.PlayerHealthMultiplier) + upgrades.PlayerHealthFlat); } }
        public float JumpHeight{ get{ return baseJumpHeight; } }
        public int NumberOfJumps{ get{ return baseNumberOfJumps; } }
        public float WalkSpeed{ get{ return baseWalkSpeed * (1 + upgrades.PlayerSpeedMultiplier) + upgrades.PlayerSpeedFlat; } }
        public float RunSpeed{ get{ return baseRunSpeed * (1 + upgrades.PlayerSpeedMultiplier) + upgrades.PlayerSpeedFlat; } }
        public float HealingRate{ get{ return healingRate; } }
        float MaxXP
        {
            get{ return 100 * (Mathf.Pow(currentLevel, 2.0f)); } 
        }

        void OnValidate() 
        {
            foreach (var attack in playerAttacks)
            {
                if(attack.collidersRoot) attack.collidersRoot.damage = attack.damage;
            }
        }
        
        void Awake()
        {
            upgrades = GetComponent<Upgrades>();
            if(upgrades)
                upgrades.onUpgradesDirty.AddListener(OnUpgradesDirty);
            else  
                upgrades = gameObject.AddComponent<Upgrades>();
            upgrades.onUpgradesDirty.AddListener(OnUpgradesDirty);
            Instance = this;
            currentHealth = MaxHealth;
        }

        void OnUpgradesDirty()
        {
            //Health
            currentHealth = Mathf.Min(currentHealth, MaxHealth);

            //Damage
            foreach (var attack in playerAttacks)
            {
                int damage = (int)(attack.damage * (1 - upgrades.PlayerDamageMultiplier) + upgrades.PlayerDamageFlat);
                if(attack.collidersRoot) attack.collidersRoot.damage = damage;
            }
        }

        public void Heal(float amt)
        {
            currentHealth += Mathf.Clamp(currentHealth + amt, 0, MaxHealth);
        }

        public bool Buy(int amt)
        {
            if(amt > Coins) return false;
            Coins -= amt;
            return true;
        }
        
        public void EarnCoins(int amt) => Coins += amt;
        
        public void Damage(int amt)
        {
            currentHealth = Mathf.Clamp(currentHealth - amt, 0, MaxHealth);
            if(currentHealth == 0) onDeath?.Invoke();
        }

        public void AddXP(int amt)
        {
            currentXP += amt * (1 + upgrades.PlayerXPMultiplier);
            if(currentXP >= MaxXP)
            {
                currentXP -= MaxXP;
                currentLevel++;
            }
        }
    }
}