using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Arvore
{
    public Vector3 pos;
    public Arvore(Vector3 v3){
        pos = v3;
    }
}


[System.Serializable]
public class CelulaGrid
{
    public int idX;
    public int idY;

    public float PosX;
    public float PosZ;
    public List<Arvore> arvores = new List<Arvore>();

    public CelulaGrid(float x, float z){
        PosX = x;
        PosZ = z;

        idX =  (int)PosX/256;

        idY =  (int)PosZ/256;    
    }
    
}


