using UnityEngine;
using UnityEngine.UI;
public class UIHPBarUpdate : MonoBehaviour
{
    [SerializeField] private Image _imageHPBar;

    
    public void HPBarUpdate(float currentHealth, float maxHealth)
    {
        var value = maxHealth / currentHealth;
        _imageHPBar.fillAmount = value;
    }
}

