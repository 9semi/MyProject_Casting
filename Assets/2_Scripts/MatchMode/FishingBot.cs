using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingBot : MonoBehaviour
{
    // 테스트를 위해 각 코루틴 함수를 수정
    readonly int totalTime = 300;

    public MatchManager _matchManager;

    public FishObjectManager _jeongdongjin;
    public FishObjectManagerSkyway _skyway;
    public FishObjectManagerHomerspit _homerspit;

    // jeongdongjin 물고기 리스트
    List<Fish> _jeongdongjinSundryList;
    List<Fish> _jeongdongjinNormalList;
    List<Fish> _jeongdongjinRareList;
    List<Fish> _jeongdongjinRandomFishList;

    // skyway 물고기 리스트
    List<FishSkyway> _skywaySundryList;
    List<FishSkyway> _skywayNormalList;
    List<FishSkyway> _skywayRareList;
    List<FishSkyway> _skywayRandomFishList;

    // homerspit 물고기 리스트
    List<FishHomerspit> _homerspitSundryList;
    List<FishHomerspit> _homerspitNormalList;
    List<FishHomerspit> _homerspitRareList;
    List<FishHomerspit> _homerspitRandomFishList;

    UserData _userData;

    public void CheckFish() // FishObjectManager에서 호출한다. 물고기 생성할 때. 
    {
        _userData = DBManager.INSTANCE.GetUserData();
        //Debug.Log(_userData._grade);

        switch (DataManager.INSTANCE._mapType)
        {
            case PublicDefined.eMapType.jeongdongjin:
                _jeongdongjinSundryList = new List<Fish>();
                _jeongdongjinNormalList = new List<Fish>();
                _jeongdongjinRareList = new List<Fish>();
                _jeongdongjinRandomFishList = new List<Fish>();
                JeongdongjinSundryFishCheck();
                JeongdongjinNormalFishCheck();
                JeongdongjinRareFishCheck();
                StartCoroutine(JeongdongjinFishBotCoroutine());
                break;
            case PublicDefined.eMapType.skyway:
                _skywaySundryList = new List<FishSkyway>();
                _skywayNormalList = new List<FishSkyway>();
                _skywayRareList = new List<FishSkyway>();
                _skywayRandomFishList = new List<FishSkyway>();
                SkywaySndryFishCheck();
                SkywayNormalFishCheck();
                SkywayRareFishCheck();
                StartCoroutine(SkywayFishBotCoroutine());
                break;
            case PublicDefined.eMapType.homerspit:
                _homerspitSundryList = new List<FishHomerspit>();
                _homerspitNormalList = new List<FishHomerspit>();
                _homerspitRareList = new List<FishHomerspit>();
                _homerspitRandomFishList = new List<FishHomerspit>();
                HomerspitSundryFishCheck();
                HomerspitNormalFishCheck();
                HomerspitRareFishCheck();
                StartCoroutine(HomerspitFishBotCoroutine());
                break;
        }
    }

    void JeongdongjinSundryFishCheck()
    {
        //    int cnt = _jeongdongjin.finespottedflounder.Length;
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        if (!_jeongdongjin.finespottedflounder[i].activeSelf)
        //            break;

        //        _jeongdongjinSundryList.Add(_jeongdongjin.finespottedflounder[i].GetComponent<Fish>());
        //    }

        //    cnt = _jeongdongjin.brownsole.Length;
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        if (!_jeongdongjin.brownsole[i].activeSelf)
        //            break;

        //        _jeongdongjinSundryList.Add(_jeongdongjin.brownsole[i].GetComponent<Fish>());
        //    }

        //    cnt = _jeongdongjin.fatgreenling.Length;
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        if (!_jeongdongjin.fatgreenling[i].activeSelf)
        //            break;

        //        _jeongdongjinSundryList.Add(_jeongdongjin.fatgreenling[i].GetComponent<Fish>());
        //    }

        //    cnt = _jeongdongjin.spottybellygreenling.Length;
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        if (!_jeongdongjin.spottybellygreenling[i].activeSelf)
        //            break;

        //        _jeongdongjinSundryList.Add(_jeongdongjin.spottybellygreenling[i].GetComponent<Fish>());
        //    }

        //    cnt = _jeongdongjin.surfperch.Length;
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        if (!_jeongdongjin.surfperch[i].activeSelf)
        //            break;

        //        _jeongdongjinSundryList.Add(_jeongdongjin.surfperch[i].GetComponent<Fish>());
        //    }

        //    cnt = _jeongdongjin.sandsmelt.Length;
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        if (!_jeongdongjin.sandsmelt[i].activeSelf)
        //            break;

        //        _jeongdongjinSundryList.Add(_jeongdongjin.sandsmelt[i].GetComponent<Fish>());
        //    }

        //    cnt = _jeongdongjin.pacificherring.Length;
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        if (!_jeongdongjin.pacificherring[i].activeSelf)
        //            break;

        //        _jeongdongjinSundryList.Add(_jeongdongjin.pacificherring[i].GetComponent<Fish>());
        //    }

        //    cnt = _jeongdongjin.babyseabass.Length;
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        if (!_jeongdongjin.babyseabass[i].activeSelf)
        //            break;

        //        _jeongdongjinSundryList.Add(_jeongdongjin.babyseabass[i].GetComponent<Fish>());
        //    }

        //    cnt = _jeongdongjin.mackerel.Length;
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        if (!_jeongdongjin.mackerel[i].activeSelf)
        //            break;

        //        _jeongdongjinSundryList.Add(_jeongdongjin.mackerel[i].GetComponent<Fish>());
        //    }

        //    cnt = _jeongdongjin.horsemackerel.Length;
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        if (!_jeongdongjin.horsemackerel[i].activeSelf)
        //            break;

        //        _jeongdongjinSundryList.Add(_jeongdongjin.horsemackerel[i].GetComponent<Fish>());
        //    }

        //    cnt = _jeongdongjin.konosiruspunctatus.Length;
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        if (!_jeongdongjin.konosiruspunctatus[i].activeSelf)
        //            break;

        //        _jeongdongjinSundryList.Add(_jeongdongjin.konosiruspunctatus[i].GetComponent<Fish>());
        //    }

        //    cnt = _jeongdongjin.halfbeak.Length;
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        if (!_jeongdongjin.halfbeak[i].activeSelf)
        //            break;

        //        _jeongdongjinSundryList.Add(_jeongdongjin.halfbeak[i].GetComponent<Fish>());
        //    }
        }
        void JeongdongjinNormalFishCheck()
        {
        //    int cnt = _jeongdongjin.bigfinsquid.Length;
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        if (!_jeongdongjin.bigfinsquid[i].activeSelf)
        //            break;

        //        _jeongdongjinNormalList.Add(_jeongdongjin.bigfinsquid[i].GetComponent<Fish>());
        //    }

        //    cnt = _jeongdongjin.commonoctopus.Length;
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        if (!_jeongdongjin.commonoctopus[i].activeSelf)
        //            break;

        //        _jeongdongjinNormalList.Add(_jeongdongjin.commonoctopus[i].GetComponent<Fish>());
        //    }

        //    cnt = _jeongdongjin.oliveflounder.Length;
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        if (!_jeongdongjin.oliveflounder[i].activeSelf)
        //            break;

        //        _jeongdongjinNormalList.Add(_jeongdongjin.oliveflounder[i].GetComponent<Fish>());
        //    }

        //    cnt = _jeongdongjin.darkbandedrockfish.Length;
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        if (!_jeongdongjin.darkbandedrockfish[i].activeSelf)
        //            break;

        //        _jeongdongjinNormalList.Add(_jeongdongjin.darkbandedrockfish[i].GetComponent<Fish>());
        //    }

        //    cnt = _jeongdongjin.koreanrockfish.Length;
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        if (!_jeongdongjin.koreanrockfish[i].activeSelf)
        //            break;

        //        _jeongdongjinNormalList.Add(_jeongdongjin.koreanrockfish[i].GetComponent<Fish>());
        //    }

        //    cnt = _jeongdongjin.goldeyerockfish.Length;
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        if (!_jeongdongjin.goldeyerockfish[i].activeSelf)
        //            break;

        //        _jeongdongjinNormalList.Add(_jeongdongjin.goldeyerockfish[i].GetComponent<Fish>());
        //    }

        //    cnt = _jeongdongjin.spotbellyrockfish.Length;
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        if (!_jeongdongjin.spotbellyrockfish[i].activeSelf)
        //            break;

        //        _jeongdongjinNormalList.Add(_jeongdongjin.spotbellyrockfish[i].GetComponent<Fish>());
        //    }

        //    cnt = _jeongdongjin.whitespottedconger.Length;
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        if (!_jeongdongjin.whitespottedconger[i].activeSelf)
        //            break;

        //        _jeongdongjinNormalList.Add(_jeongdongjin.whitespottedconger[i].GetComponent<Fish>());
        //    }

        //    cnt = _jeongdongjin.indianflathead.Length;
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        if (!_jeongdongjin.indianflathead[i].activeSelf)
        //            break;

        //        _jeongdongjinNormalList.Add(_jeongdongjin.indianflathead[i].GetComponent<Fish>());
        //    }

        //    cnt = _jeongdongjin.bluefingurnard.Length;
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        if (!_jeongdongjin.bluefingurnard[i].activeSelf)
        //            break;

        //        _jeongdongjinNormalList.Add(_jeongdongjin.bluefingurnard[i].GetComponent<Fish>());
        //    }

        //    cnt = _jeongdongjin.sandfish.Length;
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        if (!_jeongdongjin.sandfish[i].activeSelf)
        //            break;

        //        _jeongdongjinNormalList.Add(_jeongdongjin.sandfish[i].GetComponent<Fish>());
        //    }

        //    cnt = _jeongdongjin.okhotskatkamackerel.Length;
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        if (!_jeongdongjin.okhotskatkamackerel[i].activeSelf)
        //            break;

        //        _jeongdongjinNormalList.Add(_jeongdongjin.okhotskatkamackerel[i].GetComponent<Fish>());
        //    }

        //    cnt = _jeongdongjin.bigscaledredfin.Length;
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        if (!_jeongdongjin.bigscaledredfin[i].activeSelf)
        //            break;

        //        _jeongdongjinNormalList.Add(_jeongdongjin.bigscaledredfin[i].GetComponent<Fish>());
        //    }

        //    cnt = _jeongdongjin.flatheadgreymullet.Length;
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        if (!_jeongdongjin.flatheadgreymullet[i].activeSelf)
        //            break;

        //        _jeongdongjinNormalList.Add(_jeongdongjin.flatheadgreymullet[i].GetComponent<Fish>());
        //    }
        }
        void JeongdongjinRareFishCheck()
        {
        //    int cnt = _jeongdongjin.blackporgy.Length;
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        if (!_jeongdongjin.blackporgy[i].activeSelf)
        //            break;

        //        _jeongdongjinRareList.Add(_jeongdongjin.blackporgy[i].GetComponent<Fish>());
        //    }

        //    cnt = _jeongdongjin.largescaleblackfish.Length;
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        if (!_jeongdongjin.largescaleblackfish[i].activeSelf)
        //            break;

        //        _jeongdongjinRareList.Add(_jeongdongjin.largescaleblackfish[i].GetComponent<Fish>());
        //    }

        //    cnt = _jeongdongjin.japaneseamberjack.Length;
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        if (!_jeongdongjin.japaneseamberjack[i].activeSelf)
        //            break;

        //        _jeongdongjinRareList.Add(_jeongdongjin.japaneseamberjack[i].GetComponent<Fish>());
        //    }

        //    cnt = _jeongdongjin.japanesespanishmackerel.Length;
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        if (!_jeongdongjin.japanesespanishmackerel[i].activeSelf)
        //            break;

        //        _jeongdongjinRareList.Add(_jeongdongjin.japanesespanishmackerel[i].GetComponent<Fish>());
        //    }

        //    cnt = _jeongdongjin.seabass.Length;
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        if (!_jeongdongjin.seabass[i].activeSelf)
        //            break;

        //        _jeongdongjinRareList.Add(_jeongdongjin.seabass[i].GetComponent<Fish>());
        //    }

        //    cnt = _jeongdongjin.spottedseabass.Length;
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        if (!_jeongdongjin.spottedseabass[i].activeSelf)
        //            break;

        //        _jeongdongjinRareList.Add(_jeongdongjin.spottedseabass[i].GetComponent<Fish>());
        //    }
    }
    void JeongdongjinFishListSetting(int randomCount)
    {
        switch (_userData._grade)
        {
            case 1:
                for (int i = 0; i < randomCount; i++)
                {
                    int randomIndex;
                    int randomPercent = Random.Range(1, 100);

                    if (randomPercent > 29)
                    {
                        randomIndex = Random.Range(0, _jeongdongjinSundryList.Count);
                        _jeongdongjinRandomFishList.Add(_jeongdongjinSundryList[randomIndex]);
                    }
                    else if (randomPercent > 1 || _jeongdongjinRareList.Count <= 0)
                    {
                        randomIndex = Random.Range(0, _jeongdongjinNormalList.Count);
                        _jeongdongjinRandomFishList.Add(_jeongdongjinNormalList[randomIndex]);
                    }
                    else
                    {
                        randomIndex = Random.Range(0, _jeongdongjinRareList.Count);
                        _jeongdongjinRandomFishList.Add(_jeongdongjinRareList[randomIndex]);
                    }
                }
                break;
            case 2:
                for (int i = 0; i < randomCount; i++)
                {
                    int randomIndex;
                    int randomPercent = Random.Range(1, 100);

                    if (randomPercent > 35)
                    {
                        randomIndex = Random.Range(0, _jeongdongjinSundryList.Count);
                        _jeongdongjinRandomFishList.Add(_jeongdongjinSundryList[randomIndex]);
                    }
                    else if (randomPercent > 2 || _jeongdongjinRareList.Count <= 0)
                    {
                        randomIndex = Random.Range(0, _jeongdongjinNormalList.Count);
                        _jeongdongjinRandomFishList.Add(_jeongdongjinNormalList[randomIndex]);
                    }
                    else
                    {
                        randomIndex = Random.Range(0, _jeongdongjinRareList.Count);
                        _jeongdongjinRandomFishList.Add(_jeongdongjinRareList[randomIndex]);
                    }
                }
                break;
            case 3:
                for (int i = 0; i < randomCount; i++)
                {
                    int randomIndex;
                    int randomPercent = Random.Range(1, 100);

                    if (randomPercent > 40)
                    {
                        randomIndex = Random.Range(0, _jeongdongjinSundryList.Count);
                        _jeongdongjinRandomFishList.Add(_jeongdongjinSundryList[randomIndex]);
                    }
                    else if (randomPercent > 3 || _jeongdongjinRareList.Count <= 0)
                    {
                        randomIndex = Random.Range(0, _jeongdongjinNormalList.Count);
                        _jeongdongjinRandomFishList.Add(_jeongdongjinNormalList[randomIndex]);
                    }
                    else
                    {
                        randomIndex = Random.Range(0, _jeongdongjinRareList.Count);
                        _jeongdongjinRandomFishList.Add(_jeongdongjinRareList[randomIndex]);
                    }
                }
                break;
            case 4:
                for (int i = 0; i < randomCount; i++)
                {
                    int randomIndex;
                    int randomPercent = Random.Range(1, 100);

                    if (randomPercent > 45)
                    {
                        randomIndex = Random.Range(0, _jeongdongjinSundryList.Count);
                        _jeongdongjinRandomFishList.Add(_jeongdongjinSundryList[randomIndex]);
                    }
                    else if (randomPercent > 4 || _jeongdongjinRareList.Count <= 0)
                    {
                        randomIndex = Random.Range(0, _jeongdongjinNormalList.Count);
                        _jeongdongjinRandomFishList.Add(_jeongdongjinNormalList[randomIndex]);
                    }
                    else
                    {
                        randomIndex = Random.Range(0, _jeongdongjinRareList.Count);
                        _jeongdongjinRandomFishList.Add(_jeongdongjinRareList[randomIndex]);
                    }
                }
                break;
            case 5:
                for (int i = 0; i < randomCount; i++)
                {
                    int randomIndex;
                    int randomPercent = Random.Range(1, 100);

                    if (randomPercent > 50)
                    {
                        randomIndex = Random.Range(0, _jeongdongjinSundryList.Count);
                        _jeongdongjinRandomFishList.Add(_jeongdongjinSundryList[randomIndex]);
                    }
                    else if (randomPercent > 5 || _jeongdongjinRareList.Count <= 0)
                    {
                        randomIndex = Random.Range(0, _jeongdongjinNormalList.Count);
                        _jeongdongjinRandomFishList.Add(_jeongdongjinNormalList[randomIndex]);
                    }
                    else
                    {
                        randomIndex = Random.Range(0, _jeongdongjinRareList.Count);
                        _jeongdongjinRandomFishList.Add(_jeongdongjinRareList[randomIndex]);
                    }
                }
                break;
            case 6:
                for (int i = 0; i < randomCount; i++)
                {
                    int randomIndex;
                    int randomPercent = Random.Range(1, 100);

                    if (randomPercent > 55)
                    {
                        randomIndex = Random.Range(0, _jeongdongjinSundryList.Count);
                        _jeongdongjinRandomFishList.Add(_jeongdongjinSundryList[randomIndex]);
                    }
                    else if (randomPercent > 6 || _jeongdongjinRareList.Count <= 0)
                    {
                        randomIndex = Random.Range(0, _jeongdongjinNormalList.Count);
                        _jeongdongjinRandomFishList.Add(_jeongdongjinNormalList[randomIndex]);
                    }
                    else
                    {
                        randomIndex = Random.Range(0, _jeongdongjinRareList.Count);
                        _jeongdongjinRandomFishList.Add(_jeongdongjinRareList[randomIndex]);
                    }
                }
                break;
            case 7:
                for (int i = 0; i < randomCount; i++)
                {
                    int randomIndex;
                    int randomPercent = Random.Range(1, 100);

                    if (randomPercent > 60)
                    {
                        randomIndex = Random.Range(0, _jeongdongjinSundryList.Count);
                        _jeongdongjinRandomFishList.Add(_jeongdongjinSundryList[randomIndex]);
                    }
                    else if (randomPercent > 7 || _jeongdongjinRareList.Count <= 0)
                    {
                        randomIndex = Random.Range(0, _jeongdongjinNormalList.Count);
                        _jeongdongjinRandomFishList.Add(_jeongdongjinNormalList[randomIndex]);
                    }
                    else
                    {
                        randomIndex = Random.Range(0, _jeongdongjinRareList.Count);
                        _jeongdongjinRandomFishList.Add(_jeongdongjinRareList[randomIndex]);
                    }
                }
                break;
            case 8:
                for (int i = 0; i < randomCount; i++)
                {
                    int randomIndex;
                    int randomPercent = Random.Range(1, 100);

                    if (randomPercent > 65)
                    {
                        randomIndex = Random.Range(0, _jeongdongjinSundryList.Count);
                        _jeongdongjinRandomFishList.Add(_jeongdongjinSundryList[randomIndex]);
                    }
                    else if (randomPercent > 8 || _jeongdongjinRareList.Count <= 0)
                    {
                        randomIndex = Random.Range(0, _jeongdongjinNormalList.Count);
                        _jeongdongjinRandomFishList.Add(_jeongdongjinNormalList[randomIndex]);
                    }
                    else
                    {
                        randomIndex = Random.Range(0, _jeongdongjinRareList.Count);
                        _jeongdongjinRandomFishList.Add(_jeongdongjinRareList[randomIndex]);
                    }
                }
                break;
            case 9:
                for (int i = 0; i < randomCount; i++)
                {
                    int randomIndex;
                    int randomPercent = Random.Range(1, 100);

                    if (randomPercent > 70)
                    {
                        randomIndex = Random.Range(0, _jeongdongjinSundryList.Count);
                        _jeongdongjinRandomFishList.Add(_jeongdongjinSundryList[randomIndex]);
                    }
                    else if (randomPercent > 9 || _jeongdongjinRareList.Count <= 0)
                    {
                        randomIndex = Random.Range(0, _jeongdongjinNormalList.Count);
                        _jeongdongjinRandomFishList.Add(_jeongdongjinNormalList[randomIndex]);
                    }
                    else
                    {
                        randomIndex = Random.Range(0, _jeongdongjinRareList.Count);
                        _jeongdongjinRandomFishList.Add(_jeongdongjinRareList[randomIndex]);
                    }
                }
                break;
        }

        //for (int i = 0; i < _jeongdongjinRandomFishList.Count; i++)
        //{
        //    _jeongdongjinRandomFishList[i].fishData.lenth = (float)System.Math.Round(_jeongdongjinRandomFishList[i].fishData.lenth * 10000 * 0.01f, 2);
        //}
    }
    IEnumerator JeongdongjinFishBotCoroutine()
    {
        int randomCount = ReturnRandomCount();

        float delayTime = (totalTime - 20) / randomCount;

        WaitForSeconds delay = new WaitForSeconds(delayTime);

        JeongdongjinFishListSetting(randomCount);

        // test
        //foreach(Fish data in _jeongdongjinRandomFishList)
        //{
        //    Debug.Log(data.fishKoreanName + " , " + data.fishType.ToString());
        //}
        //Debug.Log(_jeongdongjinRareList.Count);
        //Debug.Log(delayTime);

        for (int i = 0; i < _jeongdongjinRandomFishList.Count; i++)
        {
            yield return delay;

            //FishData fish = _jeongdongjinRandomFishList[i].fishData;
            //_matchManager.UpdateOpponentScore(fish.lenth * 1.1f, fish.weight * 1.1f, 1, fish.price, fish.name);
        }

        //yield return new WaitForSeconds(10);
        //FishData fish = _jeongdongjinRandomFishList[0].fishData;
        //_matchManager.UpdateOpponentScore(fish.lenth + (fish.lenth * 0.1f), fish.weight + (fish.weight * 0.1f), 1, fish.price);

        yield return null;
    }

    void SkywaySndryFishCheck()
    {
        int cnt = _skyway.floridapompano.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.floridapompano[i].activeSelf)
                break;

            _skywaySundryList.Add(_skyway.floridapompano[i].GetComponent<FishSkyway>());
        }

        cnt = _skyway.lagoontriggerfish.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.lagoontriggerfish[i].activeSelf)
                break;

            _skywaySundryList.Add(_skyway.lagoontriggerfish[i].GetComponent<FishSkyway>());
        }

        cnt = _skyway.redlionfish.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.redlionfish[i].activeSelf)
                break;

            _skywaySundryList.Add(_skyway.redlionfish[i].GetComponent<FishSkyway>());
        }

        cnt = _skyway.hogfish.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.hogfish[i].activeSelf)
                break;

            _skywaySundryList.Add(_skyway.hogfish[i].GetComponent<FishSkyway>());
        }

        cnt = _skyway.gulfflounder.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.gulfflounder[i].activeSelf)
                break;

            _skywaySundryList.Add(_skyway.gulfflounder[i].GetComponent<FishSkyway>());
        }

        cnt = _skyway.bonefish.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.bonefish[i].activeSelf)
                break;

            _skywaySundryList.Add(_skyway.bonefish[i].GetComponent<FishSkyway>());
        }

        cnt = _skyway.cobia.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.cobia[i].activeSelf)
                break;

            _skywaySundryList.Add(_skyway.cobia[i].GetComponent<FishSkyway>());
        }

        cnt = _skyway.kingmackerel.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.kingmackerel[i].activeSelf)
                break;

            _skywaySundryList.Add(_skyway.kingmackerel[i].GetComponent<FishSkyway>());
        }

        cnt = _skyway.commonsnook.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.commonsnook[i].activeSelf)
                break;

            _skywaySundryList.Add(_skyway.commonsnook[i].GetComponent<FishSkyway>());
        }

        cnt = _skyway.seatrout.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.seatrout[i].activeSelf)
                break;

            _skywaySundryList.Add(_skyway.seatrout[i].GetComponent<FishSkyway>());
        }

        cnt = _skyway.gnomefish.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.gnomefish[i].activeSelf)
                break;

            _skywaySundryList.Add(_skyway.gnomefish[i].GetComponent<FishSkyway>());
        }

        cnt = _skyway.weakfish.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.weakfish[i].activeSelf)
                break;

            _skywaySundryList.Add(_skyway.weakfish[i].GetComponent<FishSkyway>());
        }

        cnt = _skyway.soinymullet.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.soinymullet[i].activeSelf)
                break;

            _skywaySundryList.Add(_skyway.soinymullet[i].GetComponent<FishSkyway>());
        }

        cnt = _skyway.mackerel.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.mackerel[i].activeSelf)
                break;

            _skywaySundryList.Add(_skyway.mackerel[i].GetComponent<FishSkyway>());
        }

    }
    void SkywayNormalFishCheck()
    {
        int cnt = _skyway.redsnapper.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.redsnapper[i].activeSelf)
                break;

            _skywayNormalList.Add(_skyway.redsnapper[i].GetComponent<FishSkyway>());
        }

        cnt = _skyway.atlanticwreckfish.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.atlanticwreckfish[i].activeSelf)
                break;

            _skywayNormalList.Add(_skyway.atlanticwreckfish[i].GetComponent<FishSkyway>());
        }

        cnt = _skyway.japaneseamberjack.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.japaneseamberjack[i].activeSelf)
                break;

            _skywayNormalList.Add(_skyway.japaneseamberjack[i].GetComponent<FishSkyway>());
        }

        cnt = _skyway.wahoo.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.wahoo[i].activeSelf)
                break;

            _skywayNormalList.Add(_skyway.wahoo[i].GetComponent<FishSkyway>());
        }

        cnt = _skyway.halibut.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.halibut[i].activeSelf)
                break;

            _skywayNormalList.Add(_skyway.halibut[i].GetComponent<FishSkyway>());
        }

        cnt = _skyway.mahimahi.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.mahimahi[i].activeSelf)
                break;

            _skywayNormalList.Add(_skyway.mahimahi[i].GetComponent<FishSkyway>());
        }

        cnt = _skyway.sheepshead.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.sheepshead[i].activeSelf)
                break;

            _skywayNormalList.Add(_skyway.sheepshead[i].GetComponent<FishSkyway>());
        }

        cnt = _skyway.blackporgy.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.blackporgy[i].activeSelf)
                break;

            _skywayNormalList.Add(_skyway.blackporgy[i].GetComponent<FishSkyway>());
        }

        cnt = _skyway.atlanticspanishmackerel.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.atlanticspanishmackerel[i].activeSelf)
                break;

            _skywayNormalList.Add(_skyway.atlanticspanishmackerel[i].GetComponent<FishSkyway>());
        }

        cnt = _skyway.blackdrum.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.blackdrum[i].activeSelf)
                break;

            _skywayNormalList.Add(_skyway.blackdrum[i].GetComponent<FishSkyway>());
        }

        cnt = _skyway.redstingray.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.redstingray[i].activeSelf)
                break;

            _skywayNormalList.Add(_skyway.redstingray[i].GetComponent<FishSkyway>());
        }

        cnt = _skyway.atlanticcod.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.atlanticcod[i].activeSelf)
                break;

            _skywayNormalList.Add(_skyway.atlanticcod[i].GetComponent<FishSkyway>());
        }

        cnt = _skyway.stripedbass.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.stripedbass[i].activeSelf)
                break;

            _skywayNormalList.Add(_skyway.stripedbass[i].GetComponent<FishSkyway>());
        }

        cnt = _skyway.seabass.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.seabass[i].activeSelf)
                break;

            _skywayNormalList.Add(_skyway.seabass[i].GetComponent<FishSkyway>());
        }

        cnt = _skyway.yellowtailamberjack.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.yellowtailamberjack[i].activeSelf)
                break;

            _skywayNormalList.Add(_skyway.yellowtailamberjack[i].GetComponent<FishSkyway>());
        }

    }
    void SkywayRareFishCheck()
    {
        int cnt = _skyway.blackgrouper.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.blackgrouper[i].activeSelf)
                break;

            _skywayRareList.Add(_skyway.blackgrouper[i].GetComponent<FishSkyway>());
        }

        cnt = _skyway.atlantictripletail.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.atlantictripletail[i].activeSelf)
                break;

            _skywayRareList.Add(_skyway.atlantictripletail[i].GetComponent<FishSkyway>());
        }

        cnt = _skyway.redporgy.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.redporgy[i].activeSelf)
                break;

            _skywayRareList.Add(_skyway.redporgy[i].GetComponent<FishSkyway>());
        }

        cnt = _skyway.redtilefish.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.redtilefish[i].activeSelf)
                break;

            _skywayRareList.Add(_skyway.redtilefish[i].GetComponent<FishSkyway>());
        }

        cnt = _skyway.indopacificsailfish.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.indopacificsailfish[i].activeSelf)
                break;

            _skywayRareList.Add(_skyway.indopacificsailfish[i].GetComponent<FishSkyway>());
        }

        cnt = _skyway.swordfish.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_skyway.swordfish[i].activeSelf)
                break;

            _skywayRareList.Add(_skyway.swordfish[i].GetComponent<FishSkyway>());
        }
    }
    void SkywayFishListSetting(int randomCount)
    {
        switch (_userData._grade)
        {
            case 1:
                for (int i = 0; i < randomCount; i++)
                {
                    int randomIndex;
                    int randomPercent = Random.Range(1, 100);

                    if (randomPercent > 29)
                    {
                        randomIndex = Random.Range(0, _skywaySundryList.Count);
                        _skywayRandomFishList.Add(_skywaySundryList[randomIndex]);
                    }
                    else if (randomPercent > 1 || _skywayRareList.Count <= 0)
                    {
                        randomIndex = Random.Range(0, _skywayNormalList.Count);
                        _skywayRandomFishList.Add(_skywayNormalList[randomIndex]);
                    }
                    else
                    {
                        randomIndex = Random.Range(0, _skywayRareList.Count);
                        _skywayRandomFishList.Add(_skywayRareList[randomIndex]);
                    }
                }
                break;
            case 2:
                for (int i = 0; i < randomCount; i++)
                {
                    int randomIndex;
                    int randomPercent = Random.Range(1, 100);

                    if (randomPercent > 35)
                    {
                        randomIndex = Random.Range(0, _skywaySundryList.Count);
                        _skywayRandomFishList.Add(_skywaySundryList[randomIndex]);
                    }
                    else if (randomPercent > 2 || _skywayRareList.Count <= 0)
                    {
                        randomIndex = Random.Range(0, _skywayNormalList.Count);
                        _skywayRandomFishList.Add(_skywayNormalList[randomIndex]);
                    }
                    else
                    {
                        randomIndex = Random.Range(0, _skywayRareList.Count);
                        _skywayRandomFishList.Add(_skywayRareList[randomIndex]);
                    }
                }
                break;
            case 3:
                for (int i = 0; i < randomCount; i++)
                {
                    int randomIndex;
                    int randomPercent = Random.Range(1, 100);

                    if (randomPercent > 40)
                    {
                        randomIndex = Random.Range(0, _skywaySundryList.Count);
                        _skywayRandomFishList.Add(_skywaySundryList[randomIndex]);
                    }
                    else if (randomPercent > 3 || _skywayRareList.Count <= 0)
                    {
                        randomIndex = Random.Range(0, _skywayNormalList.Count);
                        _skywayRandomFishList.Add(_skywayNormalList[randomIndex]);
                    }
                    else
                    {
                        randomIndex = Random.Range(0, _skywayRareList.Count);
                        _skywayRandomFishList.Add(_skywayRareList[randomIndex]);
                    }
                }
                break;
            case 4:
                for (int i = 0; i < randomCount; i++)
                {
                    int randomIndex;
                    int randomPercent = Random.Range(1, 100);

                    if (randomPercent > 45)
                    {
                        randomIndex = Random.Range(0, _skywaySundryList.Count);
                        _skywayRandomFishList.Add(_skywaySundryList[randomIndex]);
                    }
                    else if (randomPercent > 4 || _skywayRareList.Count <= 0)
                    {
                        randomIndex = Random.Range(0, _skywayNormalList.Count);
                        _skywayRandomFishList.Add(_skywayNormalList[randomIndex]);
                    }
                    else
                    {
                        randomIndex = Random.Range(0, _skywayRareList.Count);
                        _skywayRandomFishList.Add(_skywayRareList[randomIndex]);
                    }
                }
                break;
            case 5:
                for (int i = 0; i < randomCount; i++)
                {
                    int randomIndex;
                    int randomPercent = Random.Range(1, 100);

                    if (randomPercent > 50)
                    {
                        randomIndex = Random.Range(0, _skywaySundryList.Count);
                        _skywayRandomFishList.Add(_skywaySundryList[randomIndex]);
                    }
                    else if (randomPercent > 5 || _skywayRareList.Count <= 0)
                    {
                        randomIndex = Random.Range(0, _skywayNormalList.Count);
                        _skywayRandomFishList.Add(_skywayNormalList[randomIndex]);
                    }
                    else
                    {
                        randomIndex = Random.Range(0, _skywayRareList.Count);
                        _skywayRandomFishList.Add(_skywayRareList[randomIndex]);
                    }
                }
                break;
            case 6:
                for (int i = 0; i < randomCount; i++)
                {
                    int randomIndex;
                    int randomPercent = Random.Range(1, 100);

                    if (randomPercent > 55)
                    {
                        randomIndex = Random.Range(0, _skywaySundryList.Count);
                        _skywayRandomFishList.Add(_skywaySundryList[randomIndex]);
                    }
                    else if (randomPercent > 6 || _skywayRareList.Count <= 0)
                    {
                        randomIndex = Random.Range(0, _skywayNormalList.Count);
                        _skywayRandomFishList.Add(_skywayNormalList[randomIndex]);
                    }
                    else
                    {
                        randomIndex = Random.Range(0, _skywayRareList.Count);
                        _skywayRandomFishList.Add(_skywayRareList[randomIndex]);
                    }
                }
                break;
            case 7:
                for (int i = 0; i < randomCount; i++)
                {
                    int randomIndex;
                    int randomPercent = Random.Range(1, 100);

                    if (randomPercent > 60)
                    {
                        randomIndex = Random.Range(0, _skywaySundryList.Count);
                        _skywayRandomFishList.Add(_skywaySundryList[randomIndex]);
                    }
                    else if (randomPercent > 7 || _skywayRareList.Count <= 0)
                    {
                        randomIndex = Random.Range(0, _skywayNormalList.Count);
                        _skywayRandomFishList.Add(_skywayNormalList[randomIndex]);
                    }
                    else
                    {
                        randomIndex = Random.Range(0, _skywayRareList.Count);
                        _skywayRandomFishList.Add(_skywayRareList[randomIndex]);
                    }
                }
                break;
            case 8:
                for (int i = 0; i < randomCount; i++)
                {
                    int randomIndex;
                    int randomPercent = Random.Range(1, 100);

                    if (randomPercent > 65)
                    {
                        randomIndex = Random.Range(0, _skywaySundryList.Count);
                        _skywayRandomFishList.Add(_skywaySundryList[randomIndex]);
                    }
                    else if (randomPercent > 8 || _skywayRareList.Count <= 0)
                    {
                        randomIndex = Random.Range(0, _skywayNormalList.Count);
                        _skywayRandomFishList.Add(_skywayNormalList[randomIndex]);
                    }
                    else
                    {
                        randomIndex = Random.Range(0, _skywayRareList.Count);
                        _skywayRandomFishList.Add(_skywayRareList[randomIndex]);
                    }
                }
                break;
            case 9:
                for (int i = 0; i < randomCount; i++)
                {
                    int randomIndex;
                    int randomPercent = Random.Range(1, 100);

                    if (randomPercent > 70)
                    {
                        randomIndex = Random.Range(0, _skywaySundryList.Count);
                        _skywayRandomFishList.Add(_skywaySundryList[randomIndex]);
                    }
                    else if (randomPercent > 9 || _skywayRareList.Count <= 0)
                    {
                        randomIndex = Random.Range(0, _skywayNormalList.Count);
                        _skywayRandomFishList.Add(_skywayNormalList[randomIndex]);
                    }
                    else
                    {
                        randomIndex = Random.Range(0, _skywayRareList.Count);
                        _skywayRandomFishList.Add(_skywayRareList[randomIndex]);
                    }
                    //Debug.Log(randomPercent);
                }
                break;
        }
        for (int i = 0; i < _skywayRandomFishList.Count; i++)
        {
            //_skywayRandomFishList[i].fishData.lenth = (float)System.Math.Round(_skywayRandomFishList[i].fishData.lenth * 10000 * 0.01f, 2);
        }
    }
    IEnumerator SkywayFishBotCoroutine()
    {
        int randomCount = ReturnRandomCount();

        float delayTime = (totalTime - 20) / randomCount;

        WaitForSeconds delay = new WaitForSeconds(delayTime);

        SkywayFishListSetting(randomCount);

        // test
        //foreach (FishSkyway data in _skywayRandomFishList)
        //{
        //    Debug.Log(data.fishKoreanName + " , " + data.fishType.ToString());
        //}
        //Debug.Log(_skywayRareList.Count);
        //Debug.Log(delayTime);

        for (int i = 0; i < _skywayRandomFishList.Count; i++)
        {
            yield return delay;

            //FishData fish = _skywayRandomFishList[i].fishData;
           // _matchManager.UpdateOpponentScore(fish.lenth * 1.1f, fish.weight * 1.1f, 1, fish.price, fish.name);
            //Debug.Log(fish.name);
        }

        //yield return new WaitForSeconds(10);
        //FishData fish = _skywayRandomFishList[0].fishData;
        //_matchManager.UpdateOpponentScore(fish.lenth + (fish.lenth * 0.1f), fish.weight + (fish.weight * 0.1f), 1, fish.price);

        yield return null;
    }

    void HomerspitSundryFishCheck()
    {
        int cnt = _homerspit.blackfinflounder.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_homerspit.blackfinflounder[i].activeSelf)
                break;

            _homerspitSundryList.Add(_homerspit.blackfinflounder[i].GetComponent<FishHomerspit>());
        }

        cnt = _homerspit.rocksole.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_homerspit.rocksole[i].activeSelf)
                break;

            _homerspitSundryList.Add(_homerspit.rocksole[i].GetComponent<FishHomerspit>());
        }

        cnt = _homerspit.broadbandedthornyhead.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_homerspit.broadbandedthornyhead[i].activeSelf)
                break;

            _homerspitSundryList.Add(_homerspit.broadbandedthornyhead[i].GetComponent<FishHomerspit>());
        }

        cnt = _homerspit.salmonsnailfish.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_homerspit.salmonsnailfish[i].activeSelf)
                break;

            _homerspitSundryList.Add(_homerspit.salmonsnailfish[i].GetComponent<FishHomerspit>());
        }

        cnt = _homerspit.sailfinpoacher.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_homerspit.sailfinpoacher[i].activeSelf)
                break;

            _homerspitSundryList.Add(_homerspit.sailfinpoacher[i].GetComponent<FishHomerspit>());
        }

        cnt = _homerspit.manybandedsole.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_homerspit.manybandedsole[i].activeSelf)
                break;

            _homerspitSundryList.Add(_homerspit.manybandedsole[i].GetComponent<FishHomerspit>());
        }

        cnt = _homerspit.dragonpoacher.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_homerspit.dragonpoacher[i].activeSelf)
                break;

            _homerspitSundryList.Add(_homerspit.dragonpoacher[i].GetComponent<FishHomerspit>());
        }

        cnt = _homerspit.redtippedgrouper.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_homerspit.redtippedgrouper[i].activeSelf)
                break;

            _homerspitSundryList.Add(_homerspit.redtippedgrouper[i].GetComponent<FishHomerspit>());
        }

        cnt = _homerspit.pacificsaury.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_homerspit.pacificsaury[i].activeSelf)
                break;

            _homerspitSundryList.Add(_homerspit.pacificsaury[i].GetComponent<FishHomerspit>());
        }

        cnt = _homerspit.gnomefish.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_homerspit.gnomefish[i].activeSelf)
                break;

            _homerspitSundryList.Add(_homerspit.gnomefish[i].GetComponent<FishHomerspit>());
        }

        cnt = _homerspit.spotlinedsardine.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_homerspit.spotlinedsardine[i].activeSelf)
                break;

            _homerspitSundryList.Add(_homerspit.spotlinedsardine[i].GetComponent<FishHomerspit>());
        }
    }
    void HomerspitNormalFishCheck()
    {
        int cnt = _homerspit.kamchatkaflounder.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_homerspit.kamchatkaflounder[i].activeSelf)
                break;

            _homerspitNormalList.Add(_homerspit.kamchatkaflounder[i].GetComponent<FishHomerspit>());
        }

        cnt = _homerspit.flatheadsole.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_homerspit.flatheadsole[i].activeSelf)
                break;

            _homerspitNormalList.Add(_homerspit.flatheadsole[i].GetComponent<FishHomerspit>());
        }

        cnt = _homerspit.pacificoceanperch.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_homerspit.pacificoceanperch[i].activeSelf)
                break;

            _homerspitNormalList.Add(_homerspit.pacificoceanperch[i].GetComponent<FishHomerspit>());
        }

        cnt = _homerspit.beringwolffish.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_homerspit.beringwolffish[i].activeSelf)
                break;

            _homerspitNormalList.Add(_homerspit.beringwolffish[i].GetComponent<FishHomerspit>());
        }

        cnt = _homerspit.beringwolffish.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_homerspit.beringwolffish[i].activeSelf)
                break;

            _homerspitNormalList.Add(_homerspit.beringwolffish[i].GetComponent<FishHomerspit>());
        }

        cnt = _homerspit.lavenderjobfish.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_homerspit.lavenderjobfish[i].activeSelf)
                break;

            _homerspitNormalList.Add(_homerspit.lavenderjobfish[i].GetComponent<FishHomerspit>());
        }

        cnt = _homerspit.splendidalfonsino.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_homerspit.splendidalfonsino[i].activeSelf)
                break;

            _homerspitNormalList.Add(_homerspit.splendidalfonsino[i].GetComponent<FishHomerspit>());
        }

        cnt = _homerspit.alaskapollack.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_homerspit.alaskapollack[i].activeSelf)
                break;

            _homerspitNormalList.Add(_homerspit.alaskapollack[i].GetComponent<FishHomerspit>());
        }

        cnt = _homerspit.pacificcod.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_homerspit.pacificcod[i].activeSelf)
                break;

            _homerspitNormalList.Add(_homerspit.pacificcod[i].GetComponent<FishHomerspit>());
        }

        cnt = _homerspit.sablefish.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_homerspit.sablefish[i].activeSelf)
                break;

            _homerspitNormalList.Add(_homerspit.sablefish[i].GetComponent<FishHomerspit>());
        }

        cnt = _homerspit.yellowfinsole.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_homerspit.yellowfinsole[i].activeSelf)
                break;

            _homerspitNormalList.Add(_homerspit.yellowfinsole[i].GetComponent<FishHomerspit>());
        }

        cnt = _homerspit.bluefingurnard.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_homerspit.bluefingurnard[i].activeSelf)
                break;

            _homerspitNormalList.Add(_homerspit.bluefingurnard[i].GetComponent<FishHomerspit>());
        }

        cnt = _homerspit.chumsalmon.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_homerspit.chumsalmon[i].activeSelf)
                break;

            _homerspitNormalList.Add(_homerspit.chumsalmon[i].GetComponent<FishHomerspit>());
        }

        cnt = _homerspit.japanesepufferfish.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_homerspit.japanesepufferfish[i].activeSelf)
                break;

            _homerspitNormalList.Add(_homerspit.japanesepufferfish[i].GetComponent<FishHomerspit>());
        }

    }
    void HomerspitRareFishCheck()
    {
        int cnt = _homerspit.bigskate.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_homerspit.bigskate[i].activeSelf)
                break;

            _homerspitRareList.Add(_homerspit.bigskate[i].GetComponent<FishHomerspit>());
        }

        cnt = _homerspit.yellowfintuna.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_homerspit.yellowfintuna[i].activeSelf)
                break;

            _homerspitRareList.Add(_homerspit.yellowfintuna[i].GetComponent<FishHomerspit>());
        }

        cnt = _homerspit.lingcod.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_homerspit.lingcod[i].activeSelf)
                break;

            _homerspitRareList.Add(_homerspit.lingcod[i].GetComponent<FishHomerspit>());
        }

        cnt = _homerspit.yelloweyerockfish.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_homerspit.yelloweyerockfish[i].activeSelf)
                break;

            _homerspitRareList.Add(_homerspit.yelloweyerockfish[i].GetComponent<FishHomerspit>());
        }

        cnt = _homerspit.quillbackrockfish.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_homerspit.quillbackrockfish[i].activeSelf)
                break;

            _homerspitRareList.Add(_homerspit.quillbackrockfish[i].GetComponent<FishHomerspit>());
        }

        cnt = _homerspit.salmonshark.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_homerspit.salmonshark[i].activeSelf)
                break;

            _homerspitRareList.Add(_homerspit.salmonshark[i].GetComponent<FishHomerspit>());
        }

        cnt = _homerspit.halibut.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_homerspit.halibut[i].activeSelf)
                break;

            _homerspitRareList.Add(_homerspit.halibut[i].GetComponent<FishHomerspit>());
        }

        cnt = _homerspit.chinooksalmon.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (!_homerspit.chinooksalmon[i].activeSelf)
                break;

            _homerspitRareList.Add(_homerspit.chinooksalmon[i].GetComponent<FishHomerspit>());
        }

    }
    void HomerspitFishListSetting(int randomCount)
    {
        switch (_userData._grade)
        {
            case 1:
                for (int i = 0; i < randomCount; i++)
                {
                    int randomIndex;
                    int randomPercent = Random.Range(1, 100);

                    if (randomPercent > 29)
                    {
                        randomIndex = Random.Range(0, _homerspitSundryList.Count);
                        _homerspitRandomFishList.Add(_homerspitSundryList[randomIndex]);
                    }
                    else if (randomPercent > 1 || _homerspitRareList.Count <= 0)
                    {
                        randomIndex = Random.Range(0, _homerspitNormalList.Count);
                        _homerspitRandomFishList.Add(_homerspitNormalList[randomIndex]);
                    }
                    else
                    {
                        randomIndex = Random.Range(0, _homerspitRareList.Count);
                        _homerspitRandomFishList.Add(_homerspitRareList[randomIndex]);
                    }
                }
                break;
            case 2:
                for (int i = 0; i < randomCount; i++)
                {
                    int randomIndex;
                    int randomPercent = Random.Range(1, 100);

                    if (randomPercent > 35)
                    {
                        randomIndex = Random.Range(0, _homerspitSundryList.Count);
                        _homerspitRandomFishList.Add(_homerspitSundryList[randomIndex]);
                    }
                    else if (randomPercent > 2 || _homerspitRareList.Count <= 0)
                    {
                        randomIndex = Random.Range(0, _homerspitNormalList.Count);
                        _homerspitRandomFishList.Add(_homerspitNormalList[randomIndex]);
                    }
                    else
                    {
                        randomIndex = Random.Range(0, _homerspitRareList.Count);
                        _homerspitRandomFishList.Add(_homerspitRareList[randomIndex]);
                    }
                }
                break;
            case 3:
                for (int i = 0; i < randomCount; i++)
                {
                    int randomIndex;
                    int randomPercent = Random.Range(1, 100);

                    if (randomPercent > 40)
                    {
                        randomIndex = Random.Range(0, _homerspitSundryList.Count);
                        _homerspitRandomFishList.Add(_homerspitSundryList[randomIndex]);
                    }
                    else if (randomPercent > 3 || _homerspitRareList.Count <= 0)
                    {
                        randomIndex = Random.Range(0, _homerspitNormalList.Count);
                        _homerspitRandomFishList.Add(_homerspitNormalList[randomIndex]);
                    }
                    else
                    {
                        randomIndex = Random.Range(0, _homerspitRareList.Count);
                        _homerspitRandomFishList.Add(_homerspitRareList[randomIndex]);
                    }
                }
                break;
            case 4:
                for (int i = 0; i < randomCount; i++)
                {
                    int randomIndex;
                    int randomPercent = Random.Range(1, 100);

                    if (randomPercent > 45)
                    {
                        randomIndex = Random.Range(0, _homerspitSundryList.Count);
                        _homerspitRandomFishList.Add(_homerspitSundryList[randomIndex]);
                    }
                    else if (randomPercent > 4 || _homerspitRareList.Count <= 0)
                    {
                        randomIndex = Random.Range(0, _homerspitNormalList.Count);
                        _homerspitRandomFishList.Add(_homerspitNormalList[randomIndex]);
                    }
                    else
                    {
                        randomIndex = Random.Range(0, _homerspitRareList.Count);
                        _homerspitRandomFishList.Add(_homerspitRareList[randomIndex]);
                    }
                }
                break;
            case 5:
                for (int i = 0; i < randomCount; i++)
                {
                    int randomIndex;
                    int randomPercent = Random.Range(1, 100);

                    if (randomPercent > 50)
                    {
                        randomIndex = Random.Range(0, _homerspitSundryList.Count);
                        _homerspitRandomFishList.Add(_homerspitSundryList[randomIndex]);
                    }
                    else if (randomPercent > 5 || _homerspitRareList.Count <= 0)
                    {
                        randomIndex = Random.Range(0, _homerspitNormalList.Count);
                        _homerspitRandomFishList.Add(_homerspitNormalList[randomIndex]);
                    }
                    else
                    {
                        randomIndex = Random.Range(0, _homerspitRareList.Count);
                        _homerspitRandomFishList.Add(_homerspitRareList[randomIndex]);
                    }
                }
                break;
            case 6:
                for (int i = 0; i < randomCount; i++)
                {
                    int randomIndex;
                    int randomPercent = Random.Range(1, 100);

                    if (randomPercent > 55)
                    {
                        randomIndex = Random.Range(0, _homerspitSundryList.Count);
                        _homerspitRandomFishList.Add(_homerspitSundryList[randomIndex]);
                    }
                    else if (randomPercent > 6 || _homerspitRareList.Count <= 0)
                    {
                        randomIndex = Random.Range(0, _homerspitNormalList.Count);
                        _homerspitRandomFishList.Add(_homerspitNormalList[randomIndex]);
                    }
                    else
                    {
                        randomIndex = Random.Range(0, _homerspitRareList.Count);
                        _homerspitRandomFishList.Add(_homerspitRareList[randomIndex]);
                    }
                }
                break;
            case 7:
                for (int i = 0; i < randomCount; i++)
                {
                    int randomIndex;
                    int randomPercent = Random.Range(1, 100);

                    if (randomPercent > 60)
                    {
                        randomIndex = Random.Range(0, _homerspitSundryList.Count);
                        _homerspitRandomFishList.Add(_homerspitSundryList[randomIndex]);
                    }
                    else if (randomPercent > 7 || _homerspitRareList.Count <= 0)
                    {
                        randomIndex = Random.Range(0, _homerspitNormalList.Count);
                        _homerspitRandomFishList.Add(_homerspitNormalList[randomIndex]);
                    }
                    else
                    {
                        randomIndex = Random.Range(0, _homerspitRareList.Count);
                        _homerspitRandomFishList.Add(_homerspitRareList[randomIndex]);
                    }
                }
                break;
            case 8:
                for (int i = 0; i < randomCount; i++)
                {
                    int randomIndex;
                    int randomPercent = Random.Range(1, 100);

                    if (randomPercent > 65)
                    {
                        randomIndex = Random.Range(0, _homerspitSundryList.Count);
                        _homerspitRandomFishList.Add(_homerspitSundryList[randomIndex]);
                    }
                    else if (randomPercent > 8 || _homerspitRareList.Count <= 0)
                    {
                        randomIndex = Random.Range(0, _homerspitNormalList.Count);
                        _homerspitRandomFishList.Add(_homerspitNormalList[randomIndex]);
                    }
                    else
                    {
                        randomIndex = Random.Range(0, _homerspitRareList.Count);
                        _homerspitRandomFishList.Add(_homerspitRareList[randomIndex]);
                    }
                }
                break;
            case 9:
                for (int i = 0; i < randomCount; i++)
                {
                    int randomIndex;
                    int randomPercent = Random.Range(1, 100);

                    if (randomPercent > 70)
                    {
                        randomIndex = Random.Range(0, _homerspitSundryList.Count);
                        _homerspitRandomFishList.Add(_homerspitSundryList[randomIndex]);
                    }
                    else if (randomPercent > 9 || _homerspitRareList.Count <= 0)
                    {
                        randomIndex = Random.Range(0, _homerspitNormalList.Count);
                        _homerspitRandomFishList.Add(_homerspitNormalList[randomIndex]);
                    }
                    else
                    {
                        randomIndex = Random.Range(0, _homerspitRareList.Count);
                        _homerspitRandomFishList.Add(_homerspitRareList[randomIndex]);
                    }
                    //Debug.Log(randomPercent);
                }
                break;
        }
        for (int i = 0; i < _homerspitRandomFishList.Count; i++)
        {
            //_homerspitRandomFishList[i].fishData.lenth = (float)System.Math.Round(_homerspitRandomFishList[i].fishData.lenth * 10000 * 0.01f, 2);
        }
    }
    IEnumerator HomerspitFishBotCoroutine()
    {
        int randomCount = ReturnRandomCount();

        float delayTime =(totalTime - 20) / randomCount;

        WaitForSeconds delay = new WaitForSeconds(delayTime);

        HomerspitFishListSetting(randomCount);

        // test
        //foreach (FishHomerspit data in _homerspitRandomFishList)
        //{
        //    Debug.Log(data.fishKoreanName + " , " + data.fishType.ToString());
        //}
        //Debug.Log(_homerspitRareList.Count);
        //Debug.Log(delayTime);

        for (int i = 0; i < _homerspitRandomFishList.Count; i++)
        {
            yield return delay;

            //FishData fish = _homerspitRandomFishList[i].fishData;
            //_matchManager.UpdateOpponentScore(fish.lenth * 1.1f, fish.weight * 1.1f, 1, fish.price, fish.name);
            //Debug.Log(fish.name);
        }

        //yield return new WaitForSeconds(10);
        //FishData fish = _homerspitRandomFishList[0].fishData;
        //_matchManager.UpdateOpponentScore(fish.lenth + (fish.lenth * 0.1f), fish.weight + (fish.weight * 0.1f), 1, fish.price);

        yield return null;
    }


    int ReturnRandomCount()
    {
        switch(_userData._grade)
        {
            case 1:
                return Random.Range(2, 7);
            case 2:
                return Random.Range(3, 8);
            case 3:
                return Random.Range(4, 9);
            case 4:
                return Random.Range(5, 10);
            case 5:
                return Random.Range(6, 11);
            case 6:
                return Random.Range(7, 12);
            case 7:
                return Random.Range(8, 13);
            case 8:
                return Random.Range(9, 14);
            case 9:
                return Random.Range(10, 15);
            default:;
                return Random.Range(2, 7);
        }
    }
}
