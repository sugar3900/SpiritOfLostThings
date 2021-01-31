using System;
using System.Collections.Generic;

namespace GGJ {
    
    public class GameLoopController : Controllers {

        [NonSerialized]	public List<PoemLineData> poemLinesCollected = new List<PoemLineData>();

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
            
            if (poemLinesCollected.Count >= 5)
            {
                SceneController.OverlayEndScene();
            }
        }
    }
}
