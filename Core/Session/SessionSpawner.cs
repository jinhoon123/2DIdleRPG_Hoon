using CHV;
using UnityEngine;

public class SessionSpawner
{
    public void SpawnMainCharacter(DataTable_Character_Data data)
    {
        // 메인 캐릭터 생성 및 초기화
        ResourceHandler.I.InstantiateMainCharacterAsync((character =>
        {
            character.GetComponent<Character>().Init(data);
            
        }));
    }
}