using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using Unity.AppUI.Core;
using CONST;

public class CandidateWhimAbility
{
    public List<Ability_base> Ability;

    public List<string> canceledAbilities;
}

public class BattleWhimManager : MonoBehaviour
{
    public List<CandidateWhimAbility> GetCandidateWhimAbility(BattleActionList battleActionList, BattleAction executeAction)
    {
        List<Ability_base> masterActionList = MasterData.instance.masterAbilityList;
        List<string> cancledAbilityName = new List<string>();
        Ability_base targetAbility = masterActionList.FirstOrDefault(m => m.Name == executeAction.abilityName);

        // 思いつき条件技の取得
        var baseWhimAbility = targetAbility.requireAbilityForWhim;
        int baseWhimAbilityCount = baseWhimAbility.Count;

        // 最初の思いつき条件が実行予定のアビリティの思いつき条件と
        // 一致(順番も)するものを全取得
        List<Ability_base> targetAbilities = masterActionList
            .Where(m => baseWhimAbility
                .Select((value, index) => new { value, index })
                .All(b =>
                    m.requireAbilityForWhim.Count > baseWhimAbilityCount &&
                    m.requireAbilityForWhim[b.index] == b.value
                    )
                )
            .ToList();

        cancledAbilityName.Add(targetAbility.Name);

        //待機中の見方アクションを取得
        List<BattleAction> waitingPlayerActionList = battleActionList.allActionList
            .Where(b => b.character.char_role == CONST.CHARCTOR.Role.PLAYER && b.status == CONST.BATTLE_ACTION.STATUS.Waiting)
            .ToList();

        List<Ability_base> resultWhimAbility = new List<Ability_base>();
        List<CandidateWhimAbility> result = new List<CandidateWhimAbility>();


        foreach (var action in waitingPlayerActionList.Select((value, index) => new { value, index }))
        {

            var targetWhimAbility = masterActionList
                .FirstOrDefault(m => m.Name == action.value.abilityName);
            var targetWhimAbilityRequireWhim = targetWhimAbility.requireAbilityForWhim;

            List<Ability_base> whimTargetAbility = targetAbilities
            .Where(m => targetWhimAbilityRequireWhim
                .Select((value, index) => new { value, index })
                .All(b =>
                    // 実行アビリティの思いつき条件技 + 後続最初の実行アビリティの思いつき条件技
                    m.requireAbilityForWhim.Count == baseWhimAbilityCount + targetWhimAbilityRequireWhim.Count &&
                    m.requireAbilityForWhim[(baseWhimAbilityCount - 1) + b.index] == b.value
                    )
                )
            .ToList();

            // 現時点での組み合わせは一致しているが、上位技であるリスト
            List<Ability_base> upperLevelwhimTargetAbility = targetAbilities
            .Where(m => targetWhimAbilityRequireWhim
                .Select((value, index) => new { value, index })
                .All(b =>
                    // 実行アビリティの思いつき条件技 + 後続最初の実行アビリティの思いつき条件技
                    m.requireAbilityForWhim.Count > baseWhimAbilityCount + targetWhimAbilityRequireWhim.Count &&
                    m.requireAbilityForWhim[(baseWhimAbilityCount - 1) + b.index] == b.value
                    )
                )
            .ToList();

            // 候補のアビリティがない場合は以降も存在しないため中断
            if (!whimTargetAbility.Any())
            {
                break;
            }

            cancledAbilityName.Add(targetWhimAbility.Name);

            // 思いつき技数が一致したものをリストに投入
            result.Add(new CandidateWhimAbility()
            {
                Ability = whimTargetAbility,
                canceledAbilities = cancledAbilityName
            });

            // baseを今回思いつき技で取得したリストに更新
            baseWhimAbilityCount += targetWhimAbilityRequireWhim.Count;
            targetAbilities = upperLevelwhimTargetAbility;
        }

        return result;
    }

    public CandidateWhimAbility JudgeWhimAbility(List<CandidateWhimAbility> candidateWhimAbilities)
    {
        List<Ability_base> candidateWhimAbility = new List<Ability_base>();

        foreach (var whimAbility in candidateWhimAbilities)
        {
            candidateWhimAbility.AddRange(whimAbility.Ability);
        }
        candidateWhimAbility = candidateWhimAbility.OrderByDescending(c => c.Level).ToList();

        foreach (var targetAbility in candidateWhimAbility)
        {
            var targetAbilityLevel = targetAbility.Level;
            float targetAbilityWhimRate = MasterData.instance.masterDoWhimRateList.FirstOrDefault(m => m.abilityLevel == targetAbilityLevel).whimRate;

            // 判定: 技レベルが高いものから実施
            // TODO: 確率増加要素
            if (this.CheckDoWhim(targetAbilityWhimRate))
            {
                return new CandidateWhimAbility()
                {
                    Ability = new List<Ability_base> { targetAbility },
                    canceledAbilities = candidateWhimAbilities
                        .FirstOrDefault(c => c.Ability.Contains(targetAbility)).canceledAbilities,
                };
            }
        }

        return null;
    }

    private bool CheckDoWhim(float rate)
    {
        return UnityEngine.Random.value < rate ? true : false;
    }
}
