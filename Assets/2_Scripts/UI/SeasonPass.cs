using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[SerializeField]
public class SeasonPassDB
{
    // 현재 달성도
    public object Current { get; set; }
    // 목표 달성도
    public object Required { get; set; }
    // 퀘스트 내용
    public string Description { get; set; }
    public int NormalReward { get; set; }
    public int PremiumReward { get; set; }
    // 완료한 시간
    public long SucessTime { get; set; }
    // 보상 받았는지
    public bool isGetNormalReward { get; set; }
    public bool isGetPremiumReward { get; set; }
    // 완료 했는지
    public bool isSucess { get; set; }    
}
public class SeasonPass : MonoBehaviour
{
    public Button[] freeButton, premiumButton;
    public Text[] questTxt, freeTxt, premiunTxt;

    public void OnOff(bool isOn)
    {
        //SoundManager.instance.EffectPlay("UIClick");
        if(!isOn)
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
        gameObject.SetActive(isOn);
    }

    void LoadContent(Text[] questContentTxt, Text[] freeContentTxt, Text[] premiumContentTxt, Button[] freeBtn, Button[] premiumBtn,SeasonPassDB[] seasonPassDBs,int mapNum)
    {        
        for (int questNum = 0; questNum < questContentTxt.Length; questNum++)
        {
            // 무슨 퀘스트인지 문장 불러오기
            questContentTxt[questNum].text = seasonPassDBs[questNum].Description == null? null: seasonPassDBs[questNum].Description;

            // 무료 보상이 무엇인지 불러오기
            freeContentTxt[questNum].text = seasonPassDBs[questNum].NormalReward == 0 ? 0.ToString() : seasonPassDBs[questNum].NormalReward.ToString();

            // 프리미엄(패스 구매) 보상 무엇인지 불러오기
            premiumContentTxt[questNum].text = seasonPassDBs[questNum].PremiumReward == 0 ? 0.ToString() : seasonPassDBs[questNum].PremiumReward.ToString();

            // (조건) ? 참일때 수행 :(거짓일 때 (조건)? 참일때 : 이것도 거짓일 때))
            // 무료 보상 버튼 제어 시즌패스 각 DB의 성공 여부가 false일때 아예 끄고 성공했는데 보상을 안받았으면 활성화
            freeBtn[questNum].interactable = seasonPassDBs[questNum].isSucess == false ? false : seasonPassDBs[questNum].isGetNormalReward == false ? true : false;
            // 버튼에 있는 온클릭 함수 다 지우기
            freeBtn[questNum].onClick.RemoveAllListeners();
            freeBtn[questNum].onClick.AddListener( ()=> FreeGetReward(mapNum));
            premiumBtn[questNum].onClick.AddListener(() => PremiumGetReward(mapNum));
            // 프리미엄 구매 안했으면 끄고, 구매하고 안깼으면 비활성화 깼고 보상을 안받았으면 활성화
            //premiumBtn[questNum].interactable = userData.isPremium == false ? false : seasonPassDBs[questNum].isSucess == false ? false :
                //seasonPassDBs[questNum].isGetPremiumReward == false ? true : false;        
        }
    }

    void FreeGetReward(int mapNum)
    {
        switch (mapNum)
        {
            case 0:
                break;
            case 1:
                break;
        }
        //LoadContent(questTxt, freeTxt, premiunTxt, freeButton, premiumButton, userData.jeondongjinPassDB, 0);
    }

    void PremiumGetReward(int mapNum)
    {
        switch (mapNum)
        {
            case 0:
                break;
            case 1:
                break;
        }
        //LoadContent(questTxt, freeTxt, premiunTxt, freeButton, premiumButton, userData.jeondongjinPassDB, 0);
    }
}