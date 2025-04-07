using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChipBotMoveModel
{
    public event Action OnDoMotion;
    public event Action OnStoppedCurrentChip;
    public event Action OnDestroyedCurrentChip;

    private readonly IChipBank chipBankBot;
    private readonly IChipBank chipBankPlayer;

    private ChipMove currentChip;
    private Transform transformPlayer;

    private const float aimDuration = 1;
    private const float minForce = 5;
    private const float maxForce = 20;

    private IEnumerator coroutineAimShoot;

    private ISoundProvider soundProvider;

    public ChipBotMoveModel(IChipBank chipBankBot, IChipBank chipBankPlayer, ISoundProvider soundProvider)
    {
        this.chipBankBot = chipBankBot;
        this.chipBankPlayer = chipBankPlayer;
        this.soundProvider = soundProvider;
    }

    public void ActivateMove()
    {
        if(currentChip != null)
        {
            currentChip.OnStoppedCurrent -= HandleStoppedCurrentChip;
            currentChip.OnDeadCurrent -= HandleDestroyedCurrentChip;
        }

        if(coroutineAimShoot != null)
            Coroutines.Stop(coroutineAimShoot);

        coroutineAimShoot = AimShootTarget();
        Coroutines.Start(coroutineAimShoot);
    }

    private IEnumerator AimShootTarget()
    {
        yield return new WaitForSeconds(0.2f);

        Debug.Log(chipBankPlayer.GetChipMoves().Count);

        currentChip = chipBankBot.GetChipMoves()[Random.Range(0, chipBankBot.GetChipMoves().Count)];
        currentChip.OnStoppedCurrent += HandleStoppedCurrentChip;
        currentChip.OnDeadCurrent += HandleDestroyedCurrentChip;

        transformPlayer = GetClosestTransformPlayer(currentChip.transform);

        currentChip.ActivateAim();
        soundProvider.PlayOneShot("Chip_Aim");

        Vector2 startPosition = currentChip.transform.position;
        Vector2 targetPosition = transformPlayer.position;

        float elapsedTime = 0;

        Vector2 directionToTarget = (targetPosition - startPosition).normalized;

        Vector2 direction = (targetPosition - startPosition).normalized;

        while (elapsedTime < aimDuration - 0.3f)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / aimDuration;

            float scale = Mathf.Lerp(0.1f, 1, progress);
            currentChip.ScaleAim(scale);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 270;
            currentChip.RotateAim(angle);

            yield return null;
        }

        float force = Random.Range(minForce, maxForce);

        soundProvider.PlayOneShot("Chip_Fire");
        currentChip.AddForce(direction * force);
        currentChip.DeactivateAim();

        OnDoMotion?.Invoke();
    }

    
    private Transform GetClosestTransformPlayer(Transform chipBot)
    {
        Transform transform = null;
        float s = float.MaxValue;

        foreach(var chipPlayer in chipBankPlayer.GetChipMoves())
        {
            float distance = Vector2.Distance(chipBot.transform.position, chipPlayer.transform.position);

            if(distance < s)
            {
                s = distance;
                transform = chipPlayer.transform;
            }
        }

        return transform;
    }

    private void HandleStoppedCurrentChip(ChipMove chipMove)
    {
        OnStoppedCurrentChip?.Invoke();
    }

    private void HandleDestroyedCurrentChip(ChipMove chipMove)
    {
        OnDestroyedCurrentChip?.Invoke();
    }
}
