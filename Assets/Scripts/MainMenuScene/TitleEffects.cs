using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleEffects : MonoBehaviour
{
    private Image _titleImage;

    private void Awake()
    {
        _titleImage = GetComponent<Image>();
        StartCoroutine(AnimateScale());
        StartCoroutine(AnimateRotation());
        StartCoroutine(AnimatePosition());
    }

    IEnumerator AnimatePosition()
    {
        Vector3 pos = transform.position;
        float animationTime = 2f;
        Ease ease = Ease.InOutQuad;

        yield return _titleImage.transform.DOMove(pos + Vector3.one * 0.1f, animationTime / 2).SetEase(ease).WaitForCompletion();
        while (gameObject.activeInHierarchy)
        {
            yield return _titleImage.transform.DOMove(pos - Vector3.one * 0.1f, animationTime).SetEase(ease).WaitForCompletion();
            yield return _titleImage.transform.DOMove(pos + Vector3.one * 0.1f, animationTime).SetEase(ease).WaitForCompletion();
        }

        yield return null;
    }

    IEnumerator AnimateRotation()
    {
        float animationTime = 2f;
        Ease ease = Ease.InOutQuad;

        yield return _titleImage.transform.DOLocalRotate(new Vector3(0, 0, 1), animationTime / 2).SetEase(ease).WaitForCompletion();
        while (gameObject.activeInHierarchy)
        {
            yield return _titleImage.transform.DOLocalRotate(new Vector3(0, 0, -1), animationTime).SetEase(ease).WaitForCompletion();
            yield return _titleImage.transform.DOLocalRotate(new Vector3(0, 0, 1), animationTime).SetEase(ease).WaitForCompletion();
            yield return _titleImage.transform.DOLocalRotate(new Vector3(0, 0, -1), animationTime).SetEase(ease).WaitForCompletion();
            yield return _titleImage.transform.DOLocalRotate(new Vector3(0, 0, 1), animationTime).SetEase(ease).WaitForCompletion();
        }

        yield return null;
    }

    IEnumerator AnimateScale()
    {
        while (gameObject.activeInHierarchy)
        {
            yield return _titleImage.transform.DOScale(Vector3.one * 0.9f, 2f).SetEase(Ease.InOutQuad).WaitForCompletion();
            yield return _titleImage.transform.DOScale(Vector3.one * 1.0f, 2f).SetEase(Ease.InOutQuad).WaitForCompletion();
        }

        yield return null;
    }
}
