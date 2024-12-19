using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public Transform playerTransform;
    public List<Transform> terrenos;

    public List<bool> auxterrenos;
    public Vector3 v3aux = new Vector3();

    public List<CelulaGrid> Grid = new List<CelulaGrid>();

    private List<GameObject> arvores = new List<GameObject>();

    public Vector2 pontoinicial = new Vector2(0,0);
    public Vector2 pontoFinal = new Vector2(0,0);

    public GameObject arvoreref;




    void Start(){
        CelulaGrid celulainicial = new CelulaGrid(0f, 0f);

        Grid.Add(celulainicial);

        GerarArvores(terrenos[0]);
    } 

    // Update is called once per frame
    void Update()
    {
        foreach (var item in terrenos)
        {
            if(playerTransform.position.x >= item.position.x && playerTransform.position.z >=  item.position.z ){
             if(playerTransform.position.x < 
             item.position.x + 256f && playerTransform.position.z <  item.position.z + 256f){
                Debug.Log(playerTransform.position);
               
               if(pontoinicial.x != item.position.x || pontoinicial.y != item.position.z){
                    pontoinicial.x = item.position.x;
                    pontoinicial.y = item.position.z;

                    pontoFinal.x = item.position.x + 256f;
                    pontoFinal.y = item.position.z + 256f;


                    DestroyArvores();
                    Reformular();

                    if(ConteinCelula(pontoinicial.x, pontoinicial.y)){
                        CarregarArvores();
                    }else{
                        CelulaGrid celula = new CelulaGrid(pontoinicial.x, pontoinicial.y);

                        Grid.Add(celula);
                        GerarArvores(item);
                    }

                }
                
             }
            }
        }
        
    }

    private void Reformular(){

        for (int i = 0; i < 9; i++)
        {
            auxterrenos[i] = false;
        }
        
        int aux = 0;
        for (int i = -1; i <= 1; i++)
        {
            for (int a = -1; a <= 1; a++)
            {
                checkInTerrain(i, a);
            }
        }
        
       
        for (int i = -1; i <= 1; i++)
        {
            for (int a = -1; a <= 1; a++)
            {
                if(!Containterrain(i, a)){
                    foreach (var item in terrenos)
                    {
                        if(!auxterrenos[aux]){
                            v3aux = item.position;
                            v3aux.x = (256f * i) + pontoinicial.x;
                            v3aux.z = (256f * a) + pontoinicial.y;
                            item.position = v3aux;
                            auxterrenos[aux] = true;
                            break;
                        }
                        aux ++;
                    }
                    aux = 0;
                }
            }
        }
        
    }

    private void checkInTerrain(int X, int Y){
        int aux = 0;
        foreach (var item in terrenos)
        {
            if((256f * X) + pontoinicial.x == item.position.x && (256f * Y) + pontoinicial.y ==  item.position.z ){
                auxterrenos[aux] = true;
                break;
            }
            aux++;
        }
    }

    private bool Containterrain(int X, int Y){
        foreach (var item in terrenos)
        {
            if((256f * X) + pontoinicial.x == item.position.x && (256f * Y) + pontoinicial.y ==  item.position.z ){
                return true;
            }
        }
        return false;
    }

    public void DestroyArvores(){
        foreach (var item in arvores)
        {
            Destroy(item);
        }
        arvores.Clear();
    }

    public bool ConteinCelula(float x, float z){
        foreach (var item in Grid)
        {
            if(item.idX == (x/256) && item.idY == (z/256)){
                return true;
            }
        }
        return false;
    }

    private int valorCelula(float x, float z){
        int aux = 0;
        foreach (var item in Grid)
        {
            if(item.idX == (x/256) && item.idY == (z/256)){
                return aux;
            }
            aux++;
        }
        Debug.Log("Nao encontrou celula");
        return 0;

    } 


    public void GerarArvores(Transform terreno){
        int aux;
        for (int i = 0; i < 16; i++)
        {
            for (int a = 0; a < 16; a++){
                aux = Random.Range(1,10);
                if(aux == 1){
                    GameObject duplicatedArvore = Instantiate(arvoreref);

                    float PosXA = (i*16) + pontoinicial.x;

                    float PosZA = (a*16) + pontoinicial.y;

                    float altura = terreno.GetComponent<Terrain>().SampleHeight(new Vector3(PosXA, 0, PosZA));


                    Vector3 novapos = new Vector3(PosXA, altura, PosZA);
                    
                    duplicatedArvore.transform.position = novapos;

                    arvores.Add(duplicatedArvore);
                
                    Arvore arvore = new Arvore(novapos);

                    Grid[valorCelula(pontoinicial.x, pontoinicial.y)].arvores.Add(arvore);

                }
            }
        }
    }

   private void CarregarArvores(){
        List<Arvore> arvoreslist = Grid[valorCelula(pontoinicial.x, pontoinicial.y)].arvores;
        GameObject duplicatedArvore;
        foreach (var item in arvoreslist)
        {
            duplicatedArvore = Instantiate(arvoreref);

            duplicatedArvore.transform.position = item.pos;

            arvores.Add(duplicatedArvore);
        }
   }


}

    