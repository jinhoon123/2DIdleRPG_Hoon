using UnityEngine;

public class SkillProjectile : MonoBehaviour
{
    private Skill skill;

    private Vector2 skillDirection;

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

            if (CheckHit())
            {
                skill.Owner.characterTarget.GetTarget().characterHealth.UpdateHealth(100);
                gameObject.SetActive(false);
            }
        }
    }

    public void Init(Skill skill)
    {
        this.skill = skill;
        transform.position = skill.Owner.Weapon.position;
        SetDirection();
        
        inited = true;
        gameObject.SetActive(true);
    }

    private bool CheckHit()
    {
        return Vector2.Distance(transform.position, skill.Owner.characterTarget.GetTarget().transform.position) < 0.1f;
    }
    
    private void SetDirection()
    {
        if (skill.Owner.characterTarget.GetTarget() != null)
        {
            skillDirection = (skill.Owner.characterTarget.GetTarget().transform.position - skill.Owner.transform.position).normalized;
            skillDirection = new Vector2(skillDirection.x, 0f);
        }
    }
}