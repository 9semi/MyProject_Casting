using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelectScene_RankUI : MonoBehaviour
{    
    // 물고기 등급에 따른 이름 색상
    readonly Color _sundryColor = new Color(0, 0.45f, 0.6f);
    readonly Color _normalColor = new Color(0, 0.6f, 0.15f);
    readonly Color _rareColor = new Color(0.4f, 0, 0.5f);

    [SerializeField] Sprite[] _jeongdongjinFishSprites;
    [SerializeField] GameObject[] _jeongdongjinFishObjects;
    [SerializeField] Text _jeongdongjinCountText;

    [SerializeField] Sprite[] _skywayFishSprites;
    [SerializeField] GameObject[] _skywayFishObjects;
    [SerializeField] Text _skywayCountText;

    [SerializeField] Sprite[] _homerspitFishSprites;
    [SerializeField] GameObject[] _homerspitFishObjects;
    [SerializeField] Text _homerspitCountText;

    [SerializeField] Transform _content;

    public int Init(int mapNumber)
    {
        int count = 0;

        Dictionary<int, PublicDefined.stRankFishInfo> dic;
        switch (mapNumber)
        {
            case 0:
                dic = DBManager.INSTANCE.GetUserData().GetJeongdongjinRankDictionary();

                for (int i = 0; i < _content.childCount; i++)
                {
                    Fish fish = _jeongdongjinFishObjects[i].GetComponent<Fish>();

                    if (dic.ContainsKey(fish.GetFishDBNum()))
                    {
                        _content.GetChild(i).transform.GetComponent<FishRankSlot>().InitTrue(GetColorAccordingToType(fish.GetStructFishData()._gradeType), _jeongdongjinFishSprites[i],
                            fish.GetFishKoreanName(), dic[fish.GetFishDBNum()]._length, dic[fish.GetFishDBNum()]._weight);
                        count++;
                    }
                    else
                    {
                        _content.GetChild(i).transform.GetComponent<FishRankSlot>().InitFalse(_jeongdongjinFishSprites[i], fish.GetFishKoreanName());
                    }
                }
                _jeongdongjinCountText.text = count.ToString();
                return count;
            case 1:
                dic = DBManager.INSTANCE.GetUserData().GetSkywayRankDictionary();

                for (int i = 0; i < _content.childCount; i++)
                {
                    FishSkyway fish = _skywayFishObjects[i].GetComponent<FishSkyway>();

                    if (dic.ContainsKey(fish.fishDBNum))
                    {
                        _content.GetChild(i).transform.GetComponent<FishRankSlot>().InitTrue(GetColorAccordingToType(fish.fishType), _skywayFishSprites[i],
                            fish.fishKoreanName, dic[fish.fishDBNum]._length, dic[fish.fishDBNum]._weight);
                        count++;
                    }
                    else
                    {
                        _content.GetChild(i).transform.GetComponent<FishRankSlot>().InitFalse(_skywayFishSprites[i], fish.fishKoreanName);
                    }
                }
                _skywayCountText.text = count.ToString();
                return count;
            case 2:
                dic = DBManager.INSTANCE.GetUserData().GetHomerspitRankDictionary();

                for (int i = 0; i < _content.childCount; i++)
                {
                    FishHomerspit fish = _homerspitFishObjects[i].GetComponent<FishHomerspit>();

                    if (dic.ContainsKey(fish.fishDBNum))
                    {
                        _content.GetChild(i).transform.GetComponent<FishRankSlot>().InitTrue(GetColorAccordingToType(fish.fishType), _homerspitFishSprites[i],
                            fish.fishKoreanName, dic[fish.fishDBNum]._length, dic[fish.fishDBNum]._weight);
                        count++;
                    }
                    else
                    {
                        _content.GetChild(i).transform.GetComponent<FishRankSlot>().InitFalse(_homerspitFishSprites[i], fish.fishKoreanName);
                    }
                }
                _homerspitCountText.text = count.ToString();
                return count;
            default:
                return -1;
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
