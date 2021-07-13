
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using assetsUpdater.Model;

namespace assetsUpdater
{
    public class Verification
    {

        /*public static void Compare(IEnumerable<AssertUpgradePackage> assertUpgradesA,IEnumerable<AssertUpgradePackage> assertUpgradesB)
        {
            
        }*/


        public AssertUpgradePackage  DatabaseCompare(IEnumerable<BuildInDbFile> remoteFiles,
            IEnumerable<BuildInDbFile> localFiles)
        {

            var assetUpgradePackage = new AssertUpgradePackage();
            foreach (var remoteFile in remoteFiles)
                if (localFiles.Select((file => file.RelativePath)).Contains(remoteFile.RelativePath))
                {

                    foreach (var localFile in localFiles)
                    {
                        if (localFile.RelativePath == remoteFile.RelativePath)
                        {
                            //本地文件中有数据库的文件名
                            //继续判断Hash
                            if (localFile.Hash == remoteFile.Hash) //判断hash是否相等
                            {
                                //两个文件相等，文件没有改变
                            }
                            else
                            {

                                //文件改变了，添加到下载列表

                                assetUpgradePackage.DifferFile = assetUpgradePackage.DifferFile.Append(remoteFile);

                            }
                        }
                    }
                }
                else
                {
                    //本地文件中没有数据库的文件名
                    //代表新增文件（数据库新加的文件）
                    assetUpgradePackage.AddFile = assetUpgradePackage.AddFile.Append(remoteFile);
                }

            //判断本地应该删除的文件


            foreach (var localFile in localFiles)
                if (remoteFiles.Contains(localFile))
                {
                    //本地数据库文件在数据库中存在
                }
                else
                {
                    //本地数据文件在数据库中不存在
                    //代表删除文件（数据库新加的文件）
                    assetUpgradePackage.DeleteFile = assetUpgradePackage.DeleteFile.Append(localFile);
                }

            return assetUpgradePackage;
        }
        

        public Verification()
        {

        }
    }
}