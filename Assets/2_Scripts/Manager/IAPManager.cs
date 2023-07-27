using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour
{
    readonly string _pearl_10 = "com.mobilemovement.realisticfishinggamecasting.pearl10";
    readonly string _pearl_50 = "com.mobilemovement.realisticfishinggamecasting.pearl_50";
    readonly string _pearl_150 = "com.mobilemovement.realisticfishinggamecasting.pearl150";
    readonly string _pearl_300 = "com.mobilemovement.realisticfishinggamecasting.pearl300";
    readonly string _pearl_500 = "com.mobilemovement.realisticfishinggamecasting.pearl500";
    readonly string _jeongdongjinPass = "com.mobilemovement.realisticfishinggamecasting.jeongdongjinpass";
    readonly string _homerspitPass = "com.mobilemovement.realisticfishinggamecasting.homerspitpass";
    readonly string _skywayPass = "com.mobilemovement.realisticfishinggamecasting.skywaypass";
    readonly string _jeongdongjinPassInBook = "com.mobilemovement.realisticfishinggamecasting.jeongdongjinpassinbook";
    readonly string _homerspitPassInBook = "com.mobilemovement.realisticfishinggamecasting.homerspitpassinbook";
    readonly string _skywayPassInBook = "com.mobilemovement.realisticfishinggamecasting.skywaypassinbook";
    readonly string _fourthAquarium = "com.mobilemovement.realisticfishinggamecasting.fourthaquarium";
    readonly string _fifthAquarium = "com.mobilemovement.realisticfishinggamecasting.fifthaquarium";

    readonly string _platinumPackage = "com.mobilemovement.realisticfishinggamecasting.platinumpackage";
    readonly string _diamondPackage = "com.mobilemovement.realisticfishinggamecasting.diamondpackage";
    readonly string _adblockPackage = "com.mobilemovement.realisticfishinggamecasting.adblock";

    [Header("상점")]
    public PassBuyPopup _passBuyPopup;

    [Header("수족관")]
    public AquariumUI _aquariumUI;
    public FishInfoUI _fishInfoUI;
    public GameObject _aquariumBuyPopup;

    [Header("인게임")]
    public FishPopUp _fishPopup;

    [Header("도감")]
    public GameObject _buyPopup;
    public PassBuyUI _passBuyUI;

    UserData _userData;

    public void OnPurchaseComplete(Product product)
    {
        if (_userData == null)
            _userData = DBManager.INSTANCE.GetUserData();

        //Debug.Log(product.definition.id);


        if (product.definition.id.Equals(_pearl_10))
        {
            //Debug.Log("진주 10개 결제 완료");
            UpdatePearl(10);
        }

        if (product.definition.id.Equals(_pearl_50))
        {
           // Debug.Log("진주 50개 결제 완료");
            UpdatePearl(50);
        }

        if (product.definition.id.Equals(_pearl_150))
        {
            //Debug.Log("진주 150개 결제 완료");
            UpdatePearl(150);
        }

        if (product.definition.id.Equals(_pearl_300))
        {
            //Debug.Log("진주 300개 결제 완료");
            UpdatePearl(300);
        }

        if (product.definition.id.Equals(_pearl_500))
        {
            //Debug.Log("진주 500개 결제 완료");
            UpdatePearl(500);
        }

        if (product.definition.id.Equals(_jeongdongjinPass))
        {
            //Debug.Log("jeongdongjin 패스 결제 완료");
            UpdatePass(0);
        }

        if (product.definition.id.Equals(_homerspitPass))
        {
           // Debug.Log("homerspit 패스 결제 완료");
            UpdatePass(1);
        }

        if (product.definition.id.Equals(_skywayPass))
        {
            //Debug.Log("skyway 패스 결제 완료");
            UpdatePass(2);
        }

        if (product.definition.id.Equals(_fourthAquarium))
        {
            if (DataManager.INSTANCE._mapType.Equals(PublicDefined.eMapType.jeongdongjin) || DataManager.INSTANCE._mapType.Equals(PublicDefined.eMapType.homerspit)
                || DataManager.INSTANCE._mapType.Equals(PublicDefined.eMapType.homerspit))
                UpdateAquariumIngame(3);
            else
                UpdateAquarium(3);
        }

        if (product.definition.id.Equals(_fifthAquarium))
        {
            if (DataManager.INSTANCE._mapType.Equals(PublicDefined.eMapType.jeongdongjin) || DataManager.INSTANCE._mapType.Equals(PublicDefined.eMapType.homerspit)
                || DataManager.INSTANCE._mapType.Equals(PublicDefined.eMapType.homerspit))
                UpdateAquariumIngame(4);
            else
                UpdateAquarium(4); 
        }

        if (product.definition.id.Equals(_jeongdongjinPassInBook))
        {
            UpdatePassInBook(0);
        }

        if (product.definition.id.Equals(_homerspitPassInBook))
        {
            UpdatePassInBook(1);
        }

        if (product.definition.id.Equals(_skywayPassInBook))
        {
            UpdatePassInBook(2);
        }

        if (product.definition.id.Equals(_platinumPackage))
        {
            UpdatePackage(0);
        }

        if (product.definition.id.Equals(_diamondPackage)) 
        {
            UpdatePackage(1);
        }

        if(product.definition.id.Equals(_adblockPackage))
        {
            UpdateADBlock();
        }

    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        //if (_userData == null)
        //    _userData = DBManager.INSTANCE.GetUserData();

        Debug.LogError(product.definition.id + " 실패 이유 : " + failureReason);

        //if(product.definition.id.Equals(_adblockPackage))
        //{
        //    UpdateADBlock();
        //}

        //if (product.definition.id.Equals(_fourthAquarium))
        //{
        //    if (DataManager.INSTANCE._mapType.Equals(PublicDefined.eMapType.jeongdongjin) || DataManager.INSTANCE._mapType.Equals(PublicDefined.eMapType.homerspit)
        //        || DataManager.INSTANCE._mapType.Equals(PublicDefined.eMapType.homerspit))
        //        UpdateAquariumIngame(3);
        //    else
        //        UpdateAquarium(3);
        //}

        //if (product.definition.id.Equals(_fifthAquarium))
        //{
        //    if (DataManager.INSTANCE._mapType.Equals(PublicDefined.eMapType.jeongdongjin) || DataManager.INSTANCE._mapType.Equals(PublicDefined.eMapType.homerspit)
        //        || DataManager.INSTANCE._mapType.Equals(PublicDefined.eMapType.homerspit))
        //        UpdateAquariumIngame(4);
        //    else
        //        UpdateAquarium(4);
        //}

        //if (product.definition.id.Equals(_platinumPackage))
        //{
        //    UpdatePackage(0);
        //}

        //if (product.definition.id.Equals(_diamondPackage))
        //{
        //    UpdatePackage(1);
        //}

        //if (product.definition.id.Equals(_homerspitPassInBook))
        //{
        //    UpdatePassInBook(1);
        //}
    }
    void UpdatePearl(int pearl)
    {
        Dictionary<string, object> updateDic = new Dictionary<string, object>();
        _userData._pearl += pearl;
        updateDic.Add("/_pearl/", _userData._pearl);
        DBManager.INSTANCE.UpdateFirebase(updateDic);
        Shop.INSTANCE.UpdateGoldPearl();
    }


    void UpdatePass(int number)
    {
        _passBuyPopup.gameObject.SetActive(false);
        // jeongdongjin
        if (number.Equals(0))
        {
            Dictionary<string, object> updateDic = new Dictionary<string, object>();
            _userData._haveJeongdongjinPass = true;
            updateDic.Add("/_haveJeongdongjinPass", true);
            DBManager.INSTANCE.UpdateFirebase(updateDic);
        }
        // homerspit
        else if (number.Equals(1))
        {
            Dictionary<string, object> updateDic = new Dictionary<string, object>();
            _userData._haveHomerspitPass = true;
            updateDic.Add("/_haveHomerspitPass", true);
            DBManager.INSTANCE.UpdateFirebase(updateDic);
        }
        // skyway
        else
        {
            Dictionary<string, object> updateDic = new Dictionary<string, object>();
            _userData._haveSkywayPass = true;
            updateDic.Add("/_haveSkywayPass", true);
            DBManager.INSTANCE.UpdateFirebase(updateDic);
        }
    }

    void UpdatePassInBook(int number)
    {
        _buyPopup.gameObject.SetActive(false);
        
        // jeongdongjin
        if (number.Equals(0))
        {
            Dictionary<string, object> updateDic = new Dictionary<string, object>();
            _userData._haveJeongdongjinPass = true;
            updateDic.Add("/_haveJeongdongjinPass", true);
            DBManager.INSTANCE.UpdateFirebase(updateDic);
        }
        // homerspit
        else if (number.Equals(1))
        {
            Dictionary<string, object> updateDic = new Dictionary<string, object>();
            _userData._haveHomerspitPass = true;
            updateDic.Add("/_haveHomerspitPass", true);
            DBManager.INSTANCE.UpdateFirebase(updateDic);
        }
        // skyway
        else
        {
            Dictionary<string, object> updateDic = new Dictionary<string, object>();
            _userData._haveSkywayPass = true;
            updateDic.Add("/_haveSkywayPass", true);
            DBManager.INSTANCE.UpdateFirebase(updateDic);
        }

        _passBuyUI.SeasonPass.InitPassButtons();
    }

    void UpdateAquarium(int number)
    {
        _aquariumBuyPopup.SetActive(false);

        if (number.Equals(3))
        {
            Dictionary<string, object> updateDic = new Dictionary<string, object>();
            _userData._haveFourthAquarium = true;
            updateDic.Add("/_haveFourthAquarium/", true);

            DBManager.INSTANCE.UpdateFirebase(updateDic);
            _userData.UpdateCountStateList(true, 3);
            _aquariumUI.CheckAquariumPossessState();
            _aquariumUI.UpdateMoney();
            _fishInfoUI.SelectAquariumUIUpdate();
        }
        else
        {
            Dictionary<string, object> updateDic = new Dictionary<string, object>();
            _userData._haveFifthAquarium = true;
            updateDic.Add("/_haveFifthAquarium/", true);

            DBManager.INSTANCE.UpdateFirebase(updateDic);
            _userData.UpdateCountStateList(true, 4);
            _aquariumUI.CheckAquariumPossessState();
            _aquariumUI.UpdateMoney();
            _fishInfoUI.SelectAquariumUIUpdate();
        }
    }

    void UpdateAquariumIngame(int number)
    {
        _aquariumBuyPopup.SetActive(false);

        if (number.Equals(3))
        {
            Dictionary<string, object> updateDic = new Dictionary<string, object>();
            _userData._haveFourthAquarium = true;
            updateDic.Add("/_haveFourthAquarium/", true);

            DBManager.INSTANCE.UpdateFirebase(updateDic);
            _userData.UpdateCountStateList(true, 3);
            _fishPopup.UpdateAquariumState();
        }
        else
        {
            Dictionary<string, object> updateDic = new Dictionary<string, object>();
            _userData._haveFifthAquarium = true;
            updateDic.Add("/_haveFifthAquarium/", true);

            DBManager.INSTANCE.UpdateFirebase(updateDic);
            _userData.UpdateCountStateList(true, 4);
            _fishPopup.UpdateAquariumState();
        }
    }

    void UpdatePackage(int number)
    {
        if(_userData == null)
        {
            _userData = DBManager.INSTANCE.GetUserData();
        }

        if (number.Equals(0))
        {
            _userData._havePlatinumPackage = true;
            _userData.CleanUpPlatinumPackage();
            Dictionary<string, bool> dic = _userData.GetPlatinumPackage();

            for (int i = 0; i < 30; i++)
            {
                DateTime DateTime_today = DateTime.Today.AddDays(i);
                string today = DateTime_today.ToString("yyyyMMdd");
                if(i.Equals(0))
                    dic.Add(today, true);
                else
                    dic.Add(today, false);
            }
        }
        else
        {
            _userData._haveDiamondPackage = true;
            _userData.CleanUpDiamondPackage();
            Dictionary<string, bool> dic = _userData.GetDiamondPackage();

            for (int i = 0; i < 30; i++)
            {
                DateTime DateTime_today = DateTime.Today.AddDays(i);
                string today = DateTime_today.ToString("yyyyMMdd");
                if (i.Equals(0))
                    dic.Add(today, true);
                else
                    dic.Add(today, false);
            }
        }

        // test
        //foreach(KeyValuePair<string, bool> item in _userData.GetPlatinumPackage())
        //{
        //    Debug.LogError(item.Key + " , " + item.Value);
        //}
        
        // 파이어베이스 업데이트
        DBManager.INSTANCE.BuyPackage(number);
        // 패키지 구매 UI 띄우기
        _passBuyPopup.SuccessBuyPackage(number);
    }

    void UpdateADBlock()
    {
        if (_userData == null)
        {
            _userData = DBManager.INSTANCE.GetUserData();
        }

        _passBuyPopup.gameObject.SetActive(false);

        DataManager.INSTANCE._isADBlock = true;
        _userData._haveADBlock = true;
        _userData._pearl += 100;
        Shop.INSTANCE.UpdateGoldPearl();

        Dictionary<string, object> updateDic = new Dictionary<string, object>();
        updateDic.Add("/_haveADBlock/", true);
        updateDic.Add("/_pearl/", _userData._pearl);
        
        DBManager.INSTANCE.UpdateFirebase(updateDic);
    }
}