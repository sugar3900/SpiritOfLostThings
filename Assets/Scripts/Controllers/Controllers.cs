using UnityEngine;

namespace GGJ {
    
    [RequireComponent(typeof(SceneController))]
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(PlayerAnimationController))]
    [RequireComponent(typeof(GameLoopController))]
    [RequireComponent(typeof(PoemLinesController))]
    
    public class Controllers : MonoBehaviour {
        
        protected SceneController sceneController;
        protected PlayerController playerController;
        protected PlayerAnimationController playerAnimationController;
        protected GameLoopController gameLoopController;
        protected PoemLinesController poemLinesController;
    
        private void Start(){
            
            sceneController = GetComponent<SceneController>();
            playerController = GetComponent<PlayerController>();
            playerAnimationController = GetComponent<PlayerAnimationController>();
            gameLoopController = GetComponent<GameLoopController>();
            poemLinesController = GetComponent<PoemLinesController>();
        }
    }
}
