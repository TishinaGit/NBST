using UnityEngine;

public enum WeaponMode
{
	Primary,
	Secondary
}

[CreateAssetMenu]
public sealed class WeaponProperties : ScriptableObject
{
    [SerializeField] private WeaponMode _mode;
    public WeaponMode Mode => _mode;

    [SerializeField] private Projectile _projectilePrefab;
    public Projectile ProjectilePrefab => _projectilePrefab;

    [SerializeField] private float _rateOfFire;
    public float RateOfFire => _rateOfFire;

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

