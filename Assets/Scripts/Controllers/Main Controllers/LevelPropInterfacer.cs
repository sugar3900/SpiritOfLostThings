using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GGJ {
    
    public class LevelPropInterfacer : Controllers {

        [NonSerialized] public List<PoemLineProp> PoemLines = new List<PoemLineProp>();
        [NonSerialized] public MemoryTreeProp MemoryTree;

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
