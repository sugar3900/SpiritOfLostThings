using GGJ;
using UnityEngine;

namespace GGJ {
    
    public class PlayerAnimationController : MonoBehaviour {

        [SerializeField] private Animator animator;

        public void InitOrReset(){

            Idle();
        }

        public void Idle(){
            
            PlayAnimation("Idle");
        }

        public void Left(){
            
            PlayAnimation("WalkRight");
        }

        public void Right(){
            
            PlayAnimation("WalkRight");
        }

        public void Down(){
            
            PlayAnimation("WalkRight");
        }

        public void Up(){
            
            PlayAnimation("WalkRight");
        }

        private void PlayAnimation(string stateName){
            
            animator.CrossFade(stateName, 0.3f);
        }
    }
}