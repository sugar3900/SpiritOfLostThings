using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ {
    public class PlayerController : Controllers {
        
        public void InitOrReset(){
            
            // TODO: any cleanup on init or reset
        }
        
        // TODO: call these input methods
        public void OnLeftPressed(){
            
            playerAnimationController.Left();
        }
        
        public void OnRightPressed(){
            
            playerAnimationController.Right();
        }
        
        public void OnUpPressed(){
            
            playerAnimationController.Up();
        }
        
        public void OnDownPressed(){
            
            playerAnimationController.Down();
        }

        public void OnDowsePressed(){
            
            gameLoopController.Dowse();
        }
        
        public float GetDistanceFrom(GameObject gameObject){
            
            return Vector3.Distance(playerAnimationController.transform.position, gameObject.transform.position);
        }
    }
}
