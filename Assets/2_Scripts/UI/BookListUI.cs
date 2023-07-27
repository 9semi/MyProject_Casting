using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookListUI : MonoBehaviour
{
    public GameObject _jeongdongjinBookObject;
    public GameObject _homerspitBookObject;
    public GameObject _skywayBookObject;

    public void ClickBook(int number)
    {
        _jeongdongjinBookObject.SetActive(false);
        _homerspitBookObject.SetActive(false);
        _skywayBookObject.SetActive(false);
        
        switch (number)
        {
            case 0:
                _jeongdongjinBookObject.GetComponent<BookAndSeasonPass>().MapType_Lobby = number;
                _jeongdongjinBookObject.SetActive(true);
                break;
            case 1:
                _homerspitBookObject.GetComponent<BookAndSeasonPass>().MapType_Lobby = number;
                _homerspitBookObject.SetActive(true);
                break;
            case 2:
                _skywayBookObject.GetComponent<BookAndSeasonPass>().MapType_Lobby = number;
                _skywayBookObject.SetActive(true);
                break;
        }
    }
}
