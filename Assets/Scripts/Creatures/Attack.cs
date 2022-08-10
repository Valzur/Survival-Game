using UnityEngine.Events;

namespace Valzuroid.SurvivalGame.Creatures
{
    [System.Serializable]
    public class Attack 
    {
        public string name;
        public AttackTrigger collidersRoot;
        public int damage;
        public UnityEvent onAttack;
    }
}