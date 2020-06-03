﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

namespace OnlyLemons.BrawlParty.UI
{
    public class FirstSceneVfx : MenuAnimations
    {
        private RectTransform BackgroundLogoRect => _backgroundLogoRect;
        private RectTransform LogoParty => _logoParty;
        private RectTransform[] LogoBrawlLetters => _logoBrawlLetters;
        private CanvasGroup LogoCanvasGroup => _logoCanvasGroup;
        private RectTransform RectTransform => _rectTransform;
        private GameObject PressAnyButtonText => _pressAnyButtonText;
        private Image Background => _background;
        private Color NextSceneColor => _nextSceneColor;
        private string Scene => _scene;
        private GameObject ParticleObject => _particleObject;

        #region Serialize Fields
        [SerializeField]
        private RectTransform _backgroundLogoRect = null;
        [SerializeField]
        private RectTransform _logoParty = null;
        [SerializeField]
        private RectTransform _rectTransform = null;
        [SerializeField]
        private RectTransform[] _logoBrawlLetters = null;
        [SerializeField]
        private CanvasGroup _logoCanvasGroup = null;
        [SerializeField]
        private GameObject _pressAnyButtonText = null;
        [SerializeField]
        private Image _background = null;
        [SerializeField]
        private Color _nextSceneColor = Color.white;
        [SerializeField]
        private string _scene = "";
        [SerializeField]
        private GameObject _particleObject = null;
        #endregion

        public float Delay { get; set; }

        private void Start()
        {
            Appear();
        }

        //private void Update()
        //{
        //    if (Input.anyKey) Disappear();
        //}

        private void Appear()
        {
            Sequence seq = DOTween.Sequence();

            #region prepare

            Delay = 0.1f;

            BackgroundLogoRect.localScale = Vector3.zero;
            LogoParty.localScale = Vector3.zero;
            for (int i = 0; i < LogoBrawlLetters.Length; i++)
            {
                LogoBrawlLetters[i].localScale = Vector3.zero;
            }

            #endregion

            for (int i = 0; i < LogoBrawlLetters.Length; i++)
            {
                seq.Insert(0 + (Delay * i), LogoBrawlLetters[i].DOScale(1, 0.5f));
            }

            seq.Append(LogoParty.DOScale(1, 0.5f));
            seq.Append(BackgroundLogoRect.DOScale(1, 0.15f));
            seq.Append(RectTransform.DOPunchScale(Vector3.one * 0.6f, 1, 3, 0.4f));

            seq.Play();
        }

        private void Disappear()
        {
            Sequence seq = DOTween.Sequence();

            seq.OnStart(() => { PressAnyButtonText.SetActive(false); ParticleObject.SetActive(false); });

            seq.Insert(0, LogoCanvasGroup.DOFade(0, 0.5f));
            seq.Insert(0, Background.DOColor(NextSceneColor, 1f));

            //seq.OnComplete(() => { SceneManager.LoadScene(Scene); });

            seq.Play();
        }
    }
}