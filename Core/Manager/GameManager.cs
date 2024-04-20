using CHV;
using System.Collections.Generic;
using Core.Character;
using Core.Handler;
using UnityEngine;
using Utility;

public class GameManager : MonoSingleton<GameManager>
{
    #region Roots

    [SerializeField] private Transform mainCharacterRoot;
    [SerializeField] private Transform monsterRoot;
    [SerializeField] public Transform skillRoot;

    #endregion
    
    #region Characters
    
    [HideInInspector] public Character mainCharacter;
    [HideInInspector] public List<Character> monsters = new List<Character>();
    
    #endregion

    #region Handler

    public BackgroundHandler backgroundHandler;

    #endregion
    
    public GameObject mainCharacterPrefab;
    public GameObject monsterPrefab;
    
    private void Awake()
    {
        mainCharacter = Instantiate(mainCharacterPrefab, mainCharacterRoot).GetComponent<Character>();
        mainCharacter.Init(DataTable_Character_Data.GetData(10001));
        backgroundHandler.OnScrollCompletedEvent += GenerateMonster;

        GenerateMonster();
    }

    private void GenerateMonster()
    {
        for (var i = 0; i < 3; i++)
        {
            var monster = Instantiate(monsterPrefab, monsterRoot).GetComponent<Character>();
            var monsterTransform = monster.transform;
            var monsterPosition = monsterTransform.position;
            
            monsterPosition = new Vector3(monsterPosition.x - 1.0f * i, monsterPosition.y, monsterPosition.z);
            monsterTransform.position = monsterPosition;
            monster.Init(DataTable_Character_Data.GetData(20001));
            
            monster.OnCharacterDead += OnMonsterDead;
            monsters.Add(monster);
        }
    }

    private void OnMonsterDead()
    {
        if (monsters.Count == 0)
        {
            backgroundHandler.OnScrolling();
        }
    }
}