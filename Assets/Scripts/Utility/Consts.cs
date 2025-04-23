
using System.Xml.Linq;

/// <summary>
///  不変値置き場
/// </summary>
namespace CONST
{

    public static class CHARCTOR
    {

        //キャラクタ識別
        public const int PLAYER = 1;
        public const int ENEMY = 2;

        // 属性


    }
    public static class SCENE
    {
        /// <summary>
        ///  シーン
        /// </summary>
        public enum Scene
        {
            Title,
            Town,
            Quest,
            Battle
        }

    }
    public static class QUEST_MENU_STATUS
    {
        /// <summary>
        ///  シーン
        /// </summary>
        public enum MenuStatus
        {
            Main,
            PlayerMainMenu,
            ItemMenu,
            AbilityMenu
        }

    }

    public static class ITEM_MENU_STATUS
    {
        /// <summary>
        ///  アイテムメニューの各フェーズ
        /// </summary>
        public enum MenuStatus
        {
            HowItem,
            SelectItem,
            SelectTargetAndUse,
            SelectArrangement
        }

    }

    public static class ABILITY_MENU_STATUS
    {
        /// <summary>
        ///  アイテムメニューの各フェーズ
        /// </summary>
        public enum MenuStatus
        {
            HowAbility,
            SelectAbility,
            SelectTargetAndDOAbility,
            SelectArrangement
        }

    }

    public static class EQUIP_MENU_STATUS
    {
        /// <summary>
        ///  装備メニューの各フェーズ
        /// </summary>
        public enum MenuStatus
        {
            SelectEquipParts,
            SelectEquip,
        }

    }

    public static class ABILITY
    {
        /// <summary>
        ///  アイテムメニューの各フェーズ
        /// </summary>
        public enum Category
        {
            Default,
            Magic,
            SwordArts,
        }

    }

    public static class BATTLE_RATE
    {
        //===========
        //バトルパラメータ
        //===========

        //防御デフォルト倍率
        public const float RATE_DEFAULT_DEFENCE = 1.0f;
        //防御時のダメージ減少倍率
        public const float RATE_DEFENCE = 2.0f;
        //弱点属性で攻撃された場合のダメージ増加率
        public const float RATE_WEAK_ELEMENT = 1.5f;
        //耐性属性で攻撃された場合のダメージ増加率
        public const float RATE_STRONG_ELEMENT = 0.5f;
    }
    public static class BATTLE_ACTION
    {
        //==========================
        //バトルアクション
        //==========================
        //Enemy要コマンドEnum
        public enum COMMAND
        {
            Attack,
            Ability,
            Defence,
            Item
        }

    }

    namespace BATTLE
    {
        /// <summary>
        /// バトルシーンでのフェーズ状態
        /// </summary>
        public enum PHASES_STATUS
        {
            INITIALIZE,
            STANDBY_TURN,
            P_ACTION_SELECTING,
            DO_BATTLE,
            END_TURN,
            RESULT_BATTLE,
        }
    }

    // アクション
    namespace ACTION
    {
        /// <summary>
        ///  アクションの種類
        /// </summary>
        public enum TYPE
        {
            Attack,
            Buff,
            DeBuff,
            Heal,
            UseItem,
        }

        /// <summary>
        ///  アクションの発動速度の種類
        /// </summary>
        public enum Speed
        {
            Fast,
            Normal,
            Delay
        }

        /// <summary>
        ///  アクションの対象範囲
        /// </summary>
        public enum Range
        {
            Single,
            All,
        }

        /// <summary>
        /// アビリティ発動時に実行されるアクション
        /// アビリティは基本的に子アクションを順番に実行していく形で実装する
        /// </summary>
        public enum Ability_Action_Cell
        {
            SolidSingleAttack,
            MagicSingleAttack,
            Heal,
            DoItem,
        }

        /// <summary>
        /// 効果対象のstatus
        /// </summary>
        public enum TARGET_STATUS
        {
            HP,
            MP,
            None
        }
    }

    // アイテム
    namespace ITEM
    {

        // 全アイテム名のリスト
        public enum AllItemNames
        {
            BluePotion,
            BluePotionEx,
            NeoBluePotion,
            EnagyDrink,
            EnergyDrinkEx,
            EnergyDrinkNeo
        }

        public enum CATEGORY
        {
            HEAL_ITEM,
            ATTACK_ITEM,
            HEAD_EQUIP_ITEM,
            BODY_EQUIP_ITEM,
            ACCESSORY_ITEM
        }

        // 追加永続バフ
        public enum ADDBUFF
        {
            NONE,
            DAMAGE_UP,

        }

        // 追加永続デバフ
        public enum ADDDEBUFF
        {
            NONE,
            DAMAGE_DOWN,

        }
    }

    // アニメーションのスピード
    public static class ANIMATION_SPEED
    {
        // カード配置スピード
        public const float MOVE_CARD_SPEED = 0.3f;
        // カード返しスピード
        public const float FLIP_CARD_SPEED = 0.25f;
        // 非選択カード消失スピード
        public const float FADEOUT_CARD_SPEED = 0.5f;
    }

    namespace QUEST
    {
        /// <summary>
        /// カードの種類
        /// </summary>
        public enum CardType
        {
            // 敵と遭遇
            EncountEnemy,
            // アイテムを入手
            GetItem,
            // アーティファクトを獲得
            GetArtifact,
            // お金を獲得
            GetCredit,
            // イベント遭遇
            EncountEvent,
            // ボスと遭遇
            EncountBoss,
            // ランダムイベント
            Secret,
            // 削除されている
            Deleted,
            // 選択されている
            Selected,

            // 何も起こらない(テスト用)
            None

        }
    }

    public static class UTILITY
    {

        /// <summary>
        ///  属性の種類
        /// </summary>
        public enum Element
        {
            Fire,
            Ice,
            Thunder,
            None
        }

        public static int BATTLEACTION_DELAY = 1;
    }

}