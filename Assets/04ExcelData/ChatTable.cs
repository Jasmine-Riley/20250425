using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StoryType
{
	Story,
	FindDevil,
}

[ExcelAsset]
public class ChatTable : ScriptableObject
{
	public List<ChatData> Story; // Replace 'EntityType' to an actual type that is serializable.
	public List<ChatData> FindDevil;
}
