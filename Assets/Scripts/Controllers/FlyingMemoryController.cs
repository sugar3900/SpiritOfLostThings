using UnityEngine;

namespace GGJ {
    
    public class FlyingMemoryController : MonoBehaviour {
    		
        [SerializeField] private ParticleSystem flyingMemoryParticleSystem;
    
        private void Start(){
            
            flyingMemoryParticleSystem.Play();
            
            // TODO: fly self to tree
        }
    }
}

