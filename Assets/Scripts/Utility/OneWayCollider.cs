using UnityEngine;

namespace Valzuroid.SurvivalGame.Utility
{
    [RequireComponent(typeof(Collider2D))]
    public class OneWayCollider : MonoBehaviour
    {
        [SerializeField] float threshold = 0.5f;
        Collider2D col;
        float colliderHeight;
        void Awake() => col = GetComponent<Collider2D>();

        void OnEnable() => colliderHeight = col.bounds.max.y;

        void Update()
        {
            float playerHeight = Valzuroid.SurvivalGame.Creatures.Player.PlayerController.Instance.transform.position.y;
            col.enabled = colliderHeight + threshold < playerHeight;
        }
    }
}