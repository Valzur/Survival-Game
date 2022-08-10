using UnityEngine;

namespace Valzuroid.SurvivalGame.Utility
{
    public class GroundSensor : MonoBehaviour
    {
        [SerializeField] float checkHeight = 2.0f;
        bool isGrounded = false;
        float currentHeight;
        public float CurrentHeight{ get{ return currentHeight; } }
        public bool IsGrounded{ get{ return isGrounded; } }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * checkHeight);
        }
        void CheckGround()
        {
            //Check if player is standing on ground.
            int mask = LayerMask.GetMask("Environment");
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, mask);
            if(hitInfo.collider)
            {
                currentHeight = hitInfo.distance;
                isGrounded = (currentHeight < checkHeight);
                return;
            }
            isGrounded = false;
        }

        void Update()
        {
            CheckGround();
        }
    }
}