using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity_PokemonData : ScriptableObject
{	
	public List<Param> param = new List<Param> ();

	[System.SerializableAttribute]
	public class Param
	{
		
		public int ID;
		public string name;
		public string characteristic_1;
		public string characteristic_2;
		public string characteristic_3;
		public string type_1;
		public string type_2;
		public int hp;
		public int atk;
		public int def;
		public int s_atk;
		public int s_def;
		public int spd;
	}
}