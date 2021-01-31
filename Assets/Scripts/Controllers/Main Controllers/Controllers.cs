using UnityEngine;

namespace GGJ {
    
    [RequireComponent(typeof(SceneController))]
    [RequireComponent(typeof(GameLoopController))]
    [RequireComponent(typeof(LevelPropInterfacer))]

    public abstract class Controllers : MonoBehaviour {
        
        private SceneController _sceneController;
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
