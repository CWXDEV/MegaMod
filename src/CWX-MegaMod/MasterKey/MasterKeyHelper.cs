#if !DEBUG
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
                default:
                    return "5c1d0d6d86f7744bb2683e1f";
            }
        }
    }
}
#endif