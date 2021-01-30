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
			
			List<PoemLineProp> poemLines = GetAllPoemLineControllers();

			for (var i = 0; i < poemLines.Count; i++)
			{
				poemLines[i].InitOrReset( initPoemLineDatas[i], playerController, gameLoopController);
			}
		}
		
		public void DowseAllPoemLines(){
			
			List<PoemLineProp> poemLines = GetAllPoemLineControllers();

			foreach (PoemLineProp poemLine in poemLines) {
				
				poemLine.OnDowse();
			}
		}

		private List<PoemLineProp> GetAllPoemLineControllers(){

			// TODO: actually get all of the poem lines from PropRegistry
			return new List<PoemLineProp>();
		}
	}
}
