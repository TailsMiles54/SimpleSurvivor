using System;
using System.Collections.Generic;
using System.Linq;
using Character;
using DefaultNamespace;
using Photon.Pun;
using Settings;
using UnityEngine;
using UnityEngine.UI;
using SettingsProvider = Settings.SettingsProvider;

public class Player : MonoBehaviourPunCallbacks
{
    [SerializeField] private CharacterController _characterController;

    [SerializeField] private CharacterAppearance _characterAppearance;
    [SerializeField] private CharacterEquipment _characterEquipment;

    [SerializeField] private PlayerClasses _currentClass;
    
    [SerializeField] private int _speed = 1;
    [SerializeField] private int _speed_rotation = 3;
    [SerializeField] private Animator _animator;

    [SerializeField] private Slider _healthSlider;

    [SerializeField] private ParticleSystem _smokePuff; 
    [field: SerializeField] public SpellsController SpellsController { get; private set; } 

    public UserInfo UserInfo;
    
    public static event Action<Player> PlayerInitialized;
    [field: SerializeField] public Camera AvatarCamera { get; private set; } 

    private bool _dead;
    
    private List<string> _attackAnimsNames = new List<string>()
    {
        "Attack01",
        "Attack02",
        "Attack03",
        "Attack04",
        "Combo01",
        "Combo02",
        "Combo03",
        "Combo04",
        "Combo05",
    };

    private bool _inAttack;
    
    public static GameObject LocalPlayerInstance;
    
    async void Start()
    {
        if (photonView.IsMine)
        {
            LocalPlayerInstance = gameObject;
            PlayerInitialized?.Invoke(this);

            var loadedUserName = await SaveDataManager.RetrieveSpecificData("Name");

            var userInfo = new UserInfo(loadedUserName, new Levels(new List<Level>()
            {
               new Level(LevelType.MainLevel) 
            }), new Parameters(new List<Parameter>()
            {
                new Parameter(ParameterType.Health, 100)
            }));
            
            UserInfo = userInfo;
            GetNickName();
            UpdateLevelSlider();
        }
        
        _characterAppearance.LoadAppearance(photonView.IsMine);
        _characterEquipment.LoadAppearance(photonView.IsMine);

        SwitchWeapon();
    }

    private void GetNickName()
    {
        UIController.Instance.NickName.text = UserInfo.Name;
    }
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void UpdateLevelSlider()
    {
        var mainLevel = UserInfo.Level.LevelList.First(x => x.LevelType == LevelType.MainLevel);
        UIController.Instance.MainLevel.maxValue = mainLevel.ExpToNext;
        UIController.Instance.MainLevel.value = mainLevel.CurrentExp;
        UIController.Instance.LevelText.text = $"Lv: {UserInfo.Level.LevelList.First(x => x.LevelType == LevelType.MainLevel).CurrentLevel} ({UserInfo.UpgradeSpellPoints.Count})";
    }
    
    void OnDrawGizmos()
    {
        float thetaScale = 0.01f;
        int size;
        float theta = 0f;
        
        var spawnSettings = SettingsProvider.Get<SpawnSettings>();
        
        Gizmos.color = Color.red;
        
        theta = 0f;
        size = (int)(1f / thetaScale);

        List<Vector3> points = new List<Vector3>();
        
        for (int i = 0; i < size; i++)
        {
            theta += (2.0f * Mathf.PI * thetaScale);
            float x = spawnSettings.MinRadius * Mathf.Cos(theta);
            float z = spawnSettings.MinRadius * Mathf.Sin(theta);
            var point = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
            points.Add(point);
        }
        
        Gizmos.DrawLineList(points.ToArray()); 
        
        points.Clear();
        
        Gizmos.color = Color.blue;
        
        theta = 0f;
        size = (int)(1f / thetaScale);
        
        for (int i = 0; i < size; i++)
        {
            theta += (2.0f * Mathf.PI * thetaScale);
            float x = spawnSettings.SpawnRadius * Mathf.Cos(theta);
            float z = spawnSettings.SpawnRadius * Mathf.Sin(theta);
            var point = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
            points.Add(point);
        }
        
        Gizmos.DrawLineList(points.ToArray()); 

    }

    public void AddXp(int xp)
    {
        UserInfo.AddMainExp(xp);
        UpdateLevelSlider();
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
        else if (Input.GetButtonDown("Upgrade"))
        {
            if(UserInfo.UpgradeSpellPoints.Count != 0)
            {
                var spells = UserInfo.UpgradeSpellPoints.First();
                PopupController.Instance.ShowPopup(new LevelRewardPopupSettings()
                {
                    Spells = spells
                });
                
            }
        }

        _inAttack = false;
        
        foreach (var animName in _attackAnimsNames)
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName(animName))
            {
                _inAttack = true;
            }
        }

        var slots = _characterEquipment.EquipmentSlots.Where(x =>
            x.SlotType is EquipmentSlotType.LeftHand or EquipmentSlotType.RightHand);

        foreach (var equipmentSlot in slots)
        {
            var equipmentElement = equipmentSlot.AllowedElements.FirstOrDefault(x =>
                x.GetComponent<EquipmentElement>().Id == equipmentSlot.ItemId);

            if (equipmentElement != null && equipmentElement.TryGetComponent(out BoxCollider boxCollider))
            {
                boxCollider.enabled = _inAttack;
            }
        }
    }

    private void SwitchWeapon()
    {
        _animator.runtimeAnimatorController = SettingsProvider.Get<AnimatorOverrides>().ClassOverrideAnimators.First(x => x.Class == _currentClass).AnimatorOverrideController;
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
        _smokePuff.startDelay = 0.1f;
        _smokePuff.Play();
    }
    
    private void AttackCombo()
    {
        if(!photonView.IsMine)
            return;

        _animator.SetBool("InCombo", false); 
        _animator.SetTrigger("AttackCombo");
    }
    
    private void Die()
    {
        if(!photonView.IsMine)
            return;

        _dead = true;
        _animator.SetTrigger("Die");
        _animator.SetBool("Dead", _dead);
    }
    
    private void GetUp()
    {
        if(!photonView.IsMine)
            return;

        _dead = true;
        _animator.SetTrigger("GetUp");
        _animator.SetBool("Dead", _dead);
    }

    private void Attack1()
    {
        if(!photonView.IsMine)
            return;
        _animator.SetTrigger("Attack1Trigger");
    }

    private void Attack2()
    {
        if(!photonView.IsMine)
            return;
        _animator.SetTrigger("Attack2Trigger");
    }

    private void Attack3()
    {
        if(!photonView.IsMine)
            return;
        _animator.SetTrigger("Attack3Trigger");
    }

    private void Attack4()
    {
        if(!photonView.IsMine)
            return;
        _animator.SetTrigger("Attack4Trigger");
    }

    private void Dance()
    {
        if(!photonView.IsMine)
            return;
        _animator.SetTrigger("Dance");
    }

    private void LevelUp()
    {
        if(!photonView.IsMine)
            return;
        _animator.SetTrigger("LevelUp");
    }

    private void Challenging()
    {
        if(!photonView.IsMine)
            return;
        _animator.SetTrigger("Challenging");
    }

    private void Dizzy()
    {
        if(!photonView.IsMine)
            return;
        _animator.SetTrigger("Dizzy");
    }

    private void Victory()
    {
        if(!photonView.IsMine)
            return;
        _animator.SetTrigger("Victory");
    }

    private void Search()
    {
        if(!photonView.IsMine)
            return;
        _animator.SetTrigger("Search");
    }

    private void GetHit1()
    {
        if(!photonView.IsMine)
            return;
        _animator.SetTrigger("GetHit1");
    }

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