using UnityEngine;

public enum WeaponMode
{
	Primary,
	Secondary
}

[CreateAssetMenu]
public sealed class WeaponProperties : ScriptableObject
{
	[SerializeField] private WeaponMode m_Mode;
	public WeaponMode Mode => m_Mode;

	[SerializeField] private Projectile m_ProjectilePrefab;
	public Projectile ProjectilePrefab => m_ProjectilePrefab;

	[SerializeField] private float m_RateOfFire;
	public float RateOfFire => m_RateOfFire;

	[SerializeField] private int m_EnergyUsage;
	public int EnergyUsage => m_EnergyUsage;

	[SerializeField] private int m_EnergyAmountToStartFire;
	public int EnergyAmountToStartFire => m_EnergyAmountToStartFire;

	[SerializeField] private int m_EnergyRegenPerSecond;
	public float EnergyRegenPerSecond => m_EnergyRegenPerSecond;

	[SerializeField] private float m_SpreadShotRange;
	public float SpreadShotRange => m_SpreadShotRange;

	[SerializeField] private float m_SpreadShotDistanceFactor = 0.1f;
	public float SpreadShotDistanceFactor => m_SpreadShotDistanceFactor;

	[SerializeField] private AudioClip m_LaunchSFX;
	public AudioClip LaunchSFX => m_LaunchSFX;
	 
}
