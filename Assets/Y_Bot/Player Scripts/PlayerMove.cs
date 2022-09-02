﻿using System.Collections;
using System.Collections.Generic;
using System.Net;
using Packages.Rider.Editor.Util;
using TMPro;
using UnityEditorInternal;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    [Header("Player Components")] 
    private Animator _animator;
    
    [Header("Player Characteristics")] 
    
    [SerializeField] private float playerSpeed = 10f;
    [SerializeField] private float rotationSpeed = 100f;

    [SerializeField] private bool playerCollideWith; 
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (playerCollideWith)
        {
            return;
        }
        
        float translation = Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        
        transform.Translate(0,0, translation);
        transform.Rotate(0,rotation,0);
        
        if(translation > 0)
        {
            _animator.SetBool("isWalking", true);
            _animator.SetFloat("direction" , 1f);
        }
        else if(translation < 0)
        {
            _animator.SetBool("isWalking", true);
            _animator.SetFloat("direction" , -1f);
        }
        else
        {
            _animator.SetBool("isWalking" , false);
            
        }

        Running();
    }

    void Running()
    {
        if (playerCollideWith)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

            _animator.SetBool("isRunning" , true);

        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _animator.SetBool("isRunning" , false);
        }
    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.tag == "Phone" && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("phone trigger with the player");
            _animator.SetBool("isWalking" , false);
            _animator.SetBool("isRunning" , false);
            playerCollideWith = true;
            _animator.SetTrigger("picking");
        }
    }
    
    void OnTriggerExit(Collider obj)
    {
        if (obj.tag == "Player")
        {
            playerCollideWith = false;
        }
    }
}
