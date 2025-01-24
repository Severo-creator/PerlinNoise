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
public class Cogumelo
{
    public Vector3 pos;

    public Cogumelo(Vector3 v3){
        pos = v3;
    }
}

[System.Serializable]
public class Cave
{
    public Vector3 pos;
    public string seed;

    public List<Cogumelo> cogumelos = new List<Cogumelo>(); 

    public Cave(Vector3 possent){
        pos = possent;

        seed = Time.time.ToString();
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

    public List<Cave> cavernas = new List<Cave>();

    public CelulaGrid(float x, float z){
        PosX = x;
        PosZ = z;

        idX =  (int)PosX/256;

        idY =  (int)PosZ/256;

    }
    
}


