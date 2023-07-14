using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPassRewardUI : MonoBehaviour
{
    [Header("FreeReward")]
    public GameObject _freeRewardObject;
    public Text _freeRewardText_free;
    public Image _freeRewardImage_free;
    public Text _freeRewardQuantity_free;


    [Header("PremiumReward")]
    public GameObject _preRewardObject;
    public Text _preRewardText_pre;
    public Text _freeRewardText_pre;
    public Image _preRewardImage_pre;
    public Image _freeRewardImage_pre;
    public Text _preRewardQuantity_pre;
    public Text _freeRewardQuantity_pre;    

    public void InitForPremium(string preRewardText, Sprite preRewardSprite, int preRewardQuantity, string freeRewardText, Sprite freeRewardSprite, int freeRewardQuantity)
    {
        _preRewardText_pre.text = preRewardText;
        _preRewardImage_pre.sprite = preRewardSprite;
        _preRewardQuantity_pre.text = preRewardQuantity.ToString();

        _freeRewardText_pre.text = freeRewardText;
        _freeRewardImage_pre.sprite = freeRewardSprite;
        _freeRewardQuantity_pre.text = freeRewardQuantity.ToString();

        _preRewardObject.SetActive(true);
        _freeRewardObject.SetActive(false);

        StartCoroutine(OffCoroutine());
    }

    public void InitForFree(string freeRewardText, Sprite freeRewardSprite, int freeRewardQuantity)
    {
        _freeRewardText_free.text = freeRewardText;
        _freeRewardImage_free.sprite = freeRewardSprite;
        _freeRewardQuantity_free.text = freeRewardQuantity.ToString();

        _freeRewardObject.SetActive(true);
        _preRewardObject.SetActive(false);

        StartCoroutine(OffCoroutine());
    }

    IEnumerator OffCoroutine()
    {
        yield return PublicDefined._1secRealDelay;
        gameObject.SetActive(false);
    }

    public void PlayEffectAudio()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();
        gameObject.SetActive(false);
    }
    public void ClickCancel()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();
        gameObject.SetActive(false);
    }
}
