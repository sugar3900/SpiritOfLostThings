using System;
using GGJ;
using UnityEngine;

namespace GGJ {
    
    public class CharacterAnimationController : MonoBehaviour {

        [SerializeField] private Animator animator;

        private enum CharacterState {
            Idle,
            Left,
            Right,
            Up,
            Down
        }

        private CharacterState currentState;

        public void Idle(){
            
            SetState(CharacterState.Idle);
        }

        public void Left(){
            
            SetState(CharacterState.Left);
        }

        public void Right(){
            
            SetState(CharacterState.Right);
        }

        public void Down(){
            
            SetState(CharacterState.Down);
        }

        public void Up(){
            
            SetState(CharacterState.Up);
        }

        private void SetState(CharacterState state){

            if (state == currentState)
            {
                return;
            }

            currentState = state;
            
            PlayAnimation(GetAnimNameForState(state));
        }

        private string GetAnimNameForState(CharacterState state){
            
            switch (state)
            {
                case CharacterState.Idle: return "Idle";
                case CharacterState.Left: return "WalkRight";
                case CharacterState.Right: return "WalkRight";
                case CharacterState.Up: return "WalkRight";
                case CharacterState.Down: return "WalkRight";
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void PlayAnimation(string stateName){
            
            animator.CrossFade(stateName, 0.2f);
        }
    }
}