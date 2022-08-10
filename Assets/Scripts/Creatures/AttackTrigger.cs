using UnityEngine;

namespace Valzuroid.SurvivalGame.Creatures
{
    [RequireComponent(typeof(Collider2D))]
    public class AttackTrigger : MonoBehaviour
    {
        [SerializeField] bool isPlayer = true;
        Collider2D collider;
        public int damage;

        void Awake() => collider = GetComponent<Collider2D>();
        void OnEnable()
        {
            ContactFilter2D contactFilter2D = new ContactFilter2D();
            System.Collections.Generic.List<Collider2D> cols = new System.Collections.Generic.List<Collider2D>();
            //print(collider.OverlapCollider(contactFilter2D, cols));
        }

        void OnTriggerStay2D(Collider2D other) 
        {
            print(other.name);

            if(isPlayer)
            {
                Enemies.EnemyController enemyController = other.GetComponent<Enemies.EnemyController>();
                if(enemyController) enemyController.Damage(damage);
                return;
            }

            Player.PlayerController playerController = other.GetComponent<Player.PlayerController>();
            if(playerController) playerController.Damage(damage);
            
        }
    }
}