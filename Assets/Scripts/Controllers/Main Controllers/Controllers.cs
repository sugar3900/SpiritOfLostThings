using UnityEngine;

namespace GGJ {
    
    [RequireComponent(typeof(SceneController))]
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(PlayerAnimationController))]
    [RequireComponent(typeof(GameLoopController))]
    [RequireComponent(typeof(LevelPropInterfacer))]

    public abstract class Controllers : MonoBehaviour {
        
        private SceneController _sceneController;
        private PlayerController _playerController;
        private PlayerAnimationController _playerAnimationController;
        private GameLoopController _gameLoopController;
        private LevelPropInterfacer _levelPropInterfacer;

        protected SceneController SceneController {
            get {

                if (_sceneController == null)
                {
                    _sceneController = GetComponent<SceneController>();
                }

                return _sceneController;
            }
        }
        
        protected PlayerController PlayerController {
            get {

                if (_playerController == null)
                {
                    _playerController = GetComponent<PlayerController>();
                }

                return _playerController;
            }
        }
        
        protected PlayerAnimationController PlayerAnimationController {
            get {

                if (_playerAnimationController == null)
                {
                    _playerAnimationController = GetComponent<PlayerAnimationController>();
                }

                return _playerAnimationController;
            }
        }
        
        protected GameLoopController GameLoopController {
            get {

                if (_gameLoopController == null)
                {
                    _gameLoopController = GetComponent<GameLoopController>();
                }

                return _gameLoopController;
            }
        }
        
        protected LevelPropInterfacer LevelPropInterfacer {
            get {

                if (_levelPropInterfacer == null)
                {
                    _levelPropInterfacer = GetComponent<LevelPropInterfacer>();
                }

                return _levelPropInterfacer;
            }
        }
    }
}
