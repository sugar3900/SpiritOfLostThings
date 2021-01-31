using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GGJ {
    
    public class FlyingMemoryController : MonoBehaviour {
    		
        [SerializeField] private ParticleSystem flyingMemoryParticleSystem;
    
        public void Init(Vector3 position){
            
            flyingMemoryParticleSystem.Play();

            var distance = Vector3.Distance(transform.position, position);
            transform.DOScale(0, 0f);
            transform.DOScale(1, 0.5f);
            transform.DOMove(position, distance * 0.1f).SetEase(Ease.InBack).OnComplete(() => Destroy(gameObject));
            transform.rotation = new Quaternion(0f, 0f, (float) Random.Range(0, 360), 0);
            transform.DOLocalRotate(new Vector3(0, 0, 360), duration: 2).SetLoops(20);
        }
    }
}

