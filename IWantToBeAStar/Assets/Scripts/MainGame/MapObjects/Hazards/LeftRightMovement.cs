using UnityEngine;

namespace IWantToBeAStar.MainGame.MapObjects.Hazards
{
    public class LeftRightMovement : BaseHazard
    {
        public float speed;
        public bool IsRandomSpeed;

        protected override void HazardStart()
        {
            base.HazardStart();
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

            if (IsRandomSpeed)
            {
                speed = Random.Range(3f, 7f);
            }

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

            PlaySound();
        }
    }
}