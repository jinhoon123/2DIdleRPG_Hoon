using CHV;
using UnityEngine;

public class SkillProjectile : MonoBehaviour, ISkillKind
{
    private Skill skill;
    private Vector2 skillDirection;

    [SerializeField]
    private new BoxCollider2D collider;
    private bool inited;
    
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (inited)
        {
            transform.Translate(skillDirection * (Time.deltaTime * skill.Data.Speed));

            if (skill.Owner.characterTarget.GetTarget() is null)
            {
                return;
            }
            
            if (skill.Owner.characterTarget.GetTarget().characterTarget.IsHitTarget(collider.bounds))
            {
                var skillDamage = new Unified(skill.Data.Attack);
                var ownerDamage = skill.Owner.characterStat.GetStat(DataTable_Stat_Data.eStatType.Attack);
                var damage = skillDamage * ownerDamage;
                
                skill.Owner.characterTarget.GetTarget().characterHealth.UpdateHealth(damage);
                Destroy(gameObject);
            }
        }
    }
    
    public void Init(Skill inSkill)
    {
        skill = inSkill;
        transform.position = inSkill.Owner.Weapon.position;
        SetDirection();
        
        inited = true;
        gameObject.SetActive(true);
    }
    
    private void SetDirection()
    {
        skillDirection = Vector2.right;
    }
}