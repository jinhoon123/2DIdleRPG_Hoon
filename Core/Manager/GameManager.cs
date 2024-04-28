using CHV;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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

    public BackgroundHandler backHandler;
    public BackgroundHandler groundHandler;
    

    #endregion
    
    public GameObject mainCharacterPrefab;
    public GameObject monsterPrefab;
    
    private void Awake()
    {
        // 메인 캐릭터 생성 및 초기화
        mainCharacter = Instantiate(mainCharacterPrefab, mainCharacterRoot).GetComponent<Character>();
        mainCharacter.Init(DataTable_Character_Data.GetData(10001)).Forget();
        
        backHandler.OnScrollCompletedEvent += GenerateMonsters;
        
        GenerateMonster().Forget();
    }

    private void GenerateMonsters()
    {
        GenerateMonster().Forget();
    }

    private async UniTaskVoid GenerateMonster()
    {
        for (var i = 0; i < 3; i++)
        {
            var monster = Instantiate(monsterPrefab, monsterRoot).GetComponent<Character>();
            var monsterTransform = monster.transform;
            var monsterPosition = monsterTransform.position;
            
            monsterPosition = new Vector3(monsterPosition.x - 0.5f * i, monsterPosition.y, monsterPosition.z);
            monsterTransform.position = monsterPosition;
            await monster.Init(DataTable_Character_Data.GetData(20001));
            
            monster.characterHealth.OnCharacterDead += OnMonsterDead;
            monster.gameObject.name = $"monster{i}";
            monsters.Add(monster);
        }
    }

    private void OnMonsterDead()
    {
        if (monsters.Count == 0)
        {
            backHandler.OnScrolling();
            groundHandler.OnScrolling();
        }
    }
}