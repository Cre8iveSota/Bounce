using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class TextAnimateRotateColor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI tmpro = GetComponent<TextMeshProUGUI>();
        tmpro.color = Color.white;

        DOTweenTMPAnimator tmproAnimator = new DOTweenTMPAnimator(tmpro);

        for (int i = 0; i < tmproAnimator.textInfo.characterCount; ++i)
        {
            tmproAnimator.DORotateChar(i, Vector3.up * 90, 0);
            DOTween.Sequence()
                .Append(tmproAnimator.DORotateChar(i, Vector3.zero, 0.2f))
                .Append(tmproAnimator.DOShakeCharRotation(i, .5f, new Vector3(0, 0, 20)))
                .AppendInterval(.05f)
                .Append(tmproAnimator.DOColorChar(i, Color.red, 0.2f).SetLoops(2, LoopType.Yoyo))
                .SetDelay(0.05f * i);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
