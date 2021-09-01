using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoryText", menuName = "StoryText")]
public class StoryTextNote : ScriptableObject
{
    public string StoryHeader;
    public int StoryIndex;
    [TextAreaAttribute(15,20)] public string StoryText;
}
