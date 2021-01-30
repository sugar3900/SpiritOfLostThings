using UnityEngine;

namespace GGJ {
    
    public class MemoryTreeProp : MonoBehaviour {
    		
        [SerializeField] private GameObject[] mushrooms;

        public void Start(){
            
            TurnOffAllMushrooms();
        }

        public void TurnOnMushrooms(int mushroomCount){

            for (var i = 0; i < mushrooms.Length; i++)
            {
                mushrooms[i].SetActive(i < mushroomCount);
            }
        }

        private void TurnOffAllMushrooms(){

            foreach (GameObject mushroom in mushrooms)
            {
                mushroom.SetActive(false);
            }
        }
    }
}

