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
        if (Input.GetKeyDown(KeyCode.F))
        {
            _animator.SetTrigger("picking");
        }
    }
    
    void OnAnimatorIK(int layerIndex)
    {
        /*weight = _animator.GetFloat("IKPickUp"); // Control how to fast or slow you pickup the burger 
        if (weight > 0.49f)
        {
            target.parent = player;
            target.localPosition = new Vector3(0.027f, 0.211f, 0.122f);
            target.localRotation = Quaternion.Euler(-150.218f , 60.597f ,19.76601f);
        }*/
        _animator.SetIKPosition(AvatarIKGoal.RightHand , target.position);
        _animator.SetIKPositionWeight(AvatarIKGoal.RightHand , weight);
    }
}
