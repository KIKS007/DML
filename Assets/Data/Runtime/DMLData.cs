using UnityEngine;
using System.Collections;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
/// 
[System.Serializable]
public class DMLData
{
	[SerializeField]
	string personnage;
	
	[ExposeProperty]
	public string Personnage { get {return personnage; } set { personnage = value;} }
	
	[SerializeField]
	string action;
	
	[ExposeProperty]
	public string Action { get {return action; } set { action = value;} }
	
	[SerializeField]
	string contexte;
	
	[ExposeProperty]
	public string Contexte { get {return contexte; } set { contexte = value;} }
	
}