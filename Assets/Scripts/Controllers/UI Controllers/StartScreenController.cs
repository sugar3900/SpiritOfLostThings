using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ {
    public class StartScreenController : MonoBehaviour {
        
        [SerializeField] private GameObject entireScreen;
        [SerializeField] private Animator animator;
        
        public void InitOrReset(){

            animator.CrossFade("Idle", 0);
            entireScreen.SetActive(true);
        }

        public void OnPlayButtonPress(){

            animator.CrossFade("OnStartButtonClicked", 0);

            StartCoroutine(WaitThenTurnOff());
        }

        private IEnumerator WaitThenTurnOff(){
            
            yield return new WaitForSeconds(10);
            
            MakeInvisible();
        }

        public void MakeInvisible(){
        
            entireScreen.SetActive(false);
        }
    }
}
