using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailObject : MonoBehaviour
{
    public Sprite _gmSprite; // 여자 이미지
    public Sprite _userSrpite; // 별 이미지

    public Image _addresserImage;
    public Text _contentText;

    public LobbyTutorial _lobbyTutorial;

    public void Init(string content, bool isGM = true)
    {
        if (isGM)
            _addresserImage.sprite = _gmSprite;
        else
            _addresserImage.sprite = _userSrpite;

        _contentText.text = content;
    }

    public void ClickGetButton()
    {
        gameObject.SetActive(false);
    }
}
