using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using System.Threading.Tasks;

public class FlashMessage : MonoBehaviour
{
    [SerializeField] private Sprite[] _redFlashMessages;
    [SerializeField] private Sprite[] _blueFlashMessages;

    [SerializeField] private Image _flashMessageImage;

    public void SetFlashMessage(string color, int index)
    {
        if (color == "blue")
        {
            if (index >= _blueFlashMessages.Length)
            {
                Debug.Log("invalid flash message index " + index);
                return;
            }
            _flashMessageImage.sprite = _blueFlashMessages[index];
        }
        else if (color == "red")
        {
            if (index >= _redFlashMessages.Length)
            {
                Debug.Log("invalid flash message index " + index);
                return;
            }
            _flashMessageImage.sprite = _redFlashMessages[index];
        }
        else
        {
            Debug.Log("invalid flash message color " + color);
        }
    }

    public async Task ShowFlashMessage()
    {
        float screenWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        float spriteWidth = _flashMessageImage.sprite.bounds.size.x;
        Vector3 leftPosition = new Vector3(-screenWidth - spriteWidth * 0.5f, transform.position.y, transform.position.z);
        Vector3 midPosition = new Vector3(0f, transform.position.y, transform.position.z);
        Vector3 rightPosition = new Vector3(screenWidth + spriteWidth * 0.5f, transform.position.y, transform.position.z);

        this.gameObject.transform.position = leftPosition;
        this.gameObject.SetActive(true);

        await this.gameObject.transform.DOMove(midPosition, 0.5f).SetEase(Ease.OutQuad).Play().AsyncWaitForCompletion();
        await Task.Delay(System.TimeSpan.FromSeconds(1));
        await this.gameObject.transform.DOMove(rightPosition, 0.5f).SetEase(Ease.InQuad).Play().AsyncWaitForCompletion();


        Destroy(this.gameObject);
    }
}
