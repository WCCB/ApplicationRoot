using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity_PersonalityData : ScriptableObject
{	
	public List<Param> param = new List<Param> ();

	[System.SerializableAttribute]
	public class Param
	{
		
		public int ID;
		public string name;
		public double atk;
		public double def;
		public double s_atk;
		public double s_def;
		public double spd;
	}
}