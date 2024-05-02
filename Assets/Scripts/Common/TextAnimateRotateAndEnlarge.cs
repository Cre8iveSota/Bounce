using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextAnimateRotateAndEnlarge : MonoBehaviour
{
    TextMeshProUGUI text;
    int callCount = 0;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        if (text != null && callCount < 4) StartCoroutine(CallTextEnlargerRepeatedly());
    }

    IEnumerator CallTextEnlargerRepeatedly()
    {
        while (true)
        {
            if (callCount < 4)
            {
                TextEnlarger();
                callCount++;
            }
            else
            {
                yield break;
            }

            yield return new WaitForSeconds(0.9f);
        }
    }

    private void TextEnlarger()
    {
        if (text == null) return;
        DOTweenTMPAnimator tmproAnimator = new DOTweenTMPAnimator(text);

        for (int i = 0; i < tmproAnimator.textInfo.characterCount; ++i)
        {
            DOTween.Sequence()
                .Append(tmproAnimator.DOShakeCharScale(i, 0.3f, 2));
        }
    }
}
