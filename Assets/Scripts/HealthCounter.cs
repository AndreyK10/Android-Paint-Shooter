using UnityEngine;
using TMPro;

public class HealthCounter : MonoBehaviour
{
    [SerializeField]
    private HitTarget HitTarget;
    [SerializeField]
    private GameState GameState;

    public delegate void Dead();
    public event Dead OnDead;


    [SerializeField]
    private TextMeshProUGUI _healthText;
    private int _health;
    public int Health;

    private void OnEnable()
    {
        HitTarget.OnTargetHit += OnTargetHit;
        GameState.OnStateChanged += OnStateChanged;
    }

    private void OnStateChanged(GameState.GameMode gameMode)
    {
        if (gameMode == GameState.GameMode.ScoreMode)
        {
            SetHealth();
            UpdateHealth();
        }
    }

    private void OnTargetHit(Target.TargetType targetType, int value)
    {
        if (targetType == Target.TargetType.DamageTarget)
        {
            Handheld.Vibrate();
            _health -= value;
            if (_health <= 0)
            {
                OnDead?.Invoke();
            }
            UpdateHealth();
        }
    }
    private void SetHealth()
    {
        _health = Health;
    }

    private void UpdateHealth()
    {
        _healthText.text = _health.ToString();
    }



    private void OnDisable()
    {
        HitTarget.OnTargetHit -= OnTargetHit;
        GameState.OnStateChanged -= OnStateChanged;
    }

}
