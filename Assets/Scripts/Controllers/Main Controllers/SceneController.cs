using System.Collections;
using UnityEngine;

namespace GGJ {
    
    public class SceneController : Controllers {
        
        [SerializeField] private LevelGenerator levelGenerator;
        [SerializeField] private StartScreenController startScreenController;
        [SerializeField] private EndScreenController endScreenController;
        [SerializeField] private bool turnOffStartScreen = false;
        [SerializeField] private bool turnOffEndScreen = false;
        
        public void OnEnable(){
            
            levelGenerator.OnLevelGenerated += ParseLevelData;
            levelGenerator.OnLevelGenerated += SetUpGameFirstTime;
        }
        
        public void OnDisable(){
            
            levelGenerator.OnLevelGenerated -= ParseLevelData;
            levelGenerator.OnLevelGenerated -= SetUpGameFirstTime;
        }

        private void ParseLevelData(Level level)
		{
            LevelPropInterfacer.ParseLevelData(level);
            GameLoopController.InitOrReset();
        }

        private void SetUpGameFirstTime(Level level){
            
            levelGenerator.OnLevelGenerated -= SetUpGameFirstTime;

            OverlayStartScreenAndInitOrResetGame();
        }

        private void OverlayStartScreenAndInitOrResetGame(){

            if (!turnOffStartScreen)
            {
                startScreenController.InitOrReset();
            }
            
            endScreenController.MakeInvisible();
            GameLoopController.InitOrReset();
        }

        public void OverlayEndScene(){
            
            startScreenController.MakeInvisible();

            if (!turnOffEndScreen)
            {
                endScreenController.ResetAndInit(GameLoopController.poemLinesCollected, OverlayStartScreenAndInitOrResetGame);
            }
        }
    }
}


