
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
        

        public Verification()
        {

        }
    }
}