using CHV;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SkillTarget : MonoBehaviour, ISkillKind
{
    private Skill skill;
    
    private bool inited;
    private bool isHit;
   
    [SerializeField]
    private new BoxCollider2D collider;

    private Character target;
    
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (inited == false)
        {
            return;
        }
        
        if (isHit == false)
        {
            return;
        }

        if (!target)
        {
            return;
        }
        
        if (target && target.characterTarget.IsHitTarget(collider.bounds))
        {
            var skillDamage = new Unified(skill.Data.Attack);
            var ownerDamage = skill.Owner.characterStat.GetStat(DataTable_Stat_Data.eStatType.Attack);
            var damage = skillDamage * ownerDamage;

            if (target.characterHealth.isDead == false)
            {
                skill.Owner.characterTarget.GetTarget().characterHealth.UpdateHealth(damage);
            }
            
            isHit = false;
        }
    }


    public void Init(Skill inSkill)
    {
        collider = GetComponent<BoxCollider2D>();
        
        skill = inSkill;
        target = skill.Owner.characterTarget.GetTarget();

        if (target != null)
        {
            transform.position = inSkill.Owner.Weapon.position;
            transform.position = target.transform.position;
        
            inited = true;
            gameObject.SetActive(true);
        }
        else 
        {
            // 스킬이 생성되서 Target을 지정하려고 했으나 다른 요인으로 사망했을 때 
            Addressables.Release(gameObject);
        }
    }

    public void SkillHitEvent()
    {
        isHit = true;
    }

    public void SkillAnimationEnd()
    {
        Addressables.Release(gameObject);
    }
}