using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ {
    
    //[RequireComponent(typeof(SceneController))]
    //[RequireComponent(typeof(PlayerController))]
    //[RequireComponent(typeof(GameLoopController))]
    
    public class Controllers : MonoBehaviour {
        
        [SerializeField] protected SceneController sceneController;
        [SerializeField] protected PlayerController playerController;
        [SerializeField] protected GameLoopController gameLoopController;
        [SerializeField] protected PoemLinesController poemLinesController;
    
        private void Start(){
            
            sceneController = GetComponent<SceneController>();
            playerController = GetComponent<PlayerController>();
            gameLoopController = GetComponent<GameLoopController>();
            poemLinesController = GetComponent<PoemLinesController>();
        }
    }
}
