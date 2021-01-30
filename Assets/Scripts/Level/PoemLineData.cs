using System;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ
{
	public class PoemLineData : ScriptableObject {
		
		[SerializeField] public string poemLineContents;
		[SerializeField] public bool isLight;
		[SerializeField] public Sprite treeSprite;
		[SerializeField] public float maxDistanceBeforeParticlesPlay = 5;
		[SerializeField] public float maxDistanceBeforeTextFades = 5;
		[SerializeField] public Vector3 position = new Vector3();
	}
}
