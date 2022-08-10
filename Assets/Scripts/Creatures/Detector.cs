using UnityEngine;
using System.Collections.Generic;

namespace Valzuroid.SurvivalGame.Creatures
{
    public class Detector : MonoBehaviour
    {
        [SerializeField] float detectionRange;
        [SerializeField] LayerMask layerMask;
        List<Collider2D> colliders = new List<Collider2D>();
        public Collider2D[] CollidersInRange{ get{ return colliders.ToArray(); } }

        void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, detectionRange);
        }

        void Update()
        {
            colliders.Clear();
            Collider2D[] currentColliders = Physics2D.OverlapCircleAll(transform.position, detectionRange, layerMask);
            if(currentColliders.Length == 0) return;
            foreach (var collider in currentColliders)
            {
                colliders.Add(collider);
            }
        }
    }
}