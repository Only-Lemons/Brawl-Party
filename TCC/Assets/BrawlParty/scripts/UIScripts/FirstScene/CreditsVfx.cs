using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace OnlyLemons.BrawlParty.UI
{
    public class CreditsVfx : MenuAnimations
    {
        private RectTransform[] Logos => _logos;
        private RectTransform[] Textos => _textos;
        private RectTransform TextBg => _textBg;
        private Image Background => _background;

        [SerializeField]
        private RectTransform[] _textos = null;

        [SerializeField]
        private RectTransform[] _logos = null;

        [SerializeField]
        private RectTransform _textBg = null;

        [SerializeField]
        private Image _background = null;

        [SerializeField]
        private Color _color = Color.white;
        void Start()
        {
            OnAppear();
        }

        public override void OnAppear()
        {
            Sequence seq = DOTween.Sequence();

            #region Prepare
            TextBg.localPosition = new Vector2(1912, 0);
            Logos[0].localPosition = new Vector2(-960, 0);
            Textos[0].localPosition = new Vector2(0, -1082);    
            #endregion

            seq.Insert(0, Logos[0].DOLocalMoveX(0, 1f).SetEase(Ease.InOutBack));
            seq.Join(Background.DOColor(_color, 1f));
            seq.Join(TextBg.DOLocalMoveX(0, 0.5f));
            seq.Join(Textos[0].DOLocalMoveY(1700, 20));

            seq.Insert(11.5f, Logos[1].DOLocalMoveY(0, 1f).SetEase(Ease.InOutBack));
            seq.Join(Logos[0].DOLocalMoveX(-960, 0.5f));

            seq.Insert(12, Textos[1].DOLocalMoveY(1700, 20));
            seq.Insert(19.5f, Logos[2].DOLocalMoveY(0, 1f).SetEase(Ease.InOutBack));
            seq.Join(Logos[1].DOLocalMoveY(-810.25f, 0.5f));
            seq.Join(Textos[2].DOLocalMoveY(1700, 20));

            seq.Play();
        }
    }
}
