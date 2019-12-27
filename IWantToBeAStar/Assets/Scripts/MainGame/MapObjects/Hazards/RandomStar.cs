using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStar : MonoBehaviour
{
    public List<Sprite> Stars;
    // Start is called before the first frame update
    void Start()
    {
        if (Stars == null) return;

        int starNumber = Random.Range(0, Stars.Count);
        GetComponent<SpriteRenderer>().sprite = Stars[starNumber];
    }
}
