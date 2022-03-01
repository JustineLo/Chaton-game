using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Ground")
        {


            Destroy(gameObject);


        }
        if (collision.tag == "Player")
        {


            

        }

    }
}
