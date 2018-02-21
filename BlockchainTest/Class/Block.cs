using System;
using System.Text;
using System.Security.Cryptography;

namespace BlockchainTest.Class
{
    public class Block
    {
        public Block(BlockHeader blockHeader, Object[] transactions)
        {
            this.blockHeader = blockHeader;
            this.transactions = transactions;
        }

        private int blockSize;
        private BlockHeader blockHeader;
        private int transactionCount;
        private object[] transactions;

        /// <summary>
        /// 블록 헤더를 해시화하여 반환한다.
        /// </summary>
        /// <returns></returns>
        public string getBlockHash()
        {
            byte[] bytes = blockHeader.toByteArray();
            using (SHA256Managed hashstring = new SHA256Managed())
            {
                // 해시화를 두 번 연속으로 설정.
                byte[] blockHash = hashstring.ComputeHash(bytes);
                blockHash = hashstring.ComputeHash(blockHash);

                return ByteArrayToString(blockHash);
            }
        }
        
        /// <summary>
        /// 바이트어레이를 String으로 변환
        /// </summary>
        /// <param name="bts"></param>
        /// <returns></returns>
        public static string ByteArrayToString(byte[] bts)
        {
            StringBuilder strBld = new StringBuilder();
            foreach (byte bt in bts)
                strBld.AppendFormat("{0:X2}", bt);

            return strBld.ToString();
        }

    }

    public class BlockHeader
    {
        public BlockHeader(byte[] previousBlockHash, object[] transactions)
        {
            this.previousBlockHash = previousBlockHash;
            this.merkleRootHash = transactions.GetHashCode();
        }
        /// <summary>
        /// 소프트웨어, 또는 프로토콜 등의 업그레이드를 추적하기 위해 사용되는 버전 정보
        /// </summary>
        private int version;
        /// <summary>
        /// 블록체인 상의 이전 블록(부모 블록)의 해시값
        /// </summary>
        private byte[] previousBlockHash;
        /// <summary>
        /// 머클트리의 루트에 대한 해시값
        /// </summary>
        private int merkleRootHash;
        /// <summary>
        /// 해당 블록의 생성 시각
        /// </summary>
        private int timestamp;
        /// <summary>
        ///  채굴과정에서 필요한 작업 증명(Proof of Work) 알고리즘의 난이도 목표
        /// </summary>
        private static uint difficultyTarget = 5;
        /// <summary>
        ///  채굴과정의 작업 증명에서 사용되는 카운터
        /// </summary>
        private static int nonce = 0;


        /// <summary>
        /// 작업증명
        /// </summary>
        public int ProofOfWorkCount()
        {
            using (SHA256Managed hashstring = new SHA256Managed())
            {
                byte[] bt;
                string sHash = string.Empty;
                while (sHash == string.Empty || sHash.Substring(0, (int)difficultyTarget) != ("").PadLeft((int)difficultyTarget, '0'))
                {
                    bt = Encoding.UTF8.GetBytes(merkleRootHash + nonce.ToString());
                    sHash = Block.ByteArrayToString(hashstring.ComputeHash(bt));
                    nonce++;
                }
                return nonce;
            }
        }

        /// <summary>
        /// 거래내역을 byte로 반환한다.
        /// </summary>
        /// <returns></returns>
        public byte[] toByteArray()
        {
            string tmpStr = "";
            //이전 블록이 있다면 처음에 해시를 추가한다.
            if (previousBlockHash != null)
            {
                tmpStr += Convert.ToBase64String(previousBlockHash);
            }
            tmpStr += merkleRootHash.ToString();
            return Encoding.UTF8.GetBytes(tmpStr);
        }

    }
}
