using UnityEngine;

public class NextButtom : MonoBehaviour
{
    [SerializeField] private GameObject _lowImage;
    [SerializeField] private GameObject _balanceImage;
    [SerializeField] private GameObject _highImage;
    [SerializeField] private GameObject _lowNextButton;
    [SerializeField] private GameObject _balanceNextButton;
    [SerializeField] private GameObject _highNexthButton;

    public void BTM_NextHighButtonUI()
    {
        if (_highImage == true)
        {
            _lowImage.SetActive(false);
            _balanceImage.SetActive(false);
            _highImage.SetActive(true);

            _lowNextButton.SetActive(true);
            _balanceNextButton.SetActive(false);
            _highNexthButton.SetActive(false);

            QualitySettings.SetQualityLevel(2, true);
        }
    }
  
    public void BTM_NextBalanceButtonUI()
    {
        if (_balanceImage == true)
        {
            _lowImage.SetActive(false);
            _balanceImage.SetActive(true);
            _highImage.SetActive(false);

            _lowNextButton.SetActive(false);
            _balanceNextButton.SetActive(false);
            _highNexthButton.SetActive(true);

            QualitySettings.SetQualityLevel(1, true);
        }
    }

    public void BTM_NextLowButtonUI()
    {
        if (_lowImage == true)
        {
            _lowImage.SetActive(true);
            _balanceImage.SetActive(false);
            _highImage.SetActive(false);

            _lowNextButton.SetActive(false);
            _balanceNextButton.SetActive(true);
            _highNexthButton.SetActive(false);

            QualitySettings.SetQualityLevel(0, true);
        }
    }
}