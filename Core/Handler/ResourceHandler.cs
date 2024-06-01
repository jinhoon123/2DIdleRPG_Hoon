using CHV;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceHandler : MonoSingleton<ResourceHandler>
{
    public void LoadAssetAsync<T>(string key, UnityAction<T> callback = null) where T : Object
    {
        var handle = Addressables.LoadAssetAsync<T>(key);
        handle.Completed += (obj) =>
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                callback?.Invoke(handle.Result);
            }
        };
    }
    
    // 캐릭터
    public void InstantiateCharacterAsync(int tid, DataTable_Character_Data.eCharacterType type, UnityAction<Character> callback = null)
    {
        var handle = Addressables.InstantiateAsync(type.ToString() + tid.ToString());
        handle.Completed += (obj) =>
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                callback?.Invoke(obj.Result.GetComponent<Character>());
            }
        };
    }
    
    // 스킬
    public void InstantiateSkillPrefabAsync<T>(Skill skill) where T : ISkillKind
    {
        skill.Asset.skillPrefab.InstantiateAsync(Vector3.zero, Quaternion.identity, GameManager.I.skillRoot)
            .Completed += (obj) =>
        {
            var skillPrefab = obj.Result.GetComponent<T>();
            skillPrefab.Init(skill);
        };
    }
}
