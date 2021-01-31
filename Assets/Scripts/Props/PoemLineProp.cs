using System;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ {
    
    public class PoemLineProp : DynamicProp {
		
        [Header("Components")]
        [SerializeField] private ParticleSystem darkParticleSystem;
        [SerializeField] private ParticleSystem lightParticleSystem;
        [SerializeField] private FlyingMemoryController FlyingMemoryPrefab;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Text poemText;
        
        private PoemLineData poemLineData;
        private PlayerController playerController;
        private GameLoopController gameLoopController;

        public void InitOrReset(PoemLineData poemLineData, PlayerController playerController, GameLoopController gameLoopController){

            this.poemLineData = poemLineData;
            this.playerController = playerController;
            this.gameLoopController = gameLoopController;
            
            UpdateText(poemLineData.poemLineContents);
            TurnOffParticleSystems();
        }
        
        public void Update(){
			
            // Make sure this PoemLineProp was initialized before starting to 
            if (playerController != null && poemLineData != null)
            {
                UpdateTextFade();
            }
        }

        public void OnDowse(){
            
            if (playerController.GetDistanceFrom(gameObject) < poemLineData.maxDowseDistance)
            {
                PlayDowseParticles();
                PlayDowseSound();
            }
        }

        private void UpdateText(string str){
            
            poemText.text = str;
        }

        private void TurnOffParticleSystems(){
            
            darkParticleSystem.gameObject.SetActive(false);
            lightParticleSystem.gameObject.SetActive(false);
        }

        private void UpdateTextFade(){
            
            // get distance from the player
            float distance = playerController.GetDistanceFrom(gameObject);
            
            // cap it at max distance to fade
            distance = Math.Min(poemLineData.maxDistanceBeforeTextFades, distance);

            // normalize it
            float normalizedDistance = distance / poemLineData.maxDistanceBeforeTextFades;

            // flip it
            float alpha = 1 - normalizedDistance;
            
                
            poemText.color = new Color(poemText.color.r, poemText.color.g, poemText.color.b, alpha);
        }

        private void PlayDowseParticles(){
			
            ParticleSystem ps = poemLineData.isLight ? lightParticleSystem : darkParticleSystem;
            ParticleSystem otherPs = poemLineData.isLight ? darkParticleSystem : lightParticleSystem;

            ps.gameObject.SetActive(true);
            otherPs.gameObject.SetActive(false);

            ps.Clear();
            ps.Play();
        }

        private void PlayDowseSound(){
            
            audioSource.Play();
        }

        public void Collect(){
			
            gameLoopController.CollectPoemLine(poemLineData);

            Instantiate(FlyingMemoryPrefab);
        }
    }
}

