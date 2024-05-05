using System.Globalization;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damageText;
    
    public void Init(float amount)
    {
        damageText.text = amount.ToString(CultureInfo.InvariantCulture);
        Destroy(gameObject, 0.25f);
    }
    
}
