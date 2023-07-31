using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPearlSlot : MonoBehaviour
{
    [SerializeField] Item _item;
    [SerializeField] BuyPopUP _buyPop;

    public void ClickBuyButton()
    {
        //SoundManager.instance.EffectPlay("UIClick");
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
        _buyPop.PopupSetItem(_item);

    }
}
