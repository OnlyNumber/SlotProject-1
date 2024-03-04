using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchSprite : MonoBehaviour
{

    [SerializeField]
    private Image _image;

    [SerializeField]
    private Sprite _buttonSpriteOn;

    [SerializeField]
    private Sprite _buttonSpriteOff;

    public void SwitchSpriteMethod(bool sprite)
    {
        if(sprite)
        {
            _image.sprite = _buttonSpriteOn;
        }
        else
        {
            _image.sprite = _buttonSpriteOff;
        }
    }


}
