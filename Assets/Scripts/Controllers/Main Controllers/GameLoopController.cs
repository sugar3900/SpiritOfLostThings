using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ {
    
    public class GameLoopController : Controllers {

        [NonSerialized]	public List<PoemLineData> poemLinesCollected = new List<PoemLineData>();
        
        [SerializeField] private int poemLinesBeforeGameEnd = 2; //normally 5

        public void InitOrReset(){
            
            poemLinesCollected.Clear();

            LevelPropInterfacer.DoOnAllPoemLines(poemLine => poemLine.InitOrReset(CollectPoemLine));
        }

        // TODO: call dowse from input somewhere
        public void Dowse(){

            LevelPropInterfacer.DoOnAllPoemLines(poemLine => poemLine.DowseIfClose(LevelPropInterfacer.Character));
        }

        public void Update(){
            
           LevelPropInterfacer.DoOnAllPoemLines(poemLine => poemLine.UpdateTextFade(LevelPropInterfacer.Character));
        }

        private void CollectPoemLine(PoemLineData poemLineData){

            poemLinesCollected.Add(poemLineData);
            
            LevelPropInterfacer.MemoryTree.TurnOnMushrooms(poemLinesCollected.Count);
            
            if (poemLinesCollected.Count >= poemLinesBeforeGameEnd)
            {
                SceneController.OverlayEndScene();
            }
        }
    }
}
