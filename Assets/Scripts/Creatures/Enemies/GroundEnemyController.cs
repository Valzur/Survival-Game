using UnityEngine;

namespace Valzuroid.SurvivalGame.Creatures.Enemies
{
    public class GroundEnemyController : EnemyController
    {
        [SerializeField] Utility.GroundSensor groundSensor;

        void Start()
        {
            base.BaseStart();
        }

        void OnDrawGizmos() => base.BaseOnDrawGizmos();
        
        void Update()
        {
            animator.SetBool("Grounded", groundSensor.IsGrounded);
            base.BaseUpdate();
        }
        
        protected override void AttackGFX() => animator.SetTrigger("Attack");
        
        protected override void MoveTowards(Vector3 playerPos)
        {
            if(!animator.GetBool("Grounded")) return;
            animator.SetInteger("AnimState", 1);
            if(Mathf.Abs(playerPos.x - transform.position.x) < minPlayerFollowRange)
            {
                rb.velocity = Vector2.zero;
                Attack();
                return;
            } 
            if(Mathf.Abs(playerPos.x - transform.position.x) > maxPlayerFollowRange || 
            playerPos.y > transform.position.y + 4)
            {
                    rb.velocity = Vector2.zero;
                    return;
            } 

            animator.SetInteger("AnimState", 2);

            if(playerPos.x > transform.position.x)
            {
                //Basic movement.
                rb.velocity = Vector2.right * enemyProperties.Speed;
            }

            if(playerPos.x < transform.position.x) 
            {
                //Basic movement.
                rb.velocity = Vector2.left * enemyProperties.Speed;
            }

        }
    }
}