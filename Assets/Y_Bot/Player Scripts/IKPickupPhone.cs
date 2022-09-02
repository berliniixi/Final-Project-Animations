using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKPickupPhone : MonoBehaviour
{
    
    [SerializeField] private Transform target; // phone
    [SerializeField] private float weight = 1;

    private Animator _animator;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {

    }
    
    void OnAnimatorIK(int layerIndex)
    {
        weight = _animator.GetFloat("IKPickUp"); 
        _animator.SetIKPosition(AvatarIKGoal.RightHand , target.position);
        _animator.SetIKPositionWeight(AvatarIKGoal.RightHand , weight);
    }
}
