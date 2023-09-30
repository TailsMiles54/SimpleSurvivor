using System;
using System.Collections.Generic;
using Photon.Pun;
using Sirenix.OdinInspector;
using UnityEngine;

public class Player : MonoBehaviourPunCallbacks
{
    [SerializeField] private CharacterController _characterController;

    [SerializeField] private CharacterAppearance _characterAppearance;
    
    [SerializeField] private int _speed = 1;
    [SerializeField] private int _speed_rotation = 3;
    [SerializeField] private Animator _animator;

    private Parameters _parameters = new Parameters(new List<Parameter>()
    {
        new Parameter(ParameterType.Health, 100f)
    });
    
    public static GameObject LocalPlayerInstance;
    
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            _characterAppearance.LoadAppearance();
            LocalPlayerInstance = gameObject;
        }
    }
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(!photonView.IsMine)
            return;
        
        CameraController.Instance.SetupCamera(gameObject);
        
        transform.Rotate(0, Input.GetAxis("Horizontal") * _speed_rotation, 0);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        
        float curSpeed = _speed * Input.GetAxis("Vertical") * (Input.GetKey(KeyCode.LeftShift) ? 3 : 1);

        _animator.SetFloat("CurrentSpeed", curSpeed);
        _characterController.SimpleMove(forward * curSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
    }

    [Button("PickUp"), HorizontalGroup("Animations")]
    private void PickUp()
    {
        if(!photonView.IsMine)
            return;
        _animator.SetTrigger("PickUpTrigger");
    }

    [Button("Interact"), HorizontalGroup("Animations")]
    private void Interact()
    {
        if(!photonView.IsMine)
            return;
        _animator.SetTrigger("InteractTrigger");
    }

    [Button("Die"), HorizontalGroup("Animations")]
    private void Die()
    {
        if(!photonView.IsMine)
            return;
        _animator.SetTrigger("DieTrigger");
    }

    [Button("DieRecovery"), HorizontalGroup("Animations")]
    private void DieRecovery()
    {
        if(!photonView.IsMine)
            return;
        _animator.SetTrigger("DieRecoveryTrigger");
    }

    [Button("Win"), HorizontalGroup("Animations")]
    private void Win()
    {
        if(!photonView.IsMine)
            return;
        _animator.SetTrigger("WinTrigger");
    }
}