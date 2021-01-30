using System;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ {
    
    public class PoemLineController : MonoBehaviour {
		
        [Header("Components")]
        [SerializeField] private ParticleSystem darkParticleSystem;
        [SerializeField] private ParticleSystem lightParticleSystem;
        [SerializeField] private FlyingMemoryController FlyingMemoryPrefab;
        [SerializeField] private Text poemText;
        
        private PoemLineData poemLineData;
        private PlayerController playerController;
        private GameLoopController gameLoopController;

        public void InitOrReset(PoemLineData poemLineData, PlayerController playerController, GameLoopController gameLoopController){

            this.poemLineData = poemLineData;
            this.playerController = playerController;
            this.gameLoopController = gameLoopController;
            
            UpdateText(poemLineData.poemLineContents);
        }
        
        public void Update(){
			
            UpdateTextFade();
        }

        public void OnDowse(){
            
            if (playerController.GetDistanceFrom(gameObject) < poemLineData.maxDistanceBeforeParticlesPlay)
            {
                PlayPoemParticles();
            }
        }

        private void UpdateText(string str){
            
            poemText.text = str;
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

        private void PlayPoemParticles(){
			
            ParticleSystem ps = poemLineData.isLight ? lightParticleSystem : darkParticleSystem;

            ps.Clear();
            ps.Play();
        }

        public void Collect(){
			
            gameLoopController.CollectPoemLine(poemLineData);

            Instantiate(FlyingMemoryPrefab);
        }
    }
}

