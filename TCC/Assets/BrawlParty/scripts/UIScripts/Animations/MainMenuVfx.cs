using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace OnlyLemons.BrawlParty.UI
{
    public class MainMenuVfx : MenuAnimations
    {
        private RectTransform[] Buttons => _buttons;
        private Image Background => _background;
        private Color MainMenuColor => _mainMenuColor;
        private CanvasGroup DecorCanvasGroup => _decorCanvasGroup;

        [SerializeField]
        private RectTransform[] _buttons = null;
        [SerializeField]
        private Image _background = null;
        [SerializeField]
        private Color _mainMenuColor = Color.white;
        [SerializeField]
        private CanvasGroup _decorCanvasGroup = null;

        public float Delay { get; set; }
        public Tween Anim { get; set; }

        private void Start()
        {
            OnAppear();
        }

        public override void OnAppear()
        {
            Sequence seq = DOTween.Sequence();
            Anim?.Kill();

            Delay = 0.2f;

            for (int i = 0; i < Buttons.Length; i++)
            {
                seq.Insert(0 + (Delay * i), Buttons[i].DOLocalMoveX(0, 1f).SetEase(Ease.InOutBack));
            }

            seq.Insert(0, Background.DOColor(MainMenuColor, 1f));
            seq.Insert(0.25f, DecorCanvasGroup.DOFade(1, 0.5f));

            seq.Play();

            Anim = seq;
        }

        public void Disappear() 
        {
            Sequence seq = DOTween.Sequence();
            Anim?.Kill();

            Delay = 0.2f;

            for (int i = 0; i < Buttons.Length; i++)
            {
                seq.Insert(0 + (Delay * i), Buttons[i].DOLocalMoveX(-2068, 1f).SetEase(Ease.InOutBack));
            }
            seq.Insert(0.25f, DecorCanvasGroup.DOFade(0, 0.3f));

            seq.Play();

            Anim = seq;
        }
    }
}
