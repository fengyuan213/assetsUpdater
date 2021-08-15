
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using assetsUpdater.EventArgs;
using assetsUpdater.Exceptions;
using assetsUpdater.Interfaces;
using assetsUpdater.Model;
using assetsUpdater.Model.StorageProvider;

namespace assetsUpdater
{
    public class AssertVerify
    {
        public event EventHandler<MessageNotifyEventArgs> MessageNotify;

        private static async Task<RemoteDataManager> Check_UpdateInternal(DataManager dm,string url)
        {
            if (!await dm.IsDataValid())
            {
                //Data invalid return
                return null;
            }

            var remoteDb = await RemoteDataManager.GetStorageProvider(url);
            if (await IsUpdateRequired(dm.StorageProvider, remoteDb))
            {
                return new RemoteDataManager(remoteDb);
            }
            //No updates return null    
            return null;
        }
  
    
 

        public static async Task<RemoteDataManager> Check_Update(IStorageProvider localProvider,string remoteUrl)
        {
            LocalDataManager localDataManager = new LocalDataManager(localProvider);
            return  await Check_UpdateInternal(localDataManager,remoteUrl);
        }

        public static async Task<RemoteDataManager> Check_Update(string localDbPath,string remoteUrl)
        {
            LocalDataManager localDataManager = new LocalDataManager(localDbPath);
            return await Check_UpdateInternal(localDataManager,remoteUrl);
        }

        private static Task<bool> IsUpdateRequired(IStorageProvider local, IStorageProvider remote,
            bool ignoreMirrorChanges = false)
        {
            var localData = local.GetBuildInDbData();
            var remoteData = remote.GetBuildInDbData();

            if (localData.Config.MajorVersion != remoteData.Config.MajorVersion)
            {
                return Task.FromResult(false);
            }
            return ignoreMirrorChanges ? Task.FromResult(true) : Task.FromResult(localData.Config.MirrorVersion == remoteData.Config.MirrorVersion);
        }
    

    /*public static void Compare(IEnumerable<AssertUpgradePackage> assertUpgradesA,IEnumerable<AssertUpgradePackage> assertUpgradesB)
    {
        
    }*/
        public AssertUpgradePackage DatabaseCompare(IStorageProvider remoteProvider,IStorageProvider localProvider)
        {
           var remoteData=  remoteProvider.GetBuildInDbData().DatabaseFiles;
           var localData= localProvider.GetBuildInDbData().DatabaseFiles;
           return DatabaseCompare(remoteData, localData);
        }

        public AssertUpgradePackage  DatabaseCompare(IEnumerable<DatabaseFile> remoteFiles,
            IEnumerable<DatabaseFile> localFiles)
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
        

        public AssertVerify()
        {

        }
    }
}