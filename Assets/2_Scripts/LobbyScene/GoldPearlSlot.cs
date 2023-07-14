using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPearlSlot : MonoBehaviour
{
    public Item _item;
    public BuyPopUP _buyPop;

    public void ClickBuyButton()
    {
        //SoundManager.instance.EffectPlay("UIClick");
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
        _buyPop.PopupSetItem(_item);

    }
}
