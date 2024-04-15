using UnityEngine;

namespace Core.Character
{
    public class CharacterMovement : MonoBehaviour
    {
        public Character Owner { get; private set; }
        
        public void Init(Character character)
        {
            Owner = character;
        }

        
        public void UpdateMovement(float x, float y)
        {
            
        }
    }
}