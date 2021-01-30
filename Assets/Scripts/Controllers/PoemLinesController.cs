using System.Collections.Generic;
using UnityEngine;

namespace GGJ {
	
	public class PoemLinesController : Controllers {
		
		private List<PoemLineController> poemLineControllers = new List<PoemLineController>();
		
		[SerializeField] private PoemLineController poemLineControllerPrefab;
		[SerializeField] private List<PoemLineData> poemLineDatas;

		public void InitOrReset(){
			
			DestroyAllPoemLines();
			CreatePoemLines();
		}

		public void DowseAllPoemLines(){
			
			foreach (PoemLineController poemLineController in poemLineControllers) {
				
				poemLineController.OnDowse();
			}
		}
		
		private void DestroyAllPoemLines(){
			
			foreach (PoemLineController poemLineController in poemLineControllers) {
				
				Destroy(poemLineController.gameObject);
			}

			poemLineControllers.Clear();
		}

		private void CreatePoemLines(){

			foreach (PoemLineData poemLineData in poemLineDatas) {
				
				var newPoemLine = Instantiate(poemLineControllerPrefab);
				
				newPoemLine.transform.position = poemLineData.position;

				newPoemLine.InitOrReset(poemLineData, playerController, gameLoopController);

				poemLineControllers.Add(newPoemLine);
			}
		}
	}
}
