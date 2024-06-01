using CHV;
using UnityEngine;
using UnityEngine.AddressableAssets;

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
            
            var target = skill.Owner.characterTarget.GetTarget();
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
                    target.characterHealth.UpdateHealth(damage);
                }

                Addressables.Release(gameObject);
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