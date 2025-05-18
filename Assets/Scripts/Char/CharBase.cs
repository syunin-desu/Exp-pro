using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using NUnit.Framework.Internal;


//敵や、プレイヤーなどのキャラの派生元
public class CharBase : MonoBehaviour
{

    // キャラのパラメーター
    private CharParameter charParameters;

    //防御フラグ
    private bool action_defense = false;
    //キャラロール
    public int char_role;
    // 1ターン中の行動回数
    // 行動回数の増減はパラメータを直接いじらず、この変数を返して実施すること
    public int countActionATurn;


    //ターン終了時の処理
    public void resetTurnEnd_char_parameter()
    {
        this.reset_defense_flag();
    }

    //攻撃する
    public virtual void Attack(CharBase character)
    {

        character.Damage(this.charParameters.STR);

    }

    //ダメージを受ける
    public virtual void Damage(int damage)
    {
        //ダメージ計算
        // TODO ダメージ計算式を見直し
        //防御アクションによるダメージ減少率を設定
        float defenceRate = this.action_defense ? CONST.BATTLE_RATE.RATE_DEFENCE : CONST.BATTLE_RATE.RATE_DEFAULT_DEFENCE;
        int result_damage = (int)Math.Ceiling(damage / defenceRate);
        this.charParameters.currentHP -= result_damage;

        Debug.Log($"Damage ={result_damage}");

        if (this.charParameters.currentHP <= 0)
        {
            this.charParameters.currentHP = 0;
        }
    }

    /// <summary>
    /// HPを回復する
    /// </summary>
    /// <param name="healValue">回復量</param>
    public virtual void HealHP(int healValue)
    {

        Debug.Log("beforeHp:" + this.charParameters.currentHP);
        this.charParameters.currentHP += healValue;
        if (this.charParameters.currentHP > this.charParameters.maxHp)
        {
            this.charParameters.currentHP = this.charParameters.maxHp;
        }
        Debug.Log("afterHp:" + this.charParameters.currentHP);

    }

    /// <summary>
    /// MPを回復する
    /// </summary>
    /// <param name="healValue">回復量</param>
    public virtual void HealMP(int healValue)
    {

        Debug.Log("beforeMp:" + this.charParameters.currentMP);
        this.charParameters.currentMP += healValue;
        if (this.charParameters.currentMP > this.charParameters.maxMp)
        {
            this.charParameters.currentMP = this.charParameters.maxMp;
        }
        Debug.Log("afterMp:" + this.charParameters.currentMP);

    }

    /// <summary>
    /// MPが消費できれば消費、できなければfalseを返す
    /// </summary>
    /// <param name="ConsumeValue"></param>
    public bool ConsumeMP(int ConsumeValue)
    {
        Debug.Log("beforeMp:" + this.charParameters.currentMP);
        if (this.charParameters.currentMP > ConsumeValue)
        {
            this.charParameters.currentMP -= ConsumeValue;
            Debug.Log("AfterMp:" + this.charParameters.currentMP);
            return true;
        }
        else
        {
            // MPが足りていなかったためfalseを返す
            return false;
        }
    }

    //バフのリセット
    public void resetBuff()
    {
        //防御をやめる
        this.reset_defense_flag();
    }

    // 防御する
    public void Defense()
    {
        this.action_defense = true;
    }

    //防御フラグをリセット
    private void reset_defense_flag()
    {
        this.action_defense = false;

    }

    //防御フラグを取得
    public bool get_actionDefense()
    {
        return this.action_defense;
    }

    //パラメータセット
    public void SetParameter(CharParameter charParameter)
    {
        this.charParameters = charParameter;

        countActionATurn = this.charParameters.countOfActions;

    }

    //=============
    // getter,setter
    //=============

    // charParameter
    public CharParameter GetCharParameters()
    {
        return this.charParameters;
    }

    //name
    public string GetName()
    {
        return this.charParameters.Name;
    }

    //hp
    public int GetHp()
    {
        return this.charParameters.currentHP;
    }

    public int GetMaxHp()
    {
        return this.charParameters.maxHp;
    }

    public int GetMp()
    {
        return this.charParameters.currentMP;
    }

    public int GetMaxMp()
    {
        return this.charParameters.maxMp;
    }

    public int GetAttackParameter()
    {
        var attack = this.GetEquiCharParameter(CONST.CHARCTOR.ParameterCategory.ATTACK);
        return attack >= CONST.CHARCTOR.MAXCHARPARAMETERVALUE_1 ? CONST.CHARCTOR.MAXCHARPARAMETERVALUE_1 : attack;
    }

    public int GetDefenceParameter()
    {
        var defence = this.GetEquiCharParameter(CONST.CHARCTOR.ParameterCategory.DEFENCE);
        return defence >= CONST.CHARCTOR.MAXCHARPARAMETERVALUE_1 ? CONST.CHARCTOR.MAXCHARPARAMETERVALUE_1 : defence;
    }

    //speed
    public int GetSpeed()
    {
        var spd = this.charParameters.SPEED + GetEquiCharParameter(CONST.CHARCTOR.ParameterCategory.SPD);
        return spd >= CONST.CHARCTOR.MAXCHARPARAMETERVALUE_2 ? CONST.CHARCTOR.MAXCHARPARAMETERVALUE_2 : spd;
    }

    public int GetMagicPoser()
    {
        var mgc = this.charParameters.SPEED + GetEquiCharParameter(CONST.CHARCTOR.ParameterCategory.MGC);
        return mgc >= CONST.CHARCTOR.MAXCHARPARAMETERVALUE_2 ? CONST.CHARCTOR.MAXCHARPARAMETERVALUE_2 : mgc;
    }

    // INT
    public int GetInteli()
    {
        var inteligence = this.charParameters.SPEED + GetEquiCharParameter(CONST.CHARCTOR.ParameterCategory.INT);
        return inteligence >= CONST.CHARCTOR.MAXCHARPARAMETERVALUE_2 ? CONST.CHARCTOR.MAXCHARPARAMETERVALUE_2 : inteligence;
    }

    public int GetKindness()
    {
        var kid = this.charParameters.SPEED + GetEquiCharParameter(CONST.CHARCTOR.ParameterCategory.KID);
        return kid >= CONST.CHARCTOR.MAXCHARPARAMETERVALUE_2 ? CONST.CHARCTOR.MAXCHARPARAMETERVALUE_2 : kid;
    }

    //strange
    public int GetStrange()
    {
        var str = this.charParameters.SPEED + GetEquiCharParameter(CONST.CHARCTOR.ParameterCategory.STR);
        return str >= CONST.CHARCTOR.MAXCHARPARAMETERVALUE_2 ? CONST.CHARCTOR.MAXCHARPARAMETERVALUE_2 : str;
    }

    public int GetDefence()
    {
        var def = this.charParameters.SPEED + GetEquiCharParameter(CONST.CHARCTOR.ParameterCategory.DEF);
        return def >= CONST.CHARCTOR.MAXCHARPARAMETERVALUE_2 ? CONST.CHARCTOR.MAXCHARPARAMETERVALUE_2 : def;
    }

    private int GetEquiCharParameter(CONST.CHARCTOR.ParameterCategory parameterCategory)
    {
        PlayerEquipData equip = this.charParameters.equipDatas;
        List<EquipBase> armedEquipList = new List<EquipBase>();
        armedEquipList.Add(equip.weaponData as EquipBase);
        armedEquipList.Add((EquipBase)equip.armedHead);
        armedEquipList.Add((EquipBase)equip.armedBody);
        armedEquipList.Add((EquipBase)equip.armedAccessory_1);
        armedEquipList.Add((EquipBase)equip.armedAccessory_2);

        int result = 0;
        switch (parameterCategory)
        {
            case CONST.CHARCTOR.ParameterCategory.ATTACK:
                foreach (var armedEquip in armedEquipList)
                {
                    if (armedEquip.category == CONST.ITEM.CATEGORY.WEAPON_ITEM)
                    {
                        result += armedEquip.Attack;
                    }
                }
                return result;
            case CONST.CHARCTOR.ParameterCategory.DEFENCE:
                foreach (var armedEquip in armedEquipList)
                {
                    if (armedEquip.category != CONST.ITEM.CATEGORY.WEAPON_ITEM)
                    {
                        result += armedEquip.Defence;
                    }
                }
                return result;
            case CONST.CHARCTOR.ParameterCategory.MAXHP:
                foreach (var armedEquip in armedEquipList)
                {
                    result += armedEquip.addMaxHp;
                }
                return result;
            case CONST.CHARCTOR.ParameterCategory.MAXMP:
                foreach (var armedEquip in armedEquipList)
                {
                    result += armedEquip.addMaxMp;
                }
                return result;
            case CONST.CHARCTOR.ParameterCategory.STR:
                foreach (var armedEquip in armedEquipList)
                {
                    result += armedEquip.addSTR;
                }
                return result;
            case CONST.CHARCTOR.ParameterCategory.DEF:
                foreach (var armedEquip in armedEquipList)
                {
                    result += armedEquip.addDEF;
                }
                return result;
            case CONST.CHARCTOR.ParameterCategory.SPD:
                foreach (var armedEquip in armedEquipList)
                {
                    result += armedEquip.addSPD;
                }
                return result;
            case CONST.CHARCTOR.ParameterCategory.MGC:
                foreach (var armedEquip in armedEquipList)
                {
                    result += armedEquip.addMagicPower;
                }
                return result;
            case CONST.CHARCTOR.ParameterCategory.INT:
                foreach (var armedEquip in armedEquipList)
                {
                    result += armedEquip.addINT;
                }
                return result;
            case CONST.CHARCTOR.ParameterCategory.KID:
                foreach (var armedEquip in armedEquipList)
                {
                    result += armedEquip.Kindness;
                }
                return result;
            default:
                Debug.Log("Not Target Parameter Category");
                return 0;
        }
    }

    //所持アビリティ
    public List<Ability_base> GetHavingAbilities()
    {
        return this.charParameters.HavingAbility;
    }

    //特定のカテゴリの所持アビリティを取得
    public List<Ability_base> GetHavingAbilitiesForCategory(CONST.ABILITY.Category targetCategory)
    {
        return this.charParameters.HavingAbility.Where(t => t.category == targetCategory).ToList();
    }

    /// <summary>
    /// 装備中のアイテムを取得
    /// </summary>
    /// <returns></returns>
    public PlayerEquipData GetArmedEquip()
    {
        return this.charParameters.equipDatas;
    }

    public EquipBase UpdateEquip(CONST.ITEM.CATEGORY partsCategory,
        EquipBase targetEquip,
        CONST.EQUIP.PARTS_CATEGORY currentSelectedArmedParts)
    {
        if (partsCategory != targetEquip.category)
        {
            Debug.Log("指定されている部位と装備の部位が異なっています");
            return null;
        }
        switch (partsCategory)
        {
            case CONST.ITEM.CATEGORY.WEAPON_ITEM:
                EquipBase equipedWepon = this.charParameters.equipDatas.weaponData;
                this.charParameters.equipDatas.weaponData = targetEquip as WeaponData;
                return equipedWepon;

            case CONST.ITEM.CATEGORY.HEAD_EQUIP_ITEM:
                EquipBase equipedHead = this.charParameters.equipDatas.armedHead;
                this.charParameters.equipDatas.armedHead = targetEquip as HeadData;
                return equipedHead;

            case CONST.ITEM.CATEGORY.BODY_EQUIP_ITEM:
                EquipBase equipedBody = this.charParameters.equipDatas.armedBody;
                this.charParameters.equipDatas.armedBody = targetEquip as BodyData;
                return equipedBody;

            case CONST.ITEM.CATEGORY.ACCESSORY_ITEM:
                if (currentSelectedArmedParts == CONST.EQUIP.PARTS_CATEGORY.ACCESSORY1)
                {
                    EquipBase equipedAccessory1 = this.charParameters.equipDatas.armedAccessory_1;
                    this.charParameters.equipDatas.armedAccessory_1 = targetEquip as AccessoryData;
                    return equipedAccessory1;
                }
                else if (currentSelectedArmedParts == CONST.EQUIP.PARTS_CATEGORY.ACCESSORY2)
                {
                    EquipBase equipedAccessory2 = this.charParameters.equipDatas.armedAccessory_2;
                    this.charParameters.equipDatas.armedAccessory_2 = targetEquip as AccessoryData;
                    return equipedAccessory2;
                }
                else
                {
                    return null;
                }

            default:
                return null;

        }
    }


    // 弱点属性
    public List<CONST.UTILITY.Element> GetWeakElement()
    {
        return this.charParameters.WeakElement;
    }

    // 耐性属性
    public List<CONST.UTILITY.Element> GetStrongElement()
    {
        return this.charParameters.StrongElement;
    }

    // 行動回数を返す
    public int GetCharActionCount()
    {
        // パラメータから直接取得せず、別変数を返して取得する
        return this.countActionATurn;
    }

    // パラメーター一覧を返す
    public CharParameter GetCharData()
    {
        return this.charParameters;
    }
}
