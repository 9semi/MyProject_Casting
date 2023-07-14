using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo : MonoBehaviour
{
    public GameObject basicWindow, characterWidnow;

    /// <summary>
    /// 상세 정보 버튼 함수
    /// </summary>
    public void DetailInformation()
    {

    }

    /// <summary>
    /// 외형 변경 버튼 함수
    /// </summary>
    public void ChangeAppearance()
    {

    }

    /// <summary>
    /// 캐릭터 변경 버튼 함수
    /// </summary>
    public void ChangeCharcter(bool isSet)
    {
        // 캐릭터 선택창을 껐다 켰다
        characterWidnow.SetActive(isSet);

        // 기본 정보창 크거나 끄기
        basicWindow.SetActive(!isSet);
        // isSet이 true일때 가지고 있는 캐릭터 불러오기
        //isSet == true? :
    }
}