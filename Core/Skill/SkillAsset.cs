using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SkillAsset", menuName = "Assets/Skill")]
public class SkillAsset : ScriptableObject
{
    public AssetReferenceGameObject skillPrefab;
}