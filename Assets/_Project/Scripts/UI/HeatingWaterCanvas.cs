 using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeatingWaterCanvas : MonoBehaviour
{
    [SerializeField] private Image _uiThermometer;
    [SerializeField] private TMP_Text _uiThermometerValue;


    public int _maxHeat = 100;
    public int _minHeat = 0;

    public int _upAndLowerHeat = 5;

    public int _heatValue = 50;

    private void Update()
    {
        HeatingWater();
    }

    private void HeatingWater()
    {
        _uiThermometer.fillAmount = _heatValue / 100f;
        _uiThermometerValue.text = _heatValue.ToString();
    }

    public void BTM_UpDegree()
    {
        if (_heatValue >= _maxHeat)
        {
            return;
        }
        else
        {
            _heatValue += _upAndLowerHeat;
        }
    }

    public void BTM_LowerDegree()
    {
        
        if (_heatValue <= _minHeat)
        {
            return;
        }
        else
        {
            _heatValue -= _upAndLowerHeat;
        }
    }
}
