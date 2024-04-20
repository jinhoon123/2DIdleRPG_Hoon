using System;
using System.Collections;
using System.Collections.Generic;
using CHV;
using Core.Character;
using UnityEngine;

public class Skill : MonoBehaviour
{
    private Character owner;
    private Character target;
    
    private DataTable_Skill_Data data;
    
    private float cooldownTime;
    private Vector2 skillDirection;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.Translate(skillDirection * (Time.deltaTime * data.Speed));

        if (CheckHit())
        {
            target.characterHealth.UpdateHealth(100);
            gameObject.SetActive(false);
        }
    }
    
    public void Init(Character character, int skillID)
    {
        owner = character;
        target = character.characterTarget.GetTarget();
        data = DataTable_Skill_Data.GetData(skillID);
        cooldownTime = data.CooldownTime;
    }
    
    public void CalculateCooldownTime()
    {
        cooldownTime -= Time.deltaTime;
    }

    public bool IsUseSkill()
    {
        if (GameManager.I.monsters.Count == 0)
        {
            return false;
        }

        if (cooldownTime > 0)
        {
            return false;
        }

        if (owner.skillSystem.globalCooldown > 0)
        {
            return false;
        }
        
        return true;
    }
    
    public void ExecuteSkill()
    {
        ResetSkill();
        SetDirection();
        ResetCooldown();
    }

    private bool CheckHit()
    {
        return Vector2.Distance(transform.position, owner.characterTarget.GetTarget().transform.position) < 0.1f;
    }

    private void SetDirection()
    {
        if (target != null)
        {
            skillDirection = (target.transform.position - owner.transform.position).normalized;
            skillDirection = new Vector2(skillDirection.x, 0f);
        }
    }

    private void ResetSkill()
    {
        target = owner.characterTarget.GetTarget();
        
        transform.position = owner.transform.position;
        
        skillDirection = Vector2.zero;
        gameObject.SetActive(true);
    }

    private void ResetCooldown()
    {
        cooldownTime = data.CooldownTime;
        owner.skillSystem.globalCooldown = 1.0f;
    }
}
