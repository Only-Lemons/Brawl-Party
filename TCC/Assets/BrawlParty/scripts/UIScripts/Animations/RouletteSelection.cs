using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using DG.Tweening;

public class RouletteSelection : MonoBehaviour
{
    private RectTransform[] Objects => _objects;
    private Transform[] Positions => _positions;
    private Image[] Opacity => _opacity;
    private EventSystem EventSystem => _eventSystem;
    private GameObject SelectMinigamePanel => _selectMinigamePanel;

    #region SerializeFields
    [SerializeField]
    private RectTransform[] _objects = null;
    [SerializeField]
    private Transform[] _positions = null;
    [SerializeField]
    private Image[] _opacity = null;
    [SerializeField]
    private EventSystem _eventSystem = null;   
    [SerializeField]
    private GameObject _selectMinigamePanel = null;
    #endregion

    public float scrollSpeed;

#if UNITY_EDITOR
    public void OnValidate() 
    {
        for (int i = 0; i < Objects.Length; i++)
        {
            Objects[i].localPosition = Positions[i].localPosition;
        }

        Objects[0].localScale = Vector3.one * 0.7f;
        Objects[4].localScale = Objects[0].localScale;
        Objects[1].localScale = Vector3.one;
        Objects[3].localScale = Objects[1].localScale;
    }
#endif

    public void UpdateLeftSelected() 
    {
        Sequence seq = DOTween.Sequence();
        seq.Play();

        seq.Insert(0, Objects[0].DOLocalMoveX(Positions[4].localPosition.x, scrollSpeed));
        seq.Join(Objects[1].DOLocalMoveX(Positions[0].localPosition.x, scrollSpeed));
        seq.Join(Objects[2].DOLocalMoveX(Positions[1].localPosition.x, scrollSpeed));
        seq.Join(Objects[3].DOLocalMoveX(Positions[2].localPosition.x, scrollSpeed));
        seq.Join(Objects[4].DOLocalMoveX(Positions[3].localPosition.x, scrollSpeed));

        seq.Insert(0, Objects[1].DOScale(Objects[0].localScale, scrollSpeed));
        seq.Join(Objects[2].DOScale(Objects[1].localScale, scrollSpeed));
        seq.Join(Objects[3].DOScale(Objects[2].localScale, scrollSpeed));
        seq.Join(Objects[4].DOScale(Objects[3].localScale, scrollSpeed));

        seq.Insert(0, Opacity[1].DOFade(0.75f, scrollSpeed));
        seq.Join(Opacity[0].DOFade(0.75f, scrollSpeed));
        seq.Join(Opacity[2].DOFade(0.5f, scrollSpeed));
        seq.Join(Opacity[3].DOFade(0, scrollSpeed));
        seq.Join(Opacity[4].DOFade(0.5f, scrollSpeed));

        seq.OnComplete(UpdateArrayLeft);

    }

    public void UpdateRightSelected()
    {
        Sequence seq = DOTween.Sequence();
        seq.Play();

        seq.Insert(0, Objects[0].DOLocalMoveX(Positions[1].localPosition.x, scrollSpeed));
        seq.Join(Objects[1].DOLocalMoveX(Positions[2].localPosition.x, scrollSpeed));
        seq.Join(Objects[2].DOLocalMoveX(Positions[3].localPosition.x, scrollSpeed));
        seq.Join(Objects[3].DOLocalMoveX(Positions[4].localPosition.x, scrollSpeed));
        seq.Join(Objects[4].DOLocalMoveX(Positions[0].localPosition.x, scrollSpeed));

        seq.Insert(0, Objects[1].DOScale(Objects[2].localScale, scrollSpeed));
        seq.Join(Objects[2].DOScale(Objects[3].localScale, scrollSpeed));
        seq.Join(Objects[3].DOScale(Objects[4].localScale, scrollSpeed));
        seq.Join(Objects[0].DOScale(Objects[1].localScale, scrollSpeed));

        seq.Insert(0, Opacity[1].DOFade(0, scrollSpeed));
        seq.Join(Opacity[0].DOFade(0.5f, scrollSpeed));
        seq.Join(Opacity[2].DOFade(0.5f, scrollSpeed));
        seq.Join(Opacity[3].DOFade(0.75f, scrollSpeed));
        seq.Join(Opacity[4].DOFade(0.75f, scrollSpeed));

        seq.OnComplete(UpdateArrayRight);
    }

    private void UpdateArrayLeft() 
    {
        RectTransform temp = Objects[0];
        Objects[0] = Objects[1];
        Objects[1] = Objects[2];
        Objects[2] = Objects[3];
        Objects[3] = Objects[4];
        Objects[4] = temp;

        Image imgtemp = Opacity[0];
        Opacity[0] = Opacity[1];
        Opacity[1] = Opacity[2];
        Opacity[2] = Opacity[3];
        Opacity[3] = Opacity[4];
        Opacity[4] = imgtemp;


        EventSystem.SetSelectedGameObject(Objects[2].gameObject);
    }

    private void UpdateArrayRight() 
    {
        RectTransform temp = Objects[4];
        Objects[4] = Objects[3];
        Objects[3] = Objects[2];
        Objects[2] = Objects[1];
        Objects[1] = Objects[0];
        Objects[0] = temp;

        Image imgtemp = Opacity[4];
        Opacity[4] = Opacity[3];
        Opacity[3] = Opacity[2];
        Opacity[2] = Opacity[1];
        Opacity[1] = Opacity[0];
        Opacity[0] = imgtemp;

        EventSystem.SetSelectedGameObject(Objects[2].gameObject);
    }


    void OnGoRight()
    {
        if(_selectMinigamePanel.activeSelf)
        UpdateRightSelected();
    }
    void OnGoLeft()
    {
        if (_selectMinigamePanel.activeSelf)
            UpdateLeftSelected();
    }
    public void Debug() 
    {
        UnityEngine.Debug.Log("Teste" + Objects[2].name);
    }
}
