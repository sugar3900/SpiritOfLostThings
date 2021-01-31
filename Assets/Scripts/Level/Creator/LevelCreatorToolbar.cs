using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityObject = UnityEngine.Object;

namespace GGJ
{
	[Serializable]
	public class LevelCreatorToolbar
	{
		[SerializeField]
		private GameObject toolbarRoot;
		[SerializeField]
		private LevelCreatorOption optionPrefab;
		[SerializeField]
		private ScrollRect scrollRect;
		[SerializeField]
		private Transform contentArea;
		[SerializeField]
		private Dropdown dropdown;
		private PropRegistry propRegistry;
		private TileSetRegistry tileSetRegistry;
		private DynamicPropRegistry dynamicPropRegistry;
		public bool IsShown
		{
			get => toolbarRoot.activeSelf;
			set => toolbarRoot.SetActive(value);
		}
		public LevelCreatorMode Mode { get; private set; }
		public int Selection { get; set; }
		private List<LevelCreatorOption> options = new List<LevelCreatorOption>();
		public event Action<LevelCreatorMode> OnModeChanged;

		public void SetRegistries(
			TileSetRegistry tileSetRegistry,
			PropRegistry propRegistry,
			DynamicPropRegistry dynamicPropRegistry)
		{
			this.tileSetRegistry = tileSetRegistry;
			this.propRegistry = propRegistry;
			this.dynamicPropRegistry = dynamicPropRegistry;
			InitializeDropdown();
			Redraw();
		}

		private void InitializeDropdown()
		{
			LevelCreatorMode[] modes = Enum.GetValues(typeof(LevelCreatorMode)) as LevelCreatorMode[];
			List<Dropdown.OptionData> dropdownOptions = new List<Dropdown.OptionData>();
			foreach (LevelCreatorMode mode in modes)
			{
				dropdownOptions.Add(new Dropdown.OptionData(mode.ToString()));
			}
			dropdown.AddOptions(dropdownOptions);
			dropdown.onValueChanged.AddListener(SetMode);
		}

		private void SetMode(int modeIndex)
		{
			Mode = (LevelCreatorMode)modeIndex;
			OnModeChanged?.Invoke(Mode);
			Redraw();
		}

		private void Redraw()
		{
			Selection = 0;
			Clear();
			options = Populate();
			ResetScroll();
		}

		private void Clear()
		{
			int count = options.Count;
			for (int i = count - 1; i >= 0; i--)
			{
				UnityObject.Destroy(options[i].gameObject);
			}
			options = null;
		}

		private List<LevelCreatorOption> Populate()
		{
			switch (Mode)
			{
				case LevelCreatorMode.Tiles:
					return PopulateTileSets();
				case LevelCreatorMode.Props:
					return PopulateProps();
				case LevelCreatorMode.DynamicProp:
					return PopulateDynamicProps();
				default:
					return null;
			}
		}

		private List<LevelCreatorOption> PopulateTileSets()
		{
			List<LevelCreatorOption> tileOptions = new List<LevelCreatorOption>();
			for (int i = 0; i < tileSetRegistry.Count; i++)
			{
				TileSet tileSet = tileSetRegistry.TileSets[i];
				if (tileSet != null)
				{
					Sprite sprite = tileSet.GetSprite(TileCase.Solid);
					tileOptions.Add(CreateOption(tileSet.Id, sprite, i));
				}
				else
				{
					Debug.LogWarning("The TileSet registry contains a missing reference.");
				}
			}
			return tileOptions;
		}

		private List<LevelCreatorOption> PopulateProps()
		{
			List<LevelCreatorOption> propOptions = new List<LevelCreatorOption>();
			for (int i = 0; i < propRegistry.Count; i++)
			{
				Prop prop = propRegistry.Props[i];
				if (prop != null)
				{
					Sprite sprite = prop.Sprite;
					propOptions.Add(CreateOption(prop.Id, sprite, i));
				}
				else
				{
					Debug.LogWarning("The Prop registry contains a missing reference.");
				}
			}
			return propOptions;
		}

		private List<LevelCreatorOption> PopulateDynamicProps()
		{
			List<LevelCreatorOption> dynamicPropOptions = new List<LevelCreatorOption>();
			for (int i = 0; i < dynamicPropRegistry.Count; i++)
			{
				DynamicProp dynamicProp = dynamicPropRegistry.DynamicProps[i];
				if (dynamicProp != null)
				{
					Sprite sprite = dynamicProp.Sprite;
					dynamicPropOptions.Add(CreateOption(dynamicProp.Id, sprite, i));
				}
				else
				{
					Debug.LogWarning("The DynamicProp registry contains a missing reference.");
				}
			}
			return dynamicPropOptions;
		}

		private LevelCreatorOption CreateOption(string id, Sprite sprite, int index)
		{
			LevelCreatorOption option = UnityObject.Instantiate(optionPrefab, contentArea);
			option.Initialize(id, sprite, () => Select(index));
			if (index != Selection)
			{
				option.Unhighlight();
			}
			return option;
		}

		private void ResetScroll()
		{
			scrollRect.horizontalNormalizedPosition = 0;
		}

		private void Select(int index)
		{
			Selection = index;
			for (int i = 0; i < options.Count; i++)
			{
				if (i == index)
				{
					options[i].Highlight();
				}
				else
				{
					options[i].Unhighlight();
				}
			}
		}
	}
}
