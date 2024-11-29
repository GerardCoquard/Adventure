using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEditor;

public class Dice3DManager : MonoBehaviour
{
    private List<Dice3D> _dicePool;
    private string _path = "Assets/Resources/RenderTextures";
    [SerializeField] private int diceSpacing;
    [SerializeField] private int textureWidth;
    [SerializeField] private int textureHeight;

    private void Start()
    {
        _dicePool = new List<Dice3D>(transform.GetComponentsInChildren<Dice3D>());
        foreach (Dice3D dice in _dicePool)
        {
            dice.gameObject.SetActive(false);
        }
    }

    public List<Dice3D> GetPool()
    {
        return _dicePool;
    }

    public void ResetPool()
    {
        _dicePool = new List<Dice3D>(transform.GetComponentsInChildren<Dice3D>());
        UnlinkRenderTextures();
        DeleteRenderTextures();
        CreateRenderTextures();
        LinkRenderTextures();
        RepositionDices();
        ResizeFloorCollider();
    }

    private void DeleteRenderTextures()
    {
        if (Directory.Exists(_path))
        {
            string[] files = Directory.GetFiles(_path);

            foreach (string file in files)
            {
                File.Delete(file);
            }
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    private void CreateRenderTextures()
    {
        if (Directory.Exists(_path))
        {
            for (int i = 0; i < _dicePool.Count; i++)
            {
                RenderTexture renderTexture = new RenderTexture(textureWidth, textureHeight, 24);
                string assetPath = $"{_path}/RenderTexture" + i +".renderTexture";
                AssetDatabase.CreateAsset(renderTexture, assetPath);
            }
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
    }
    
    private void UnlinkRenderTextures()
    {
        foreach (Dice3D dice in _dicePool)
        {
            dice.cam.targetTexture = null;
        }
    }

    private void LinkRenderTextures()
    {
        RenderTexture[] renderTextures = Resources.LoadAll<RenderTexture>("RenderTextures");
        
        for (int i = 0; i < _dicePool.Count; i++)
        {
            _dicePool[i].cam.targetTexture = renderTextures[i];
        }
    }
    
    private void RepositionDices()
    {
        int sideLength = Mathf.CeilToInt(Mathf.Sqrt(_dicePool.Count));

        Vector3 startPos = transform.position + new Vector3(-sideLength / 2f * diceSpacing, 0, -sideLength / 2f * diceSpacing) + new Vector3(diceSpacing / 2f, 0, diceSpacing / 2f);
        int indx = 0;
        for (int i = 0; i < sideLength; i++)
        {
            for (int j = 0; j < sideLength; j++)
            {
                Vector3 pos = startPos + new Vector3(j * diceSpacing, 0, i * diceSpacing);
                _dicePool[indx].SetStartPos(pos);
                indx++;
                if(indx >= _dicePool.Count) return;
            }
        }
    }

    private void ResizeFloorCollider()
    {
        BoxCollider box = GetComponent<BoxCollider>();
        
        int size = Mathf.CeilToInt(Mathf.Sqrt(_dicePool.Count));
        box.size = new Vector3(size*diceSpacing, 0.5f, size*diceSpacing);
        box.center = new Vector3(0, -0.5f, 0);
    }
}
