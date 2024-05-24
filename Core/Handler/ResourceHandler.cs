using CHV;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceHandler : MonoSingleton<ResourceHandler>
{
    public void InstantiateAsset<T>(string key, UnityAction<T> callback = null) where T : Object
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
    public void InstantiateMainCharacter(UnityAction<GameObject> callback = null)
    {
        var handle = Addressables.LoadAssetAsync<GameObject>("MainCharacter");
        handle.Completed += (obj) =>
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject prefab = obj.Result;
                GameObject character = Instantiate(prefab, GameManager.I.mainCharacterRoot);
                callback?.Invoke(character);
            }
        };
    }
    
    // 스킬
    public void InstantiateSkillPrefab<T>(Skill skill) where T : ISkillKind
    {
        skill.Asset.skillPrefab.InstantiateAsync(Vector3.zero, Quaternion.identity, GameManager.I.skillRoot)
            .Completed += (obj) =>
        {
            var skillPrefab = obj.Result.GetComponent<T>();
            skillPrefab.Init(skill);
        };
    }
}
