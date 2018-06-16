using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;  


public class Player : MonoBehaviour {
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
   
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    bool isAlive = true;

    public Transform projectileSpawnPoint;
    public Projectile projectilePrefab;
    public float projectileSpeed;



    // Use this for initialization
    void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();


        if (projectileSpeed <= 0)
            {
            projectileSpeed = 7.0f;
            }
        }

    // Update is called once per frame
    void Update () {
        Run();
        Jump();
        FlipSprite();

        if (Input.GetButtonDown("Fire1"))
        {

        }
    

    }

    void fire()
    {
        Projectile temp = Instantiate(projectilePrefab, projectileSpawnPoint.position,
            projectileSpawnPoint.rotation);
        temp.speed = projectileSpeed;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Collectible")
        {
            Destroy(c.gameObject);
        }

    }
            private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // value is between -1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);

        myRigidbody.velocity = playerVelocity;

        bool playerHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHorizontalSpeed);
        myAnimator.SetBool("Jump", playerHorizontalSpeed);
    }

    private void FlipSprite()
    {
        bool playerHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

    private void Jump()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidbody.velocity += jumpVelocityToAdd;  

        }
    }

    
}
