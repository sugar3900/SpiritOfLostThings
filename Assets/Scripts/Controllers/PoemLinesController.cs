using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ {
	
	public class PoemLinesController : Controllers {
		
		[SerializeField] private List<PoemLineData> initPoemLineDatas;
		
	    [NonSerialized]	public List<PoemLineData> poemLinesCollected;

		public void InitOrReset(){

			poemLinesCollected.Clear();

			InitOrResetAllPoemLines();
		}

		public void CollectPoem(PoemLineData poemLineDataCollected){
			
			poemLinesCollected.Add(poemLineDataCollected);
		}

		private void InitOrResetAllPoemLines(){
			
			List<PoemLineController> poemLines = GetAllPoemLineControllers();

			for (var i = 0; i < poemLines.Count; i++)
			{
				poemLines[i].InitOrReset( initPoemLineDatas[i], playerController, gameLoopController);
			}
		}
		
		public void DowseAllPoemLines(){
			
			List<PoemLineController> poemLines = GetAllPoemLineControllers();

			foreach (PoemLineController poemLine in poemLines) {
				
				poemLine.OnDowse();
			}
		}

		private List<PoemLineController> GetAllPoemLineControllers(){

			// TODO: actually get all of the poem lines from PropRegistry
			return new List<PoemLineController>();
		}
	}
}
