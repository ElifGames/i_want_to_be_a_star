using UnityEngine;

namespace IWantToBeAStar.MainGame.MapObjects.Hazards
{
    public class LeftRightMovement : BaseHazard
    {
        #region Unity Settings
        public float Speed;
        public bool IsRandomSpeed;
        #endregion

        protected override void HazardAwake()
        {
        }

        protected override void HazardFixedUpdate()
        {
        }

        protected override void HazardStart()
        {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

            if (IsRandomSpeed)
            {
                System.Random random = new System.Random();
                Speed = random.Next(3, 10);
            }

            if (transform.position.x == GameData.LeftPosition.x)
            {
                // 머리가 오른쪽을 보도록 뒤집기
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;

                rigidbody.velocity = transform.right * Speed;
            }
            else
            {
                rigidbody.velocity = transform.right * -Speed;
            }
        }

        protected override void HazardUpdate()
        {
        }
    }
}