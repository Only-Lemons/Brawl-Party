using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace OnlyLemons.BrawlParty.UI
{
    public class SelectModeVfx : MenuAnimations
    {
        private Image Background => _background;
        private Color Color => _color;
        private RectTransform[] Buttons => _buttons;
        private Transform[] Position => _position;

        [SerializeField]
        private Image _background = null;
        [SerializeField]
        private Color _color = Color.white;
        [SerializeField]
        private RectTransform[] _buttons = null;
        [SerializeField]
        private Transform[] _position = null;
        [SerializeField]
        private MainMenuVfx _mainMenuVfx = null;
        private Tween Anim { get; set; }
        private float Delay { get; set; } = 0.15f;

        // Start is called before the first frame update
        void Start()
        {
            OnAppear();
        }

        public override void OnAppear()
        {
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].localPosition = new Vector2(Buttons[i].transform.localPosition.x, 800);
            }

            Sequence seq = DOTween.Sequence();
            seq.Play();

            for (int i = 0; i < Buttons.Length; i++)
            {
                seq.Insert(0 + (Delay * i), Buttons[i].DOLocalMoveY(Position[i].transform.localPosition.y, 0.5f + (Delay * i)).SetEase(Ease.InOutCirc));
            }

            seq.Insert(0, Background.DOColor(Color, 1));
        }

        public override void OnDisappear()
        {
            Sequence seq = DOTween.Sequence();
            seq.Play();

            for (int i = 0; i < Buttons.Length; i++)
            {
                seq.Insert(0 + (Delay * i), Buttons[i].DOLocalMoveY(800, 0.5f + (Delay * i)).SetEase(Ease.InOutCirc));
            }

            seq.OnComplete(() => { _mainMenuVfx.gameObject.SetActive(true); _mainMenuVfx.OnAppear(); this.gameObject.SetActive(false); });
        }
    }
}
