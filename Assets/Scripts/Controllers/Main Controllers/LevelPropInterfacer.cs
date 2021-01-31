using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ
{
	public class LevelPropInterfacer : Controllers
	{
		public List<PoemLineProp> PoemLines { get; } = new List<PoemLineProp>();
		public MemoryTreeProp MemoryTree { get; private set; }
		public CharacterProp Character { get; private set; }

		public void ParseLevelData(Level level)
		{
			List<DynamicProp> dynamicProps = level.DynamicProps;
			PoemLines.Clear();
			MemoryTree = null;
			Character = null;
			Debug.Log("Rebuild");

			foreach (DynamicProp dynamicProp in dynamicProps)
			{
				if (dynamicProp is PoemLineProp poemLine)
				{
					PoemLines.Add(poemLine);
				}
				else if (dynamicProp is MemoryTreeProp memoryTreeProp)
				{
					MemoryTree = memoryTreeProp;
				}
				else if (dynamicProp is CharacterProp characterProp)
				{
					Character = characterProp;
				}
			}
		}

		public void DoOnAllPoemLines(Action<PoemLineProp> doOnEach)
		{
			foreach (PoemLineProp poemLine in PoemLines)
			{
				if (poemLine != null)
				{
					doOnEach(poemLine);
				}
			}
		}
	}
}
