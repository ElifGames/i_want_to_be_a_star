using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IWantToBeAStar
{
    public class DestroyByTime : MonoBehaviour
    {
        public float lifeTime;
        // Use this for initialization
        void Start()
        {
            Destroy(gameObject, lifeTime);
        }
    }
}