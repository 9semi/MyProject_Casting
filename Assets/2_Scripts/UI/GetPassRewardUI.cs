using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPassRewardUI : MonoBehaviour
{
    [Header("FreeReward")]
    [SerializeField] GameObject _freeRewardObject;
    [SerializeField] Text _freeRewardText_free;
    [SerializeField] Image _freeRewardImage_free;
    [SerializeField] Text _freeRewardQuantity_free;


    [Header("PremiumReward")]
    [SerializeField] GameObject _preRewardObject;
   [SerializeField] Text _preRewardText_pre;
   [SerializeField] Text _freeRewardText_pre;
   [SerializeField] Image _preRewardImage_pre;
   [SerializeField] Image _freeRewardImage_pre;
   [SerializeField] Text _preRewardQuantity_pre;
   [SerializeField] Text _freeRewardQuantity_pre;    

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
