using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class LogoSceneChange : MonoBehaviour
{
    public string scene;

    [SerializeField]
    private RectTransform _gameLogo = null;

    [SerializeField]
    private TextMeshProUGUI _anyButtonColor = null;

    public RectTransform _splashBG = null;

    Sequence seq;

    private void Start()
    {
        seq = DOTween.Sequence();
        _gameLogo.localScale = Vector3.zero;
        _anyButtonColor.color = new Vector4(1, 1, 1, 0);

        seq.Insert(0, _gameLogo.DOScale(Vector3.one, 1.5f).SetEase(Ease.InOutBack));
        seq.Append(_anyButtonColor.DOFade(1, 0.3f));
    }

    private void Update()
    {
        if (Input.anyKey) 
        {
            SceneManager.LoadScene(scene);
        }

    }
}
