using System;
using System.Collections;
using System.Collections.Generic;
using GGJ;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenController : MonoBehaviour {

    [TextArea] [SerializeField] private string firstLineOfPoem = "";
    [SerializeField] private GameObject entireScreen;
    [SerializeField] private Text poemLineText;
    [SerializeField] private Sprite[] treeSpriteVariants;
    [SerializeField] private Image treeImage;

    private Action closeCallback;
    
    public void ResetAndInit(List<PoemLineData> poemLinesCollected, Action closeCallback){

        this.closeCallback = closeCallback;
        
        entireScreen.SetActive(true);
        FillInPoem(poemLinesCollected);
        CustomizeTree(poemLinesCollected);

        StartCoroutine(WaitThenClose());
    }

    public void MakeInvisible(){
        
        entireScreen.SetActive(false);
    }
    
    private IEnumerator WaitThenClose(){
           
        // TODO: change this to close on click later?
        yield return new WaitForSeconds(15);
        CloseAndRunCallback();
    }

    private void CloseAndRunCallback(){
        
        closeCallback?.Invoke();
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
       
        string poemContents = firstLineOfPoem;

        foreach (PoemLineData poemLineData in poemLinesCollected)
        {
            string poemLine = poemLineData.poemLineContents;
            poemContents += poemLine;

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
