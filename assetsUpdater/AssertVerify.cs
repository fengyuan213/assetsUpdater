#region Using

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using assetsUpdater.EventArgs;
using assetsUpdater.Interfaces;
using assetsUpdater.Model;
using assetsUpdater.Model.StorageProvider;
using assetsUpdater.Utils;

#endregion

namespace assetsUpdater
{
    public static class AssertVerify
    {
        public static event EventHandler<MessageNotifyEventArgs> MessageNotify;

        internal static void OnMessageNotify(object sender, MessageNotifyEventArgs e)
        {
            MessageNotify?.Invoke(sender, e);
        }

        internal static void OnMessageNotify(object sender, string message, Exception? e = null)
        {
            MessageNotify?.Invoke(sender,
                new MessageNotifyEventArgs(e == null ? MsgL.Info : MsgL.Error, message, e != null, e));
        }

        internal static void OnMessageNotify(object sender, string message, MsgL level, Exception? e = null)
        {
            MessageNotify?.Invoke(sender, new MessageNotifyEventArgs(level, message, e != null, e));
        }

        internal static void OnMessageNotify(object sender, MsgL Level, string message, bool hasError, Exception e,
            object obj = null)
        {
            MessageNotify?.Invoke(sender, new MessageNotifyEventArgs(Level, message, hasError, e, obj));
        }

        private static async Task<(RemoteDataManager remoteDataManager, bool isUpdateRequired)> Check_UpdateInternal(
            DataManager dm, string url)
        {
            if (!await dm.IsDataValid())
                //Data invalid return
                throw new InvalidDataException("local db invalid");
            //return (new RemoteDataManager(), false);

            var remoteDb = await RemoteDataManager.GetStorageProvider(url);

            if (await IsUpdateRequired(dm.StorageProvider, remoteDb)) return (new RemoteDataManager(remoteDb), true);
            //No updates return default

            return (new RemoteDataManager(remoteDb), false);
        }

        public static async Task<(RemoteDataManager remoteDataManager, bool isUpdateRequired)> Check_Update(
            IStorageProvider localProvider, string remoteUrl)
        {
            if (string.IsNullOrWhiteSpace(remoteUrl)) throw new ArgumentNullException(remoteUrl);
            var localDataManager = new LocalDataManager(localProvider);
            return await Check_UpdateInternal(localDataManager, remoteUrl);
        }

        public static async Task<(RemoteDataManager remoteDataManager, bool isUpdateRequired)> Check_Update(
            string localDbPath, string remoteUrl)
        {
            if (string.IsNullOrWhiteSpace(localDbPath) || string.IsNullOrWhiteSpace(remoteUrl))
                throw new ArgumentNullException(remoteUrl);
            var localDataManager = new LocalDataManager(localDbPath);
            return await Check_UpdateInternal(localDataManager, remoteUrl);
        }

        private static Task<bool> IsUpdateRequired(IStorageProvider local, IStorageProvider remote,
            bool ignoreMirrorChanges = false)
        {
            var localData = local.GetBuildInDbData();
            var remoteData = remote.GetBuildInDbData();

            if (localData.Config.MajorVersion != remoteData.Config.MajorVersion ||
                localData.Config.MinorVersion != remoteData.Config.MinorVersion)
                return Task.FromResult(false);

            return ignoreMirrorChanges
                ? Task.FromResult(true)
                : Task.FromResult(localData.Config.MinorVersion == remoteData.Config.MinorVersion);
        }

        /*public static void Compare(IEnumerable<AssertUpgradePackage> assertUpgradesA,IEnumerable<AssertUpgradePackage> assertUpgradesB)
        {
        }*/

        public static Task<AssertUpgradePackage> DatabaseCompare(IStorageProvider remoteProvider,
            IStorageProvider localProvider)
        {
            var remoteData = remoteProvider.GetBuildInDbData().DatabaseFiles;
            var localData = localProvider.GetBuildInDbData().DatabaseFiles;
            var package = DatabaseCompare(remoteData, localData);

            return Task.FromResult(package);
        }

        public static AssertUpgradePackage DatabaseCompare(IEnumerable<DatabaseFile> remoteFiles,
            IEnumerable<DatabaseFile> localFiles)
        {
            var assetUpgradePackage = new AssertUpgradePackage();
            foreach (var remoteFile in remoteFiles)
                if (localFiles.Contains(remoteFile, new DbFileValueEqualityComparer()))
                {
                    foreach (var localFile in localFiles)
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
                else
                {
                    //�����ļ���û�����ݿ���ļ���
                    //���������ļ������ݿ��¼ӵ��ļ���
                    assetUpgradePackage.AddFile = assetUpgradePackage.AddFile.Append(remoteFile);
                }

            //�жϱ���Ӧ��ɾ�����ļ�

            foreach (var localFile in localFiles)

                if (remoteFiles.Contains(localFile, new DbFileValueEqualityComparer()))
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
    }
}