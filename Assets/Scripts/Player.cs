using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    
    [SerializeField] private int _speed = 1;
    [SerializeField] private int _speed_rotation = 3;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rigidbody;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * _speed_rotation, 0);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        
        float curSpeed = _speed * Input.GetAxis("Vertical") * (Input.GetKey(KeyCode.LeftShift) ? 3 : 1);

        _animator.SetFloat("CurrentSpeed", curSpeed);
        Debug.Log(curSpeed);
        _characterController.SimpleMove(forward * curSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
    }

    [Button("PickUp"), HorizontalGroup("Animations")]
    private void PickUp()
    {
        _animator.SetTrigger("PickUpTrigger");
    }

    [Button("Interact"), HorizontalGroup("Animations")]
    private void Interact()
    {
        _animator.SetTrigger("InteractTrigger");
    }

    [Button("Die"), HorizontalGroup("Animations")]
    private void Die()
    {
        _animator.SetTrigger("DieTrigger");
    }

    [Button("DieRecovery"), HorizontalGroup("Animations")]
    private void DieRecovery()
    {
        _animator.SetTrigger("DieRecoveryTrigger");
    }

    [Button("Win"), HorizontalGroup("Animations")]
    private void Win()
    {
        _animator.SetTrigger("WinTrigger");
    }
}