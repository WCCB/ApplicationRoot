using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity_TypeAffinityData : ScriptableObject
{	
	public List<Param> param = new List<Param> ();

	[System.SerializableAttribute]
	public class Param
	{
		
		public int ID;
		public string name;
		public double normal;
		public double fire;
		public double water;
		public double electric;
		public double grass;
		public double ice;
		public double fighting;
		public double poison;
		public double ground;
		public double flying;
		public double psychic;
		public double bug;
		public double rock;
		public double ghost;
		public double dragon;
		public double dark;
		public double steel;
		public double fairlie;
	}
}