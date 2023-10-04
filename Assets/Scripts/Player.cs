using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Settings;
using Sirenix.OdinInspector;
using UnityEngine;

public class Player : MonoBehaviourPunCallbacks
{
    [SerializeField] private CharacterController _characterController;

    [SerializeField] private CharacterAppearance _characterAppearance;

    [SerializeField] private PlayerClasses _currentClass;
    
    [SerializeField] private int _speed = 1;
    [SerializeField] private int _speed_rotation = 3;
    [SerializeField] private Animator _animator;

    private bool _dead;

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
        
        if(!_dead)
            transform.Rotate(0, Input.GetAxis("Mouse X") * _speed_rotation, 0);
        
        float curSpeed = _speed * Input.GetAxis("Vertical");
        float curHorizontalSpeed = _speed * Input.GetAxis("Horizontal");

        curSpeed = Input.GetButton("Shift") ? curSpeed * 2 : curSpeed;
        
        _animator.SetFloat("y", curSpeed);
        _animator.SetFloat("x", curHorizontalSpeed);


        if (Input.GetButtonDown("Switch weapon"))
        {
            SwitchWeapon();
        }
        else if (Input.GetButtonDown("Skill1"))
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
        else if (Input.GetButtonDown("Roll"))
        {
            Roll();
        }
        else if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void SwitchWeapon()
    {
        _animator.runtimeAnimatorController = SettingsProvider.Get<AnimatorOverrides>().ClassOverrideAnimators
            .First(x => x.Class == _currentClass).AnimatorOverrideController;
    }

    private void Jump()
    {
        if (_characterController.isGrounded && !_animator.GetCurrentAnimatorStateInfo(0).IsName("JumpFull_Normal_RM_SwordAndShield"))
        {
            _animator.SetTrigger("Jump");
        }
    }

    private void Roll()
    {
        _animator.SetTrigger("Roll");
    }
    
    [Button("AttackCombo"), HorizontalGroup("Attacks")]
    private void AttackCombo()
    {
        if(!photonView.IsMine)
            return;

        _animator.SetBool("InCombo", false); 
        _animator.SetTrigger("AttackCombo");
    }
    
    [Button("Die"), HorizontalGroup("Actions")]
    private void Die()
    {
        if(!photonView.IsMine)
            return;

        _dead = true;
        _animator.SetTrigger("Die");
        _animator.SetBool("Dead", _dead);
    }
    
    [Button("GetUp"), HorizontalGroup("Actions")]
    private void GetUp()
    {
        if(!photonView.IsMine)
            return;

        _dead = true;
        _animator.SetTrigger("GetUp");
        _animator.SetBool("Dead", _dead);
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

    [Button("Dance"), HorizontalGroup("Actions2")]
    private void Dance()
    {
        if(!photonView.IsMine)
            return;
        _animator.SetTrigger("Dance");
    }

    [Button("LevelUp"), HorizontalGroup("Actions2")]
    private void LevelUp()
    {
        if(!photonView.IsMine)
            return;
        _animator.SetTrigger("LevelUp");
    }

    [Button("Challenging"), HorizontalGroup("Actions2")]
    private void Challenging()
    {
        if(!photonView.IsMine)
            return;
        _animator.SetTrigger("Challenging");
    }

    [Button("Dizzy"), HorizontalGroup("Actions2")]
    private void Dizzy()
    {
        if(!photonView.IsMine)
            return;
        _animator.SetTrigger("Dizzy");
    }

    [Button("Victory"), HorizontalGroup("Actions2")]
    private void Victory()
    {
        if(!photonView.IsMine)
            return;
        _animator.SetTrigger("Victory");
    }

    [Button("Search"), HorizontalGroup("Actions2")]
    private void Search()
    {
        if(!photonView.IsMine)
            return;
        _animator.SetTrigger("Search");
    }

    [Button("GetHit1"), HorizontalGroup("Hits")]
    private void GetHit1()
    {
        if(!photonView.IsMine)
            return;
        _animator.SetTrigger("GetHit1");
    }

    [Button("GetHit2"), HorizontalGroup("Hits")]
    private void GetHit2()
    {
        if(!photonView.IsMine)
            return;
        _animator.SetTrigger("GetHit2");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
    }
}