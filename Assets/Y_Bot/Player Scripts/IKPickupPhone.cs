using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKPickupPhone : MonoBehaviour
{
    
    [SerializeField] private Transform target; // Phone
    [SerializeField] private Transform playerHand;
    [SerializeField] private float weight = 1;

    private Animator _animator;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
    }
}
