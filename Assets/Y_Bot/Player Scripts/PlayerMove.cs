using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    [Header("Player Components")] 
    private Animator _animator;

    [Header("Player Characteristics")] 
    [SerializeField] private float playerSpeed = 10f;
    [SerializeField] private float rotationSpeed = 100f;
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _animator.SetBool("isRunning" , true);
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            _animator.SetBool("isRunning" , false);
        }
    }
    
}
