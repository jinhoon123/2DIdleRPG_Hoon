using System.Collections.Generic;
using CHV;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SessionSpawner
{
    private SessionManager owner;
    
    public Character MainCharacter;
    public readonly List<Character> Monsters = new List<Character>();

    public SessionSpawner(SessionManager inOwner)
    {
        owner = inOwner;
    }
    
    public void SpawnMainCharacter(DataTable_Character_Data data)
    {
        // 메인 캐릭터 생성 및 초기화
        ResourceHandler.I.InstantiateCharacterAsync(data.TID, data.CharacterType, (character =>
        {
            character.transform.SetParent(GameManager.I.mainCharacterRoot);
            character.transform.position = GameManager.I.mainCharacterRoot.position;
            character.transform.rotation = Quaternion.identity;
            
            MainCharacter = character.GetComponent<Character>();
            MainCharacter.Init(data);
        }));
    }

    public void SpawnMonster(DataTable_Character_Data data)
    {
        for (var i = 0; i < 3; i++)
        {
            var index = i;
            
            ResourceHandler.I.InstantiateCharacterAsync(data.TID, data.CharacterType, (character =>
            {
                character.transform.SetParent(GameManager.I.monsterRoot);
                character.transform.position = GameManager.I.monsterRoot.position;
                character.transform.rotation = Quaternion.identity;
                
                var monsterTransform = character.transform;
                var monsterPosition = monsterTransform.position;
                
                monsterPosition = new Vector3(monsterPosition.x - 0.5f * index, monsterPosition.y, monsterPosition.z);
                monsterTransform.position = monsterPosition;
                
                character.Init(data);
                character.characterEvent.OnCharacterDead += () =>
                {
                    Monsters.Remove(character);
                    Addressables.Release(character.gameObject);
                };

                character.characterEvent.OnCharacterDead += owner.SessionEventManager.OnMonsterDead;
                
                Monsters.Add(character);
            }));
        }
    }
}