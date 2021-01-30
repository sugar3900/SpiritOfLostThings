using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GGJ {
    
    public class GameLoopController : Controllers {

        [SerializeField] private PoemLinesController poemLinesController;
        
        private List<PoemLineData> poemLinesCollected;

        public void InitOrReset(){
            
            poemLinesCollected.Clear();
            
            poemLinesController.InitOrReset();
        }

        public void Dowse(){

            poemLinesController.DowseAllPoemLines();
        }
        
        public void CollectPoemLine(PoemLineData poemLineData){

            poemLinesCollected.Add(poemLineData);

            if (poemLinesCollected.Count >= 5)
            {
                sceneController.GoToEndGamePoemScene();
            }
            
            // TODO: send poem item to tree
        }
    }
}
