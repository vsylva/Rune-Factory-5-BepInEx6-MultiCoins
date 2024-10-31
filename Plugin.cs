using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;


namespace MultiCoins;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
public class Plugin : BasePlugin
{
    public static ConfigEntry<int> MultiCoinsConf;

    public override void Load()
    {
        MultiCoinsConf = Config.Bind<int>("倍率", "金币", 10, "金币获得的倍率。默认: 10 倍");

        new Harmony(PluginInfo.PLUGIN_NAME).PatchAll();
    }
}

[HarmonyPatch(typeof(SV), nameof(SV.AddPlayerGold))]
class SVAddPlayerGoldPatch
{
    public static void Prefix(ref int coins)
    {
        if (coins > 0)
        {
            coins *= Plugin.MultiCoinsConf.Value;
        }
        else
        {
            coins = coins;
        }
    }
}
