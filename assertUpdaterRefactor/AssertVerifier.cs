using assertUpdaterRefactor.DbModel;
using assertUpdaterRefactor.Operations;
using assertUpdaterRefactor.Utils.Comparer;

namespace assertUpdaterRefactor
{
    public class AssertVerifier
    {

        private static bool IsUpdateRequired(DataManager local, DataManager remote,
            bool ignoreMirrorChanges = false)
        {

            DbData localData = local.Data;

            DbData remoteData = remote.Data;

            return localData.Config.MajorVersion == remoteData.Config.MajorVersion &&
                localData.Config.MinorVersion == remoteData.Config.MinorVersion
&& (ignoreMirrorChanges || localData.Config.MinorVersion == remoteData.Config.MinorVersion);
        }



        public static AssertUpgradePackage DatabaseCompare(DataManager local, DataManager remote)
        {
            IEnumerable<DbFile> localFiles = local.Data.DatabaseFiles;
            IEnumerable<DbFile> remoteFiles = remote.Data.DatabaseFiles;


            AssertUpgradePackage assetUpgradePackage = new();
            foreach (DbFile remoteFile in remoteFiles)
            {
                if (localFiles.Contains(remoteFile, new DbFileFileNameValueOnlyEqualityComparer()))
                {
                    foreach (DbFile localFile in localFiles)
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
            }

            //判断本地应该删除的文件
            foreach (DbFile localFile in localFiles)
            {
                if (remoteFiles.Contains(localFile, new DbFileFileNameValueOnlyEqualityComparer()))
                {
                    //本地数据库文件在数据库中存在
                }
                else
                {
                    //本地数据文件在数据库中不存在
                    //代表删除文件（数据库新加的文件）

                    assetUpgradePackage.DeleteFile = assetUpgradePackage.DeleteFile.Append(localFile);
                }
            }

            return BuildOperationUpgrade(assetUpgradePackage);
            AssertUpgradePackage BuildOperationUpgrade(AssertUpgradePackage assertUpgradePackage)
            {
                List<IOperation> operations = new();
                string vcs = local.Data.Config.VersionControlFolder;

                foreach (DbFile dbFile in assertUpgradePackage.AddFile)
                {
                    AddFileAssertOperation operation = new(vcs, dbFile);
                    operations.Add(operation);
                }
                foreach (DbFile dbFile in assertUpgradePackage.DifferFile)
                {
                    DifferFileAssertOperation operation = new(vcs, dbFile);
                    operations.Add(operation);
                }
                foreach (DbFile dbFile in assertUpgradePackage.DeleteFile)
                {

                    DeleteFileAssertOperation operation = new(vcs, dbFile);
                    operations.Add(operation);
                }
                assetUpgradePackage.Operations = operations.ToArray();

                return assetUpgradePackage;

            }
        }


    }

}
