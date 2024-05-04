
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utility;

public class UIManager : MonoSingleton<UIManager>
{
   [SerializeField] 
   private Canvas DamageCanvas;
   
   
   public void InstantiateDamageUIPrefab(string key, Vector2 position, float damage)
   {
      Addressables.InstantiateAsync(key, Vector3.zero, Quaternion.identity, DamageCanvas.transform).
         Completed += (obj) =>
      {
         // Convert the world position to viewport position
         Vector2 viewportPos = Camera.main.WorldToViewportPoint(position);
                
         // Set the anchor position of the instantiated UI prefab to the viewport position
         RectTransform rectTransform = obj.Result.GetComponent<RectTransform>();
         rectTransform.anchorMin = viewportPos;
         rectTransform.anchorMax = viewportPos;
                
         // Reset the local position
         rectTransform.anchoredPosition = Vector2.zero;
            
         obj.Result.GetComponent<DamageText>().Init(damage);
      };  
   }
}
