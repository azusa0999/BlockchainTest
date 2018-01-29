using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        public string getBlockHash()
        {
            byte[] bytes = blockHeader.toByteArray();
            SHA256Managed hashstring = new SHA256Managed();
            byte[] blockHash = hashstring.ComputeHash(bytes);
            blockHash = hashstring.ComputeHash(blockHash);

            return string.Join(",", blockHash);
        }
    }

    public class BlockHeader
    {
        public BlockHeader(byte[] previousBlockHash, object[] transactions)
        {
            this.previousBlockHash = previousBlockHash;
            this.merkleRootHash = transactions.GetHashCode();
        }
        
        private int version;
        private byte[] previousBlockHash;
        private int merkleRootHash;
        private int timestamp;
        private int difficultyTarget;
        private int nonce;

        public byte[] toByteArray()
        {
            string tmpStr = "";
            if (previousBlockHash != null)
            {
                tmpStr += string.Join(",", previousBlockHash);
            }
            tmpStr += merkleRootHash;
            return Encoding.UTF8.GetBytes(tmpStr);
        }
    }
}
