using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GGJ {
    
    public class FlyingMemoryController : MonoBehaviour {
    		
        [SerializeField] private ParticleSystem flyingMemoryParticleSystem;
    
        private void Start(){
            
            flyingMemoryParticleSystem.Play();
            
            transform.DOMove(new Vector3(2,3,4), 20);
            transform.rotation = new Quaternion(0f, 0f, (float) Random.Range(0, 360), 0);
            transform.DOLocalRotate(new Vector3(0, 0, 360), duration: 5);
        }
    }
}

