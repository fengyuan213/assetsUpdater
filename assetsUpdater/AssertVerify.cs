
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
                            //�����ļ��������ݿ���ļ���
                            //�����ж�Hash
                            if (localFile.Hash == remoteFile.Hash) //�ж�hash�Ƿ����
                            {
                                //�����ļ���ȣ��ļ�û�иı�
                            }
                            else
                            {

                                //�ļ��ı��ˣ���ӵ������б�

                                assetUpgradePackage.DifferFile = assetUpgradePackage.DifferFile.Append(remoteFile);

                            }
                        }
                    }
                }
                else
                {
                    //�����ļ���û�����ݿ���ļ���
                    //���������ļ������ݿ��¼ӵ��ļ���
                    assetUpgradePackage.AddFile = assetUpgradePackage.AddFile.Append(remoteFile);
                }

            //�жϱ���Ӧ��ɾ�����ļ�


            foreach (var localFile in localFiles)
                if (remoteFiles.Contains(localFile))
                {
                    //�������ݿ��ļ������ݿ��д���
                }
                else
                {
                    //���������ļ������ݿ��в�����
                    //����ɾ���ļ������ݿ��¼ӵ��ļ���
                    
                    assetUpgradePackage.DeleteFile = assetUpgradePackage.DeleteFile.Append(localFile);
                }
            
            return assetUpgradePackage;
        }
        

        public AssertVerify()
        {

        }
    }
}