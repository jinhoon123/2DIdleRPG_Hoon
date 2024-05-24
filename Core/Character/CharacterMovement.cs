using CHV;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Character owner;
        
    public void Init(Character inOwner)
    {
        owner = inOwner;
        
        GameManager.I.backHandler.OnScrollStartEvent += OnMovement;
        GameManager.I.backHandler.OnScrollCompletedEvent += OnStop;
    }


    private void OnMovement()
    {
        if (owner.characterHealth.isDead == false)
        {
            owner.characterAnimation.PlayAnimation("Run");
        }
        else
        {
            GameManager.I.backHandler.OnScrollStartEvent -= OnMovement;
        }
    }

    private void OnStop()
    {
        if (owner.characterHealth.isDead == false)
        {
            owner.characterAnimation.PlayAnimation("Idle");
        }
        else
        {
            GameManager.I.backHandler.OnScrollStartEvent -= OnStop;
        }
    }
}