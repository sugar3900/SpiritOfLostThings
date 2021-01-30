using UnityEngine;

namespace GGJ
{
	public class MemoryItem : ScriptableObject {
		
		[SerializeField] private string name;
		[SerializeField] private Sprite propImage;
		[SerializeField] private Sprite treeInsertImage;
		[SerializeField] private PoemLine poemLineDark;
		[SerializeField] private PoemLine poemLineLight;
	}
}
