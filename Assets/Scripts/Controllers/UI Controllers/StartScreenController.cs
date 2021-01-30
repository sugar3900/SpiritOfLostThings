using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ {
    public class StartScreenController : MonoBehaviour {
        
        [SerializeField] private GameObject entireScreen;

        private Action playButtonCallback;
        
        public void InitOrReset(Action playButtonCallback){

            entireScreen.SetActive(true);
            this.playButtonCallback = playButtonCallback;
        }

        public void OnPlayButtonPress(){
            
            playButtonCallback?.Invoke();
        }

        public void MakeInvisible(){
        
            entireScreen.SetActive(false);
        }
    }
}
