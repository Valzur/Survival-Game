using UnityEngine;

namespace Valzuroid.SurvivalGame.Utility
{
    [RequireComponent(typeof(Camera))]
    public class FollowPlayerX : MonoBehaviour
    {
        [SerializeField] float Zpos = -10;
        [SerializeField] float minHeight = 0;

        void Update()
        {
            Vector3 targetPos = transform.position;
            Vector3 playerPos = Creatures.Player.PlayerController.Instance.transform.position;
            targetPos.z = Zpos;
            targetPos.x = playerPos.x;
            targetPos.y = Mathf.Max(playerPos.y, minHeight);
            transform.position = targetPos;
        }
    }
}