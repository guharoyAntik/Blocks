using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private bool _soundsEnabled;
    public bool SoundsEnabled
    {
        get
        {
            return _soundsEnabled;
        }
        set
        {
            _soundsEnabled = value;
            PlayerPrefs.SetInt("SoundsEnabled", (value == true ? 1 : 0));
        }
    }

    [Header("Audio Sources")]
    [SerializeField]
    private AudioSource _uiSounds;

    [SerializeField]
    private AudioSource _gameSounds;

    [Header("Audio Clips")]
    [SerializeField]
    private AudioClip _buttonClickClip;
    [SerializeField]
    private AudioClip _gameOverClip;
    [SerializeField]
    private AudioClip _placeBlockClip;

    [SerializeField]
    private AudioClip[] _cellsClearedClips;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        if (PlayerPrefs.HasKey("SoundsEnabled"))
        {
            _soundsEnabled = (PlayerPrefs.GetInt("SoundsEnabled") == 1);
        }
        else
        {
            SoundsEnabled = true;
        }

        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(_uiSounds.gameObject);
        DontDestroyOnLoad(_gameSounds.gameObject);
    }

    #region UI Sounds

    public void PlayButtonClickSound()
    {
        if (_soundsEnabled)
        {
            _uiSounds.clip = _buttonClickClip;
            _uiSounds.Play();
        }
    }

    public void PlayGameOverSound()
    {
        if (_soundsEnabled)
        {
            _uiSounds.clip = _gameOverClip;
            _uiSounds.Play();
        }
    }

    #endregion

    #region Game sounds

    public void PlayPlaceBlockSound()
    {
        if (_soundsEnabled)
        {
            _gameSounds.clip = _placeBlockClip;
            _gameSounds.Play();
        }
    }

    public void PlayCellsClearedSound(int clearedCells)
    {
        if (_soundsEnabled && clearedCells > 0)
        {
            if (clearedCells == 4)
            {
                _gameSounds.clip = _cellsClearedClips[0];
            }
            else if (clearedCells == 7 || clearedCells == 8)
            {
                _gameSounds.clip = _cellsClearedClips[1];
            }
            else if (clearedCells == 10)
            {
                _gameSounds.clip = _cellsClearedClips[2];
            }
            else if (clearedCells == 12)
            {
                _gameSounds.clip = _cellsClearedClips[3];
            }
            //might require changes
            _gameSounds.Play();
        }
    }

    #endregion
}
