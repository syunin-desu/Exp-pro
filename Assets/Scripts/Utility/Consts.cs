
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
        //シーン関係
        public const int SCENE_QUEST = 1;
        public const int SCENE_BATTLE = 2;

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
            Heal
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