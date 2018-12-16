using UnityEngine;

namespace IWantToBeAStar.MapObjects
{
    public class PlayerMove : MonoBehaviour
    {
        public float speed = 10f;
        public MapSize mapSize;

        // Use this for initialization
        private void Start()
        {
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            #region 마우스 커서 따라가기

            transform.position = Vector2.Lerp
                (transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), speed);

            // 플레이어 회전 부분
            /*
            Vector3 difference =
                Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            difference.Normalize();
            float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);
            */

            // 플레이어가 맵 밖을 나가지 않도록 하는 코드
            transform.position = new Vector2(
                Mathf.Clamp(transform.position.x, mapSize.xMin, mapSize.xMax),
                Mathf.Clamp(transform.position.y, mapSize.yMin, mapSize.yMax));

            #endregion 마우스 커서 따라가기

            // 아래 코드는 키보드 입력을 이용한 움직임 코드임
            /*
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");

            Vector2 velocity = new Vector2(inputX, inputY);

            velocity *= speed;
            rigidbody.velocity = velocity;

            rigidbody.position = new Vector2(
                Mathf.Clamp(rigidbody.position.x, mapSize.xMin, mapSize.xMax),
                Mathf.Clamp(rigidbody.position.y, mapSize.yMin, mapSize.yMax));
            */
        }
    }

    [System.Serializable]
    public class MapSize
    {
        public float xMin, xMax, yMin, yMax;
    }
}