using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    public float speed=20;
    public int health=3;

    private Rigidbody2D rb;
    private Vector2 moveAmount;

    public Image[] coeurs;
    public Sprite coeurRouge;
    public Sprite coeurNoir;

    public int nbDiamonds=0;
    public Image[] diamonds;
    public Sprite emptyDiamond;
    public Sprite fullDiamond;

    private Animator anim;
    private Animator hitScreenAnim;
    private Animator chatAnim;
    public GameObject hitScreen;
    public GameObject chat;

    public float growDuration = 5;
    private float growTime;
    private bool isScaled=false;
    private bool isSpeed=false;
    private bool isHoldingDiamond = false;
    public GameObject diamond;

    public GameObject beerSoundObject;
    public GameObject burgerSoundObject;
    public GameObject diamondSoundObject;
    public GameObject winSoundObject;
    public GameObject loseSoundObject;

    private GameObject transitionSound;

    private SceneTransitions sceneTransitions;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        hitScreenAnim = hitScreen.GetComponent<Animator>();
        chatAnim = chat.GetComponent<Animator>();
        sceneTransitions = FindObjectOfType<SceneTransitions>();
    }


    private void Update()
    {

        anim.SetBool("isRunning", false);
        // Moving on the screen

        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        moveAmount = moveInput.normalized * speed;

        // Run animation

        if (moveInput != Vector2.zero)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        // Holding Diamond animation

        if (isHoldingDiamond == true)
        {
            anim.SetBool("holdDiamond", true);
        }
        else
        {
            anim.SetBool("holdDiamond", false);
        }

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);

    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (isScaled == false && collision.tag == "Burger")
        {
            Instantiate(burgerSoundObject, collision.transform.position, collision.transform.rotation);
            Destroy(collision.gameObject);
            transform.localScale = Vector2.one * 1.5f;
            growTime = Time.time;
            isScaled = true;

            StartCoroutine(ResetTimeBurger(5f));



        }

        IEnumerator ResetTimeBurger(float wait)
        {
            isScaled = true;

            yield return new WaitForSeconds(wait);

            transform.localScale = Vector2.one;
            isScaled = false;

        }

        if (isSpeed == false && collision.tag == "Beer")
        {
            Instantiate(beerSoundObject, collision.transform.position, collision.transform.rotation);
            speed = 40;
            growTime = Time.time;
            isSpeed = true;

            StartCoroutine(ResetTimeBeer(5f));

        }

        IEnumerator ResetTimeBeer(float wait)
        {
            isSpeed = true;

            yield return new WaitForSeconds(wait);

            speed = 20;
            isSpeed = false;

        }

        if (isHoldingDiamond == false && collision.tag == "Diamond")
        {
            Instantiate(diamondSoundObject, collision.transform.position, collision.transform.rotation);
            isHoldingDiamond = true;
            Destroy(collision.gameObject);
        }

        if (isHoldingDiamond == true && collision.tag == "Chat")
        {
            transform.GetChild(3).gameObject.SetActive(false);
            speed = 0;
            chatAnim.SetTrigger("getDiamond");
            isHoldingDiamond = false;
            StartCoroutine(KissCinematic());

        }

        IEnumerator KissCinematic()
        {
         
            yield return new WaitForSeconds(2.4f);
            Heal();
            yield return new WaitForSeconds(0.6f);
            getDiamond();
            yield return new WaitForSeconds(0.5f);
            speed = 20;
            isHoldingDiamond = false;
            
            if(nbDiamonds == 3)
            {
                DontDestroyOnLoad(Instantiate(winSoundObject, transform.position, transform.rotation));
                sceneTransitions.LoadScene("Win");
            }

        }


    }

    public void getDiamond()
    {
        nbDiamonds++;

        for (int i = 0; i < 3; i++)
        {
            if (i < nbDiamonds)
            {
                diamonds[i].sprite = fullDiamond;
            }
            else
            {
                diamonds[i].sprite = emptyDiamond;
            }
         
        }
    }

    public void TakeDamages()
    {
        hitScreenAnim.SetTrigger("hitScreen");
        health -= 1;
        UpdateHealth(health);

        if (health <= 0)
        {
            
            DontDestroyOnLoad(Instantiate(loseSoundObject, transform.position, transform.rotation));
            sceneTransitions.LoadScene("Lose");
            Destroy(this.gameObject);
        }
    }

    void UpdateHealth(int health)
    {
        for (int i = 0; i < coeurs.Length; i++)
        {
            if (i < health)
            {
                coeurs[i].sprite = coeurRouge;
            }
            else
            {
                coeurs[i].sprite = coeurNoir;
            }
        }
    }

    public void Heal ()
    {
        if(health < 3)
        {
            health++;   
        }
        UpdateHealth(health);
    }
}
