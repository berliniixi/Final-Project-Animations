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

    [SerializeField]  bool playerCollideWith;  // bool value to switch off and on the player controller movements

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
        if (playerCollideWith)       //  if player collide with something, do not let it move
        {
            return;
        }

        if (controlledBy != null)   // if player click the mouse, the control goes to the mouse and the player can not control the player with WASD; 
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
        if (obj.tag == "Phone" && Input.GetKeyDown(KeyCode.F))
        {
            _animator.SetBool("isWalking" , false);
            _animator.SetBool("isRunning" , false);
            _animator.SetBool("isAnswering" , true);
            FindObjectOfType<ShowUI>().HideMessage();
            playerCollideWith = true;
            _animator.SetTrigger("picking");
        }
    }

    public void PlayerCollidesEnd() // this method is for the animation event, when the player stop talking to phone 
                                    // playerCollideWith it became false to give the player the ability to control it again
    {
        playerCollideWith = false;
        _animator.SetBool("isAnswering" , false);
        _animator.ResetTrigger("picking");
    }
    
    void OnAnimatorIK(int layerIndex)
    {
        weight = _animator.GetFloat("IKPickup");
        if (weight > 0.7f && _animator.GetBool("isAnswering"))
        {
            phone.parent = hand;
            phone.localPosition = new Vector3(0f, 0.094f, 0.054f); // position of the telephone get after the player hold it in her hand
            phone.localRotation = Quaternion.Euler(0,0,0);  // rotation of the telephone get after the player hold it in her hand
        }
        else if (weight > 0.7f && !_animator.GetBool("isAnswering"))
        {
            phone.parent = receiver;
            phone.localPosition = Vector3.zero;
            phone.localRotation = Quaternion.identity;
        }
        
        _animator.SetIKPosition(AvatarIKGoal.RightHand , receiver.position);
        _animator.SetIKPositionWeight(AvatarIKGoal.RightHand , weight);
    }
    
}
