using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//[System.Serializable]
//public class FishBookInfoDB
//{
//    public string Name { get; set; }
//    public string CatchInfo { get; set; }
//    public string SizeInfo { get; set; }
//    public string BaitInfo { get; set; }
//    public string MoveInfo { get; set; }
//}
public class BookSlot : MonoBehaviour /*,IPointerDownHandler,IPointerUpHandler*/
{
    // 물고기 등급에 따른 이름 색상
    readonly Color _sundryColor = new Color(0, 0.45f, 0.6f);
    readonly Color _normalColor = new Color(0, 0.6f, 0.15f);
    readonly Color _rareColor = new Color(0.4f, 0, 0.5f);

    // 0번 : 서식지 정보, 1번 : 크기 정보, 2번: 미끼,루어정보, 3번: 움직임 정보 
    public string[] info;
    public Text nameTxt;
    public Image fishImage;    
    public BookInfo bookInfo;
    public GameObject _starObject;

    [HideInInspector]
    public PublicDefined.eFishType _type;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => OpenFishInfo(true));
    }
    public void OpenFishInfo(bool isSet)
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
        bookInfo._content.anchoredPosition = Vector2.zero;

        if (isSet)
        {
            for (int i = 0; i < bookInfo.info.Length; i++)
            {
                if (i >= info.Length)
                    bookInfo.info[i].gameObject.SetActive(false);
                else
                {
                    bookInfo.info[i].text = info[i];
                    bookInfo.info[i].gameObject.SetActive(true);
                }
                    
            }
            bookInfo.fishImage.sprite = fishImage.sprite;
            bookInfo.fishName.text = nameTxt.text;
            bookInfo.fishName.color = GetColorAccordingToType(_type);
            bookInfo.gameObject.SetActive(isSet);            
        }
    }

    Color GetColorAccordingToType(PublicDefined.eFishType type)
    {
        switch (type)
        {
            case PublicDefined.eFishType.Sundry:
                return _sundryColor;
            case PublicDefined.eFishType.Normal:
                return _normalColor;
            case PublicDefined.eFishType.Rare:
                return _rareColor;
            default:
                return Color.white;
        }
    }
}
