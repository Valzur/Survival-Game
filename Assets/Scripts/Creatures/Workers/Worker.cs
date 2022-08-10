using UnityEngine;
using Valzuroid.SurvivalGame.Utility;

namespace Valzuroid.SurvivalGame.Creatures.Workers
{
    [RequireComponent(typeof(GroundSensor))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Worker : MonoBehaviour
    {
        public VoidEventBool onWorkerJobStatusChange;
        [SerializeField] [Range(0, 20)] float movementRange;
        [SerializeField] float speed = 1.0f;
        Buildings.Job job;
        bool isWorking;
        float targetLocation;
        Animator workerAnimator;
        Rigidbody2D rb;
        GroundSensor groundSensor;
        public bool IsWorking{ get{ return isWorking; } }

        void Awake() 
        {
            groundSensor = GetComponent<GroundSensor>();
            rb = GetComponent<Rigidbody2D>();
            workerAnimator = GetComponent<Animator>();
        }

        void Start() => WorkersManager.Instance.AddWorker();

        void Update()
        {
            if(isWorking)
            {
                EmployedBehaviour();
                return;
            } 
            UnEmployedBehaviour();
            SearchForAJob();
        }

        void SearchForAJob()
        {
            job = WorkersManager.Instance.JobSearch();
            if(!job) return;
            if(!job.Work()) return;
            
            job.onJobFinished.AddListener(OnJobFinished);
            onWorkerJobStatusChange?.Invoke(true);
            targetLocation = job.transform.position.x;
            isWorking = true;
        }

        void UnEmployedBehaviour()
        {
            if(Mathf.Abs(transform.position.x - targetLocation) < 0.1f)
            {
                targetLocation = Random.Range(-movementRange, movementRange);
            }
            MoveTowards(targetLocation);
            return;
        }
        
        void EmployedBehaviour()
        {
            MoveTowards(targetLocation);
            if(job) return;
            isWorking = false;
            onWorkerJobStatusChange?.Invoke(false);
        }

        void MoveTowards(float? position)
        {
            if(!groundSensor.IsGrounded) return;
            if(!position.HasValue) return;
            if(Mathf.Abs(transform.position.x - position.Value) < 0.1f) 
            {
                rb.velocity = Vector2.zero;
                return;
            }
            float velocity = (position.Value > transform.position.x)? speed: -speed;
            rb.velocity = new Vector2(velocity, 0);
        }
        
        void OnJobFinished()
        {
            job = null;
            onWorkerJobStatusChange?.Invoke(false);
            isWorking = false;
        }
    }
}