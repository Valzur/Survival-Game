using UnityEngine;

namespace Valzuroid.SurvivalGame.Utility
{
    public class ParallaxEffect : MonoBehaviour
    {
        [SerializeField] Transform[] BGLayers;
        [SerializeField][Range(0, 0.1f)] float scale = 0.1f;
        Vector3 prevPlayerPos;

        void Start() => prevPlayerPos = SurvivalGame.Creatures.Player.PlayerController.Instance.transform.position;

        void Update()
        {
            Vector3 currentPlayerPos = SurvivalGame.Creatures.Player.PlayerController.Instance.transform.position;
            for (int i = 0; i < BGLayers.Length; i++)
            {
                Vector3 newPos = BGLayers[i].transform.position;
                newPos += (currentPlayerPos - prevPlayerPos) * (BGLayers.Length - i)/BGLayers.Length * scale;
                BGLayers[i].transform.position = newPos;
            }
            prevPlayerPos = currentPlayerPos;
        }
    }
}