using CHV;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using Utility;

public class ResourceHandler : MonoSingleton<ResourceHandler>
{
    // 스킬
    public async UniTask InstantiateSkillAsset<T>(string key, UnityAction<T> callback) where T : Object
    {
        var handle = Addressables.LoadAssetAsync<T>(key);
        await handle.Task;
    
        callback.Invoke(handle.Result);
    }
    
    public void InstantiateSkillPrefab(Skill skill)
    {
        if (skill.Data.Kind == DataTable_Skill_Data.eKind.Projectile)
        {
            skill.Asset.skillPrefab.InstantiateAsync(Vector3.zero, Quaternion.identity, GameManager.I.skillRoot)
                .Completed += (obj) =>
            {
                var skillPrefab = obj.Result.GetComponent<SkillProjectile>();
                skillPrefab.Init(skill);
            };
        }
    }
}