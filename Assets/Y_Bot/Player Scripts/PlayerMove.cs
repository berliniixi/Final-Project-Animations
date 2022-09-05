using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    [Header("Player Components")] 
    private Animator _animator;
    [SerializeField] private Transform phone;
    [SerializeField] private Transform receiver;
    [SerializeField] private Transform hand;
    
    [Header("Player Characteristics")] 
    
    [SerializeField] private float playerSpeed = 10f;
    [SerializeField] private float rotationSpeed = 100f;

    [SerializeField] private bool playerCollideWith;  // bool value to switch off and on the player controller movements

    public static GameObject controlledBy;
    
    [SerializeField] private float weight = 1;

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
        if (playerCollideWith || controlledBy != null)
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
    
    void OnTriggerStay(Collider obj)        //The player can not move when he answer the phone until the animation stops 
    
    {
        Debug.Log("phone trigger with the player");
        if (obj.tag == "Phone" && Input.GetKeyDown(KeyCode.F))
        {
            _animator.SetBool("isWalking" , false);
            _animator.SetBool("isRunning" , false);
            _animator.SetBool("isAnswering" , true);
            playerCollideWith = true;
            Debug.Log("F was pressed");
            _animator.SetTrigger("picking");
        }
    }

    public void PlayerCollidesEnd() // this method is for the animation event, when the player stop talking to phone 
                                    // playerCollideWith it became false to give the player the ability to control the player again
    {
        playerCollideWith = false;
    }
    
}
