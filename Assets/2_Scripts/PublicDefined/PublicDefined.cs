using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicDefined : MonoBehaviour
{
    static public readonly WaitForSeconds _01secDelay = new WaitForSeconds(0.1f);
    static public readonly WaitForSeconds _02secDelay = new WaitForSeconds(0.2f);
    static public readonly WaitForSeconds _05secDelay = new WaitForSeconds(0.5f);
    static public readonly WaitForSeconds _1secDelay = new WaitForSeconds(1f);
    static public readonly WaitForSeconds _15secDelay = new WaitForSeconds(1.5f);
    static public readonly WaitForSeconds _2secDelay = new WaitForSeconds(2f);
    static public readonly WaitForSeconds _25secDelay = new WaitForSeconds(2.5f);
    static public readonly WaitForSeconds _3secDelay = new WaitForSeconds(3f);
    static public readonly WaitForSeconds _35secDelay = new WaitForSeconds(3.5f);
    static public readonly WaitForSeconds _4secDelay = new WaitForSeconds(4f);

    // È¿°úÀ½ (½Ã°£ÀÌ ¸ØÃçµµ ½Ã°£ÀÌ Èê·¯¾ßÇÑ´Ù.)
    static public readonly WaitForSecondsRealtime _02secRealDelay = new WaitForSecondsRealtime(0.2f);
    static public readonly WaitForSecondsRealtime _03secRealDelay = new WaitForSecondsRealtime(0.3f);
    static public readonly WaitForSecondsRealtime _05secRealDelay = new WaitForSecondsRealtime(0.5f);
    static public readonly WaitForSecondsRealtime _09secRealDelay = new WaitForSecondsRealtime(0.9f);
    static public readonly WaitForSecondsRealtime _1secRealDelay = new WaitForSecondsRealtime(1f);
    static public readonly WaitForSecondsRealtime _2secRealDelay = new WaitForSecondsRealtime(2f);
    static public readonly WaitForSecondsRealtime _5secRealDelay = new WaitForSecondsRealtime(5f);
    static public readonly WaitForSecondsRealtime _10secRealDelay = new WaitForSecondsRealtime(10f);


    public struct stFishInfo
    {
        public int _fishNumber; // DB ¹øÈ£
        public string _name;
        public float _weight;
        public float _length;
        public int _price;
        public int _fishType;
        public string _key;

        public stFishInfo(int DBNumber, string name, float length, float weight, int price, eFishType fishType)
        {
            _fishNumber = DBNumber;
            _name = name;
            _length = length;
            _weight = weight;
            _price = price;
            _fishType = (int)fishType;
            _key = string.Empty;
        }

        public void SetKey(string key)
        {
            _key = key;
        }
    }

    public struct stRankFishInfo
    {
        public int _fishNumber;
        public string _name;
        public float _length;
        public float _weight;
        public int _fishType;
        public stRankFishInfo(int DBNumber, string n, float l, float w, eFishType t)
        {
            _fishNumber = DBNumber;
            _name = n;
            _length = l;
            _weight = w;
            _fishType = (int)t;
        }
    }
    
    public enum eFishType
    {
        Sundry,
        Normal,
        Rare
    }

    public enum ePassType // 0 
    {
        jeongdongjin,
        skyway,
        homerspit,

        Count,
    }

    public enum eRodType //1000
    {
        RedRod,
        OrangeRod,
        YellowRod,
        GreenRod,
        BlueRod,
        PurpleRod,
        PinkRod,

        Count,
    }
    public enum eReelType // 6000
    {
        RedReel,
        OrangeReel,
        YellowReel,
        GreenReel,
        BlueReel,
        PurpleReel,
        PinkReel,

        Count,
    }
    public enum eBaitType  // 2000
    {
        rkwo,
        rkfcl,
        rp,
        rhemddj,
        rhswoddl,
        rnf,
        Rhctodn,
        sjqcl,
        ekfvoddl,
        eorn,
        eogk,
        ehenrrp,
        enejwlrp,
        Ekroql,
        ajdrp,
        apxkfwlrm,
        aufcl,
        audxo,
        ahfotodn,
        ansdj,
        alRnfkwl,
        alsanftodn,
        qkdlqmfpdltus,
        qpddpeha,
        qhflauf,
        qndwkddj,
        qmffnfjsj,
        qlddj,
        todn,
        tjdrp,
        thfk,
        tnddj,
        tmvns,
        tmvlsxpdlfwlrm,
        tldzldalshdn,
        Thr,
        dprl,
        dusdj,
        dufqlddj,
        dhwlddj,
        wjsroddl,
        wjsqhr,
        wjsdj,
        wjddjfl,
        whro,
        ckarotwlfjddl,
        cjdrotwlfjddl,
        cjddj,
        zmflf,
        zmsRhclrhrl,
        xkdlfkqk,
        vkfotodn,
        vpstmfqpdlxm,
        vhqvj,
        vmffhxldalshdn,
        gkrrhdcl,
        ghdrotwlfjddl,
        ghdgkq,

        Count,
    }

    public enum ePasteBaitType // 3000
    {
        rhswoddlEjrqkq,
        zmflfEjrqkq,
        whroqntmfjrl,

        Count,
    }

    public enum eFloatType // Âî 4000
    {
        Standard,
        Count,
    }

    public enum eSinkerType // ºÀµ¹ 5000
    {
        one,
        two,
        three,
        four,
        five,

        Count,
    }

    public enum eBaitBoxType // 7000
    {
        jeongdongjinbaitbox,
        skywaybaitbox,
        homerspirbaitbox,

        Count,
    }

    public enum eItemType
    {
        // ÆÐ½º
        Pass,
        //³¬½Ë´ë
        Rod,
        //¹Ì³¢
        Bait,
        // ¶±¹ä
        Pastebait,
        // Âî
        Float,
        //Âî,ºÀµ¹
        Sinker,
        //¹É°í±â
        Reel,
        // ¹Ì³¢, ¶±¹ä »óÀÚ
        BaitBox,
        // °ñµå
        Gold,
        // ÁøÁÖ
        Pearl,

        // ¿ùÁ¤¾× ÆÐÅ°Áö
        Package,
    }

    public enum eMapType
    {
        jeongdongjin,
        skyway,
        homerspit,
        lobby,
        tutorial,
    }

    public enum eJeongdongjinPass
    {
        wjdehdwlsanfrhrl3akflwkqrl,
        tnwhrrhksdpwjdehdwlsanfrhrlsjgrl,
        tnwhrrhksemfdjrkrl,
        tjdeowkqrl,
        wjdehdwlsrlfhr5akflcodnrl,
        ehekfl35cmdltkdwkqrl,
        wjdehdwlsalRlvordufrl,
        whvlqhffkr40cmdltkdwkqrl,
        qndwkddj3akflwkqrl,
        wjdehdwlsrlfhr10akflcodnrl,
        qhffkr_qnfqhffkr_roqhffkrgksakflTlrwkqrl,
        ansmldhwlddj5akflwkqrl,
        sjqcl1_5kgdltkdwkqrl,
        rhemddj20akflwkqrl,
        wjdehdwlsrlfhr15akflcodnrl,
        dladustndj50cmdltkdwkqrl,
        tnwhrrhksdptjanfrhrlvkfrl,
        gkrrhdcl20akflwkqrl,
        didxo70cmdltkdwkqrl,
        wjdehdwlsrlfhr20akflcodnrl,
        fndjskRtlfhghkddjwkqrl,
        tnddj3kgdltkdwkqrl,
        gmlrnldjwhd1akflwkqrl,
        dprlfmfdldydgoansmldhwlddj10akflwkqrl,
        wjdehdwlsrlfhr25akflcodnrl,
        anfrhrl5kgdltkdwkqrl,
        rkatjdehawkqrl,
        vhqvjfmfdldydgoqkddjwkqrl,
        tnwhrrhksdpwjdehdwlsgmlrnlanfrhrlsjgrl,
        wjdehdwlsrlfhr30akflcodnrl,
        Count,
    }
    public enum eSkywayPass
    {
        tmzkdldnpdlanfrhrl3akflwkqrl,
        tnwhrrhksdptmzkdldnpdlanfrhrlsjgrl,
        tnwhrrhksemfdjrkrl,
        vlzkthvltnlwkqrl,
        tmzkdldnpdlrlfhr5akflcodnrl,
        rjfvmrkwkal35cmdltkdwkqrl,
        wkdrnflfwkdckrgkrl,
        Rhcltkacl20kgdltkdwkqrl,
        rhemddj5akflwkqrl,
        tmzkdldnpdlrlfhr10akflcodnrl,
        vmffhxldalshdnfmfdldydgoRhcltkacl5akflwkqrl,
        qkddj2akflwkqrl,
        qntlfl2akflwkqrl,
        eotjdidfpzmvltnl3akflwkqrl,
        tmzkdldnpdlrlfhr15akflcodnrl,
        rkatjdeha40cmdltkdwkqrl,
        tnwhrrhksdptjanfrhrlvkfrl,
        didajfleha50cmdltkdwkqrl,
        thrdlarmasnseha70cmdltkdwkqrl,
        tmzkdldnpdlrlfhr20akflcodnrl,
        akstorl120cmdltkdwkqrl,
        dhkdrhemddj10kgdltkdwkqrl,
        gmlrnldjwhd1akflwkqrl,
        wjrehawkqrl,
        tmzkdldnpdlrlfhr25akflcodnrl,
        anfrhrl30kgdltkdwkqrl,
        qmfforrmfnvjwkqrl,
        ehctoclwkqrl,
        tnwhrrhksdptmzkdldnpdlgmlrnldjwhdsjgrl,
        tmzkdldnpdlrlfhr30akflcodnrl,
        Count,
    }

    public enum eHomerspitPass
    {
        ghajtmvltanfrhrl3akflwkqrl,
        tnwhrrhksdpghajtmvltanfrhrlsjgrl,
        tnwhrrhksemfdjrkrl,
        skfrownfrhrlwkqrl,
        ghajtmvltrlfhr5akflcodnrl,
        shfkdrkrtltjeo14cmdltkdwkqrl,
        ghajtmvltalRlvordufrl,
        dmseorn70cmdltkdwkqrl,
        wkdansqhffkr3akflwkqrl,
        ghajtmvltrlfhr10akflcodnrl,
        ghktkfcl_akthcl_rkrtlrkwkalgksakflTlrwkqrl,
        wkqnfreha5akflwkqrl,
        qlwrmasnseha1kgdltkdwkqrl,
        wjddjfl20akflwkqrl,
        ghajtmvltrlfhr15akflcodnrl,
        dusdj70cmdltkdwkqrl,
        tnwhrrhksdptjanfrhrlvkfrl,
        Rhdcl20akflwkqrl,
        eorn75cmdltkdwkqrl,
        ghajtmvltrlfhr20akflcodnrl,
        fndjskRtlfhdmseornwkqrl,
        audxo3kgdltkdwkqrl,
        gmlrnldjwhd1akflwkqrl,
        tldzldalshdnfmfdldydgoaudxo10akflwkqrl,
        ghajtmvltrlfhr25akflcodnrl,
        anfrhrl10kgdltkdwkqrl,
        dkrtkddjwkqrl,
        vhqvjfmfdldydgoghkdekfkddjwkqrl,
        tnwhrrhksdpghajtmvltgmlrnlanfrhrlsjgrl,
        ghajtmvltrlfhr30akflcodnrl,
        Count,
    }

    public enum eBGMType
    {
        loginscene,
        loadingscene,
        lobbyscene,
        jeongdongjinBGM,
        skywayBGM,
        homerspitBGM,
        jeongdongjinFighting,
        skywayFighting,
        homerspitFighting,
        bitting,
        firstAquarium,
        secondAquarium,
        thirdAquarium,
        fourthAquarium,
        fifthAquarium,
        ost,

    }

    public enum eEffectSoundType
    {
        gameStartButton,
        dontUse,
        dontUse2,
        dontUse3,
        dontUse4,
        dontUse5,
        dontUse6,
        dontUse7,
        dontUse8,
        seagull,
        dontUse9,
        casting,
        reelUntied,
        sinkerReachesTheSurface,
        dontUse10,
        reeling,
        fishResist,
        rodSpring,
        fishRaise,
        dontUse11,
        baitbundleReachesTheSurface,
        fishCatchSuccess,
        dontUse12,
        meteor,
        specialAttackSuccess,
        specialAttackFail,
        dontUse13,
        fishFlap,
        dontUse14,
        radar,
        lamp,
        mainClick,
        exit,
        profileSave,
        profileRemove,
        profileArrow,
        profileSelect,
        aquariumControl,
        aquariumFix,
        aquariumRenewal,
        buyOK,
        coin,
        equipRod,
        equipReel,
        equipBait,
        equipLureBait,
        equipPastebait,
        equipFloat,
        equipSinker,
        equipRemove,
        bluetoothScan,
        matchModeGradeUp,
        matchModeLose,
        matchModeDraw,
        matchModeMatching,
        matchModeCount,
    }
}
