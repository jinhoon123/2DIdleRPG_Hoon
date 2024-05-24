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

    public void Init(DataTable_Character_Data inData)
    {
        if (inData.CharacterType == DataTable_Character_Data.eCharacterType.MainCharacter)
        {
            Debug.Log("메인캐릭터 Init");
        }
        
        InitData(inData);
        InitComponents();

        inited = true;
        
        if (inData.CharacterType == DataTable_Character_Data.eCharacterType.MainCharacter)
        {
            Debug.Log($"메인캐릭터 Inited State : {inited}");
        }
    }

    private void InitData(DataTable_Character_Data inData)
    {
        Data = inData;
    }
        
    private void InitComponents()
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
        skillSystem.Init(this);

        Weapon = transform.Find("Weapon");
    }
}