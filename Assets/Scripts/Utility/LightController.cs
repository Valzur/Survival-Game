using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Valzuroid.SurvivalGame.Utility
{
    [RequireComponent(typeof(Light2D))]
    public class LightController : MonoBehaviour
    {
        Light2D lighting;
        
        void Start()
        {
            lighting = GetComponent<Light2D>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}