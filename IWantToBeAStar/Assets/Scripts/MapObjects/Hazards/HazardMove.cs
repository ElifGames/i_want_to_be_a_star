using UnityEngine;

namespace IWantToBeAStar.MapObjects.Hazards
{
    public class HazardMove : MonoBehaviour
    {
        public float speed;

        // Use this for initialization
        private void Start()
        {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.velocity = transform.up * speed;
        }

        // Update is called once per frame
        private void Update()
        {
        }
    }
}