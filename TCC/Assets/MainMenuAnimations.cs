using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainMenuAnimations : MonoBehaviour
{
    [SerializeField]
    private RectTransform[] _buttons = null;

    private float _delayBetweenAnims = 0.4f;

    Sequence seq;

    // Start is called before the first frame update
    void Start()
    {

        seq = DOTween.Sequence();
        seq.Insert(0, _buttons[0].DOLocalMoveX(0, _delayBetweenAnims).SetEase(Ease.InOutBack));
        seq.Insert(0.1f, _buttons[1].DOLocalMoveX(0, _delayBetweenAnims).SetEase(Ease.InOutBack));
        seq.Insert(0.2f, _buttons[2].DOLocalMoveX(0, _delayBetweenAnims).SetEase(Ease.InOutBack));
        seq.Insert(0.3f, _buttons[3].DOLocalMoveX(0, _delayBetweenAnims).SetEase(Ease.InOutBack));
        seq.Insert(0.4f, _buttons[4].DOLocalMoveX(0, _delayBetweenAnims).SetEase(Ease.InOutBack));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
