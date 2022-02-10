using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAction : MonoBehaviour
{
    [SerializeField] int value;
    [SerializeField] Color color;
    void Start()
    {
        GetComponent<SpriteRenderer>().color = color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            print("touched played bruh");
            GameManager.instance.AddScore(value);
            Destroy(gameObject);
        }
    }
}
