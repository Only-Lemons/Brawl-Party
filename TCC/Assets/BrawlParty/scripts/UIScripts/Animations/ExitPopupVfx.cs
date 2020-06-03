using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace OnlyLemons.BrawlParty.UI
{
    public class ExitPopupVfx : MenuAnimations
    {
        private RectTransform RectTransform => _rectTransform;
        private Image Background => _background;

        [SerializeField]
        private RectTransform _rectTransform = null;
        [SerializeField]
        private Image _background = null;

        void Start()
        {
            OnAppear();
        }

        public override void OnAppear()
        {
            #region prepare
            _rectTransform.localScale = Vector3.zero;

            var Color = Background.color;
            Color.a = 0;
            Background.color = Color;
            #endregion

            Sequence seq = DOTween.Sequence();

            seq.Insert(0, Background.DOFade(0.45f, 0.3f));
            seq.Insert(0, RectTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutBack));

            seq.Play();
        }

        public override void OnDisappear()
        {
            Sequence seq = DOTween.Sequence();

            seq.Insert(0, Background.DOFade(0, 0.3f));
            seq.Insert(0, RectTransform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InOutBack));

            seq.OnComplete(() => this.gameObject.SetActive(false));

            seq.Play();
        }

        public void CloseGame() 
        {
            Debug.Log("KITOOOOOOU!");
            Application.Quit();
        }

    }
}
