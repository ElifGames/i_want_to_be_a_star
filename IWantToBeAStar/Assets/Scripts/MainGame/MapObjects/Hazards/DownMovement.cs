using UnityEngine;

namespace IWantToBeAStar.MainGame.MapObjects.Hazards
{
    public class DownMovement : BaseHazard
    {
        #region Unity Settings
        public float Speed;
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
            rigidbody.velocity = transform.up * -Speed;
        }

        protected override void HazardUpdate()
        {
        }
    }
}