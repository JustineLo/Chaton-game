using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerController : MonoBehaviour
{
   

    void Start()
    {
      
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player" || collision.tag == "Ground")
        {


            Destroy(gameObject);


        }
    }
}
