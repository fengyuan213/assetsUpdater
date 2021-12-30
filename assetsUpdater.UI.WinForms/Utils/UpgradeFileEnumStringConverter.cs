using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using assetsUpdater.Model;
using assetsUpdater.Utils;
// ReSharper disable InconsistentNaming

namespace assetsUpdater.UI.WinForms.Utils
{
    public static class UpgradeFileEnumStringConverter
    {
        public static readonly string DisplayString_AddFile = "新增";
        public static readonly string DisplayString_DifferFile = "更新";
        public static readonly string DisplayString_DeleteFile = "删除";

        public static string Convert(UpgradeFileType fileType)
        {
            string str = "";

            switch (fileType)
            {
                case UpgradeFileType.AddFile:
                    str = DisplayString_AddFile;
                    break;
                case UpgradeFileType.DifferFile:
                    str = DisplayString_DifferFile;

                    break;
                case UpgradeFileType.DeleteFile:
                    str=DisplayString_DeleteFile;
                    break;
                default:
                    break;
            }

            return str;
        }
        public static UpgradeFileType Convert(string s)
        {
            UpgradeFileType type;
            if (s==DisplayString_AddFile)
            {
                type=UpgradeFileType.AddFile;
            }
            else if (s == DisplayString_DifferFile)
            {
                type = UpgradeFileType.DifferFile;
            }
            else if (s==DisplayString_DeleteFile)
            {
                type=UpgradeFileType.DeleteFile;
            }
            else
            {
                throw new InvalidCastException($"Can't cast {s} to {nameof(UpgradeFileType)}");
            }
            return type;
        }
    }

}
