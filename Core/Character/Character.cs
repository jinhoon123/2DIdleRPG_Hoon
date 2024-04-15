using CHV;
using UnityEngine;

namespace Core.Character
{
    public class Character : MonoBehaviour
    {
        #region Components

        private CharacterMovement characterMovement;
        public CharacterTarget characterTarget;
        
        private SkillSystem skillSystem;

        #endregion

        #region Data
        
        public DataTable_Character_Data Data;
        
        #endregion

        private void Update()
        {
            characterTarget.UpdateTarget();
            skillSystem.UpdateSkill();
        }

        public void Init(DataTable_Character_Data inData)
        {
            InitData(inData);
            InitComponents();
        }

        private void InitData(DataTable_Character_Data inData)
        {
            Data = inData;
        }
        
        private void InitComponents()
        {
            characterMovement = GetComponent<CharacterMovement>();
            characterMovement.Init(this);

            characterTarget = GetComponent<CharacterTarget>();
            characterTarget.Init(this);

            skillSystem = GetComponent<SkillSystem>();
            skillSystem.Init(this);
        }
    }
}