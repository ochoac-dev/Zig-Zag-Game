using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public Transform rayStart;
    public GameObject crystalEffect;

    private Rigidbody rb;
    private bool walkingRight = true;
    private Animator Anim;
    private GameManager gameManager;


    // Start is called before the first frame update
    void Awake()
    {
       rb = GetComponent<Rigidbody>(); 

        Anim= GetComponent<Animator>();

        gameManager = FindObjectOfType<GameManager>();
    }

    private void FixedUpdate()
    {

        if(!gameManager.gameStarted)
        {
            return;
        } else
        {
            Anim.SetTrigger("gameStarted");
        }


        rb.transform.position = transform.position + transform.forward * 2 * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            Switch();
        
        }

        RaycastHit hit;

        if(!Physics.Raycast(rayStart.position, -transform.up, out hit, Mathf.Infinity))
        {
            Anim.SetTrigger("isFalling");
        }

        if(transform.position.y < -2) {
            gameManager.EndGame();
                }


    }

    private void Switch()
    {
        if(!gameManager.gameStarted) { return; }    


        walkingRight= !walkingRight;

        if(walkingRight)
        {
            transform.rotation = Quaternion.Euler(0, 45, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, -45, 0);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Crystal")
        {

            gameManager.IncreaseScore();

            GameObject g = Instantiate(crystalEffect, rayStart.transform.position, Quaternion.identity);
            Destroy(g, 2);
            Destroy(other.gameObject);

        }
    }
}
