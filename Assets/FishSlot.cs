//using CollectFuntion;
//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//public class FishSlot : MonoBehaviour
//{
//    public GameObject checkMark;
//    public Image fishFaceImage;
//    public Slider fishGrowGage;
//    public TextMeshProUGUI nameText;
//    public TextMeshProUGUI costText;
//    public TextMeshProUGUI timeText;
//    public bool isCheck;
//    //public AquariumManager aquariumMgr;
//    public int cost;
//    public long time;
//    public AquariumFish aquariumFish;
//    private void Awake()
//    {
//        aquariumMgr = GameObject.FindGameObjectWithTag("AquariumManager").GetComponent<AquariumManager>();
//    }

//    public void Check()
//    {
//        isCheck = isCheck.Equals(false) ? true : false;
//        checkMark.SetActive(isCheck);

//        if (checkMark.activeSelf)
//            aquariumMgr.sumCost = aquariumMgr.sumCost > 0 ? aquariumMgr.sumCost += aquariumFish.finalPrice : aquariumFish.finalPrice;

//        else
//            aquariumMgr.sumCost = aquariumMgr.sumCost > 0 ? aquariumMgr.sumCost -= aquariumFish.finalPrice : 0;

//        aquariumMgr.fishListPanel.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text = Function.GetThousandCommaText(aquariumMgr.sumCost);
//        //if (isCheck)
//        //    aquariumMgr.fishListPanel.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text =  costText.text;
//    }

//}
