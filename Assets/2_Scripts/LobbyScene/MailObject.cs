using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailObject : MonoBehaviour
{
    [SerializeField] Sprite _gmSprite; // ���� �̹���
    [SerializeField] Sprite _userSrpite; // �� �̹���

    [SerializeField] Image _addresserImage;
    [SerializeField] Text _contentText;

    [SerializeField] LobbyTutorial _lobbyTutorial;

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
