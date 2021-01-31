using System;
using System.Collections.Generic;

namespace GGJ {
    
    public class LevelPropInterfacer : Controllers {

        [NonSerialized] public List<PoemLineProp> PoemLines = new List<PoemLineProp>();
        [NonSerialized] public MemoryTreeProp MemoryTree;
        [NonSerialized] public CharacterProp Character;

        public void ParseLevelData(Level level){
            
            var dynamicProps = level.DynamicProps;

            foreach (DynamicProp dynamicProp in dynamicProps)
            {
                if (dynamicProp is PoemLineProp poemLine)
                {
                    PoemLines.Add(poemLine);
                }
                else if (dynamicProp is MemoryTreeProp memoryTreeProp)
                {
                    MemoryTree = memoryTreeProp;
                }
                
                else if (dynamicProp is CharacterProp characterProp)
                {
                    Character = characterProp;
                }
            }
        }

        public void DoOnAllPoemLines(Action<PoemLineProp> doOnEach){

            foreach (var poemLine in PoemLines)
            {
                doOnEach(poemLine);
            }
        }
    }
}
