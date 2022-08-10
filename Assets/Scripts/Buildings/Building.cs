using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Valzuroid.SurvivalGame.Utility;

namespace Valzuroid.SurvivalGame.Buildings
{
    [System.Serializable]
    public struct BuildConstraints
    {
        public bool up;
        public bool right;
        public bool left;
    }

    [RequireComponent(typeof(SpriteRenderer))]
    public class Building : MonoBehaviour
    {
        #region Initializations
        public static UnityEvent onBuildingsChanged = new UnityEvent();
        public VoidEvent onBuildingDestroyed;
        public VoidEvent onBuildingDamaged;
        [SerializeField] BuildConstraints buildConstraints;
        [SerializeField] int baseBuildingMaxHealth = 2000;
        [SerializeField] Job job;
        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] Sprite inProgressSprite;
        [SerializeField] Sprite finishedSprite;
        [SerializeField] GameObject hpObject;
        [SerializeField] Image hpIMG;
        [SerializeField] int price;
        [SerializeField] Vector2 checkDistance;
        [SerializeField][Multiline] string description;
        int buildingMaxHealth{ get{ return (int)(baseBuildingMaxHealth * (1 + upgrades.TowerHealthMultiplier) + upgrades.TowerHealthFlat); } }
        Connections connections;
        bool isGrounded;
        int _currentHealth;
        bool isColliding = false;
        bool onFloor = false;
        Upgrades upgrades;
        public bool IsGrounded{ get{ return isGrounded; } }
        public bool CanBuild{ get{ return !isColliding && isGrounded; } }
        public int CurrentHealth
        { 
            get{ return _currentHealth; }
            private set {
                _currentHealth = value;
                if(hpIMG) hpIMG.fillAmount = value / buildingMaxHealth;
                } 
        }
        public BuildConstraints BuildingConstraints{ get{ return buildConstraints; } }
        public Connections Connections{ get{ return connections; } }
        public Sprite BuildingSprite{ get{ return finishedSprite; } }
        public int Price{ get{ return price; } }
        #endregion

        void OnDrawGizmos()
        {
            //Horizontal rays
            Vector3 pos = transform.position;
            pos.y -= checkDistance.y / 2.0f + 1;
            while(pos.y <= transform.position.y + checkDistance.y / 2.0f + 1)
            {
                Gizmos.DrawLine(pos, pos + Vector3.left * checkDistance.x);
                Gizmos.DrawLine(pos, pos + Vector3.right * checkDistance.x);
                pos.y ++;
            }

            //Vertical rays
            pos = transform.position;
            pos.x -= checkDistance.x / 2.0f + 1;
            while(pos.x <= transform.position.x + checkDistance.x / 2.0f + 1)
            {
                Gizmos.DrawLine(pos, pos + Vector3.down * checkDistance.y);
                Gizmos.DrawLine(pos, pos + Vector3.up * checkDistance.y);
                pos.x ++;
            }
        }

        void Awake()
        {
            upgrades = GetComponent<Upgrades>();
            if(upgrades)
                upgrades.onUpgradesDirty.AddListener(OnUpgradesDirty);
            else  
                upgrades = gameObject.AddComponent<Upgrades>();
        }
        
        void Start()
        {
            job.onJobStarted.AddListener(OnBuildingStarted);
            job.onJobFinished.AddListener(OnBuildingFinished);
            job.onJobStopped.AddListener(OnBuildingStopped);

            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = -(int)transform.position.y;
            spriteRenderer.sprite = inProgressSprite;
            Color color = spriteRenderer.color;
            color.a = 0.3f;
            spriteRenderer.color = color;
            CurrentHealth = buildingMaxHealth;
        }
        
        void Update()
        {
            if(job.IsSeeking || job.IsWorking || job.IsDone) return;
            FindConnections();
            CheckIsGrounded();
        }

        void OnMouseEnter() => hpObject.SetActive(true);

        void OnMouseExit() => hpObject.SetActive(false);

        void OnEnable() => onBuildingsChanged?.Invoke();
        
        void OnDisable() => onBuildingsChanged?.Invoke();

        void OnCollisionStay2D(Collision2D col)
        {
            isColliding = true;
        }

        void OnCollisionExit2D(Collision2D col)
        {
            isColliding = false;
        }

        void OnUpgradesDirty()
        {
            //Health
            CurrentHealth = Mathf.Min(CurrentHealth, buildingMaxHealth);
        }
        void OnBuildingStarted()
        {
            Color color = spriteRenderer.color;
            color.a = 0.5f;
            spriteRenderer.color = color;
        }

        void OnBuildingFinished()
        {
            spriteRenderer.sprite = finishedSprite;
            Color color = spriteRenderer.color;
            color.a = 1;
            spriteRenderer.color = color;
        }

        void OnBuildingStopped()
        {
            Color color = spriteRenderer.color;
            color.a = 0.3f;
            spriteRenderer.color = color;
        }

        public void Damage(int damage)
        {
            onBuildingDamaged?.Invoke();
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, buildingMaxHealth);
            if(CurrentHealth == 0) onBuildingDestroyed?.Invoke();
        }

        void FindConnections()
        {
            onFloor = false;
            connections.EmptyAll();
            //Horizontal rays
            Vector2 pos = transform.position;
            pos.y -= checkDistance.y / 2.0f + 1;
            while(pos.y <= transform.position.y + checkDistance.y / 2.0f + 1)
            {
                int mask = LayerMask.GetMask("Environment", "Building");
                RaycastHit2D rightRay = Physics2D.Raycast(pos, Vector2.right, checkDistance.x, mask);
                RaycastHit2D leftRay = Physics2D.Raycast(pos, Vector2.left, checkDistance.x, mask);
                
                if(leftRay)
                {
                    if(leftRay.transform.tag == "Building")
                        connections.LeftConnections.Add(leftRay.transform.GetComponent<Building>());
                    else onFloor = true;
                } 
                if(rightRay)
                {
                    if(rightRay.transform.tag == "Building")
                        connections.RightConnections.Add(rightRay.transform.GetComponent<Building>());
                    else onFloor = true;
                } 
                pos.y ++;
            }

            //Vertical rays
            pos = transform.position;
            pos.x -= checkDistance.x / 2.0f + 1;
            while(pos.x <= transform.position.x + checkDistance.x / 2.0f + 1)
            {
                int mask = LayerMask.GetMask("Environment", "Building");
                RaycastHit2D topRay = Physics2D.Raycast(pos, Vector2.up, checkDistance.y, mask);
                RaycastHit2D bottomRay = Physics2D.Raycast(pos, Vector2.down, checkDistance.y, mask);

                if(topRay)
                {
                    if(topRay.transform.tag == "Building")
                        connections.TopConnections.Add(topRay.transform.GetComponent<Building>());
                    else onFloor = true;
                } 
                if(bottomRay)
                {
                    if(bottomRay.transform.tag == "Building")
                        connections.BottomConnections.Add(bottomRay.transform.GetComponent<Building>());
                    else onFloor = true;
                } 
                pos.x ++;
            }
        }

        void CheckIsGrounded()
        {
            isGrounded = false;
            if(onFloor) 
            {
                isGrounded = true;
                return;
            }
            if(!connections.isConnected) return;
            foreach (var item in connections.BottomConnections)
            {
                if(item.IsGrounded)
                {
                    isGrounded = true;
                    return;
                }
            }

            foreach (var item in connections.TopConnections)
            {
                if(item.IsGrounded)
                {
                    isGrounded = true;
                    return;
                }
            }

            foreach (var item in connections.LeftConnections)
            {
                if(item.IsGrounded)
                {
                    isGrounded = true;
                    return;
                }
            }

            foreach (var item in connections.RightConnections)
            {
                if(item.IsGrounded)
                {
                    isGrounded = true;
                    return;
                }
            }
        }

        public void ConfirmBuilding() => job.SubscribeJob();
    }
}