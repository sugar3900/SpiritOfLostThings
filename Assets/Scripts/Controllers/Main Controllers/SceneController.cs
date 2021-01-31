using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GGJ {
    
    public class SceneController : Controllers {
        
        [SerializeField] private LevelGenerator levelGenerator;
        [SerializeField] private StartScreenController startScreenController;
        [SerializeField] private EndScreenController endScreenController;

        public void OnEnable(){
            
            levelGenerator.onLevelGenerated += SetUpGameFirstTime;
        }
        
        public void OnDisable(){
            
            levelGenerator.onLevelGenerated -= SetUpGameFirstTime;
        }

        private void SetUpGameFirstTime(Level level){
            
            levelGenerator.onLevelGenerated -= SetUpGameFirstTime;

            LevelPropInterfacer.ParseLevelData(level);
            
            // Reset core Controllers
            GameLoopController.InitOrReset();
            PlayerController.InitOrReset();
            PlayerAnimationController.InitOrReset();
            
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
            PlayerController.InitOrReset();
            PlayerAnimationController.InitOrReset();
        }

        public void OverlayEndScene(){
            
            startScreenController.MakeInvisible();
            endScreenController.ResetAndInit(GameLoopController.poemLinesCollected);

            StartCoroutine(WaitThenGoToStartScreen());
        }

        private IEnumerator WaitThenGoToStartScreen(){
            
            yield return new WaitForSeconds(8);
            OverlayStartScreen();
        }
    }
}


