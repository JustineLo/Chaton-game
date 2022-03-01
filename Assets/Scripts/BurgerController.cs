using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerController : MonoBehaviour
{
    
        void Start()
        {

        }

        void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.tag == "Player" || collision.tag == "Ground")
            {
               

               


            }
        }

    
}
