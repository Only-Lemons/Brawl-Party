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

        [SerializeField]
        private RectTransform[] _buttons = null;
        [SerializeField]
        private Image _background = null;
        [SerializeField]
        private Color _mainMenuColor = Color.white;

        public float Delay { get; set; }
        public Tween Anim { get; set; }

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

            seq.Play();

            Anim = seq;
        }
    }
}
