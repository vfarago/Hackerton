using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMaterial : MonoBehaviour
{
    //  å ��ȣ(1�� , 2�� ���..)�� ���� Material�� MainTexture �������ִ� ��ũ��Ʈ 

    [SerializeField]
    private GameObject[] wallArr;

    public Texture redTexture;
    private int bookMaxNum=3;
    private Texture[][] textures;

    void Start()
    {
        textures = new Texture[bookMaxNum][];
        LoadTexture();
    }

    private void LoadTexture()
    {
        for(int i=0; i< bookMaxNum;++i)
        {
            object[] temp = Resources.LoadAll("Texture/1");
            Texture[] textureArr = new Texture[temp.Length];
            for (int j = 0; j < temp.Length; ++j)
            {                
                textureArr[j] = temp[j] as Texture;
            }

            textures[i] = textureArr;
        }
    }

    void Update()
    {
        //  Test
        if (Input.GetKeyDown(KeyCode.Return))
            ChangeObjectMaterial(1);
    }

    public void ChangeObjectMaterial(int bookNumber)
    {
        for(int i=0; i<wallArr.Length; ++i)
        {
            for(int j=0; j< wallArr[i].GetComponent<MeshRenderer>().materials.Length; ++j)
            {
                if (wallArr[i].GetComponent<MeshRenderer>().materials[j].name.Contains("Wall"))
                    wallArr[i].GetComponent<MeshRenderer>().materials[j].mainTexture = textures[bookNumber][i];
            }

        }
    }
}
