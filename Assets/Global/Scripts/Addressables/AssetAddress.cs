using System.Collections.Generic;

namespace Global.Scripts.Addressables {
    public static class AssetAddress {
        public static class DefaultLocalGroup {}

        public static class Scenes {
            public static string GameScene = "GameScene";
            public static List<string> AllAddressablesInGroup = new() { "GameScene" };
        }

        public static class Configurations { }

        public static List<string> AllAddressablesInGroups = new() { "GameScene" };
    }
}