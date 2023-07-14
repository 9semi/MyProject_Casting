using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DepthDistanceUI : MonoBehaviour
{
    [SerializeField] Text _depth_distance_Text;

    private void Awake()
    {
        GameObject.FindGameObjectWithTag("UI").GetComponent<InGameUIManager>().GetDepthDistanceText(_depth_distance_Text);
    }
}
