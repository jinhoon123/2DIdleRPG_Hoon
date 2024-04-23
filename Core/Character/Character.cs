using CHV;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Character : MonoBehaviour
{
    #region Components

    private CharacterMovement characterMovement;
        
    public CharacterTarget characterTarget;
    public CharacterHealth characterHealth;
        
    public SkillSystem skillSystem;

    #endregion

    #region Data
        
    public DataTable_Character_Data Data;
        
    #endregion

    #region Events

    public delegate void CharacterEvent();

    public event CharacterEvent OnCharacterDead; 

    #endregion

    #region State

    private bool inited;

    #endregion

    #region Transform

    public Transform Weapon;

    #endregion

    private void Update()
    {
        if (inited)
        {
            characterTarget.UpdateTarget();
            skillSystem.UpdateSkill();
        }
    }

    public async UniTask Init(DataTable_Character_Data inData)
    {
        InitData(inData);
        await InitComponents();

        inited = true;
    }

    private void InitData(DataTable_Character_Data inData)
    {
        Data = inData;
    }
        
    private async UniTask InitComponents()
    {
        characterMovement = GetComponent<CharacterMovement>();
        characterMovement.Init(this);

        characterTarget = GetComponent<CharacterTarget>();
        characterTarget.Init(this);

        characterHealth = GetComponent<CharacterHealth>();
        characterHealth.Init(this);

        skillSystem = GetComponent<SkillSystem>();
        await skillSystem.Init(this);

        Weapon = transform.Find("Weapon");
    }


    public void Dead()
    {
        GameManager.I.monsters.Remove(this);
        OnCharacterDead?.Invoke();
        Destroy(gameObject);
    }
}