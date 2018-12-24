using UnityEngine;

namespace IWantToBeAStar.MainGame
{
    public class DestroyByTime : MonoBehaviour
    {
        public float lifeTime;

        // Use this for initialization
        private void Start()
        {
            Destroy(gameObject, lifeTime);
        }
    }
}