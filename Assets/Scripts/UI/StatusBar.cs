using UnityEngine;

// Bar at the top right of the screen that shows icons for status effects if they occur
public class StatusBar : MonoBehaviour
{
    public GameObject attackBoostIcon;
    public GameObject speedBoostIcon;

    private (GameObject Icon, Timer Countdown) activeAttackBoost;
    private (GameObject Icon, Timer Countdown) activeSpeedBoost;

    private void OnEnable() {
        AttackBoost.OnAttackBoostApplied += AddAttackBoostIcon;
        // AttackBoost.OnAttackBoostRemoved += RemoveAttackBoostIcon;
        SpeedBoost.OnSpeedBoostApplied += AddSpeedBoostIcon;
        // SpeedBoost.OnSpeedBoostRemoved += RemoveSpeedBoostIcon;
    }

    private void OnDisable() {
        AttackBoost.OnAttackBoostApplied -= AddAttackBoostIcon;
        // AttackBoost.OnAttackBoostRemoved -= RemoveAttackBoostIcon;
        SpeedBoost.OnSpeedBoostApplied -= AddSpeedBoostIcon;
        // SpeedBoost.OnSpeedBoostRemoved -= RemoveSpeedBoostIcon;
    }

    // TODO: Add text for boost amount on top of the icon
    private void AddAttackBoostIcon(float duration, int damageBoost)
    {
        GameObject icon = Instantiate(attackBoostIcon, transform);
        activeAttackBoost = (icon, icon.GetComponentInChildren<Timer>());
        activeAttackBoost.Countdown.OnTimerEnd += RemoveAttackBoostIcon;
        activeAttackBoost.Countdown.StartTimer(duration);
    }

    private void RemoveAttackBoostIcon()
    {
        activeAttackBoost.Countdown.StopTimer();
        Destroy(activeAttackBoost.Icon);
    }

    private void RemoveAttackBoostIcon(float duration, int damageBoost)
    {
        activeAttackBoost.Countdown.StopTimer();
        Destroy(activeAttackBoost.Icon);
    }

    private void AddSpeedBoostIcon(float duration, float speedAmount)
    {
        GameObject icon = Instantiate(speedBoostIcon, transform);
        activeSpeedBoost = (icon, icon.GetComponentInChildren<Timer>());
        activeSpeedBoost.Countdown.OnTimerEnd += RemoveSpeedBoostIcon;
        activeSpeedBoost.Countdown.StartTimer(duration);
    }

    private void RemoveSpeedBoostIcon()
    {
        activeSpeedBoost.Countdown.StopTimer();
        Destroy(activeSpeedBoost.Icon);
    }

    private void RemoveSpeedBoostIcon(float duration, float speedAmount)
    {
        activeSpeedBoost.Countdown.StopTimer();
        Destroy(activeSpeedBoost.Icon);
    }
}
