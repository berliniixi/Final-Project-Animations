using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc : MonoBehaviour
{
    private Animator _animator;
    private PlayerMove _playerMove;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _playerMove = FindObjectOfType<PlayerMove>();
    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.tag == "Player")
        {
            Debug.Log("You have to call the...");
            _playerMove.PlayerCollidesEnd();
            _animator.SetTrigger("Talking");
        }
    }
    public void EnableThePlayerMoveAfterNPCsStopTalking()
    {
        _playerMove.PlayerCollidesEnd();
    }
}
