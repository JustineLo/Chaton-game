using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanardController : MonoBehaviour
{
    private Transform player;
    Animator cameraAnim;
    public GameObject soundObject;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cameraAnim = Camera.main.GetComponent<Animator>();
        
        
    }

    void Update()
    {
       


    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (player != null && collision.tag == "Player")
        {
            cameraAnim.SetTrigger("shake");
            Instantiate(soundObject, transform.position, transform.rotation);

            player.GetComponent<playerController>().TakeDamages();
            Destroy(this.gameObject);


        }

        if (collision.tag == "Player" || collision.tag == "Ground")
        {
    
            Destroy(gameObject);

        }

    }
}
