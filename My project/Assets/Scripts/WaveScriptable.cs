using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveScriptable", menuName = "Wave")]
public class WaveScriptable : ScriptableObject
{
    public int n_monsters;
    public List<GameObject> monster;
}   
