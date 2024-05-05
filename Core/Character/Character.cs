using CHV;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Character : MonoBehaviour
{
    #region Components

    public CharacterStat characterStat;
    
    public CharacterMovement characterMovement;
    public CharacterAnimation characterAnimation;
        
    public CharacterTarget characterTarget;
    public CharacterHealth characterHealth;

    public CharacterEvent characterEvent;
    public SkillSystem skillSystem;

    #endregion

    #region Data
        
    public DataTable_Character_Data Data;
        
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
            
            skillSystem.UpdateBasicAttack();
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
        characterStat = GetComponent<CharacterStat>();
        characterStat.Init(this);
        
        characterMovement = GetComponent<CharacterMovement>();
        characterMovement.Init(this);

        characterAnimation = GetComponent<CharacterAnimation>();
        characterAnimation.Init(this);
        
        characterTarget = GetComponent<CharacterTarget>();
        characterTarget.Init(this);

        characterHealth = GetComponent<CharacterHealth>();
        characterHealth.Init(this);

        characterEvent = GetComponent<CharacterEvent>();
        characterEvent.Init(this);

        skillSystem = GetComponent<SkillSystem>();
        await skillSystem.Init(this);

        Weapon = transform.Find("Weapon");
    }
}