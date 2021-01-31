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
            
            PlayerAnimationController.Left();
        }
        
        public void OnRightPressed(){
            
            PlayerAnimationController.Right();
        }
        
        public void OnUpPressed(){
            
            PlayerAnimationController.Up();
        }
        
        public void OnDownPressed(){
            
            PlayerAnimationController.Down();
        }

        public void OnDowsePressed(){
            
            GameLoopController.Dowse();
        }
        
        public float GetDistanceFrom(GameObject gameObject){
            
            return Vector3.Distance(PlayerAnimationController.transform.position, gameObject.transform.position);
        }
    }
}
