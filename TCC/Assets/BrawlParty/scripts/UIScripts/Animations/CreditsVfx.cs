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
        private MainMenuVfx _mainMenuVfx = null;

        [SerializeField]
        private CanvasGroup _thanksForPlaying = null;

        [SerializeField]
        private Color _color = Color.white;

        public Tween Anim { get; set; }

        public override void OnAppear()
        {
            Sequence seq = DOTween.Sequence();

            #region Prepare
            Anim?.Kill();

            for (int i = 0; i < Logos.Length; i++)
            {
                Logos[i].gameObject.SetActive(true);
                Textos[i].gameObject.SetActive(true);
            }

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
            seq.Insert(30, Logos[2].DOLocalMoveY(818, 0.3f));
            seq.Join(Logos[0].DOLocalMoveX(0, 1f).SetEase(Ease.InOutBack));
            seq.Join(_thanksForPlaying.DOFade(1, 1f));

            seq.Play();

            Anim = seq;
        }

        public override void OnDisappear()
        {
            Sequence seq = DOTween.Sequence();

            for (int i = 0; i < Logos.Length; i++)
            {
                Logos[i].gameObject.SetActive(false);
                Textos[i].gameObject.SetActive(false);
            }

            seq.Insert(0, TextBg.DOLocalMoveX(1910, 0.5f));

            seq.OnComplete(() => { _mainMenuVfx.gameObject.SetActive(true);  _mainMenuVfx.OnAppear(); this.gameObject.SetActive(false); });
            seq.Play();

            //Anim = seq;
        }
    }
}
