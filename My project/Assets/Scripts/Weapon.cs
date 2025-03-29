using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
public class Weapon : ScriptableObject
{
    public float weapon_damage;
    public float weapon_speed;
    public float weapon_life;
    public float weapon_range;   
    public Sprite weapon_icon;
    public string weapon_name;
    public AudioClip weapon_sound;
    public float weapon_sound_pitch;
    public int weapon_value;
}
