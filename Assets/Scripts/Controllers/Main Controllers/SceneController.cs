using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GGJ {
    
    public class SceneController : Controllers {
        
        [SerializeField] private StartScreenController startScreenController;
        [SerializeField] private EndScreenController endScreenController;

        public void OnEnable(){
            
            GoToStartScene();
        }

        public void GoToStartScene(){
           
            startScreenController.InitOrReset(GoToGameScene);
            endScreenController.MakeInvisible();
        }

        public void GoToGameScene(){
            
            startScreenController.MakeInvisible();
            endScreenController.MakeInvisible();
            
            // Reset core Controllers
            gameLoopController.InitOrReset();
            poemLinesController.InitOrReset();
            playerController.InitOrReset();
            playerAnimationController.InitOrReset();
        }

        public void GoToEndGamePoemScene(){
            
            startScreenController.MakeInvisible();
            endScreenController.ResetAndInit(poemLinesController.poemLinesCollected);

            StartCoroutine(WaitThenGoToGameScene());
        }

        private IEnumerator WaitThenGoToGameScene(){
            
            yield return new WaitForSeconds(5);
            GoToStartScene();
        }
    }
}


