using UnityEditor;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using CONST.ACTION;
using System;
using System.Linq;
using NUnit.Framework.Internal;

[ExecuteInEditMode]
public class ImportBaseClass : MonoBehaviour
{
    public void OnClickedAbilityImport()
    {
        if (this.ClearMasterData("Assets/Resources/Data/MasterDatas/ability/"))
        {
            List<string[]> csvDatas = this.ImportMasterDataFromCSV("Data/MasterDataCsv/AbilityMaster");
            // Levelが小さいアビリティからScriptableObjectを生成
            // WhimAbilityを設定する際のエラー対策
            csvDatas.OrderBy(s => s[4]);

            foreach (var csvData in csvDatas)
            {
                var tempAbilityObj = ScriptableObject.CreateInstance<Ability_base>();

                var category = this.ConvertCSVToAbilityCategory(csvData[1]);
                if (category is null)
                {
                    Debug.Log("[" + csvData[1] + "]カテゴリに不正な値が入力されています");
                }

                var type = this.ConvertCSVToAbilityType(csvData[5]);
                if (type is null)
                {
                    Debug.Log("[" + csvData[5] + "]タイプに不正な値が入力されています");
                }

                tempAbilityObj.id = csvData[0];
                tempAbilityObj.category = category ?? CONST.ABILITY.Category.Default;
                tempAbilityObj.Name = csvData[2];
                tempAbilityObj.displayName = csvData[3];
                tempAbilityObj.Level = int.Parse(csvData[4]);
                tempAbilityObj.Type = type ?? CONST.ACTION.TYPE.Attack;
                tempAbilityObj.timingType = (CONST.ACTION.Speed)Enum.Parse(typeof(CONST.ACTION.Speed), csvData[6]);
                tempAbilityObj.speed_rank = int.Parse(csvData[7]);
                tempAbilityObj.Range = (CONST.ACTION.Range)Enum.Parse(typeof(CONST.ACTION.Range), csvData[8]);
                tempAbilityObj.requiredMp = int.Parse(csvData[9]);
                tempAbilityObj.Element = (CONST.UTILITY.Element)Enum.Parse(typeof(CONST.UTILITY.Element), csvData[10]);
                tempAbilityObj.power = int.Parse(csvData[11]);
                var tempActionCell = csvData[12].Split('-');
                List<CONST.ACTION.Ability_Action_Cell> tempExecuteActionList = new List<Ability_Action_Cell>();
                foreach (var tempAction in tempActionCell)
                {
                    Debug.Log(tempAction);
                    var targetAction = (CONST.ACTION.Ability_Action_Cell)
                        Enum.Parse(typeof(CONST.ACTION.Ability_Action_Cell), tempAction, true);
                    tempExecuteActionList.Add(targetAction);
                }
                tempAbilityObj.executeActionList = tempExecuteActionList;

                var tempBuffs = csvData[14].Split('-');
                List<BuffData> buffs = new List<BuffData>();
                foreach (var tempBuff in tempBuffs)
                {
                    if (tempBuff == "")
                    {
                        continue;
                    }
                    var targetBuff = this.GetBuffMasterDataFromAsset_MasterDataCreator()
                        .FirstOrDefault(a => a.buffName == tempBuff);
                    buffs.Add(
                         targetBuff
                        );
                }
                tempAbilityObj.buffs = buffs;
                tempAbilityObj.description = csvData[15];
                var savedPath = "Assets/Resources/Data/MasterDatas/ability/";
                AssetDatabase.CreateAsset(tempAbilityObj, Path.Combine(savedPath, tempAbilityObj.Name.ToString()) + ".asset");
            }


            Debug.Log("アビリティのインポート完了");
        }
        else
        {
            Debug.Log("既存アセットのリセット失敗");
        }
    }

    public void OnClickedAbilityWhimSetting()
    {

        List<string[]> csvDatas = this.ImportMasterDataFromCSV("Data/MasterDataCsv/AbilityMaster");
        var masterAbilityData = GetAbilityMasterDataFromAsset_MasterDataCreator();
        foreach (var csvData in csvDatas)
        {

            var targetAbility = masterAbilityData.FirstOrDefault(m => m.id == csvData[0]);
            var tempWhimActions = csvData[13].Split('-');
            List<Ability_base> tempRequireAbilityForWhim = new List<Ability_base>();
            foreach (var tempAction in tempWhimActions)
            {
                if (tempAction == "")
                {
                    continue;
                }
                Ability_base targetWhim = masterAbilityData
                    .FirstOrDefault(a => a.Name == tempAction);
                tempRequireAbilityForWhim.Add(
                     targetWhim
                    );
            }
            targetAbility.requireAbilityForWhim = tempRequireAbilityForWhim;
        }

        Debug.Log("アビリティWhim設定完了");

    }

    public void OnClickedItemImport()
    {
        if (this.ClearMasterData("Assets/Resources/Data/MasterDatas/Item/"))
        {
            List<string[]> csvDatas = this.ImportMasterDataFromCSV("Data/MasterDataCsv/ItemMaster");

            foreach (var csvData in csvDatas)
            {
                var tempItemObj = ScriptableObject.CreateInstance<UsedItemData>();

                tempItemObj.Name = csvData[0];
                tempItemObj.displayName = csvData[1];
                tempItemObj.id = csvData[2];
                tempItemObj.Rarity = int.Parse(csvData[3]);
                tempItemObj.category = (CONST.ITEM.CATEGORY)Enum.Parse(typeof(CONST.ITEM.CATEGORY), csvData[4]);
                tempItemObj.PurchasePrice = int.Parse(csvData[5]);
                tempItemObj.Sellingrice = int.Parse(csvData[6]);
                tempItemObj.item_description = csvData[7];
                tempItemObj.Type = (CONST.ACTION.TYPE)Enum.Parse(typeof(CONST.ACTION.TYPE), csvData[8]);
                tempItemObj.Target_status = (CONST.ACTION.TARGET_STATUS)Enum.Parse(typeof(CONST.ACTION.TARGET_STATUS), csvData[9]);
                tempItemObj.speed_rank = int.Parse(csvData[10]);
                tempItemObj.Range = (CONST.ACTION.Range)Enum.Parse(typeof(CONST.ACTION.Range), csvData[11]);
                tempItemObj.Element = (CONST.UTILITY.Element)Enum.Parse(typeof(CONST.UTILITY.Element), csvData[12]);
                tempItemObj.value = int.Parse(csvData[13]);
                List<CONST.ITEM.ADDBUFF> tempUsedBuff = new List<CONST.ITEM.ADDBUFF>();
                var tempBuffs = csvData[14].Split('-');
                foreach (var tempBuff in tempBuffs)
                {
                    if (tempBuff == "")
                    {
                        continue;
                    }
                    tempUsedBuff.Add(
                         (CONST.ITEM.ADDBUFF)Enum.Parse(typeof(CONST.ITEM.ADDBUFF), tempBuff)
                        );
                }
                tempItemObj.Usedbuff = tempUsedBuff;
                List<CONST.ITEM.ADDDEBUFF> tempUsedDebuff = new List<CONST.ITEM.ADDDEBUFF>();
                var tempDeBuffs = csvData[15].Split('-');
                foreach (var tempDeBuff in tempDeBuffs)
                {
                    if (tempDeBuff == "")
                    {
                        continue;
                    }
                    tempUsedDebuff.Add(
                         (CONST.ITEM.ADDDEBUFF)Enum.Parse(typeof(CONST.ITEM.ADDDEBUFF), tempDeBuff)
                        );
                }
                tempItemObj.Useddebuff = tempUsedDebuff;



                var savedPath = "Assets/Resources/Data/MasterDatas/Item/";
                AssetDatabase.CreateAsset(tempItemObj, Path.Combine(savedPath, tempItemObj.Name) + ".asset");
            }

            Debug.Log("Itemのインポート完了");
        }
        else
        {
            Debug.Log("既存アセットのリセット失敗");
        }
    }
    public void OnClickedEquipImport()
    {
        if (this.ClearMasterData("Assets/Resources/Data/MasterDatas/Equip/ACCESSORY_ITEM/") &&
            this.ClearMasterData("Assets/Resources/Data/MasterDatas/Equip/BODY_EQUIP_ITEM/") &&
            this.ClearMasterData("Assets/Resources/Data/MasterDatas/Equip/HEAD_EQUIP_ITEM/") &&
            this.ClearMasterData("Assets/Resources/Data/MasterDatas/Equip/WEAPON_ITEM/"))
        {
            List<string[]> csvDatas = this.ImportMasterDataFromCSV("Data/MasterDataCsv/EquipMaster");
            var savedMasterFolderPath = "Assets/Resources/Data/MasterDatas/Equip/";

            foreach (var csvData in csvDatas)
            {

                var category = (CONST.ITEM.CATEGORY)Enum.Parse(typeof(CONST.ITEM.CATEGORY), csvData[4]);

                switch (category)
                {
                    case CONST.ITEM.CATEGORY.WEAPON_ITEM:
                        this.CreateWeponMasterData(csvData, category, savedMasterFolderPath);
                        break;
                    case CONST.ITEM.CATEGORY.BODY_EQUIP_ITEM:
                        this.CreateBodyMasterData(csvData, category, savedMasterFolderPath);
                        break;
                    case CONST.ITEM.CATEGORY.HEAD_EQUIP_ITEM:
                        this.CreateHeadMasterData(csvData, category, savedMasterFolderPath);
                        break;
                    case CONST.ITEM.CATEGORY.ACCESSORY_ITEM:
                        this.CreateAccessoryMasterData(csvData, category, savedMasterFolderPath);
                        break;
                }

            }

            Debug.Log("Itemのインポート完了");
        }
        else
        {
            Debug.Log("既存アセットのリセット失敗");
        }
    }

    private void CreateWeponMasterData(string[] csvData, CONST.ITEM.CATEGORY category, string savePath)
    {
        var tempEquipObj = ScriptableObject.CreateInstance<WeaponData>();

        tempEquipObj.Name = csvData[0];
        tempEquipObj.displayName = csvData[1];
        tempEquipObj.id = csvData[2];
        tempEquipObj.Rarity = int.Parse(csvData[3]);
        tempEquipObj.PurchasePrice = int.Parse(csvData[5]);
        tempEquipObj.Sellingrice = int.Parse(csvData[6]);
        tempEquipObj.item_description = csvData[7];
        tempEquipObj.Attack = int.Parse(csvData[8]);
        tempEquipObj.Defence = int.Parse(csvData[9]);
        tempEquipObj.addMaxHp = int.Parse(csvData[10]);
        tempEquipObj.addMaxMp = int.Parse(csvData[11]);
        tempEquipObj.addSTR = int.Parse(csvData[12]);
        tempEquipObj.addDEF = int.Parse(csvData[13]);
        tempEquipObj.addSPD = int.Parse(csvData[14]);
        tempEquipObj.addMagicPower = int.Parse(csvData[15]);
        tempEquipObj.addINT = int.Parse(csvData[16]);
        tempEquipObj.Kindness = int.Parse(csvData[17]);
        List<CONST.ITEM.ADDBUFF> tempUsedBuff = new List<CONST.ITEM.ADDBUFF>();
        var tempBuffs = csvData[18].Split('-');
        foreach (var tempBuff in tempBuffs)
        {
            if (tempBuff == "")
            {
                continue;
            }
            tempUsedBuff.Add(
                 (CONST.ITEM.ADDBUFF)Enum.Parse(typeof(CONST.ITEM.ADDBUFF), tempBuff)
                );
        }
        tempEquipObj.buff = tempUsedBuff;
        List<CONST.ITEM.ADDDEBUFF> tempUsedDebuff = new List<CONST.ITEM.ADDDEBUFF>();
        var tempDeBuffs = csvData[19].Split('-');
        foreach (var tempDeBuff in tempDeBuffs)
        {
            if (tempDeBuff == "")
            {
                continue;
            }
            tempUsedDebuff.Add(
                 (CONST.ITEM.ADDDEBUFF)Enum.Parse(typeof(CONST.ITEM.ADDDEBUFF), tempDeBuff)
                );
        }
        tempEquipObj.debuff = tempUsedDebuff;



        var savedPath = savePath + tempEquipObj.category.ToString() + "/";
        AssetDatabase.CreateAsset(tempEquipObj, Path.Combine(savedPath, tempEquipObj.Name) + ".asset");
    }

    // 内部処理は上記関数と同様
    private void CreateBodyMasterData(string[] csvData, CONST.ITEM.CATEGORY category, string savePath)
    {
        var tempEquipObj = ScriptableObject.CreateInstance<BodyData>();

        tempEquipObj.Name = csvData[0];
        tempEquipObj.displayName = csvData[1];
        tempEquipObj.id = csvData[2];
        tempEquipObj.Rarity = int.Parse(csvData[3]);
        tempEquipObj.PurchasePrice = int.Parse(csvData[5]);
        tempEquipObj.Sellingrice = int.Parse(csvData[6]);
        tempEquipObj.item_description = csvData[7];
        tempEquipObj.Attack = int.Parse(csvData[8]);
        tempEquipObj.Defence = int.Parse(csvData[9]);
        tempEquipObj.addMaxHp = int.Parse(csvData[10]);
        tempEquipObj.addMaxMp = int.Parse(csvData[11]);
        tempEquipObj.addSTR = int.Parse(csvData[12]);
        tempEquipObj.addDEF = int.Parse(csvData[13]);
        tempEquipObj.addSPD = int.Parse(csvData[14]);
        tempEquipObj.addMagicPower = int.Parse(csvData[15]);
        tempEquipObj.addINT = int.Parse(csvData[16]);
        tempEquipObj.Kindness = int.Parse(csvData[17]);
        List<CONST.ITEM.ADDBUFF> tempUsedBuff = new List<CONST.ITEM.ADDBUFF>();
        var tempBuffs = csvData[18].Split('-');
        foreach (var tempBuff in tempBuffs)
        {
            if (tempBuff == "")
            {
                continue;
            }
            tempUsedBuff.Add(
                 (CONST.ITEM.ADDBUFF)Enum.Parse(typeof(CONST.ITEM.ADDBUFF), tempBuff)
                );
        }
        tempEquipObj.buff = tempUsedBuff;
        List<CONST.ITEM.ADDDEBUFF> tempUsedDebuff = new List<CONST.ITEM.ADDDEBUFF>();
        var tempDeBuffs = csvData[19].Split('-');
        foreach (var tempDeBuff in tempDeBuffs)
        {
            if (tempDeBuff == "")
            {
                continue;
            }
            tempUsedDebuff.Add(
                 (CONST.ITEM.ADDDEBUFF)Enum.Parse(typeof(CONST.ITEM.ADDDEBUFF), tempDeBuff)
                );
        }
        tempEquipObj.debuff = tempUsedDebuff;



        var savedPath = savePath + tempEquipObj.category.ToString() + "/";
        AssetDatabase.CreateAsset(tempEquipObj, Path.Combine(savedPath, tempEquipObj.Name) + ".asset");
    }

    // 内部処理は上記関数と同様
    private void CreateHeadMasterData(string[] csvData, CONST.ITEM.CATEGORY category, string savePath)
    {
        var tempEquipObj = ScriptableObject.CreateInstance<HeadData>();

        tempEquipObj.Name = csvData[0];
        tempEquipObj.displayName = csvData[1];
        tempEquipObj.id = csvData[2];
        tempEquipObj.Rarity = int.Parse(csvData[3]);
        tempEquipObj.PurchasePrice = int.Parse(csvData[5]);
        tempEquipObj.Sellingrice = int.Parse(csvData[6]);
        tempEquipObj.item_description = csvData[7];
        tempEquipObj.Attack = int.Parse(csvData[8]);
        tempEquipObj.Defence = int.Parse(csvData[9]);
        tempEquipObj.addMaxHp = int.Parse(csvData[10]);
        tempEquipObj.addMaxMp = int.Parse(csvData[11]);
        tempEquipObj.addSTR = int.Parse(csvData[12]);
        tempEquipObj.addDEF = int.Parse(csvData[13]);
        tempEquipObj.addSPD = int.Parse(csvData[14]);
        tempEquipObj.addMagicPower = int.Parse(csvData[15]);
        tempEquipObj.addINT = int.Parse(csvData[16]);
        tempEquipObj.Kindness = int.Parse(csvData[17]);
        List<CONST.ITEM.ADDBUFF> tempUsedBuff = new List<CONST.ITEM.ADDBUFF>();
        var tempBuffs = csvData[18].Split('-');
        foreach (var tempBuff in tempBuffs)
        {
            if (tempBuff == "")
            {
                continue;
            }
            tempUsedBuff.Add(
                 (CONST.ITEM.ADDBUFF)Enum.Parse(typeof(CONST.ITEM.ADDBUFF), tempBuff)
                );
        }
        tempEquipObj.buff = tempUsedBuff;
        List<CONST.ITEM.ADDDEBUFF> tempUsedDebuff = new List<CONST.ITEM.ADDDEBUFF>();
        var tempDeBuffs = csvData[19].Split('-');
        foreach (var tempDeBuff in tempDeBuffs)
        {
            if (tempDeBuff == "")
            {
                continue;
            }
            tempUsedDebuff.Add(
                 (CONST.ITEM.ADDDEBUFF)Enum.Parse(typeof(CONST.ITEM.ADDDEBUFF), tempDeBuff)
                );
        }
        tempEquipObj.debuff = tempUsedDebuff;



        var savedPath = savePath + tempEquipObj.category.ToString() + "/";
        AssetDatabase.CreateAsset(tempEquipObj, Path.Combine(savedPath, tempEquipObj.Name) + ".asset");
    }

    // 内部処理は上記関数と同様
    private void CreateAccessoryMasterData(string[] csvData, CONST.ITEM.CATEGORY category, string savePath)
    {
        var tempEquipObj = ScriptableObject.CreateInstance<AccessoryData>();

        tempEquipObj.Name = csvData[0];
        tempEquipObj.displayName = csvData[1];
        tempEquipObj.id = csvData[2];
        tempEquipObj.Rarity = int.Parse(csvData[3]);
        tempEquipObj.PurchasePrice = int.Parse(csvData[5]);
        tempEquipObj.Sellingrice = int.Parse(csvData[6]);
        tempEquipObj.item_description = csvData[7];
        tempEquipObj.Attack = int.Parse(csvData[8]);
        tempEquipObj.Defence = int.Parse(csvData[9]);
        tempEquipObj.addMaxHp = int.Parse(csvData[10]);
        tempEquipObj.addMaxMp = int.Parse(csvData[11]);
        tempEquipObj.addSTR = int.Parse(csvData[12]);
        tempEquipObj.addDEF = int.Parse(csvData[13]);
        tempEquipObj.addSPD = int.Parse(csvData[14]);
        tempEquipObj.addMagicPower = int.Parse(csvData[15]);
        tempEquipObj.addINT = int.Parse(csvData[16]);
        tempEquipObj.Kindness = int.Parse(csvData[17]);
        List<CONST.ITEM.ADDBUFF> tempUsedBuff = new List<CONST.ITEM.ADDBUFF>();
        var tempBuffs = csvData[18].Split('-');
        foreach (var tempBuff in tempBuffs)
        {
            if (tempBuff == "")
            {
                continue;
            }
            tempUsedBuff.Add(
                 (CONST.ITEM.ADDBUFF)Enum.Parse(typeof(CONST.ITEM.ADDBUFF), tempBuff)
                );
        }
        tempEquipObj.buff = tempUsedBuff;
        List<CONST.ITEM.ADDDEBUFF> tempUsedDebuff = new List<CONST.ITEM.ADDDEBUFF>();
        var tempDeBuffs = csvData[19].Split('-');
        foreach (var tempDeBuff in tempDeBuffs)
        {
            if (tempDeBuff == "")
            {
                continue;
            }
            tempUsedDebuff.Add(
                 (CONST.ITEM.ADDDEBUFF)Enum.Parse(typeof(CONST.ITEM.ADDDEBUFF), tempDeBuff)
                );
        }
        tempEquipObj.debuff = tempUsedDebuff;



        var savedPath = savePath + tempEquipObj.category.ToString() + "/";
        AssetDatabase.CreateAsset(tempEquipObj, Path.Combine(savedPath, tempEquipObj.Name) + ".asset");
    }

    public void OnClickedEnemyImport()
    {
        if (this.ClearMasterData("Assets/Resources/Data/MasterDatas/Enemy/"))
        {

            List<string[]> csvDatas = this.ImportMasterDataFromCSV("Data/MasterDataCsv/EnemyMaster");

            foreach (var csvData in csvDatas)
            {

                var tempEnemyObj = ScriptableObject.CreateInstance<CharData>();

                tempEnemyObj.Name = csvData[0];
                tempEnemyObj.currentHP = int.Parse(csvData[1]);
                tempEnemyObj.currentMP = int.Parse(csvData[2]);
                tempEnemyObj.maxHp = int.Parse(csvData[3]);
                tempEnemyObj.maxMp = int.Parse(csvData[4]);
                tempEnemyObj.maxMp = int.Parse(csvData[4]);
                tempEnemyObj.STR = int.Parse(csvData[5]);
                tempEnemyObj.DEF = int.Parse(csvData[6]);
                tempEnemyObj.SPEED = int.Parse(csvData[7]);
                tempEnemyObj.MagicPower = int.Parse(csvData[8]);
                tempEnemyObj.INT = int.Parse(csvData[9]);
                tempEnemyObj.Kindness = int.Parse(csvData[10]);
                tempEnemyObj.ROLE = (CONST.CHARCTOR.Role)Enum.Parse(typeof(CONST.CHARCTOR.Role), csvData[11]);
                tempEnemyObj.countOfActions = int.Parse(csvData[12]);
                tempEnemyObj.charClass = (CONST.CHARCTOR.Class)Enum.Parse(typeof(CONST.CHARCTOR.Class), csvData[13]);
                List<Ability_base> tempHavingAbility = new List<Ability_base>();
                var tempHavingAbilitis = csvData[14].Split('-');
                foreach (var havingAbilityName in tempHavingAbilitis)
                {
                    if (havingAbilityName == "")
                    {
                        continue;
                    }
                    tempHavingAbility.Add(
                         this.GetAbilityMasterDataFromAsset_MasterDataCreator().FirstOrDefault(a => a.Name == havingAbilityName)
                        );
                }
                tempEnemyObj.HavingAbility = tempHavingAbility;
                tempEnemyObj.weaponData = this.GetEquipMasterDataFromAsset_MasterDataCreator()
                    .FirstOrDefault(e => e.Name == csvData[15] && e.category == CONST.ITEM.CATEGORY.WEAPON_ITEM) as WeaponData;
                tempEnemyObj.armedHead = this.GetEquipMasterDataFromAsset_MasterDataCreator()
                    .FirstOrDefault(e => e.Name == csvData[16] && e.category == CONST.ITEM.CATEGORY.HEAD_EQUIP_ITEM) as HeadData;
                tempEnemyObj.armedBody = this.GetEquipMasterDataFromAsset_MasterDataCreator()
                    .FirstOrDefault(e => e.Name == csvData[17] && e.category == CONST.ITEM.CATEGORY.BODY_EQUIP_ITEM) as BodyData;
                tempEnemyObj.armedAccessory_1 = this.GetEquipMasterDataFromAsset_MasterDataCreator()
                    .FirstOrDefault(e => e.Name == csvData[18] && e.category == CONST.ITEM.CATEGORY.ACCESSORY_ITEM) as AccessoryData;
                tempEnemyObj.armedAccessory_2 = this.GetEquipMasterDataFromAsset_MasterDataCreator()
                    .FirstOrDefault(e => e.Name == csvData[19] && e.category == CONST.ITEM.CATEGORY.ACCESSORY_ITEM) as AccessoryData;
                var tempWeakElements = csvData[20].Split('-');
                var WeakElements = new List<CONST.UTILITY.Element>();
                foreach (var tempWeakElement in tempWeakElements)
                {
                    if (tempWeakElement == "")
                    {
                        continue;
                    }
                    WeakElements.Add(
                         (CONST.UTILITY.Element)Enum.Parse(typeof(CONST.UTILITY.Element), tempWeakElement)
                        );
                }
                tempEnemyObj.WeakElement = WeakElements;

                var StrongElements = new List<CONST.UTILITY.Element>();
                var tempStrongElements = csvData[21].Split('-');
                foreach (var tempStrongElement in tempStrongElements)
                {
                    if (tempStrongElement == "")
                    {
                        continue;
                    }
                    StrongElements.Add(
                         (CONST.UTILITY.Element)Enum.Parse(typeof(CONST.UTILITY.Element), tempStrongElement)
                        );
                }
                tempEnemyObj.StrongElement = StrongElements;


                var savedPath = "Assets/Resources/Data/MasterDatas/Enemy/";
                AssetDatabase.CreateAsset(tempEnemyObj, Path.Combine(savedPath, tempEnemyObj.Name) + ".asset");
            }

            Debug.Log("Enemyのインポート完了");
        }
        else
        {
            Debug.Log("既存アセットのリセット失敗");
        }
    }

    public void OnClickedPlayerImport()
    {
        if (this.ClearMasterData("Assets/Resources/Data/MasterDatas/Player/"))
        {

            List<string[]> csvDatas = this.ImportMasterDataFromCSV("Data/MasterDataCsv/PlayerMaster");

            foreach (var csvData in csvDatas)
            {

                var tempPlayerObj = ScriptableObject.CreateInstance<CharData>();

                tempPlayerObj.Name = csvData[0];
                tempPlayerObj.currentHP = int.Parse(csvData[1]);
                tempPlayerObj.currentMP = int.Parse(csvData[2]);
                tempPlayerObj.maxHp = int.Parse(csvData[3]);
                tempPlayerObj.maxMp = int.Parse(csvData[4]);
                tempPlayerObj.maxMp = int.Parse(csvData[4]);
                tempPlayerObj.STR = int.Parse(csvData[5]);
                tempPlayerObj.DEF = int.Parse(csvData[6]);
                tempPlayerObj.SPEED = int.Parse(csvData[7]);
                tempPlayerObj.MagicPower = int.Parse(csvData[8]);
                tempPlayerObj.INT = int.Parse(csvData[9]);
                tempPlayerObj.Kindness = int.Parse(csvData[10]);
                tempPlayerObj.ROLE = (CONST.CHARCTOR.Role)Enum.Parse(typeof(CONST.CHARCTOR.Role), csvData[11]);
                tempPlayerObj.countOfActions = int.Parse(csvData[12]);
                tempPlayerObj.charClass = (CONST.CHARCTOR.Class)Enum.Parse(typeof(CONST.CHARCTOR.Class), csvData[13]);
                List<Ability_base> tempHavingAbility = new List<Ability_base>();
                var tempHavingAbilitis = csvData[14].Split('-');
                foreach (var havingAbilityName in tempHavingAbilitis)
                {
                    if (havingAbilityName == "")
                    {
                        continue;
                    }
                    tempHavingAbility.Add(
                         this.GetAbilityMasterDataFromAsset_MasterDataCreator().FirstOrDefault(a => a.Name == havingAbilityName)
                        );
                }
                tempPlayerObj.HavingAbility = tempHavingAbility;
                tempPlayerObj.weaponData = this.GetEquipMasterDataFromAsset_MasterDataCreator()
                    .FirstOrDefault(e => e.Name == csvData[15] && e.category == CONST.ITEM.CATEGORY.WEAPON_ITEM) as WeaponData;
                tempPlayerObj.armedHead = this.GetEquipMasterDataFromAsset_MasterDataCreator()
                    .FirstOrDefault(e => e.Name == csvData[16] && e.category == CONST.ITEM.CATEGORY.HEAD_EQUIP_ITEM) as HeadData;
                tempPlayerObj.armedBody = this.GetEquipMasterDataFromAsset_MasterDataCreator()
                    .FirstOrDefault(e => e.Name == csvData[17] && e.category == CONST.ITEM.CATEGORY.BODY_EQUIP_ITEM) as BodyData;
                tempPlayerObj.armedAccessory_1 = this.GetEquipMasterDataFromAsset_MasterDataCreator()
                    .FirstOrDefault(e => e.Name == csvData[18] && e.category == CONST.ITEM.CATEGORY.ACCESSORY_ITEM) as AccessoryData;
                tempPlayerObj.armedAccessory_2 = this.GetEquipMasterDataFromAsset_MasterDataCreator()
                    .FirstOrDefault(e => e.Name == csvData[19] && e.category == CONST.ITEM.CATEGORY.ACCESSORY_ITEM) as AccessoryData;
                var tempWeakElements = csvData[20].Split('-');
                var WeakElements = new List<CONST.UTILITY.Element>();
                foreach (var tempWeakElement in tempWeakElements)
                {
                    if (tempWeakElement == "")
                    {
                        continue;
                    }
                    WeakElements.Add(
                         (CONST.UTILITY.Element)Enum.Parse(typeof(CONST.UTILITY.Element), tempWeakElement)
                        );
                }
                tempPlayerObj.WeakElement = WeakElements;

                var StrongElements = new List<CONST.UTILITY.Element>();
                var tempStrongElements = csvData[21].Split('-');
                foreach (var tempStrongElement in tempStrongElements)
                {
                    if (tempStrongElement == "")
                    {
                        continue;
                    }
                    StrongElements.Add(
                         (CONST.UTILITY.Element)Enum.Parse(typeof(CONST.UTILITY.Element), tempStrongElement)
                        );
                }
                tempPlayerObj.StrongElement = StrongElements;


                var savedPath = "Assets/Resources/Data/MasterDatas/Player/";
                AssetDatabase.CreateAsset(tempPlayerObj, Path.Combine(savedPath, tempPlayerObj.Name) + ".asset");
            }

            Debug.Log("Playerのインポート完了");
        }
        else
        {
            Debug.Log("既存アセットのリセット失敗");
        }
    }

    public void OnClickedBuffImport()
    {
        if (this.ClearMasterData("Assets/Resources/Data/MasterDatas/Buff/"))
        {
            List<string[]> csvDatas = this.ImportMasterDataFromCSV("Data/MasterDataCsv/BuffMaster");

            foreach (var csvData in csvDatas)
            {
                var tempBuffObj = ScriptableObject.CreateInstance<BuffData>();

                tempBuffObj.buffName = csvData[0];
                tempBuffObj.buffCategory = (CONST.CHARCTOR.BuffCategory)Enum.Parse(typeof(CONST.CHARCTOR.BuffCategory), csvData[1]);
                tempBuffObj.value_degree = int.Parse(csvData[2]);
                tempBuffObj.value_rate = int.Parse(csvData[3]);
                tempBuffObj.effectPeriod_Category = (CONST.CHARCTOR.EffectPeriod_Category)Enum.Parse(typeof(CONST.CHARCTOR.EffectPeriod_Category), csvData[4]);
                tempBuffObj.effectPeriod = int.Parse(csvData[5]);

                var savedPath = "Assets/Resources/Data/MasterDatas/Buff/";
                AssetDatabase.CreateAsset(tempBuffObj, Path.Combine(savedPath, tempBuffObj.buffName.ToString()) + ".asset");
            }

            Debug.Log("Buffのインポート完了");
        }
        else
        {
            Debug.Log("既存アセットのリセット失敗");
        }
    }
    public void OnClickedClassImport()
    {
        if (this.ClearMasterData("Assets/Resources/Data/MasterDatas/BattleClass/"))
        {
            List<string[]> csvDatas = this.ImportMasterDataFromCSV("Data/MasterDataCsv/ClassMaster");

            foreach (var csvData in csvDatas)
            {
                var tempClassObj = ScriptableObject.CreateInstance<Class_Base>();

                tempClassObj.passiveAbilityName = csvData[0];
                tempClassObj.skillAbilityName = csvData[1];
                tempClassObj.UltimateAbilityName = csvData[2];
                tempClassObj.charClass = (CONST.CHARCTOR.Class)Enum.Parse(typeof(CONST.CHARCTOR.Class), csvData[3]);

                var savedPath = "Assets/Resources/Data/MasterDatas/BattleClass/";
                AssetDatabase.CreateAsset(tempClassObj, Path.Combine(savedPath, tempClassObj.charClass.ToString()) + ".asset");
            }

            Debug.Log("Classのインポート完了");
        }
        else
        {
            Debug.Log("既存アセットのリセット失敗");
        }
    }
    public void OnClickedFloorCardImport()
    {
        var _log = "ぷらいべーと！";
        Debug.Log(_log);
    }
    public void OnClickedWhimRateImport()
    {
        if (this.ClearMasterData("Assets/Resources/Data/MasterDatas/WhimAbilityRate/"))
        {
            List<string[]> csvDatas = this.ImportMasterDataFromCSV("Data/MasterDataCsv/WhimRateMaster");

            foreach (var csvData in csvDatas)
            {
                var tempWhimObj = ScriptableObject.CreateInstance<DoWhimRateForAbilityLevel>();

                tempWhimObj.abilityLevel = int.Parse(csvData[0]);
                tempWhimObj.whimRate = float.Parse(csvData[1]);

                var savedPath = "Assets/Resources/Data/MasterDatas/WhimAbilityRate/";
                AssetDatabase.CreateAsset(tempWhimObj, Path.Combine(savedPath, "Level_" + tempWhimObj.abilityLevel.ToString()) + ".asset");
            }

            Debug.Log("Whimのインポート完了");
        }
        else
        {
            Debug.Log("既存アセットのリセット失敗");
        }


    }
    public void OnClickedAllImport()
    {
        var _log = "ぷらいべーと！";
        Debug.Log(_log);
    }


    private List<string[]> ImportMasterDataFromCSV(string MasterDataCsvPath)
    {

        List<string[]> csvDatas = new List<string[]>();
        var whimRateAsset = Resources.Load(MasterDataCsvPath) as TextAsset;

        StringReader reader = new StringReader(whimRateAsset.text);
        bool isHeader = true;

        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine();
            if (!isHeader)
            {
                csvDatas.Add(line.Split(','));
            }
            else
            {
                isHeader = false;
            }
        }
        return csvDatas;
    }

    private bool ClearMasterData(string path)
    {
        string[] unusedFolder = { path };
        foreach (var asset in AssetDatabase.FindAssets("", unusedFolder))
        {
            var deleteTargetPath = AssetDatabase.GUIDToAssetPath(asset);
            if (!AssetDatabase.DeleteAsset(deleteTargetPath))
            {
                return false;
            }
        }
        return true;
    }

    private CONST.ABILITY.Category? ConvertCSVToAbilityCategory(string param)
    {

        switch (param)
        {
            case "Ability":
                return CONST.ABILITY.Category.Ability;
            case "Magic":
                return CONST.ABILITY.Category.Magic;
            case "SwordArts":
                return CONST.ABILITY.Category.SwordArts;
            case "Default":
                return CONST.ABILITY.Category.Default;
            default:
                return null;
        }
    }

    private CONST.ACTION.TYPE? ConvertCSVToAbilityType(string param)
    {

        switch (param)
        {
            case "Attack":
                return CONST.ACTION.TYPE.Attack;
            case "Buff":
                return CONST.ACTION.TYPE.Buff;
            case "DeBuff":
                return CONST.ACTION.TYPE.DeBuff;
            case "Heal":
                return CONST.ACTION.TYPE.Heal;
            case "UseItem":
                return CONST.ACTION.TYPE.UseItem;
            default:
                return null;
        }
    }

    private CONST.ACTION.TYPE? ConvertCSVToTimingType(string param)
    {

        switch (param)
        {
            case "Attack":
                return CONST.ACTION.TYPE.Attack;
            case "Buff":
                return CONST.ACTION.TYPE.Buff;
            case "DeBuff":
                return CONST.ACTION.TYPE.DeBuff;
            case "Heal":
                return CONST.ACTION.TYPE.Heal;
            case "UseItem":
                return CONST.ACTION.TYPE.UseItem;
            default:
                return null;
        }
    }

    private List<Ability_base> GetAbilityMasterDataFromAsset_MasterDataCreator()
    {
        return Resources
        .LoadAll("Data/MasterDatas/ability/", typeof(Ability_base))
        .Cast<Ability_base>()
        .ToList();
    }

    private List<BuffData> GetBuffMasterDataFromAsset_MasterDataCreator()
    {
        return Resources
        .LoadAll("Data/MasterDatas/Buff/", typeof(BuffData))
        .Cast<BuffData>()
        .ToList();
    }

    private List<EquipBase> GetEquipMasterDataFromAsset_MasterDataCreator()
    {
        return Resources
        .LoadAll("Data/MasterDatas/Equip/", typeof(EquipBase))
        .Cast<EquipBase>()
        .ToList();
    }
}
