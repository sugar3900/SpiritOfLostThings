using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ {
	
	public class PoemLinesController : Controllers {
		
		[SerializeField] private LevelGenerator levelGenerator;
		
		[SerializeField] private List<PoemLineData> initPoemLineDatas;
		
		
	    [NonSerialized]	public List<PoemLineData> poemLinesCollected;

	    private List<PoemLineProp> poemLineProps = new List<PoemLineProp>();

		public void InitOrReset(){

			// clear all lists
			poemLineProps.Clear();
			poemLinesCollected.Clear();
			
			// Listen for Prop Creation events
			levelGenerator.onDynamicPropCreated -= OnDynamicPropCreated;
			levelGenerator.onDynamicPropCreated += OnDynamicPropCreated;
			
			// 
			InitOrResetAllPoemLines();
		}

		private void OnDynamicPropCreated(DynamicProp dynamicProp){
			
			if (dynamicProp is PoemLineProp prop)
			{
				poemLineProps.Add(prop);
			}
		}

		public void CollectPoem(PoemLineData poemLineDataCollected){
			
			poemLinesCollected.Add(poemLineDataCollected);
		}

		private void InitOrResetAllPoemLines(){
			
			List<PoemLineProp> poemLines = GetAllPoemLineProps();

			for (var i = 0; i < poemLines.Count; i++)
			{
				poemLines[i].InitOrReset( initPoemLineDatas[i], playerController, gameLoopController);
			}
		}
		
		public void DowseAllPoemLines(){
			
			List<PoemLineProp> poemLines = GetAllPoemLineProps();

			foreach (PoemLineProp poemLine in poemLines) {
				
				poemLine.OnDowse();
			}
		}

		private List<PoemLineProp> GetAllPoemLineProps(){

			// TODO: actually get all of the poem lines from PropRegistry
			return new List<PoemLineProp>();
		}
	}
}
