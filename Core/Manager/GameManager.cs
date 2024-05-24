using CHV;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    #region Roots

    [SerializeField] public Transform mainCharacterRoot;
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
        SessionManager.I.Initialize();
        SessionManager.I.RequestSession(eSession.Game);
        
        backHandler.OnScrollCompletedEvent += GenerateMonsters;

        GenerateMonster();
    }

    private void GenerateMonsters()
    {
        GenerateMonster();
    }

    private void GenerateMonster()
    {
        for (var i = 0; i < 3; i++)
        {
            var monster = Instantiate(monsterPrefab, monsterRoot).GetComponent<Character>();
            var monsterTransform = monster.transform;
            var monsterPosition = monsterTransform.position;
            
            monsterPosition = new Vector3(monsterPosition.x - 0.5f * i, monsterPosition.y, monsterPosition.z);
            monsterTransform.position = monsterPosition;
            monster.Init(DataTable_Character_Data.GetData(20001));
            
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