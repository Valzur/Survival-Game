using UnityEngine;


namespace Valzuroid.SurvivalGame.Creatures.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(EnemyProperties))]
    public abstract class EnemyController : MonoBehaviour
    {
        [SerializeField] protected float maxPlayerFollowRange = 10f;
        [SerializeField] protected float minPlayerFollowRange = 1.0f;
        protected Rigidbody2D rb;
        protected Animator animator;
        protected EnemyProperties enemyProperties;
        protected bool isAlive = true;
        float timeLeftToAttack;

        protected void BaseOnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, minPlayerFollowRange);
            Gizmos.DrawWireSphere(transform.position, maxPlayerFollowRange);
        }
        
        protected void BaseStart()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            enemyProperties = GetComponent<EnemyProperties>();
        }

        protected void BaseUpdate()
        {
            if(!isAlive) return;
            Vector3 playerPos = Player.PlayerController.Instance.transform.position;
            timeLeftToAttack -= Time.deltaTime;
            RotatePlayer(playerPos);
            FollowPlayer(playerPos);
        }

        void RotatePlayer(Vector3 playerPos)
        {
            //Rotate based on player position.
            if(playerPos.x > transform.position.x) transform.localScale = Vector3.one;
            if(playerPos.x < transform.position.x) transform.localScale = new Vector3(-1, 1, 1);
        }

        void FollowPlayer(Vector3 playerPos)
        {
            MoveTowards(playerPos);
        }

        void OnDeath()
        {
            isAlive = false;
            animator.SetTrigger("Death");
        }

        protected abstract void MoveTowards(Vector3 playerPos);
        protected void Attack()
        {
            if(timeLeftToAttack > 0) return;
            timeLeftToAttack = 1 / enemyProperties.AttackSpeed;
            AttackGFX();
        }
        protected abstract void AttackGFX();

        public void ActivateAttackCollider()
        {
            string animationName;
            animationName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
            foreach (var attack in enemyProperties.EnemyAttacks)
            {
                if(attack.name == animationName) attack.collidersRoot.gameObject.SetActive(true);
            }
        }

        public void DeActivateAttackCollider()
        {
            string animationName;
            animationName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
            foreach (var attack in enemyProperties.EnemyAttacks)
            {
                if(attack.name == animationName) attack.collidersRoot.gameObject.SetActive(false);
            }
        }


        public void Damage(int amt)
        {
            enemyProperties.Damage(amt);
            animator.SetTrigger("Hurt");
        }

        
    }
}