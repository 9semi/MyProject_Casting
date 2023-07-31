using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailUI : MonoBehaviour
{
    [SerializeField] Transform _content;

    int _mailCount;

    void Start()
    {
        if(DataManager.INSTANCE._tutorialIsDone)
        {
            for (int i = 0; i < _content.childCount; i++)
            {
                _content.GetChild(i).gameObject.SetActive(false);
            }
        }
        else
        {
            _content.GetChild(0).GetComponent<MailObject>().Init("3È£ ³¬½Ë´ë / 3È£ ¸±");

            for (int i = 1; i < _content.childCount; i++)
            {
                _content.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
