using UnityEngine;

namespace Valzuroid.SurvivalGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public Utility.VoidEventBool onDayStateChange;
        public bool IsNight{ get { return isNight; } }
        [SerializeField] Vector3 center;
        [SerializeField] float radius;
        [SerializeField] float nightLength;
        [SerializeField] float dayLength;
        [SerializeField] Vector2Int gridSize;
        [SerializeField] Transform sunReference;
        [SerializeField] Transform moonReference;
        float[,] lightGrid;
        bool isNight = false;
        float progress;

        void OnDrawGizmos() 
        {
            //Gizmos.DrawLine(pos + Vector3.up * 2, pos);
            Gizmos.DrawWireSphere(center, radius);
        }

        void Awake() => Instance = this;

        void Start()
        {
            moonReference.gameObject.SetActive(false);
            lightGrid = new float[gridSize.x, gridSize.y];
        }

        void Update()
        {
            progress += Time.deltaTime / (isNight ? nightLength: dayLength);
            float timeLeft = (1f - progress);
            timeLeft *= (isNight ? nightLength: dayLength);
            UIManager.Instance.UpdateCycle((int) timeLeft, isNight);
            sunReference.position = GetCirclePos(Mathf.PI + progress * 2 * Mathf.PI);
            moonReference.position = GetCirclePos(Mathf.PI + progress * 2 * Mathf.PI);
            if(progress >= 1) SwitchState();
        }

        Vector3 GetCirclePos(float t)
        {
            Vector3 pos = Vector3.zero;
            pos.x = center.x + radius * Mathf.Sin(t);
            pos.y = center.y + radius * Mathf.Cos(t);
            return pos;
        }

        void SwitchState()
        {
            progress = 0;
            if(isNight)
            {
                isNight = false;
                moonReference.gameObject.SetActive(false);
                sunReference.gameObject.SetActive(true);
                onDayStateChange?.Invoke(IsNight);
                return;
            }
            isNight = true;
            sunReference.gameObject.SetActive(false);
            moonReference.gameObject.SetActive(true);

            onDayStateChange?.Invoke(IsNight);
        }
    }
}