using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace OnlyLemons.BrawlParty.UI
{
    public class MainMenuVfx : MenuAnimations
    {
        private RectTransform[] Buttons => _buttons;

        [SerializeField]
        private RectTransform[] _buttons = null;

        public float Delay { get; set; }

        private void Start()
        {
            OnAppear();
        }

        public override void OnAppear()
        {
            Sequence seq = DOTween.Sequence();

            Delay = 0.2f;

            for (int i = 0; i < Buttons.Length; i++)
            {
                seq.Insert(0 + (Delay * i), Buttons[i].DOLocalMoveX(0, 1f).SetEase(Ease.InOutBack));
            }

            seq.Play();
        }
    }
}
