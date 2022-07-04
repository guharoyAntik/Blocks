using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MainMenuButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => { OpenMainMenu(); });
    }

    public void OpenMainMenu()
    {
        SoundManager.Instance.PlayButtonClickSound();
        GameManager.Instance.MainMenu();
    }
}
