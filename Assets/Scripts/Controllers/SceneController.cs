using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GGJ {
    
    public class SceneController : Controllers {
        
        public void GoToStartScene(){
           
            // TODO
        }

        public void GoToGameScene(){
            
            gameLoopController.InitOrReset();
            playerController.InitOrReset();
        }

        public void GoToEndGamePoemScene(){
            
            // TODO
        }
    }
}


