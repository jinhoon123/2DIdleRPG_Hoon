using UnityEngine;


[RequireComponent(typeof(Animator))]
public class CharacterAnimation : MonoBehaviour
{
   private Character owner;
   private Animator animator;
   
   public void Init(Character inOwner)
   {
      owner = inOwner;
      animator = GetComponent<Animator>();
   }
   
   public void PlayAnimation(string animationName)
   {
      animator.CrossFade(animationName, 0.001f);
   }
   
   public bool IsAnimationPlaying(string animationName)
   {
      var animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
      return animatorStateInfo.IsName(animationName);
   }
}
