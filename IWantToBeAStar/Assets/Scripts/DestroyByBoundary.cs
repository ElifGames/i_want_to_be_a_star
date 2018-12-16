using UnityEngine;

namespace IWantToBeAStar
{
    public class DestroyByBoundary : MonoBehaviour
    {
        // Use this for initialization
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Destroy(other.gameObject);
        }
    }
}