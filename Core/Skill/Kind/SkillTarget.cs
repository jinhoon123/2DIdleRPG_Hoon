using CHV;
using UnityEngine;

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
        if (isHit == false || inited == false)
        {
            return;
        }
        
        if (target is null || target.characterHealth.isDead)
        {
            isHit = false;
            return;
        }
            
        if (target.characterTarget.IsHitTarget(collider.bounds))
        {
            var skillDamage = new Unified(skill.Data.Attack);
            var ownerDamage = skill.Owner.characterStat.GetStat(DataTable_Stat_Data.eStatType.Attack);
            var Damage = skillDamage * ownerDamage;
            
            skill.Owner.characterTarget.GetTarget().characterHealth.UpdateHealth(Damage);
            isHit = false;
        }
    }


    public void Init(Skill inSkill)
    {
        collider = GetComponent<BoxCollider2D>();
        
        skill = inSkill;
        target = skill.Owner.characterTarget.GetTarget();
        transform.position = inSkill.Owner.Weapon.position;
        transform.position = skill.Owner.characterTarget.GetTarget().transform.position;
        inited = true;
        gameObject.SetActive(true);
    }

    public void SkillHitEvent()
    {
        isHit = true;
    }

    public void SkillAnimationEnd()
    {
        Destroy(gameObject);
    }
}