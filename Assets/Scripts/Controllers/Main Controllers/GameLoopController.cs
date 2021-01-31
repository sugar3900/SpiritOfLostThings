using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GGJ {
    
    public class GameLoopController : Controllers {

        [NonSerialized]	public List<PoemLineData> poemLinesCollected = new List<PoemLineData>();

        public void InitOrReset(){
            
            poemLinesCollected.Clear();

            LevelPropInterfacer.DoOnAllPoemLines(poemLine => poemLine.InitOrReset(CollectPoemLine));
        }

        public void Dowse(){

            LevelPropInterfacer.DoOnAllPoemLines(poemLine => poemLine.DowseIfClose(PlayerController));
        }

        public void Update(){
            
           LevelPropInterfacer.DoOnAllPoemLines(poemLine => poemLine.UpdateTextFade(PlayerController));
        }

        private void CollectPoemLine(PoemLineData poemLineData){

            poemLinesCollected.Add(poemLineData);
            
            LevelPropInterfacer.MemoryTree.TurnOnMushrooms(poemLinesCollected.Count);
            
            if (poemLinesCollected.Count >= 5)
            {
                SceneController.OverlayEndScene();
            }
        }
    }
}
