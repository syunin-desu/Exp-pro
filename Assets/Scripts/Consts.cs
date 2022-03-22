namespace CONST
{

    public static class CHARCTOR
    {

        //キャラクタ識別
        public const int PLAYER = 1;
        public const int ENEMY = 2;

        /// <summary>属性</summary>
        public static string[] ELEMENT_LIST = {
        "Fire", "Ice", "Thunder","None"
    };

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
            Attack = 1,
            Ability,
            Defence,
            Item
        }

        //攻撃
        public const string ATTACK = "Attack";
        //防御
        public const string ABILITY = "Ability";
        //防御
        public const string DEFENCE = "Defence";
        //防御
        public const string ITEM = "Item";

        //==================
        //アクションタイミング
        //==================
        public const string ACTION_FAST = "Fast";
        public const string ACTION_NORMAL = "Normal";
        public const string ACTION_DELAY = "Delay";

    }

    public static class ABILITY
    {
    }

}