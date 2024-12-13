using UnityEngine;

/// <summary>
/// Base class for all interactive objects on scene
/// </summary>
public abstract class Entity : MonoBehaviour
{
	/// <summary>
	/// Name of an object for user
	/// </summary>
	[SerializeField] private string m_Nickname;
	public string Nickname => m_Nickname;


}
