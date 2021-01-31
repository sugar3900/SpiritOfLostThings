﻿using System;
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

        private Action<PoemLineData> OnCollectedCallback;
        
        public PoemLineData poemLineData;

        public void InitOrReset(Action<PoemLineData> onCollectedCallback){

            OnCollectedCallback = onCollectedCallback;

            UpdateText(poemLineData.poemLineContents);
            TurnOffParticleSystems();
        }

        public void DowseIfClose(PlayerController player){
            
            float maxDistanceForDowsing = poemLineData.maxDowseDistance;

            if (player.GetDistanceFrom(gameObject) < maxDistanceForDowsing)
            {
                PlayDowseParticles();
                PlayDowseSound();
            }
        }
        
        public void Collect(){

            OnCollectedCallback(poemLineData);

            // TODO:
            //Instantiate(FlyingMemoryPrefab);
        }
        
        public void UpdateTextFade(PlayerController player){
            
            // get distance from the player
            float distance = player.GetDistanceFrom(gameObject);
            
            // cap it at max distance to fade
            distance = Math.Min(poemLineData.maxDistanceBeforeTextFades, distance);

            // normalize it
            float normalizedDistance = distance / poemLineData.maxDistanceBeforeTextFades;

            // flip it
            float alpha = 1 - normalizedDistance;
            
                
            poemText.color = new Color(poemText.color.r, poemText.color.g, poemText.color.b, alpha);
        }

        private void UpdateText(string str){
            
            poemText.text = str;
        }

        private void TurnOffParticleSystems(){
            
            darkParticleSystem.gameObject.SetActive(false);
            lightParticleSystem.gameObject.SetActive(false);
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
    }
}

