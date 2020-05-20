using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ToggleController : MonoBehaviour
{
    [SerializeField]
    private RectTransform _handleRech = null;

    [SerializeField]
    private Image _spriteOn = null;

    [SerializeField]
    private Image _spriteOff = null;

    [SerializeField]
    private Image _bgImage = null;

    [SerializeField]
    private float posOn = 0;

    [SerializeField]
    private float posOff = 1;


    public Color bgColorOn;
    public Color bgColorOff;
    bool deactived = true;
    Sequence seq;

    public void SetupToggle() 
    {
        seq = DOTween.Sequence();

        if (deactived) 
        {
            seq.Insert(0, _handleRech.DOLocalMoveX(posOn, 0.2f));
            seq.Join(_handleRech.DOScaleY(0.6f, 0.1f));
            seq.Append(_handleRech.DOScaleY(1, 0.1f));
            seq.Insert(0.1f, _spriteOn.DOFade(0, 0.1f));
            seq.Join(_spriteOff.DOFade(1, 0.1f));
            seq.Join(_bgImage.DOColor(bgColorOn, 0.1f));
            deactived = false;
        } else
        {
            seq.Insert(0, _handleRech.DOLocalMoveX(posOff, 0.2f));
            seq.Join(_handleRech.DOScaleY(0.6f, 0.1f));
            seq.Append(_handleRech.DOScaleY(1, 0.1f));
            seq.Insert(0.1f, _spriteOff.DOFade(0, 0.1f));
            seq.Join(_spriteOn.DOFade(1, 0.1f));
            seq.Join(_bgImage.DOColor(bgColorOff, 0.1f));
            deactived = true;
        }
    }

}
