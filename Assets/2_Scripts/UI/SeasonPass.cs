using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[SerializeField]
public class SeasonPassDB
{
    // ���� �޼���
    public object Current { get; set; }
    // ��ǥ �޼���
    public object Required { get; set; }
    // ����Ʈ ����
    public string Description { get; set; }
    public int NormalReward { get; set; }
    public int PremiumReward { get; set; }
    // �Ϸ��� �ð�
    public long SucessTime { get; set; }
    // ���� �޾Ҵ���
    public bool isGetNormalReward { get; set; }
    public bool isGetPremiumReward { get; set; }
    // �Ϸ� �ߴ���
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
            // ���� ����Ʈ���� ���� �ҷ�����
            questContentTxt[questNum].text = seasonPassDBs[questNum].Description == null? null: seasonPassDBs[questNum].Description;

            // ���� ������ �������� �ҷ�����
            freeContentTxt[questNum].text = seasonPassDBs[questNum].NormalReward == 0 ? 0.ToString() : seasonPassDBs[questNum].NormalReward.ToString();

            // �����̾�(�н� ����) ���� �������� �ҷ�����
            premiumContentTxt[questNum].text = seasonPassDBs[questNum].PremiumReward == 0 ? 0.ToString() : seasonPassDBs[questNum].PremiumReward.ToString();

            // (����) ? ���϶� ���� :(������ �� (����)? ���϶� : �̰͵� ������ ��))
            // ���� ���� ��ư ���� �����н� �� DB�� ���� ���ΰ� false�϶� �ƿ� ���� �����ߴµ� ������ �ȹ޾����� Ȱ��ȭ
            freeBtn[questNum].interactable = seasonPassDBs[questNum].isSucess == false ? false : seasonPassDBs[questNum].isGetNormalReward == false ? true : false;
            // ��ư�� �ִ� ��Ŭ�� �Լ� �� �����
            freeBtn[questNum].onClick.RemoveAllListeners();
            freeBtn[questNum].onClick.AddListener( ()=> FreeGetReward(mapNum));
            premiumBtn[questNum].onClick.AddListener(() => PremiumGetReward(mapNum));
            // �����̾� ���� �������� ����, �����ϰ� �Ȳ����� ��Ȱ��ȭ ���� ������ �ȹ޾����� Ȱ��ȭ
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