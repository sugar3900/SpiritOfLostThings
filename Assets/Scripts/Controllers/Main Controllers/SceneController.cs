using System.Collections;
using UnityEngine;

namespace GGJ {
    
    public class SceneController : Controllers {
        
        [SerializeField] private LevelGenerator levelGenerator;
        [SerializeField] private StartScreenController startScreenController;
        [SerializeField] private EndScreenController endScreenController;

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
            OverlayStartScreen();
        }

        private void OverlayStartScreen(){
           
            startScreenController.InitOrReset(GoToGameScene);
            endScreenController.MakeInvisible();
        }

        public void GoToGameScene(){
            
            startScreenController.MakeInvisible();
            endScreenController.MakeInvisible();
            
            // Reset core Controllers
            GameLoopController.InitOrReset();
        }

        public void OverlayEndScene(){
            
            startScreenController.MakeInvisible();
            endScreenController.ResetAndInit(GameLoopController.poemLinesCollected, OverlayStartScreen);
        }
    }
}


