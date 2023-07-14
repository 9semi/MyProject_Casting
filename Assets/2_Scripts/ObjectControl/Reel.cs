using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reel : MonoBehaviour
{
    MeshRenderer _mesh;
    public Material[] _reelMaterialArray;

    private void Awake()
    {
        _mesh = GetComponent<MeshRenderer>();
    }
    public void ChangeMaterial(int number)
    {
        _mesh.material = _reelMaterialArray[number];
    }
}
