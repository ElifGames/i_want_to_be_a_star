using UnityEngine;

namespace IWantToBeAStar.MainGame.MapObjects.Hazards
{
    public class DownMovement : MonoBehaviour
    {
        public float speed;

        // Start is called before the first frame update
        private void Start()
        {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.velocity = transform.up * -speed;
        }
    }
}