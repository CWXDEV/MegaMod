using CWX_MegaMod.Config;

namespace CWX_MegaMod.MasterKey
{
    public static class MasterKeyHelper
    {
        public static string GetMasterKey(EMasterKeys masterKey)
        {
            switch (masterKey)
            {
                case EMasterKeys.Yellow:
                    return "5c1d0d6d86f7744bb2683e1f";
                case EMasterKeys.Green:
                    return "5c1d0dc586f7744baf2e7b79";
                case EMasterKeys.Blue:
                    return "5c1d0c5f86f7744bb2683cf0";
                case EMasterKeys.Red:
                    return "5c1d0efb86f7744baf2e7b7b";
                case EMasterKeys.Violet:
                    return "5c1e495a86f7743109743dfb";
                case EMasterKeys.Black:
                    return "5c1d0f4986f7744bb01837fa";
                case EMasterKeys.Access:
                    return "5c94bbff86f7747ee735c08f";
                case EMasterKeys.Storage:
                    return "66acd6702b17692df20144c0";
                case EMasterKeys.Residential:
                    return "6711039f9e648049e50b3307";
                case EMasterKeys.ElevenSR:
                    return "5e42c81886f7742a01529f57";
                case EMasterKeys.TwentyOneWS:
                    return "5e42c83786f7742a021fdf3c";
                case EMasterKeys.BlueMarked:
                    return "5efde6b4f5448336730dbd61";
                default:
                    return "5c94bbff86f7747ee735c08f";
            }
        }
    }
}