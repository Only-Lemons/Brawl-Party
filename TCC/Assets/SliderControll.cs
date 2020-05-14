using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderControll : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textSlider = null;

    [SerializeField]
    private Slider _slider = null;
    
    private void Update()
    {
        UpdateSlider();
    }

    public void UpdateSlider() 
    {
        _textSlider.text = _slider.value.ToString("f0");
    }
}
