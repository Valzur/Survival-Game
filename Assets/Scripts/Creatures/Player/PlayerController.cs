using UnityEngine;
using UnityEngine.Events;

namespace Valzuroid.SurvivalGame.Creatures.Player
{
    [RequireComponent(typeof(PlayerProperties))]
    [RequireComponent(typeof(Animator))]
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;
        [SerializeField] float checkHeight = 0.7f;
        Rigidbody2D rb;
        PlayerProperties playerProperties;
        Animator playerAnimator;
        int remainingJumps;
        bool isAlive = true;
        public UnityEvent onJump = new UnityEvent();

        void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * checkHeight);    
        }

        void OnEnable()
        { 
            rb = GetComponent<Rigidbody2D>();
            playerProperties = GetComponent<PlayerProperties>();
            playerAnimator = GetComponent<Animator>();

            playerProperties.onDeath += OnDeath;

            Instance = this;
            remainingJumps = playerProperties.NumberOfJumps - 1;
        }

        void Update()
        {
            if(!isAlive) return;
            CheckGround();
            RotatePlayer();
            CheckInput();
            Heal();
        }

        void CheckGround()
        {
            //Check if player is standing on ground.
            int mask = LayerMask.GetMask("Environment");
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, checkHeight, mask);
            if(hitInfo.collider)
            {
                remainingJumps = playerProperties.NumberOfJumps - 1;
                playerAnimator.SetBool("isFalling", false);
            } 
            else playerAnimator.SetBool("isFalling", true);
        }

        void RotatePlayer()
        {
            //Rotate based on mouse position.
            float mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            if(mousePos > transform.position.x) transform.localScale = Vector3.one;
            if(mousePos < transform.position.x) transform.localScale = new Vector3(-1, 1, 1);
        }

        void CheckInput()
        {
            float speed = Input.GetAxis("Horizontal");
            playerAnimator.SetFloat("speed", speed);
            playerAnimator.SetBool("isMoving", speed != 0);
            rb.velocity = new Vector2( speed * playerProperties.RunSpeed, rb.velocity.y);

            playerAnimator.SetBool("isCrouching", Input.GetKey(KeyCode.LeftControl));

            
            if(Input.GetMouseButtonDown(1)) playerAnimator.SetTrigger("Kick");
            if(playerAnimator.GetBool("isCrouching")) return;

            if(Input.GetKeyDown(KeyCode.Space) && remainingJumps > 0)
            {
                onJump?.Invoke();
                remainingJumps--;
                rb.AddForce(playerProperties.JumpHeight * Vector3.up);
                playerAnimator.SetTrigger("Jump");
            } 
            if(Input.GetMouseButtonDown(0)) playerAnimator.SetTrigger("Punch");

        }

        void Heal()
        {
            if(GameManager.Instance.IsNight) return;
            if(!isAlive) return;
            playerProperties.Heal(Time.deltaTime * playerProperties.HealingRate);
        }

        void OnDeath()
        {
            isAlive = false;
        }
        
        public void ActivateAttackCollider()
        {
            string animationName;
            animationName = playerAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
            foreach (var attack in playerProperties.PlayerAttacks)
            {
                if(attack.name == animationName) 
                {
                    attack.collidersRoot.gameObject.SetActive(true);
                    attack.onAttack?.Invoke();
                }
            }
        }

        public void DeActivateAttackCollider()
        {
            string animationName;
            animationName = playerAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
            foreach (var attack in playerProperties.PlayerAttacks)
            {
                if(attack.name == animationName) attack.collidersRoot.gameObject.SetActive(false);
            }
        }

        public void Damage(int amt)
        {
            playerProperties.Damage(amt);
            playerAnimator.SetTrigger("Hurt");
        }
    }
}