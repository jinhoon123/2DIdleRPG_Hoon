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
    private bool useSkill;
    private Vector2 skillDirection;
    
    public void Init(Character character, int skillID)
    {
        owner = character;
        target = character.characterTarget.GetTarget();
        
        data = DataTable_Skill_Data.GetData(skillID);

        cooldownTime = data.CooldownTime;

        transform.position = owner.transform.position;
    }
    
    public void CalculateCooldownTime()
    {
        cooldownTime -= Time.deltaTime;
    }

    public bool IsUseSkill()
    {
        if (cooldownTime < 0)
        {
            return true;
        }

        return false;
    }

    public void Update()
    {
        if (useSkill)
        {
            transform.Translate(skillDirection * (Time.deltaTime * 3.0f));

            if (CheckHit())
            {
                Destroy(this.gameObject);
                useSkill = false;
            }
        }
    }
    
    public void ExecuteSkill()
    {
        if (useSkill)
        {
            return;
        }
        
        target = owner.characterTarget.GetTarget();

        if (target != null)
        {
            skillDirection = (target.transform.position - owner.transform.position).normalized;
            skillDirection = new Vector2(skillDirection.x, 0f);

            useSkill = true;
        }
    }


    private bool CheckHit()
    {
        if (Vector2.Distance(transform.position, owner.characterTarget.GetTarget().transform.position) < 0.1f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
