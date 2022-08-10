using UnityEngine;

namespace Valzuroid.SurvivalGame.Creatures.Enemies
{
    public class ShowEnemyHealth : MonoBehaviour
    {
        [SerializeField] EnemyProperties enemyProperties;
        [SerializeField] UnityEngine.UI.Image hpBar;

        void Start()
        {
            if(!enemyProperties) return;
            enemyProperties.onHealthChanged += OnHealthChanged;
        }

        void OnHealthChanged()
        {
            hpBar.fillAmount = enemyProperties.currentHealth / enemyProperties.MaxHealth;
        }
    }
}