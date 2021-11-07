using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SettingsUIManager : MonoBehaviour
{
    [SerializeField] private Image _toggleSoundImage;

    [SerializeField] private TextMeshProUGUI _toggleSoundText;

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

    public void OpenMainMenu()
    {
        SoundManager.Instance.PlayButtonClickSound();
        GameManager.Instance.MainMenu();
    }
}
