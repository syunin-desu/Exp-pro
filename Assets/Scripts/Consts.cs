namespace Const
{

    public static class CO
    {

        //キャラクタ識別
        public const int PLAYER = 1;
        public const int ENEMY = 2;

        //シーン関係
        public const int SCENE_QUEST = 1;
        public const int SCENE_BATTLE = 2;

        //===========
        //バトルパラメータ
        //===========

        //防御デフォルト倍率
        public const float RATE_DEFAULT_DEFENCE = 1.0f;
        //防御時のダメージ減少倍率
        public const float RATE_DEFENCE = 2.0f;

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


    }

}