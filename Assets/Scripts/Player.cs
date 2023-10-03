using System;
using System.Collections;
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
    
    void Start()
    {
        if (photonView.IsMine)
        {
            LocalPlayerInstance = gameObject;
        }
        
        _characterAppearance.LoadAppearance(photonView.IsMine);
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
        
        transform.Rotate(0, Input.GetAxis("Mouse X") * _speed_rotation, 0);
        
        float curSpeed = _speed * Input.GetAxis("Vertical");
        float curHorizontalSpeed = _speed * Input.GetAxis("Horizontal");

        _animator.SetFloat("y", curSpeed);
        _animator.SetFloat("x", curHorizontalSpeed);


        if (Input.GetButtonDown("Skill1"))
        {
            Attack1();
        }
        else if(Input.GetButtonDown("Skill2"))
        {
            Attack2();
        }
        else if(Input.GetButtonDown("Skill3"))
        {
            Attack3();
        }
        else if(Input.GetButtonDown("Skill4"))
        {
            Attack4();
        }
        else if(Input.GetButtonDown("Fire1"))
        {
            AttackCombo();
        }
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

    [Button("AttackCombo"), HorizontalGroup("Attacks")]
    private void AttackCombo()
    {
        if(!photonView.IsMine)
            return;

        _animator.SetBool("InCombo", false); 
        _animator.SetTrigger("AttackCombo");
    }

    [Button("Attack1"), HorizontalGroup("Attacks")]
    private void Attack1()
    {
        if(!photonView.IsMine)
            return;
        _animator.SetTrigger("Attack1Trigger");
    }

    [Button("Attack2"), HorizontalGroup("Attacks")]
    private void Attack2()
    {
        if(!photonView.IsMine)
            return;
        _animator.SetTrigger("Attack2Trigger");
    }

    [Button("Attack3"), HorizontalGroup("Attacks")]
    private void Attack3()
    {
        if(!photonView.IsMine)
            return;
        _animator.SetTrigger("Attack3Trigger");
    }

    [Button("Attack4"), HorizontalGroup("Attacks")]
    private void Attack4()
    {
        if(!photonView.IsMine)
            return;
        _animator.SetTrigger("Attack4Trigger");
    }
}