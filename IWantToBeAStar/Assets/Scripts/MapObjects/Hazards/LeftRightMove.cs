using UnityEngine;
using System.Collections;

namespace IWantToBeAStar.MapObjects.Hazards
{
    public class LeftRightMove : MonoBehaviour
    {
        public float speed;

        // Use this for initialization
        void Start()
        {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

            if (transform.position.x == GameData.LeftPosition.x)
            {
                // 머리가 오른쪽을 보도록 뒤집기
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;

                rigidbody.velocity = transform.right * speed;
            }
            else
            {
                rigidbody.velocity = transform.right * -speed;
            }
        }
    }

}
