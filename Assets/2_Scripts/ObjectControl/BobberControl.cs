using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BobberControl : MonoBehaviour
{
    private float quillPoy = 100f;
    public Rigidbody rd;
    GameManager gameMgr;
    public Transform myTr;
    public Text distTx;
    private float dist;
    public bool isBobberCollision = false;
    public Transform particleObject;

    void Start()
    {
        gameMgr = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        myTr = GetComponent<Transform>();
        rd = GetComponent<Rigidbody>();
        particleObject = GameObject.FindGameObjectWithTag("Object").transform.GetChild(0);
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Water"))
        {
            gameMgr.NeedleInWater = true;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
