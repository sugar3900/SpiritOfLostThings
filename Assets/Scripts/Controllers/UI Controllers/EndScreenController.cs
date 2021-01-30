using System;
using System.Collections.Generic;
using GGJ;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenController : MonoBehaviour {

    [SerializeField] private GameObject entireScreen;
    [SerializeField] private Text poemLineText;
    [SerializeField] private Sprite[] treeSpriteVariants;
    [SerializeField] private Image treeImage;
    
    public void ResetAndInit(List<PoemLineData> poemLinesCollected){
        
        entireScreen.SetActive(true);
        FillInPoem(poemLinesCollected);
        CustomizeTree(poemLinesCollected);
    }

    public void MakeInvisible(){
        
        entireScreen.SetActive(false);
    }

    private void FillInPoem(List<PoemLineData> poemLinesCollected){

        poemLineText.text = GetPoemContents(poemLinesCollected);
    }
    
    private void CustomizeTree(List<PoemLineData> poemLinesCollected){

        int poemLightness = CalculatePoemLightness(poemLinesCollected);

        int treeSpriteIndex = MapPoemLightnessToTreeSpriteIndex(poemLightness, poemLinesCollected.Count);

        treeImage.sprite = treeSpriteVariants[treeSpriteIndex];
    }

    private string GetPoemContents(List<PoemLineData> poemLinesCollected){
       
        string poemContents = "";

        foreach (PoemLineData poemLineData in poemLinesCollected)
        {
            poemContents += poemLineData.poemLineContents;

            poemContents += "\n";
        }

        return poemContents;
    }

    private int MapPoemLightnessToTreeSpriteIndex(int poemLightness, int maxPoemLightness){
        
        int minPoemLightness = 0;
        int minTreeSpriteVariant = 0;
        int maxTreeSpriteVariant = treeSpriteVariants.Length - 1;
        
        int treeVariantIndex = poemLightness / maxPoemLightness * (maxTreeSpriteVariant);

        return treeVariantIndex;
    }

    private int CalculatePoemLightness(List<PoemLineData> poemLinesCollected){

        int poemLightness = 0;

        foreach (PoemLineData poemLineData in poemLinesCollected)
        {
            if (poemLineData.isLight)
            {
                poemLightness ++;
            }
        }

        // Returns a number between 0 (darkest poem) and poemLinesCollected.length (lightest poem)
        return poemLightness;
    }
}
