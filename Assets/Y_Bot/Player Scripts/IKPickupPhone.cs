using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IKPickupPhone : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject anchor;
    
    
    [SerializeField] private bool isWalkingTowards;
    [SerializeField] private bool standingNear;

    private Animator _animator;

    private PlayerMove _playerMove;
    
    void Start()
    {
        _animator = Player.GetComponent<Animator>();
        _playerMove = FindObjectOfType<PlayerMove>();
    }

    void Update()
    {
        if (isWalkingTowards)
        {
            AutoWalkTowards();
        }
    }
    void AutoWalkTowards()
    {
        Vector3 targetDir = new Vector3
        (anchor.transform.position.x - Player.transform.position.x, // TelephoneBooth position - Character position in the x-axis
            0f,
            anchor.transform.position.z - Player.transform.position.z); // TelephoneBooth position - Character position in the z-axis
        Quaternion rot = Quaternion.LookRotation(targetDir); // LookRotation method-> is for turning the player to the targetDir()  
        Player.transform.rotation = Quaternion.Slerp(Player.transform.rotation, rot, 0.05f);    //create a smoothly rotation from a to b in some specific time

        Player.transform.Translate(targetDir * Time.deltaTime * 0.6f); 
        
        // Distance() Returns the distance between a and b

        if (Vector3.Distance(Player.transform.position, anchor.transform.position) < 0.4f)  // Check the distance of the player from the target(TelephoneBooth)
        
            // if it's less than the 0.4f then it will do the follow

        {
            _animator.SetBool("isAnswering" , true);           
            _animator.SetBool("isWalking", false);

            Player.transform.rotation = anchor.transform.rotation; // the Character takes the current rotation of TelephoneBooth (Anchor)

            isWalkingTowards = false;
            standingNear = true;
            FindObjectOfType<ShowUI>().HideMessage();
        }
    }
    

    void OnMouseDown()      // mouse click 
    {
        if (!standingNear)
        {
            _animator.SetFloat("direction" , 1);
            _animator.SetBool("isWalking", true);
            isWalkingTowards = true;
            PlayerMove.controlledBy = gameObject;
        }
        else  
        {
            _animator.SetBool("isAnswering", false);
            _playerMove.PlayerCollidesEnd();
            isWalkingTowards = false;
            PlayerMove.controlledBy = null;
            standingNear = false;
            Debug.LogError("I am False now");
        }
    }
    
    private void FixedUpdate()
    {
        AnimLerp();
    }
    
    void AnimLerp()    
    {
        if (!standingNear) return;

        if (Vector3.Distance(Player.transform.position ,anchor.transform.position) > 0.3f)
        {
            Player.transform.rotation = Quaternion.Lerp(        // Rotation
                Player.transform.rotation,   // Form this rotation
                anchor.transform.rotation,      //To this rotation
                Time.deltaTime * 0.5f   //In this time
                );        

            Player.transform.position = Vector3.Lerp(   //Translation
                Player.transform.position,
                anchor.transform.position,
                Time.deltaTime * 0.5f
                );
        }
        else
        {
            Player.transform.position = anchor.transform.position;
            Player.transform.rotation = anchor.transform.rotation;
        }
    }
}
