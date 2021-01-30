using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GGJ {
    
    public class GameLoopController : Controllers {

        

        public void InitOrReset(){
            
            
        }

        public void Dowse(){

            poemLinesController.DowseAllPoemLines();
        }
        
        public void CollectPoemLine(PoemLineData poemLineData){

            poemLinesController.CollectPoem(poemLineData);
            
            if (poemLinesController.poemLinesCollected.Count >= 5)
            {
                sceneController.GoToEndGamePoemScene();
            }
        }
    }
}
