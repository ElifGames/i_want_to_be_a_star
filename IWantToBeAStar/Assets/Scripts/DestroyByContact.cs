﻿using System;
using UnityEngine;

namespace IWantToBeAStar
{
    public class DestroyByContact : MonoBehaviour
    {
        // Use this for initialization
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Boundary")
            {
                return;
            }
            if (other.tag == "Player")
            {
                GameData.IsGameEnd = true;
            }
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}