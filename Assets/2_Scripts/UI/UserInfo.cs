using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo : MonoBehaviour
{
    public GameObject basicWindow, characterWidnow;

    /// <summary>
    /// �� ���� ��ư �Լ�
    /// </summary>
    public void DetailInformation()
    {

    }

    /// <summary>
    /// ���� ���� ��ư �Լ�
    /// </summary>
    public void ChangeAppearance()
    {

    }

    /// <summary>
    /// ĳ���� ���� ��ư �Լ�
    /// </summary>
    public void ChangeCharcter(bool isSet)
    {
        // ĳ���� ����â�� ���� �״�
        characterWidnow.SetActive(isSet);

        // �⺻ ����â ũ�ų� ����
        basicWindow.SetActive(!isSet);
        // isSet�� true�϶� ������ �ִ� ĳ���� �ҷ�����
        //isSet == true? :
    }
}