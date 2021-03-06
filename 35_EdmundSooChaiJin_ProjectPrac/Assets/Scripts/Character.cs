﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private float moveSpeed = 5.0f;
    private float jumpForce = 5.0f;

    private int healthCount = 100;
    private int coinCount = 0;

    private bool onGround = true;

    private Rigidbody2D rb;
    private Animator animator;

    public AudioClip[] audioClips;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float hVelocity = 0;
        float vVelocity = 0;

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            hVelocity = -moveSpeed;
            transform.localScale = new Vector2(-1, 1);

            animator.SetFloat("xVelocity", Mathf.Abs(hVelocity));

            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(audioClips[3]);
            }
        }

        else if(Input.GetKeyUp(KeyCode.LeftArrow))
        {
            animator.SetFloat("xVelocity", 0);
        }

        if(Input.GetKey(KeyCode.RightArrow))
        {
            hVelocity = moveSpeed;
            transform.localScale = new Vector2(1, 1);

            animator.SetFloat("xVelocity", Mathf.Abs(hVelocity));

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(audioClips[3]);
            }
        }

        else if(Input.GetKeyUp(KeyCode.RightArrow))
        {
            animator.SetFloat("xVelocity", 0);
        }

        if(Input.GetKeyDown(KeyCode.Space) && onGround == true)
        {
            vVelocity = jumpForce;

            animator.SetTrigger("JumpTrigger");

            onGround = false;

            audioSource.PlayOneShot(audioClips[2]);
        }

        hVelocity = Mathf.Clamp(rb.velocity.x + hVelocity, -5, 5);

        rb.velocity = new Vector2(hVelocity, rb.velocity.y + vVelocity);
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if(collision2D.gameObject.CompareTag("Enemy"))
        {
            healthCount -= 10;

            audioSource.PlayOneShot(audioClips[1]);

            if (healthCount < 10)
            {
                Destroy(gameObject);
            }
        }

        if(collision2D.gameObject.CompareTag("Coin"))
        {
            coinCount += 1;

            Destroy(collision2D.gameObject);

            audioSource.PlayOneShot(audioClips[0]);
        }

        if(collision2D.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }
}
