using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Purchasing;

public class SettingsUIManager : MonoBehaviour
{
    [Header("Sounds Button")]
    [SerializeField] private Image _toggleSoundImage;
    [SerializeField] private TextMeshProUGUI _toggleSoundText;

    [Header("Remove Ads Button")]
    [SerializeField] private IAPButton _removeAdsButton;

    private void Awake()
    {
        _removeAdsButton.onPurchaseComplete.RemoveAllListeners();
        _removeAdsButton.onPurchaseComplete.AddListener(IAPManager.Instance.OnPurchaseComplete);
        _removeAdsButton.onPurchaseFailed.RemoveAllListeners();
        _removeAdsButton.onPurchaseFailed.AddListener(IAPManager.Instance.OnPurchaseFailed);
    }

    private void Start()
    {
        ToggleSound();
        ToggleSound();
    }

    public void SoundsOption()
    {
        ToggleSound();
        SoundManager.Instance.PlayButtonClickSound();
    }

    private void ToggleSound()
    {
        SoundManager.Instance.SoundsEnabled = (SoundManager.Instance.SoundsEnabled != true);
        _toggleSoundImage.color = (SoundManager.Instance.SoundsEnabled ? Color.green : Color.gray);
        _toggleSoundText.text = (SoundManager.Instance.SoundsEnabled ? "SOUNDS ON" : "SOUNDS OFF");
    }
}
